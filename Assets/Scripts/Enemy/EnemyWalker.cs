using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : Enemy
{
    Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();

        if (speed <= 0)
            speed = 2.5f;

    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curClips = anim.GetCurrentAnimatorClipInfo(0);

        if (curClips[0].clip.name == "DinoPAnim")
        {
            if (sr.flipX)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }

        if (_health <= 0)
            DestroyMyself();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier"))
        {
            sr.flipX = !sr.flipX;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GameManager.instance.lives--;
    }

    public void DestroyMyself()
    {
        Destroy(gameObject.transform.parent.gameObject.transform.parent.gameObject);
        Destroy(gameObject);
    }
}
