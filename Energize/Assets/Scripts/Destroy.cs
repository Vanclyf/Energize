using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    float timer;
    public int destroyTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
