namespace VRTK.Examples
{
	using System.Collections;
    using UnityEngine;

    public class Remote_Beam : MonoBehaviour
    {
		public bool startDisabled;

        private Vector2 touchAxis;
        private float rotationSpeed = 180f;
        private float currentYaw;
        private float currentPitch;

		void Start() {
			// Have to use a coroutine for this, otherwise VRTK won't teleport properly...
			if (startDisabled) {
				StartCoroutine (WaitThenDisable ());
			}
		}
        public void SetTouchAxis(Vector2 data)
        {
            touchAxis = data;
        }

        private void FixedUpdate()
        {
            currentYaw += touchAxis.y * rotationSpeed * Time.deltaTime;
            currentPitch += touchAxis.x * rotationSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.AngleAxis(currentPitch, Vector3.up) * Quaternion.AngleAxis(currentYaw, Vector3.left);
        }

		private IEnumerator WaitThenDisable() {
			yield return new WaitForSeconds (0.5f);
			this.gameObject.SetActive (false);
		}
    }
}