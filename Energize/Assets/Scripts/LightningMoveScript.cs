using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    //audio
    [SerializeField] private AudioClip playerLand, playerJump;
    private AudioSource _audioSource;


    // Use this for initialization
    void Start () {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        lr = transform.parent.GetComponent<LineRenderer>();
        color1 = Color.white;
        color2 = Color.blue;
        color3 = Color.red;
        wattBarManager = GetComponent<WattBarManager>();
        _audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update () {
        if (moving)
        {
            transform.position += dir * vel * Time.deltaTime;

        }
        else
        {
            if (wallCollision)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);

            }

            timeGoneBy += Time.deltaTime;
            if (timeGoneBy >= 1)
            {
                if (wallHit == 1)
                {
                    wattBarManager.LoseWatt(0.5f);
                    lostWattage += 0.5f;
                }else if(wallHit == 2)
                {
                    wattBarManager.LoseWatt(1f);
                    lostWattage += 1.2f;
                }else if (wallHit == 3)
                {
                    wattBarManager.LoseWatt(2f);
                    lostWattage -= 2f;
                }
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
        moving = false;
        lostWattage = 0;
        if (!moving) {

            wallCollision = true;
            if (!_audioSource.isPlaying) {
                 _audioSource.PlayOneShot(playerLand, 0.5f);
            }
           
            if (collision.gameObject.tag == "MetalWall" )
            {
                _audioSource.PlayOneShot(playerLand);
                wallHit = 1;
                OnWall();
                wallCollision = true;
            }
            else if(collision.gameObject.tag == "WoodWall")
            {
                wallHit = 2;
                OnWall();
                wallCollision = true;

            }
            else if(collision.gameObject.tag == "RubberWall")
            {
                wallHit = 3;
                OnWall();
                wallCollision = true;
            }
            else if(collision.gameObject.tag == "TopNBottom")
            {
                OnWall();
                dir = new Vector3(dir.x, dir.y * -1, dir.z);
                ButtonForMove(dir, vel);
            }
            else if (collision.gameObject.tag == "Mirror")
            {
                OnWall();
                dir = new Vector3(dir.x * -1, dir.y, dir.z);
                ButtonForMove(dir, vel);
            }
            else if (collision.gameObject.tag == "NextLevel")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                OnWall();
            }
        }

    }

    void OnWall()
    {
        MoveObject();
        lr.startColor = color3;
        lr.endColor = color3;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        lr.startColor = color1;
        lr.endColor = color2;
        GameObject particleSystem = new GameObject();

        particleSystem = Instantiate(Resources.Load("ShockParticleEmitter"), transform.position, Quaternion.identity) as GameObject;
        if (!_audioSource.isPlaying) { 
            _audioSource.PlayOneShot(playerJump, 0.5f);
        }

        wallCollision = false;

        particleSystem = Instantiate(Resources.Load("ShockParticleEmitter")) as GameObject;
        particleSystem.transform.parent = transform.parent;
        particleSystem.transform.position = transform.position;
        Destroy(particleSystem, 2f);
    }
}
