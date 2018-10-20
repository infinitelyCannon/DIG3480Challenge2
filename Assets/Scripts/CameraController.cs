using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject Player;

    private Vector3 offset;
    private AudioSource sound;

	// Use this for initialization
	void Start () {
        offset = transform.position - Player.transform.position;
        sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.transform.position.x > 0 && Player.transform.position.x < 190)
        {
            transform.position = new Vector3(Player.transform.position.x + offset.x, transform.position.y, -10);
        }
    }

    public void Stop()
    {
        if (sound.isPlaying)
            sound.Stop();
    }
}
