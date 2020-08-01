using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 5f;
    private float movement = 0f;
    private Rigidbody2D rigidBody;
    public Transform groundCheckPoint; //should be at lowest point on sprite. Centre if ball, at feet if character
    public float groundCheckRadius;
    public LayerMask groundLayer; // can tick multiple layers
    private bool isTouchingGround;
    private Animator playerAnimation;
    public Vector3 respawnPoint;
    public Vector3 restartPoint;
    public LevelManager gameLevelManager;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        respawnPoint = transform.position; //first respawn point
        restartPoint = transform.position; // restart of level
        gameLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        movement = Input.GetAxis("Horizontal");

        if(movement > 0f) //right
        {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(0.5f,0.5f); // would it be easier to access this as oppose to har coding? idk
        }
        else if(movement < 0f) //left
        {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(-0.5f, 0.5f);
        }
        else // if you don't want the player moving unless user inputs
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }

        if((Input.GetButtonDown("Jump") || Input.GetKey("up")) && isTouchingGround) //spacebar is "jump"
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }

        // sending info to animator
        playerAnimation.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);
        playerAnimation.SetFloat("VerticalSpeed", rigidBody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "FallDetector")
        {
            gameLevelManager.Respawn();
        }
        if(other.tag == "Checkpoint")
        {
            respawnPoint = other.transform.position;
        }
    }
}
