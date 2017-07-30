using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetController : MonoBehaviour {

    public bool isGrounded;

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "CrawlerHead") {
            transform.parent.GetComponent<PlayerController> ().SetGravityScale (1f);
            col.transform.parent.gameObject.GetComponent<Crawler> ().KillEnemy ();
            Vector2 forceToBeAdded = col.transform.parent.gameObject.GetComponent<Crawler> ().getOffMyBack * Vector2.up;
            transform.parent.GetComponent<Rigidbody2D> ().AddForce (forceToBeAdded, ForceMode2D.Impulse);
        }
        else if(col.gameObject.tag == "Crawler") {
            transform.parent.GetComponent<PlayerController> ().SetGravityScale (1f);
            Vector2 forceToBeAdded = col.transform.GetComponent<Crawler> ().getOffMyBack * new Vector2(1,1);
            Debug.Log (forceToBeAdded.ToString ());
            transform.parent.GetComponent<Rigidbody2D> ().AddForce (forceToBeAdded, ForceMode2D.Impulse);
        }
    }

    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.tag == "Ground") {
            isGrounded = true;
            //Debug.Log ("Setting isGrounded to true");
            transform.parent.GetComponent<PlayerController> ().SetGravityScale (1f);
        }
        else if (col.gameObject.tag == "Crawler") {
            transform.parent.GetComponent<PlayerController> ().SetGravityScale (1f);
            Vector2 forceToBeAdded = col.transform.GetComponent<Crawler> ().getOffMyBack * new Vector2 (1, 1);
            Debug.Log (forceToBeAdded.ToString ());
            transform.parent.GetComponent<Rigidbody2D> ().AddForce (forceToBeAdded, ForceMode2D.Impulse);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Ground") {
            isGrounded = false;
            //Debug.Log ("Setting isGrounded to false");
        }
    }
}
