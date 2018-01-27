using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightningMoveScript : MonoBehaviour {

    public GameObject obj1, obj2;
    public Transform trans1, trans2, trans3, trans4;
    public float time;
    public Text text;

    int i = 1;


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "X = " + obj2.transform.position.x + "Y = " + obj2.transform.position.y + "X = " + obj2.transform.position.z;

    }

    public void ButtonForMove(Vector3 posTarget)
    {
        obj2.transform.position = new Vector3(posTarget.x, posTarget.y, 1);

    }

    public void MoveObject()
    {
        iTween.MoveTo(obj1, obj2.transform.position, time);

    }
}
