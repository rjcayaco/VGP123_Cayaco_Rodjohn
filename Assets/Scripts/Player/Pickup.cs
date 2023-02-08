using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UIElements;

public class Pickup : MonoBehaviour
{
    public enum PickupType
    {
        Powerup,
        Life,
        Score
    }

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
                    temp.lives++;

                    break;
                case PickupType.Score:
                    temp.Score++;
                    break;
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
