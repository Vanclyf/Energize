using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightningMoveScript : MonoBehaviour {

    public GameObject obj1, obj2;
    public float time;
    Rigidbody2D m_Rigidbody;
    Vector3 dir;
    float vel;
    int i = 1;
    public bool moving;
    private bool wallCollision;
    public Text text;
    LineRenderer lr;
    Color color1, color2, color3;
    WattBarManager wattBarManager;
    float lostWattage = 0;
    int wallHit;
    float timeGoneBy = 0;

    // Use this for initialization
    void Start () {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        lr = transform.parent.GetComponent<LineRenderer>();
        color1 = Color.white;
        color2 = Color.blue;
        color3 = Color.red;
        wattBarManager = GetComponent<WattBarManager>();
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
            timeGoneBy += Time.deltaTime;
            if (timeGoneBy >= 1)
            {

                if (wallHit == 1)
                {
                    wattBarManager.LoseWatt(0.5f);
                    lostWattage += 0.5f;
                }else if(wallHit == 2)
                {
                    wattBarManager.LoseWatt(1.2f);
                    lostWattage += 1.2f;
                }else if (wallHit == 3)
                {
                    wattBarManager.LoseWatt(2f);
                    lostWattage -= 2f;
                }
                text.text = lostWattage + "%";
                timeGoneBy = 0;

            }
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
        //iTween.MoveTo(obj1, obj2.transform.position, time);
        iTween.MoveTo(obj1, iTween.Hash("x", transform.position.x, "y", transform.position.y, "speed", 500,
            "easetype", "linear"));


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        lostWattage = 0;
        MoveObject();
        moving = false;
        lr.startColor = color3;
        lr.endColor = color3;
        if (!moving) {
            wallCollision = true;
            if(collision.gameObject.tag == "MetalWall" )
            {
                wallHit = 1;
            }else if(collision.gameObject.tag == "WoodWall")
            {
                wallHit = 2;
            }else if(collision.gameObject.tag == "RubberWall")
            {
                wallHit = 3;
            }
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        lr.startColor = color1;
        lr.endColor = color2;
        GameObject particleSystem = new GameObject();

        particleSystem = Instantiate(Resources.Load("ShockParticleEmitter"), transform.position, Quaternion.identity) as GameObject;

        Destroy(particleSystem, 2f);
    }
}
