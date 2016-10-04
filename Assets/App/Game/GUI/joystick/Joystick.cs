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

		RectTransform socket;
		RectTransform handle;
		RectTransform stick;

		float input = 0;

		/* Allow getting the joystick state. */
		public float Input() {
			return input;
		}

		void Start() {

			// socket is child object named "socket"
			socket = (RectTransform)transform.Find("socket");

			// joystick handle (ball) object is child object named "handle"
			handle = (RectTransform)socket.Find("handle");

			// joystick stick bit object is child object named "stick"
			stick =  (RectTransform)socket.Find("stick");

			// set it to the original position
			ResetStickImage();
		}

		public virtual void OnDrag(PointerEventData p) {
			Vector2 position = Vector2.zero;

			bool inside = RectTransformUtility.ScreenPointToLocalPointInRectangle(
					socket, p.position, p.pressEventCamera, out position);

			if (inside) {
				input = position.x / (socket.sizeDelta.x / 2);

				input = Mathf.Clamp(input, -1, 1);

				float theta = (Mathf.Acos(input) - Mathf.PI/2) / (1.5f) + Mathf.PI/2;

				float stickX = socket.sizeDelta.x / 2 * Mathf.Cos(theta);
				float stickY = socket.sizeDelta.y / 2 * Mathf.Sin(theta);
				handle.anchoredPosition = new Vector2(stickX, stickY);

				float stickT = Mathf.Rad2Deg * theta - 90;
				stick.eulerAngles = new Vector3 (0, 0, stickT);
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
			handle.anchoredPosition = new Vector2(0, socket.sizeDelta.y / 2);
			stick.eulerAngles = Vector3.zero;
		}
	}
}
