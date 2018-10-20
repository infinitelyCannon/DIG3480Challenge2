using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    private AudioSource sound;

    public float speed;

	// Use this for initialization
	void Start () {
        sound = GetComponent<AudioSource>();
        sound.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().PlaySound(2);
            collision.gameObject.GetComponent<PlayerController>().AddToScore(425);
            //Have the player grow
            Destroy(gameObject);
        }
    }
}
