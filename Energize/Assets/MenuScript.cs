using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

    public GameObject gameView, menuView, outScreen, inScreen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameStart()
    {
        /*iTween.MoveTo(gameView, inScreen.transform.position, 1);
        iTween.MoveTo(menuView, outScreen.transform.position, 1);*/

        gameView.SetActive(true);
        menuView.SetActive(false);
    }
}
