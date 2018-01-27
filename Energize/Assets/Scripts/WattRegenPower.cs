using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WattRegenPower : MonoBehaviour {
    [SerializeField] private GameObject player;
    private int wattIncrease, maxWatt;
    public bool onCooldown;
    private WattBarManager WattBarManager;
    private float coolDownTimer;
   
    const float _FIRST_SPAWN_DELAY = 20f;
    const float _MIN_SPAWN_DELAY = 25f;
    const float _MAX_SPAWN_DELAY = 40f;


    // Use this for initialization
    void Start () {
        InitializeVariable();
        InitializeCooldown();
	}

    private void InitializeCooldown()
    {
        coolDownTimer += Time.deltaTime;
        if (coolDownTimer >= _FIRST_SPAWN_DELAY)
        {
            _Spawn();
            
        }
    }

    private void InitializeVariable(){
        wattIncrease = 30;
        maxWatt = 100;
        this.gameObject.SetActive(false);
        WattBarManager = player.GetComponent<WattBarManager>();
    }

    // Update is called once per frame
    void Update()
    {
        coolDownTimer += Time.deltaTime ;
        if (coolDownTimer >= Random.Range(_MIN_SPAWN_DELAY, _MAX_SPAWN_DELAY)){
            _Spawn();
        }
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

            this.gameObject.SetActive(false);
            onCooldown = true;


        }
    }

    private void _Spawn()
    {
        coolDownTimer = 0;
     
        Vector3 pos = new Vector3(Random.Range(0.2f , 0.8f), Random.Range(1.1f, 1.3f ), 0);
        gameObject.GetComponent<Transform>().localPosition = Camera.main.ViewportToWorldPoint(pos);
        //Vector3 playerPos = player.GetComponent<Transform>().localPosition;
        this.gameObject.SetActive(true);
        
        



    }

}
