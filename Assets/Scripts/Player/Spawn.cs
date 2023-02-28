using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    //varible to store objects
    public GameObject[] pickups;

    // Start is called before the first frame update
    void Start()
    {
        //one line of code to spawn a random object
        Instantiate(pickups[Random.Range(0, pickups.Length)], transform.position, transform.rotation);
    }
}
