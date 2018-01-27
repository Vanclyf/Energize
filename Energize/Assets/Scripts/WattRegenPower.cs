using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WattRegenPower : MonoBehaviour {
    [SerializeField] private GameObject player;
    private int wattIncrease;

	// Use this for initialization
	void Start () {
        InitializeVariable();
	}

    private void InitializeVariable(){
        wattIncrease = 30;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.GetComponent<WattBarManager>().hitpoint += wattIncrease;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
