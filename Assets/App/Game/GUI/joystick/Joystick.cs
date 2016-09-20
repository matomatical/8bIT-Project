/*
 * On-screen joystick logic.
 *
 * Matt Farrugiam <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler,
			IPointerDownHandler {

		RectTransform baseImage;
		RectTransform stickImage;

		float input = 0;

		/* Allow getting the joystick state. */
		public float Input() {
			return input;
		}

		void Start() {
			// first child object is the base
			baseImage = transform.GetChild(0).gameObject
				.GetComponent<Image>().GetComponent<RectTransform>();

			// ITS first child object is the stick
			stickImage = transform.GetChild(0).GetChild(0)
				.GetComponent<Image>().GetComponent<RectTransform>();

			// set it to the original position
			ResetStickImage();
		}

		public virtual void OnDrag(PointerEventData p) {
			Vector2 position = Vector2.zero;

			bool inside = RectTransformUtility.ScreenPointToLocalPointInRectangle(
					baseImage, p.position, p.pressEventCamera, out position);

			if (inside) {
				input = position.x / (baseImage.sizeDelta.x / 2);

				input = Mathf.Clamp(input, -1, 1);

				float theta = (Mathf.Acos(input) - Mathf.PI/2) / (1.5f) + Mathf.PI/2;
				float stickX = baseImage.sizeDelta.x / 3 * Mathf.Cos(theta);
				float stickY = baseImage.sizeDelta.y / 3 * Mathf.Sin(theta);
				stickImage.anchoredPosition = new Vector2(stickX, stickY);
			}
		}

		public virtual void OnPointerDown(PointerEventData ped) {
			OnDrag(ped);
		}

		public virtual void OnPointerUp(PointerEventData ped) {
			// reset inputDirection and joystick position
			input = 0;

			ResetStickImage();
		}

		void ResetStickImage() {
			stickImage.anchoredPosition = new Vector2(0, baseImage.sizeDelta.y / 4);
		}
	}
}
