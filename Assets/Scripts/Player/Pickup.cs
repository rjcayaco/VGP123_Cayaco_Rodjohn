using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UIElements;



public class Pickup : MonoBehaviour
{
    AudioSourceManager asm;

    public enum PickupType
    {
        Powerup,
        Life,
        Score
    }

    public AudioClip PickupSound;


    public PickupType currentPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerController temp = collision.gameObject.GetComponent<PlayerController>();

            switch (currentPickup)
            {
                case PickupType.Powerup:
                    temp.StartJumpForceChange();

                    break;
                case PickupType.Life:
                    GameManager.instance.lives++;

                    break;
                case PickupType.Score:
                    GameManager.instance.Score++;
                    break;
            }

            if(PickupSound)
            {
                collision.gameObject.GetComponent<AudioSourceManager>().PlayOneShot(PickupSound, false);
            }


            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
