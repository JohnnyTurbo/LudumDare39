using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpHeight;
    public float jumpImpulseForce, jumpNormalForce;
    public float fallingGravity;
    public Transform groundCheck;

    bool canJump, isJumping, resettingJump;
    float maxJumpElevation;
    FeetController feet;
    Vector2 playerInput;
    Rigidbody2D playerRb;
    Transform playerTransform;

    void Awake() {
        playerRb = this.gameObject.GetComponent<Rigidbody2D> ();
        if (!playerRb) {
            Debug.LogError ("There is no Rigidbody attached to the gameObject " + this.gameObject.name);
        }

        playerTransform = this.gameObject.GetComponent<Transform> ();
        if (!playerTransform) {
            Debug.LogError ("There is no Transform attached to the gameObject " + this.gameObject.name);
        }
        feet = transform.Find ("FootTrigger").GetComponent<FeetController>();
        if (!feet) {
            Debug.LogError ("Cannot find FeetController component on gameObject called 'FootTrigger' from: " + gameObject.name);
        }
    }

    void Start() {
        canJump = true;
        isJumping = false;
    }

    void Update() {
        ObtainPlayerInput ();
        //Debug.Log (IsGrounded().ToString ());
        if(playerRb.velocity.y < 0) {
            playerRb.gravityScale = fallingGravity;
        }
    }

    void FixedUpdate() {
        MovePlayer ();
    }

    void ObtainPlayerInput() {
        playerInput = new Vector2 (Input.GetAxis ("Horizontal"), 0);
        if(Input.GetButton ("Jump")) {
            Jump ();
        }
        if (Input.GetButtonUp ("Jump")) {
            isJumping = false;
            playerRb.gravityScale = fallingGravity;
        }
    }

    void MovePlayer() {
        //playerRb.MovePosition (new Vector2(playerRb.position.x + playerInput.x * speed * Time.deltaTime, playerRb.position.y));
        //playerRb.AddForce(new Vector2(playerInput.x * speed, 0));
        playerRb.velocity = new Vector2 (playerInput.x * speed, playerRb.velocity.y);
        /*
        if (playerInput.y > 0) {
            if (feet.isGrounded && !isJumping) {
                //Start Jump w/ impulse force
                StartJump ();
            }
            else if(canJump && playerRb.velocity.y >= jumpHeight) {
                //Stop adding upward force
                canJump = false;
            }
            else {
                //add normal force upwards
                playerRb.AddForce (Vector2.up * jumpNormalForce);
            }
        }
        else if(!feet.isGrounded) {
            canJump = false;
        }
        else {
            canJump = true;
        }
        */
    }

    void Jump() {
        //Debug.Log ("Jump()");
        if(feet.isGrounded && !isJumping) {
            Debug.Log ("Starting Jump!");
            isJumping = true;
            maxJumpElevation = playerTransform.position.y + jumpHeight;
            Debug.Log (maxJumpElevation);
            playerRb.AddForce (Vector2.up * jumpImpulseForce, ForceMode2D.Impulse);
        }
        else if (isJumping && playerTransform.position.y >= maxJumpElevation) {
            Debug.Log ("At Max Height!");
            playerRb.gravityScale = fallingGravity;
            isJumping = false;
        }
        else if (isJumping) {
            Debug.Log ("Adding Force up!");
            playerRb.AddForce (Vector2.up * jumpNormalForce);
        }
    }

    void StartJump() {
        Debug.Log ("Starting Jump");
        playerRb.AddForce (Vector2.up * jumpImpulseForce);
        isJumping = true;
        maxJumpElevation = playerTransform.position.y + jumpHeight;
        Debug.Log (maxJumpElevation);
    }

    public void SetGravityScale(float grav) {
        playerRb.gravityScale = grav;
    }
}
