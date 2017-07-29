using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float speed;

    Vector3 velocity = Vector3.zero;
    void FixedUpdate() {
        transform.position = Vector3.SmoothDamp (transform.position, new Vector3 (player.transform.position.x, player.transform.position.y, 
                                                    transform.position.z), ref velocity, speed);
    }

}
