using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    public float cameraSpeed;


	// Use this for initialization
	void Start () {
        cameraSpeed = 0;
	}

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(transform.position.x, transform.position.y + cameraSpeed, transform.position.z);
    }
}
