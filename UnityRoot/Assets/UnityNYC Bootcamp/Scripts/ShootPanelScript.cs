using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPanelScript : SimplePanelScript {

	void Start() {
		timedAutoDeactivate = true;
	}

	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay (contact.point, contact.normal, Color.white);
		}
		if (collision.collider.tag.Equals("Bullet")) {
			this.Activate ();
		}
	}
}
