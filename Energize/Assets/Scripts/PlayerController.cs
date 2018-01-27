using UnityEngine;



public class PlayerController : MonoBehaviour {
    private int speed = 5;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    private Vector3 pos;
    public LightningMoveScript lightningMoveScript;
    public GameObject wattManager;
    private Vector2 nextPosition;
    private bool isMoving;
    public GameObject anchorPointsParent;
    Transform[] anchorPointsArray;
    private Vector2 dir;
    bool inAir = false;


    // Use this for initialization
    void Start () {
        initializeVariables();
        lightningMoveScript = GetComponent<LightningMoveScript>();

    }

    private void initializeVariables()
    {
        pos = transform.position;
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
        Vector2 nextPosition = new Vector2();
        anchorPointsArray = new Transform[anchorPointsParent.transform.childCount];
        for(int i = 0; i < anchorPointsArray.Length; i++)
        {
            anchorPointsArray[i] = anchorPointsParent.transform.GetChild(i);
        }

    }

    // Update is called once per frame
    private void Update()
    {
        if(inAir == true)
        {
            inAir = lightningMoveScript.moving;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isMoving = false;
        }
        if(isMoving == true)
        {
            gotoPosition(nextPosition);
        }
        pos = transform.position;

        if(inAir == false)
        {
            if (Input.touchCount == 1) // user is touching the screen with a single touch
            {

                Touch touch = Input.GetTouch(0); // get the touch
                if (touch.phase == TouchPhase.Began) //check for the first touch
                {
                    //lightningMoveScript.();
                    fp = touch.position;
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
                {
                    lp = touch.position;
                    Vector3 Pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                }
                else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
                {

                    lp = touch.position;  //last touch position. Ommitted if you use list


                    /*for (int i = 0; i < anchorPointsArray.Length; i++)
                    {
                        text.text = "In For" + i;

                        if (anchorPointsArray[i].position.x - lp.x < anchorPointsArray[i + 1].position.x - lp.x &&
                            anchorPointsArray[i].position.y - lp.y < anchorPointsArray[i + 1].position.y - lp.y)
                        {
                            text.text = "in if" + i;

                            lightningMoveScript.ButtonForMove(anchorPointsArray[i].position);
                            text.text = "End If" + i;


                        }

                    }*/


                    dir = lp - fp;
                    dir.Normalize();

                    lightningMoveScript.ButtonForMove(dir, 250);
                    inAir = true;


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
        if(collision.gameObject.tag == "Obstacles")
        {
            wattManager.GetComponent<WattBarManager>().TurnDown4Watt(20f);
        }
    }

    private void newSwipeMovement()
    {
       
    }

    private void gotoPosition(Vector2 pos)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pos, step);
    }


    public GameObject FindClosestAnchor()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Anchorpoint");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

}

