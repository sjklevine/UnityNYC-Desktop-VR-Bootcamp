using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class SimplePanelScript : MonoBehaviour {
	public Material yellowMaterial;
	public Material whiteMaterial;

	protected bool timedAutoDeactivate = false;
	protected float activatedTime = 0.25f;
	protected float timer;

	protected virtual void Update() {
		if (timedAutoDeactivate && timer > 0) {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				this.Deactivate ();
			}
		}
	}

	public void Activate()
	{
		this.GetComponent<Image> ().material = yellowMaterial;
		if (timedAutoDeactivate) { timer = activatedTime; }
	}

	public void Deactivate()
	{
		this.GetComponent<Image>().material = whiteMaterial;
	}
}
