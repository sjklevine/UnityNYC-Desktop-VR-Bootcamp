using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GazePanelScript : SimplePanelScript {
	
	void Start() {
		timedAutoDeactivate = true;
	}

	protected override void Update() {
		base.Update ();

		// Hacky, functional, classic.
		if (Input.GetButtonDown ("Fire1")) {
			// Just do a camera raycast - if it hits here, activate!
			RaycastHit hit;
			int gazeOnlyLayerMask = LayerMask.GetMask ("GazeOnly");
			if (Physics.Raycast (
				VRTK.VRTK_DeviceFinder.HeadsetTransform().position,
				VRTK.VRTK_DeviceFinder.HeadsetTransform().forward,
				out hit,
				Mathf.Infinity,
				gazeOnlyLayerMask)
			) {
				if (hit.collider.gameObject == this.gameObject) {
					Debug.Log ("Activate!");
					this.Activate ();
				}
			}						
		}
	}
}
