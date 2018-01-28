using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WattRegenPower : MonoBehaviour {
    [SerializeField] private GameObject player;
    private int wattIncrease, maxWatt;
    public bool onCooldown;
    private WattBarManager WattBarManager;



    // Use this for initialization
    void Start () {
        InitializeVariable();
	}

  

    private void InitializeVariable(){
        wattIncrease = 30;
        maxWatt = 100;

        WattBarManager = player.GetComponent<WattBarManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
           
            float hp = WattBarManager.hitpoint;
            
            if(hp + wattIncrease > maxWatt)
            {
                WattBarManager.GainWatt( (hp+wattIncrease) - maxWatt);
            }
            else
            {
                WattBarManager.GainWatt(wattIncrease);
            }

        }
    }

}
