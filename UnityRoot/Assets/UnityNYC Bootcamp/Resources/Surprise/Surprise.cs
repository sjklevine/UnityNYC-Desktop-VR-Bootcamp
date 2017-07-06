using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Surprise : MonoBehaviour {
	private Button myButton;
	private float timeoutPeriod = 5.0f;

	private void Start()
	{
		myButton = this.GetComponent<Button> ();
	}

	public void DoSurprise() {
		// Fun little animation for the end of the Interactions section.
		// Spoilers ahead...

		// -------------------------

		// Remove any current floor...
		GameObject currentFloor = GameObject.FindGameObjectWithTag("Floor");
		GameObject.Destroy (currentFloor);

		// Instantiate a secret from "Resources".
		Instantiate(Resources.Load("Surprise/SecretFloor", typeof(GameObject)));

		// -------------------------

		// Disable the button for some time.
		myButton.interactable = false;
		StartCoroutine (EnableAfterTime());
	}

	private IEnumerator EnableAfterTime() {
		yield return new WaitForSeconds (timeoutPeriod);
		myButton.interactable = true;
		Debug.Log ("BACK");
	}
}
