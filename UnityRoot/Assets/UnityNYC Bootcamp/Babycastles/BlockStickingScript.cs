using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BlockStickingScript : MonoBehaviour {

    private VRTK_InteractableObject interactionScript;

	void Start () {
        interactionScript = this.GetComponent<VRTK_InteractableObject>();		
	}

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("ON COLLISION ENTER CALLED, hit " + collision.gameObject.name + ", isgrabbed = " + interactionScript.IsGrabbed());
        if (interactionScript.IsGrabbed())
        {
            // Don't stick to the floor!
            if (collision.gameObject.tag.Equals("Floor"))
            {
                return;
            }
            GameObject.Destroy(collision.gameObject.GetComponent<Rigidbody>());
            GameObject.Destroy(collision.gameObject.GetComponent<VRTK_InteractableObject>());
            collision.gameObject.transform.SetParent(this.transform);
        }
    }
}
