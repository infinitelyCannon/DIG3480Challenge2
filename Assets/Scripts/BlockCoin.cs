using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCoin : MonoBehaviour {

    private AudioSource sound;

	// Use this for initialization
	void Start () {
        sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TriggerCoin()
    {
        sound.Play();
        Destroy(gameObject, 3f);
    }
}
