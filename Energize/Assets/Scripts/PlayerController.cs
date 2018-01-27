using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerController : MonoBehaviour {
    private int speed = 5;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    private Vector3 pos;
    public LightningMoveScript lightningMoveScript;
    public GameObject wattManager;
    private Vector2 nextPosition;
    public GameObject anchorPointsParent;
    public GameObject canvas;
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
        anchorPointsArray = new Transform[anchorPointsParent.transform.childCount];
        for(int i = 0; i < anchorPointsArray.Length; i++)
        {
            anchorPointsArray[i] = anchorPointsParent.transform.GetChild(i);
        }

    }

    // Update is called once per frame
    private void Update()
    {
        RectTransform objectRectTransform = canvas.GetComponent<RectTransform>();
        if (transform.position.y > 280f || transform.position.y < -50)
        {
            SceneManager.LoadScene(0);
        }

        if(inAir == true)
        {
            inAir = lightningMoveScript.moving;
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
            }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacles")
        {
            wattManager.GetComponent<WattBarManager>().TurnDown4Watt(20f);
        }
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

