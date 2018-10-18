using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private AudioSource sound;

	// Use this for initialization
	void Start () {
        sound = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            collision.gameObject.GetComponent<PlayerController>().AddToScore(100);
            sound.Play();
            Destroy(gameObject, 0.5f);
        }
    }
}
