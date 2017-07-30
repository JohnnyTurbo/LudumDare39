using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpHeight;
    public float jumpImpulseForce, jumpNormalForce;
    public float fallingGravity;
    public float recoveryDelay;
    public Slider healthBar;

    bool isJumping, resettingJump, canRecover, isRecovering;
    float maxJumpElevation, health, timeOfRecovery;
    float  pSpeed, pRecovery, pLaser;
    FeetController feet;
    Vector2 playerInput;
    Rigidbody2D playerRb;
    Transform playerTransform;
    SpriteRenderer playerSR;
    GameController theGC;

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
        theGC = GameObject.Find ("GameController").GetComponent<GameController> ();
    }

    void Start() {
        isJumping = false;
        canRecover = true;
        health = 100f;
        pSpeed = theGC.speedPU.effect;
        pRecovery = theGC.recoveryPU.effect;
        pLaser = theGC.recoveryPU.effect;
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
        if(health <= 0) {
            //GameOver();
        }
        healthBar.value = health / 100f;
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
            playerRb.velocity = new Vector2 (playerInput.x * pSpeed, playerRb.velocity.y);
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
            timeOfRecovery = Time.time + recoveryDelay;
        }
        else {
            timeOfRecovery += recoveryDelay;
        }
    }
    
    IEnumerator Recover() {
        isRecovering = true;
        canRecover = true;
        float recoveryTime = ((100f - health) * 0.01f * pRecovery);
        float timer = 0f;
        float startHealth = health;
        while (health < 100f) {
            if (!canRecover) {
                yield break;
            }
            timer += Time.deltaTime;
            health = Mathf.Lerp (startHealth, 100f, timer / recoveryTime);
            yield return null;
        }
        if(health >= 100f) {
            health = 100f;
        }
        isRecovering = false;

    }
}
