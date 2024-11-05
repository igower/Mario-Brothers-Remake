using System;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
   private Rigidbody2D rigidBody;

   private Camera cam;
   private new Collider2D collider;

   private float inputAxis;

   private Vector2 velocity;
   public float movementSpeed = 8f;

   public float maxJump = 5f;
   public float jumpTime = 1f;

   public float jumpForce => (2f * maxJump) / (jumpTime / 2f);
   public float gravity => (-2f * maxJump) / Mathf.Pow((jumpTime / 2f), 2);

   public bool onGround{get; private set;}
   public bool isJumping{get; private set;}
   public bool isSliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);
   public bool isRunning => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;

   private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        cam = Camera.main;
    }

    private void OnEnable()
    {
        rigidBody.isKinematic=false;
        collider.enabled = true;
        velocity = Vector2.zero;
        isJumping = false;
    }

    private void OnDisable()
    {
        rigidBody.isKinematic=true;
        collider.enabled = false;
        velocity = Vector2.zero;
        isJumping = false;
    }

    private void Update()
    {
        //Do horizontal movement
        HorizontalMovement();

        //check if mario is on the ground by sending out a raycast
        onGround = rigidBody.Raycast(Vector2.down);

        if(onGround)
        {
            GroundedMovement();
        }

        //keep mario tethered to the earth each frame
        ApplyGravity();
    }

    private void GroundedMovement()
    {
        //prevent buildup of gravity while grounded
        velocity.y = Mathf.Max(velocity.y, 0f);
        //if velocity is negative, mario isnt Jumping 
        isJumping = velocity.y > 0f;

        // jump if player hits jump button
        if(Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            isJumping = true;
            AudioManager.Instance.PlayerPlay(AudioManager.Instance.jump);
        }
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        //Smooth movement using MoveTowards()
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * movementSpeed, movementSpeed * Time.deltaTime);

        //wall?
        if (rigidBody.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0;
        }

        if(velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f,180f, 0f);
        }


    }

    private void ApplyGravity()
    {
        //Is mario falling?
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        //If so, make gravity stronger
        float mult = falling ? 2f : 1f;
        velocity.y += gravity * Time.deltaTime * mult;

        //Terminal velocity
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void FixedUpdate()
    {
        
        Vector2 pos = rigidBody.position;
        pos += velocity * Time.fixedDeltaTime;
        
        Vector2 leftEdge = cam.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        //ensure mario is not out of bounds
        if (pos.x != Mathf.Clamp(pos.x, leftEdge.x + 0.4f, rightEdge.x - 0.4f))
        {
            pos.x = Mathf.Clamp(pos.x, leftEdge.x + 0.4f, rightEdge.x - 0.4f);
            //cancel out existing velocity if mario is blocked
            velocity.x = 0;
        }

        rigidBody.MovePosition(pos);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            if (transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = jumpForce / 2f;
                isJumping = true;
            }
        }
        else if(collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }
}
