using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject brick;
    float timer;
    public int interval;
    Transform[] bricks;


    // Use this for initialization
    void Start () {
        int children = transform.childCount;
        bricks = new Transform[children];
        for (int i =0; i< bricks.Length; i++)
        {
            bricks[i] = transform.GetChild(i);
        }
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if((int)timer % interval == (int) 0 && (int)timer != (int)0)
        {
            int rand = Random.Range(0, bricks.Length);

            Instantiate(brick, bricks[rand].transform);
            timer = 0;
        }
	}
}
