using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningMoveScript : MonoBehaviour {

    public GameObject obj1, obj2;
    public Transform trans1, trans2, trans3, trans4;
    public float time;
    int i = 1;


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void ButtonForMove()
    {
        switch (i)
        {
            case 1:
                obj2.transform.position = trans1.position;
                MoveObject();
                i++;
                return;

            case 2:
                obj2.transform.position = trans2.position;
                MoveObject();
                i = 1;
                return;
        }
    }

    void MoveObject()
    {
        iTween.MoveTo(obj1, obj2.transform.position, time);

    }
}
