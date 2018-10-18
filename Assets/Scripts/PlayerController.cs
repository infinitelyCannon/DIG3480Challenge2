using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rBody;
    private bool facingRight = true;
    private bool isDead = false;
    private Animator anim;
    private int score = 0;
    private AudioSource sound;

    public float speed;
    public float jumpForce;

    private bool isOnGround;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask allGround;

	// Use this for initialization
	void Start () {
        rBody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape)) Application.Quit();

        if (Input.GetKeyDown(KeyCode.Z)) Debug.Log("Score: " + score);
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
        }
    }

    public void AddToScore(int value)
    {
        score += value;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground" && isOnGround && !isDead)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                rBody.velocity = Vector2.up * jumpForce;
                sound.Play();
            }
        }
    }
}
