using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{

    public static bool gameIsPaused;

    [Header ("Images")]
    [SerializeField] private Image[] hearts;

    [Header("Buttons")]
    public Button startButton;
    public Button settingButton;
    public Button quitButton;
    public Button returnToMenuButton;
    public Button returnToGameButton;

    [Header("Menu")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    [Header ("Text")]
    public Text volSliderText;
    public Text livesText;
    public Text Score;

    [Header("Slider")]
    public Slider volSlider;

    public void StartGame()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            SceneManager.LoadScene(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (startButton)
            startButton.onClick.AddListener(StartGame);

        if (settingButton)
            settingButton.onClick.AddListener(ShowSettingsMenu);

        if (quitButton)
            quitButton.onClick.AddListener(QuitGame);

        if (volSlider)
            volSlider.onValueChanged.AddListener(OnSliderValueChanged);

        if(returnToMenuButton)
            returnToMenuButton.onClick.AddListener(MainMenu);

        if(returnToGameButton)
            returnToGameButton.onClick.AddListener(ResumeGame);

        if (livesText)
            GameManager.instance.onLifeValueChanged.AddListener(UpdateLifeText);

        if (Score)
            GameManager.instance.onScoreValuelChanged.AddListener(UpdateScore);
    }

    void ShowSettingsMenu()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
        

        if (volSlider && volSliderText)
            volSliderText.text = volSlider.value.ToString();
    }

    void MainMenu()
    {
        if(SceneManager.GetActiveScene().name == "Level")
        {

            if (Time.timeScale == 0)
                Time.timeScale = 1;
                gameIsPaused = false;

            SceneManager.LoadScene("Title");
        }
        else
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
        }

    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // add isPause for pause menu
        #else
            Application.Quit();
        #endif
    }

    void OnSliderValueChanged(float value)
    {
        if (volSliderText)
            volSliderText.text = value.ToString();
    }

    void ResumeGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPaused = false;
        #endif

        pauseMenu.SetActive(false);

        Time.timeScale = 1;
        gameIsPaused = false;
    }

    void UpdateLifeText(int value)
    {
        //livesText.text = value.ToString();

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < GameManager.instance.lives)
            {
                hearts[i].color = Color.white;
            }
            else
            {
                hearts[i].color = Color.black;
            }
        }
    }

    void UpdateScore(int value)
    {
        Score.text= value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu) return;

        if (Input.GetKeyDown(KeyCode.P))
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);

                //
                if (pauseMenu.activeSelf)
                {
                    Time.timeScale = 0f; 
                    gameIsPaused= true;

                }
                else
                {
                    Time.timeScale = 1;
                    gameIsPaused= false;
                }

            }
    }
}
