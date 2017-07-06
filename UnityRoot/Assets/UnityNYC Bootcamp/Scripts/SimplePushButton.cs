using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimplePushButton : MonoBehaviour {
	private Button myButton;

	void OnEnable() {
		// Just in case buttons stick disabled from dead coroutines.
		myButton = this.GetComponent<Button> ();
		myButton.interactable = true;
	}

	void OnTriggerEnter(Collider other) {
		//Debug.Log ("TRIGGER ENTER - " + other.name);
		// This is hacky as fuuu, but it works
		if (other.name.Equals("Sphere") && myButton.IsInteractable()) {
			Debug.Log ("SimpleButtonPush! " + this.name);
			myButton.onClick.Invoke ();
		}
	}
}
