using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using VRTK;

public class SimplePanelScript : MonoBehaviour {
	public Material defaultMaterial;
	public Material activateMaterial;
	public UnityEvent onActivate;

	protected bool timedAutoDeactivate = false;
	protected float activatedTime = 0.25f;
	protected float timer;
	protected bool isActivated;

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
		this.GetComponent<Image> ().material = activateMaterial;
		if (timedAutoDeactivate) { timer = activatedTime; }
		if (onActivate != null) { onActivate.Invoke (); }
		isActivated = true;
	}

	public void Deactivate()
	{
		this.GetComponent<Image>().material = defaultMaterial;
		isActivated = false;
	}
}
