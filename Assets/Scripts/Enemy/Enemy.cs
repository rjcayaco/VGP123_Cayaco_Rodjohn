using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    AudioSourceManager asm;

    protected SpriteRenderer sr;
    protected Animator anim;

    protected int _health;
    public int maxHeath;

    public AudioClip deathsound;

    public int heath
    {
        get => _health;
        set
        {
            _health = value;

            if (_health > maxHeath)
                _health = maxHeath;

            if (_health <= 0)
                Death();
        }
    }

    public virtual void Death()
    {
        anim.SetTrigger("Death");
        
        if(deathsound)
            GameManager.instance.playerInstance.GetComponent<AudioSourceManager>().PlayOneShot(deathsound, false);

        Destroy(gameObject);

    }


    // Start is called before the first frame update
    public virtual void Start()
    {
        asm = GetComponent<AudioSourceManager>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (maxHeath <= 0)
            maxHeath = 5;

        heath = maxHeath;
    }

    public virtual void TakeDamage(int damage)
    {
        heath -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
