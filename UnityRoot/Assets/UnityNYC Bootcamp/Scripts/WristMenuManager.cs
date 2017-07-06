// Based off of Controller_Menu from the VRTK Examples.
using UnityEngine;
using VRTK;

public class WristMenuManager : MonoBehaviour
{
    public GameObject menuObject;
	public bool rightHand;

    private GameObject clonedMenuObject;

    private bool menuInit = false;
    private bool menuActive = false;

    private void Start()
    {
        //GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += new ControllerInteractionEventHandler(DoMenuOn);
        //GetComponent<VRTK_ControllerEvents>().ButtonTwoReleased += new ControllerInteractionEventHandler(DoMenuOff);
        menuInit = false;
        menuActive = false;
    }

    private void InitMenu()
    {
        clonedMenuObject = Instantiate(menuObject, transform.position, Quaternion.identity) as GameObject;
		if (!rightHand) {
			Transform handMenuCanvas = clonedMenuObject.transform.GetChild (0);
			handMenuCanvas.Rotate(Vector3.up*180f, Space.Self);
		}
        clonedMenuObject.SetActive(true);
        menuInit = true;
    }

    private void DoMenuOn(object sender, ControllerInteractionEventArgs e)
    {
        if (!menuInit)
        {
            InitMenu();
        }
        if (clonedMenuObject != null)
        {
            clonedMenuObject.SetActive(true);
            menuActive = true;
        }
    }

    private void DoMenuOff(object sender, ControllerInteractionEventArgs e)
    {
        if (clonedMenuObject != null)
        {
            clonedMenuObject.SetActive(false);
            menuActive = false;
        }
    }

    private void Update()
    {
        // Try to pop the menu when the WRIST is within an angle threshold from facing the headset.
		Transform headset = VRTK.VRTK_DeviceFinder.HeadsetTransform();
		if (headset != null) {
			Vector3 angleToHeadset = headset.position - this.transform.position;
			Vector3 wristAngle = this.transform.right;
			if (rightHand) { wristAngle *= -1; }; // Wrist ~= x or -x axis depending on hand

			// For sanity, draw these two angles as gizmos.
			Debug.DrawLine (this.transform.position, this.transform.position + angleToHeadset.normalized, Color.red);
			Debug.DrawLine (this.transform.position, this.transform.position + wristAngle.normalized, Color.green);

			float angleAwayFromFace = Vector3.Angle (angleToHeadset, wristAngle);
			float thresholdY = 30;
			//Debug.Log("Controller getting y angle: " + this.transform.parent.localRotation.eulerAngles.y);
			if (angleAwayFromFace < thresholdY) {
				this.DoMenuOn (null, new ControllerInteractionEventArgs ());
			} else {
				this.DoMenuOff (null, new ControllerInteractionEventArgs ());
			}
		}

        if (clonedMenuObject != null && menuActive)
        {
            clonedMenuObject.transform.rotation = transform.rotation;
            clonedMenuObject.transform.position = transform.position;
        }
    }
}