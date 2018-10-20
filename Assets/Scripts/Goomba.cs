using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour {

    private bool isDead = false;
    private bool goRight = true;
    private Animator anim;
    private AudioSource sound;

    public float speed;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(goRight && !isDead)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.right, speed * Time.deltaTime);
        }
        else if(!goRight && !isDead)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.left, speed * Time.deltaTime);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            goRight = !goRight;
        }

        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().isOnGround)
        {
            isDead = true;
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
    }

    public void PlayDeath()
    {
        sound.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<PlayerController>().isOnGround)
        {
            isDead = true;
            anim.SetTrigger("Death");
            collision.gameObject.GetComponent<PlayerController>().AddToScore(150);
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(gameObject, 1.3f);
        }
    }
}
