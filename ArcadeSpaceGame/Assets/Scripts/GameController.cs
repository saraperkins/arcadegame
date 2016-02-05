using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    //these are phillip's
	public int mult;
    public PlayerController player;
    public EnemyController enemy;

    //end phillip

	public GUIText scoreText;
	private int score;
	public string scoreString;
    public GUIText spinText;
    public string spinString;

	public GUIText restartText;

	public GameObject gameOverText;
    public GameObject Background;
    public GameObject JailImage;
    public GameObject GameOverScore;
    public GameObject GameRestart;

	private bool gameOver;
	private bool restart;

	void Start () {
		gameOver = false;
		restart = false;
		restartText.text = "";
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
			if (Input.GetKeyDown (KeyCode.R))
            {
                Application.LoadLevel (Application.loadedLevel);
			}
		}
        if(player.transform.position.x < enemy.transform.position.x)
        {
            GameOver();
        }
		if (gameOver) {
			//restartText.text = "Press 'R' for Restart";
			//restart = true;

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
		//gameOverText.text = "Game Over!";
		gameOver = true;
        StartCoroutine(doGameOver());
	}

    IEnumerator doGameOver()
    {
        Background.SetActive(true);
        yield return new WaitForSeconds(2);
        JailImage.SetActive(true);
        gameOverText.SetActive(true);
        yield return new WaitForSeconds(1);
        GameOverScore.GetComponent<Text>().text = "Score: " + score;
        GameOverScore.SetActive(true);
        yield return new WaitForSeconds(1);
        GameRestart.SetActive(true);
        restart = true;
    }
}