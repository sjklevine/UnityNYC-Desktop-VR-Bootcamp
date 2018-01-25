namespace VRTK.Examples
{
	using UnityEngine;

	public class Remote_Beam_Controller : MonoBehaviour
	{
		public Remote_Beam remoteBeamScript;

		private void Start()
		{
			GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += new ControllerInteractionEventHandler(DoTouchpadAxisChanged);
			GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd += new ControllerInteractionEventHandler(DoTouchpadTouchEnd);
		}

		private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
		{
			remoteBeamScript.SetTouchAxis(e.touchpadAxis);
		}

		private void DoTouchpadTouchEnd(object sender, ControllerInteractionEventArgs e)
		{
			remoteBeamScript.SetTouchAxis(Vector2.zero);
		}
	}
}