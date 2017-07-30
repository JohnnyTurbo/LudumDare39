using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : MonoBehaviour {

    public float health, moveSpeed, attackStrength, upwardsForce, getOffMyBack;
    public Sprite deadSprite;
    public bool startFlipped;

    Rigidbody2D crawlerRb;
    CircleCollider2D headCollider;
    BoxCollider2D bodyCollider;
    SpriteRenderer crawlerSprRenderer;
    Animator crawlerAnim;

    void Awake() {
        crawlerRb = this.GetComponent<Rigidbody2D> ();
        if (!crawlerRb) {
            Debug.LogError ("Cannot find the Rigidbody2D component on gameObject " + gameObject.name);
        }

        headCollider = transform.GetComponentInChildren<CircleCollider2D> ();
        if (!headCollider) {
            Debug.LogError ("Cannot find the CircleCollider2D component on child gameObject of gameObject " + gameObject.name);
        }

        bodyCollider = transform.GetComponent<BoxCollider2D> ();
        if (!bodyCollider) {
            Debug.LogError ("Cannot find the BoxCollider2D component on gameObject " + gameObject.name);
        }

        crawlerSprRenderer = this.GetComponent<SpriteRenderer> ();
        if (!crawlerSprRenderer) {
            Debug.LogError ("Cannot find the SpriteRenderer component on gameObject " + gameObject.name);
        }
        crawlerAnim = this.GetComponent<Animator> ();
        if (!crawlerAnim) {
            Debug.LogError ("Cannot find the Animator component on gameObject " + gameObject.name);
        }
    }

    void Start() {
        if (startFlipped) {
            TurnAround ();
        }
    }

    void Update() {
        if(crawlerRb.velocity.x == 0) {
            crawlerRb.AddForce (Vector2.up, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate() {
        MoveCrawler ();
    }

    void MoveCrawler() {
        crawlerRb.velocity = new Vector2(transform.localScale.x * moveSpeed * -1, crawlerRb.velocity.y);
    }


    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Wall") {
            TurnAround ();
        }

        if (col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerController> ().HarmPlayer (10f);
        }

    }

    void TurnAround() {
        Vector3 swichDir = transform.localScale;
        swichDir.x *= -1;
        transform.localScale = swichDir;
    }

    public void KillEnemy() {
        crawlerRb.AddForce (Vector2.up * upwardsForce, ForceMode2D.Impulse);
        bodyCollider.enabled = false;
        headCollider.enabled = false;
        crawlerAnim.enabled = false;
        crawlerSprRenderer.sprite = deadSprite;
    }
}
