using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public GameObject player;
    public Text text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "X = " + player.GetComponent<Rigidbody2D>().position.x + "Y = " + player.GetComponent<Rigidbody2D>().position.y;

    }
}
