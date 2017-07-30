using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float panSpeed, zoomSpeed;
    public float camMinSize, camMaxSize, playerMaxVelocity;
    public Vector3 camOffset;

    Camera thisCam;

    void Awake() {
        thisCam = this.GetComponent<Camera> ();
        if (!thisCam) {
            Debug.LogError ("Cannot find the Camera component on gameObject: " + gameObject.name);
        }
    }

    Vector3 velocity = Vector3.zero;
    float velocityFloat = 0f;

    void FixedUpdate() {
        transform.position = Vector3.SmoothDamp (transform.position, new Vector3 (player.transform.position.x, player.transform.position.y, 
                                                    transform.position.z), ref velocity, panSpeed) + camOffset;
        float value = (player.GetComponent<Rigidbody2D> ().velocity.y / playerMaxVelocity);
        
        float targetSize = Mathf.Lerp (camMinSize, camMaxSize, value);
        thisCam.orthographicSize = Mathf.SmoothDamp (thisCam.orthographicSize, targetSize, ref velocityFloat, zoomSpeed);
    }

}
