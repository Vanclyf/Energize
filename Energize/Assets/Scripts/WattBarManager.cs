using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WattBarManager : MonoBehaviour
{

    public Image currentWattBar;
    public Text text;

    private enum BATTERY_STATES
    {
        BATTERY_GREEN = 0,
        BATTERY_YELLOW = 1,
        BATTERY_ORANGE = 2,
        BATTERY_RED = 3,
        BATTERY_DEAD = 4
    }

    private BATTERY_STATES currentState;
    public float hitpoint = 100;
    private float maxHitpoint = 100;

    private void Start()
    {
        currentState = BATTERY_STATES.BATTERY_GREEN;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TurnDown4Watt(1);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            GainWatt(1);
        }
    }

    private void UpdateHealthBar()
    {
        float ratio = hitpoint / maxHitpoint;
        currentWattBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        CheckChange();
        text.text = (ratio * 100).ToString() + '%';
        if(hitpoint < 55)
        {
            text.color = Color.white;
        }

        if(hitpoint >= 55)
        {
            text.color = Color.black;
        }
    }

    /// <summary>
    /// When the player loses watt
    /// </summary>
    /// <param name="wattLost">The watt lost.</param>
    public void TurnDown4Watt(float wattLost)
    {
        hitpoint -= wattLost;
        if (hitpoint <= 0)
        {
            hitpoint = 0;
            //INITIATE DEAD PROTOCOL
        }
        UpdateHealthBar();

    }
    /// <summary>
    /// When the player gains watt
    /// </summary>
    /// <param name="wattGained">The watt gained.</param>
    public void GainWatt(float wattGained)
    {
        hitpoint += wattGained;
        Debug.Log("HitPoints: " + hitpoint);
        if (hitpoint >= 100)
        {
            hitpoint = 100;
        }
        UpdateHealthBar();


    }
    /// <summary>
    /// Checks the change in the Color of the status bar.
    /// </summary>
    private void CheckChange()
    {
        if (hitpoint <= 100 && hitpoint > 75)
        {
            currentState = BATTERY_STATES.BATTERY_GREEN;
            ChangeColor();
        }
        else if (hitpoint <= 75 && hitpoint > 50)
        {
            currentState = BATTERY_STATES.BATTERY_YELLOW;
            ChangeColor();
        }

        else if (hitpoint <= 50 && hitpoint > 25)
        {
            currentState = BATTERY_STATES.BATTERY_ORANGE;
            ChangeColor();

        }

        else if (hitpoint <= 25 && hitpoint > 0)
        {
            currentState = BATTERY_STATES.BATTERY_RED;
            ChangeColor();

        }
        else if (hitpoint == 0)
        {
            currentState = BATTERY_STATES.BATTERY_DEAD;
            ChangeColor();

        }
    }

    /// <summary>
    /// Changes the color of the bar.
    /// </summary>
    private void ChangeColor()
    {
        switch (currentState)
        {
            case BATTERY_STATES.BATTERY_GREEN:
                currentWattBar.color = Color.green;
                break;
            case BATTERY_STATES.BATTERY_YELLOW:
                currentWattBar.color = new Color(255, 30, 0);
                break;
            case BATTERY_STATES.BATTERY_ORANGE:
                currentWattBar.color = Color.yellow;
                break;
            case BATTERY_STATES.BATTERY_RED:
                currentWattBar.color = Color.red;
                break;
            case BATTERY_STATES.BATTERY_DEAD:
                break;
            default:
                break;
        }
    }
}
