using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy 
{
    public float projectileFireRate;
    float timeSinceLastFire;
    Shoot shootScript;

    public float minDistance;

    //public GameObject Player;

    // Update is called once per frame
    void Update()
    {
      
        float distance = GameObject.FindGameObjectWithTag("Player").transform.position.x - gameObject.transform.position.x;

        if(distance < minDistance && distance > -minDistance )
        {
            if (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
            {
                sr.flipX = true;
            }
            else
                sr.flipX = false;

            AnimatorClipInfo[] curClips = anim.GetCurrentAnimatorClipInfo(0);

            if (curClips[0].clip.name != "EggBatterAttack")
            {
                if (Time.time >= timeSinceLastFire + projectileFireRate)
                {
                    anim.SetTrigger("Shoot");
                }
            }

        }

    }

    public override void Start()
    {
        base.Start();

        shootScript = GetComponent<Shoot>();
        shootScript.OnProjectileSpawned.AddListener(UpdateTimeSinceLastFire);

    }

   /* public override void Death()
    {
        Destroy(gameObject);
    }
   */
    private void OnDisable()
    {
        shootScript.OnProjectileSpawned.RemoveListener(UpdateTimeSinceLastFire);
    }

    void UpdateTimeSinceLastFire()
    {
        timeSinceLastFire = Time.time;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, minDistance);
    }
}
