using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverPanelScript : SimplePanelScript, IPointerEnterHandler, IPointerExitHandler {

	void IPointerEnterHandler.OnPointerEnter (PointerEventData eventData)
	{
		this.Activate ();
	}
	void IPointerExitHandler.OnPointerExit (PointerEventData eventData)
	{
		this.Deactivate ();
	}
}
