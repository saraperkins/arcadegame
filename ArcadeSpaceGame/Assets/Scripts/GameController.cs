using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    //these are phillip's
	public int mult;
    public PlayerController player;
    //end phillip

	public GUIText scoreText;
	private int score;
	public string scoreString;
    public GUIText spinText;
    public string spinString;

	public GUIText restartText;
	public GUIText gameOverText;

	private bool gameOver;
	private bool restart;

	void Start () {
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
        UpdateSpins();

        /*
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            player = playerControllerObject.GetComponent<PlayerController>();
        }
        if (player == null)
        {
            Debug.Log("Cannot find 'PlayerController' script");
        }
         */
	}

	void Update () {
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		if (gameOver) {
			restartText.text = "Press 'R' for Restart";
			restart = true;

		}


	}

	void LateUpdate(){
        if (!gameOver)
        {
            score += mult;
            UpdateScore();
        }
	}
		
	public void AddScore ( int newScoreValue) {
		score += newScoreValue;
		if (!gameOver) {
			UpdateScore ();
		}
	}


	void UpdateScore () {
		
		scoreText.text = scoreString + score;
	}

    public void UpdateSpins (){
        spinText.text = spinString + player.GetSpins();
    }

	public void GameOver () {
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}