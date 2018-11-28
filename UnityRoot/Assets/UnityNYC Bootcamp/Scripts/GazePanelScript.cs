using UnityEngine;

public class GazePanelScript : SimplePanelScript {
	
	void Start() {
		timedAutoDeactivate = true;
	}

	protected override void Update() {
		base.Update ();

        if (VRTK.VRTK_DeviceFinder.HeadsetTransform() != null) { 
            // Just do a camera raycast EVERY FRAME - if it hits here, activate!
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
                    if (!this.isActivated)
                    {
                        //Debug.Log("Activate!");
                        this.Activate();
                    }
                }
		    }						
	    }
    }
}
