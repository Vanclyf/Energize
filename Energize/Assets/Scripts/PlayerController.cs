using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Vector3 firstPoint;   //First touch position
    private Vector3 lastPoint;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    private Vector2 position;
    private SpriteRenderer _SpriteRenderer;
    private Color _newColor;


    // Use this for initialization
    void Start () {
        initializeVariables();
    }

    private void initializeVariables()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        dragDistance = Screen.width * 50 / 100;
         position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        swipeMovement();
    }

    private void swipeMovement()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                firstPoint = touch.position;
                lastPoint = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lastPoint = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lastPoint = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lastPoint.x - firstPoint.x) > dragDistance || Mathf.Abs(lastPoint.y - firstPoint.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lastPoint.x - firstPoint.x) > Mathf.Abs(lastPoint.y - firstPoint.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lastPoint.x > firstPoint.x))  //If the movement was to the right)
                        {   //Right swipe
                            gameObject.SetActive(false);
                            _SpriteRenderer.color = Color.white;
                        }
                        else
                        {   //Left swipe
                            _SpriteRenderer.color = Color.green;
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lastPoint.y > firstPoint.y)  //If the movement was up
                        {   //Up swipe
                            _SpriteRenderer.color = Color.red;
                        }
                        else
                        {   //Down swipe
                            _SpriteRenderer.color = Color.blue;
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }
}

