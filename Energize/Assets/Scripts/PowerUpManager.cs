using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
    [SerializeField] private float coolDownTimer;

    const float _MIN_SPAWN_DELAY = 25f;
    const float _MAX_SPAWN_DELAY = 40f;
    GameObject WattRegen;
    // Use this for initialization
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        coolDownTimer += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDownTimer >= Random.Range(_MIN_SPAWN_DELAY, _MAX_SPAWN_DELAY))
        {
            WattRegen = Instantiate(Resources.Load("turnUp4Watt")) as GameObject;
            WattRegen.GetComponent<WattRegenPower>()._Spawn();
            coolDownTimer = 0;
        }
    }
}
