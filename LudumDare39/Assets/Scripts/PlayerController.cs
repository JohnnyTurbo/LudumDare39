using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpHeight;
    public float jumpImpulseForce, jumpNormalForce;
    public float fallingGravity;
    public float recoveryTime;
    public float zeroToAHunnid;
    public Transform groundCheck;

    bool canJump, isJumping, resettingJump, canRecover, isRecovering;
    float maxJumpElevation, health, timeOfRecovery;
    FeetController feet;
    Vector2 playerInput;
    Rigidbody2D playerRb;
    Transform playerTransform;
    SpriteRenderer playerSR;

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
        playerSR = this.gameObject.GetComponent<SpriteRenderer> ();
        if (!playerSR) {
            Debug.LogError ("There is no SpriteRenderer component on the gameObject " + this.gameObject.name);
        }
    }

    void Start() {
        canJump = true;
        isJumping = false;
        canRecover = true;
        health = 100f;
    }

    void Update() {
        ObtainPlayerInput ();
        //Debug.Log (IsGrounded().ToString ());
        if(playerRb.velocity.y < 0) {
            playerRb.gravityScale = fallingGravity;
        }

        if(health < 100f && Time.time > timeOfRecovery && !isRecovering) {
            StartCoroutine ("Recover");
        }
    }

    void FixedUpdate() {
        MovePlayer ();
        //Debug.Log ("Player velocity: " + playerRb.velocity);
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
            playerRb.velocity = new Vector2 (playerInput.x * speed, playerRb.velocity.y);
        if (playerInput.x > 0) {
            //Face right
            Vector3 scaleTool = transform.localScale;
            scaleTool.x = 1f;
            transform.localScale = scaleTool;
        }
        else if (playerInput.x < 0) {
            //Face left
            Vector3 scaleTool = transform.localScale;
            scaleTool.x = -1f;
            transform.localScale = scaleTool;
        }
    }

    void Jump() {
        //Debug.Log ("Jump()");
        if(feet.isGrounded && !isJumping) {
            //Debug.Log ("Starting Jump!");
            isJumping = true;
            maxJumpElevation = playerTransform.position.y + jumpHeight;
            Debug.Log (maxJumpElevation);
            playerRb.AddForce (Vector2.up * jumpImpulseForce, ForceMode2D.Impulse);
        }
        else if (isJumping && playerTransform.position.y >= maxJumpElevation) {
            //Debug.Log ("At Max Height!");
            playerRb.gravityScale = fallingGravity;
            isJumping = false;
        }
        else if (isJumping) {
            //Debug.Log ("Adding Force up!");
            playerRb.AddForce (Vector2.up * jumpNormalForce);
        }
    }

    void ShootLaser() {

    }

    public void SetGravityScale(float grav) {
        playerRb.gravityScale = grav;
    }

    public void HarmPlayer(float attackStrength) {
        health -= attackStrength;
        if (canRecover) {
            canRecover = false;
            timeOfRecovery = Time.time + recoveryTime;
        }
        else {
            timeOfRecovery += recoveryTime;
        }
    }
    
    IEnumerator Recover() {
        isRecovering = true;

        while (health < 100f) {
            health += (100f - health) * .01f * zeroToAHunnid * Time.deltaTime;
            yield return null;
        }
        if(health >= 100f) {
            health = 100f;
        }
        isRecovering = false;
        canRecover = true;
    }
}
