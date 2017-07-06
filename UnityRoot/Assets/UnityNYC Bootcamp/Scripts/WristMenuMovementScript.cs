using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WristMenuMovementScript : MonoBehaviour {
	public enum MovementTypes { None, PointShoot, RemoteBeam }
	public MovementTypes currentMovementType = MovementTypes.None;

	public Button movementButtonNone;
	public Button movementButtonPointShoot;
	public Button movementButtonRemoteBeam;

	void Start() {

	}

	public void OnButtonPress(string buttonString) {
		EventSystem.current.SetSelectedGameObject(null);

		switch (buttonString) {
		case "PointShoot":
			currentMovementType = MovementTypes.PointShoot;
			movementButtonPointShoot.Select();
			break;
		case "RemoteBeam":
			currentMovementType = MovementTypes.RemoteBeam;
			movementButtonRemoteBeam.Select();
			break;
		}
	}
}
