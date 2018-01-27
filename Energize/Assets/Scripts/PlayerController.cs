using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private int speed = 5;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    private Vector3 pos;
    public LightningMoveScript lightningMoveScript;

    // Use this for initialization
    void Start () {
        initializeVariables();
    }

    private void initializeVariables()
    {
        pos = transform.position;
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen

    }

    // Update is called once per frame
    private void Update()
    {
        pos = transform.position;
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
                Vector3 Pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                lightningMoveScript.ButtonForMove(Pos);

            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list
                lightningMoveScript.MoveObject();


                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                
                    pos = Camera.main.ScreenToWorldPoint(lp);
                    pos.z = transform.position.z;


                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");

                }
            }
            else
            {

            }


            /** if (Input.touchCount > 0)
             {
                 Vector2 touchPosition = Input.GetTouch(0).position;
                 touchPosition.x = this.transform.position.x - Camera.main.transform.position.x;
                 touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                 touchPosition.y = this.transform.position.y;
                 this.transform.position = Vector2.MoveTowards(this.transform.position, touchPosition, Time.deltaTime * speed);
             }**/
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private void newSwipeMovement()
    {
       
    }

    
}

