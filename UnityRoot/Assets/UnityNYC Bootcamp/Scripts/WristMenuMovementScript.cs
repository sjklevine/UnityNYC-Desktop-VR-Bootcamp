using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using VRTK;

public class WristMenuMovementScript : MonoBehaviour {
	public enum MovementTypes { None, StraightPointer, BezierPointer, RemoteBeam }
	public MovementTypes currentMovementType = MovementTypes.None;
	public bool restrictToNodes;

	public GameObject movementButtonNone;
	public GameObject movementButtonStraightPointer;
	public GameObject movementButtonBezierPointer;
	public GameObject movementButtonRemoteBeam;
	public Toggle restrictToNodesToggle;

	void OnEnable() {
		// Because this prefab dies (or is at least inactivated) a whole lot, make sure the right buttons are pressed.
		// Also, make this a coroutine, so we can wait a frame, because Unity.
		StartCoroutine(UpdateUI());
	}

	public void OnButtonPress(string buttonString) {
		//EventSystem.current.SetSelectedGameObject(null);
		StartCoroutine(DeactivateMovements ());
		switch (buttonString) {
		case "None":
			currentMovementType = MovementTypes.None;
			EventSystem.current.SetSelectedGameObject(movementButtonNone);
			// All done!
			break;
		case "StraightPointer":
			currentMovementType = MovementTypes.StraightPointer;
			EventSystem.current.SetSelectedGameObject(movementButtonStraightPointer);
			VRTK_BasePointerRenderer straightRenderer = VRTK.VRTK_DeviceFinder.GetControllerRightHand ().GetComponent<VRTK_StraightPointerRenderer> ();		
			VRTK.VRTK_DeviceFinder.GetControllerRightHand ().GetComponent<VRTK_Pointer> ().pointerRenderer = straightRenderer;
			break;
		case "BezierPointer":
			currentMovementType = MovementTypes.BezierPointer;
			EventSystem.current.SetSelectedGameObject(movementButtonBezierPointer);
			VRTK_BasePointerRenderer bezierRenderer = VRTK.VRTK_DeviceFinder.GetControllerRightHand ().GetComponent<VRTK_BezierPointerRenderer> ();		
			VRTK.VRTK_DeviceFinder.GetControllerRightHand ().GetComponent<VRTK_Pointer> ().pointerRenderer = bezierRenderer;
			break;
		case "RemoteBeam":
			currentMovementType = MovementTypes.RemoteBeam;
			EventSystem.current.SetSelectedGameObject(movementButtonRemoteBeam);
			// Just need to reactivate the beam.
			GameObject.Find("/RemoteBeam").transform.GetChild(0).gameObject.SetActive (true);
			break;
		}
	}

	public void OnToggleChange(bool newState) {
		// Hide/Show the nodes!
		foreach (Transform t in GameObject.Find("/DestinationPoints").transform) {
			t.gameObject.SetActive (newState);
		}

		// Edit the VRTK target policy!  It's on the right hand, but the Remote uses it too.
		VRTK_PolicyList policy = VRTK.VRTK_DeviceFinder.GetControllerRightHand ().GetComponent<VRTK_PolicyList> ();
		if (newState) {
			policy.identifiers = new List<string> { "MazeWalls", "Default" };
		} else {
			policy.identifiers = new List<string> { "MazeWalls", "DestinationPoints" };
		}
		restrictToNodes = newState;

	}

	private IEnumerator UpdateUI() {
		yield return null; // Thanks, Unity

		// Buttons!
		EventSystem.current.SetSelectedGameObject(null);
		switch (currentMovementType) {
		case MovementTypes.None: EventSystem.current.SetSelectedGameObject(movementButtonNone); break;
		case MovementTypes.StraightPointer: EventSystem.current.SetSelectedGameObject(movementButtonStraightPointer); break;
		case MovementTypes.BezierPointer: EventSystem.current.SetSelectedGameObject(movementButtonBezierPointer); break;
		case MovementTypes.RemoteBeam: EventSystem.current.SetSelectedGameObject(movementButtonRemoteBeam); break;
		}

		// Toggle!
		restrictToNodesToggle.isOn = this.restrictToNodes;			
	}

	private IEnumerator DeactivateMovements() {
		// Disable RemoteBeam by actually setting the Remote to inactive!
		// This line sucks because Unity doesn't let you find inactive gameobjects easily, and we can't save a reference.
		GameObject.Find("/RemoteBeam").transform.GetChild(0).gameObject.SetActive (false);

		// Just killing the reference to Renderers in the Pointer stops the StraightPointer & BezierPointer methods!
		// Right hand might not be available, so wait until it is.
		while (!VRTK.VRTK_DeviceFinder.GetControllerRightHand ()) {
			yield return new WaitForSeconds (0.5f);
		}
		VRTK.VRTK_DeviceFinder.GetControllerRightHand ().GetComponent<VRTK_Pointer> ().pointerRenderer = null;
	}
}
