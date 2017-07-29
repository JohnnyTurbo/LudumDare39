using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetController : MonoBehaviour {

    public bool isGrounded;

    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.tag == "Ground") {
            isGrounded = true;
            //Debug.Log ("Setting isGrounded to true");
            transform.parent.GetComponent<PlayerController> ().SetGravityScale (1f);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Ground") {
            isGrounded = false;
            //Debug.Log ("Setting isGrounded to false");
        }
    }
}
