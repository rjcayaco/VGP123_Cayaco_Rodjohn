using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;
   

    public UnityEvent OnProjectileSpawned;
    public float projectileSpeed;
    public Transform spawnPointRight;
    public Transform spawnPointLeft;

    public AudioClip Projsound;


    public ProjectileEnemies projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (projectileSpeed <= 0)
            projectileSpeed = 7.0f;

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
            Debug.Log("Please setup default values on " + gameObject.name);
    }

    public void Fire()
    {
        

        if (!sr.flipX)
        {
            ProjectileEnemies curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.speed = -projectileSpeed;
        }
        else
        {
            ProjectileEnemies curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.speed = projectileSpeed;
        }


        GameManager.instance.playerInstance.GetComponent<AudioSourceManager>().PlayOneShot(Projsound, false);


        OnProjectileSpawned?.Invoke();
    }
}
