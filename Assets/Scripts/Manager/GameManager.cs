using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Security.Cryptography;
using UnityEngine.Audio;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{


    public UnityEvent<int> onLifeValueChanged;
    public UnityEvent<int> onScoreValuelChanged;

    public AudioClip Death;

    private static GameManager _instance = null;

    public static GameManager instance
    {
        get => _instance;
    }

    public int maxLives = 5;
    private int _lives = 3;

    public int lives

    {
        get { return _lives; }
        set
        {
            if (_lives > value)
                Respawn();
                
                

            _lives = value;

            if (_lives > maxLives)
                _lives = maxLives;

           
           onLifeValueChanged?.Invoke(_lives);



                Debug.Log("Lives have been set to: " + _lives.ToString());
        }
    }



    public int _score = 0;
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;

            onScoreValuelChanged?.Invoke(_score);

            Debug.Log("Score: " + _score.ToString());
        }
    }


    public PlayerController playerPrefab;
    [HideInInspector] public PlayerController playerInstance = null;
    [HideInInspector] public Level currentLevel = null;
    [HideInInspector] public Transform currentSpawnPoint;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

        lives = maxLives;
    }

    public void SpawnPlayer(Transform spawnPoint)
    {
        playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        currentSpawnPoint = spawnPoint;
    }

    void Respawn()
    {
        if (playerInstance)
        {
            playerInstance.transform.position = currentSpawnPoint.position;
            GameManager.instance.playerInstance.GetComponent<AudioSourceManager>().PlayOneShot(Death, false);

        }
           


    
    }


    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
                SceneManager.LoadScene(1);
            else if (SceneManager.GetActiveScene().buildIndex == 2)
                SceneManager.LoadScene(0);
            else
                SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.K))
            lives--;

        if (lives < 1)
            GameOver();
 
        

    }

    public void UpdateCheckpoint(Transform spawnPoint)
    {
        currentSpawnPoint = spawnPoint;
    }

    public void GameOver()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            SceneManager.LoadScene(2);
    }


}
