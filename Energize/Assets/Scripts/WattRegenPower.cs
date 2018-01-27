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

        player = GameObject.Find("playerTemp");
        WattBarManager = player.GetComponent<WattBarManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && onCooldown == false)
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

            GameObject.Destroy(this);
            onCooldown = true;


        }
    }

    public void _Spawn()
    {
            
        Vector3 pos = new Vector3(Random.Range(0.2f , 0.8f), Random.Range(1.1f, 1.3f ), 0);
        gameObject.GetComponent<Transform>().localPosition = Camera.main.ViewportToWorldPoint(pos);
        //Vector3 playerPos = player.GetComponent<Transform>().localPosition;
        
        onCooldown = false;

    }

}
