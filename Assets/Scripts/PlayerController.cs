using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rBody;
    private bool facingRight = true;
    private bool isDead = false;
    private Animator anim;
    private int score = 0;
    private AudioSource sound;

    public Text scoreText;
    public Text endText;
    public float speed;
    public float jumpForce;
    public string[] solidTags;
    public AudioClip[] soundFXs;

    [HideInInspector]
    public bool isOnGround;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask allGround;

	// Use this for initialization
	void Start () {
        rBody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
        scoreText.text = "MARIO\n0";
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape)) Application.Quit();

        if (transform.position.y <= -6f)
        {
            GameObject.Find("Main Camera").GetComponent<CameraController>().Stop();
            isDead = true;
            anim.SetBool("isJumping", false);
            anim.SetTrigger("Death");
            GameOver();
        }

        if (isDead && Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        if (isDead)
        {
            endText.text = "Game Over\nPress Spacebar to restart.";
        }
        else
        {
            endText.text = "Game Over\nYou Won!";
        }
    }

    public void Die()
    {
        GameObject.Find("Main Camera").GetComponent<CameraController>().Stop();
        isDead = true;
        anim.SetTrigger("Death");
        rBody.gravityScale = 0;
        GameOver();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (!isDead)
        {
            rBody.velocity = new Vector2(moveHorizontal * speed, rBody.velocity.y);

            anim.SetInteger("Velocity", (int)moveHorizontal);

            isOnGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, allGround);

            anim.SetBool("isJumping", !isOnGround);

            if (!facingRight && moveHorizontal > 0)
                Flip();
            else if (facingRight && moveHorizontal < 0)
                Flip();

            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) && isOnGround)
            {
                rBody.velocity = Vector2.up * jumpForce;
                PlaySound(0);
            }
        }
    }

    public void AddToScore(int value, bool playSound = false)
    {
        score += value;
        scoreText.text = "MARIO\n" + score;

        if (playSound) PlaySound(1);
    }

    public void PlaySound(int index)
    {
        sound.clip = soundFXs[index];
        sound.Play();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    bool isSolidTag(string tag)
    {
        foreach(string t in solidTags)
        {
            if (tag.Equals(t)) return true;
        }

        return false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        /*if (isSolidTag(collision.collider.tag) && isOnGround && !isDead)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                rBody.velocity = Vector2.up * jumpForce;
                PlaySound(0);
            }
        }*/
    }
}
