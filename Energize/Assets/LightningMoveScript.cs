using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightningMoveScript : MonoBehaviour {

    public GameObject obj1, obj2;
    public Transform trans1, trans2, trans3, trans4;
    public float time;
    Rigidbody2D m_Rigidbody;
    Vector3 dir;
    float vel;
    int i = 1;
    public bool moving;
    public Text text;




    // Use this for initialization
    void Start () {
        m_Rigidbody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update () {
        if (moving)
        {
            transform.position += dir * vel * Time.deltaTime;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);

        }

    }

    public void ButtonForMove(Vector2 direction, float velocity)
    {
        dir = direction;
        vel = velocity;
        //obj2.transform.position = new Vector3(posTarget.x, posTarget.y, 1);
        m_Rigidbody.velocity = transform.forward * velocity;
        moving = true;
    }

    public void MoveObject()
    {
        iTween.MoveTo(obj1, obj2.transform.position, time);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        text.text = "COLLOOOOOOOOO";

        MoveObject();
        moving = false;
    }
}
