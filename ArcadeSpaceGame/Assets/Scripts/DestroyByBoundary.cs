using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	private GameController gameController;
	public int shipSavedScoreValue;

	void Start () {
		

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}

	}

	 // adding scores for objects dodged example
	void OnTriggerExit(Collider other) {
        if (other.tag != "Enemy")
        {

            Destroy(other.gameObject);
        }

		if (other.tag == "Enemy") {
			//gameController.AddShip ();
			//gameController.AddScore (shipSavedScoreValue);
		}


	}
	
}