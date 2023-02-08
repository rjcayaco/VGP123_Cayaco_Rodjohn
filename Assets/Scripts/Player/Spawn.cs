using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject powerup;
    public GameObject lives;
    public GameObject score;
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6 ; i++) 
        {
            int randomPickup = Random.Range(0, 2);
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            switch (randomPickup) 
            {
            
                case 0:
                    Instantiate(powerup, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
                    break;
                case 1:
                    Instantiate(lives, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
                    break;
                case 2:
                    Instantiate(score, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
                    break;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void SpawnPowerup()
    {
        int randomPickup = Random.Range(0,3);
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Instantiate(powerup[randomPickup], transform.position + new Vector3(randomX, randomY, 0), transform.rotation);


    }
    void SpawnLife()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Instantiate(lives, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
    }
    void SpawnScore()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Instantiate(score, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
    }*/
}
