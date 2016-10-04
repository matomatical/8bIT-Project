/*
 * On-screen button logic.
 *
 * Matt Farrugiam <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class Button : MonoBehaviour, IPointerUpHandler,
			IPointerDownHandler, IDragHandler {

		private bool input = false;


		/* Allow getting the button state. */
		public bool Input(){
			return input;
		}


		private Image image;
		public Sprite buttonOff, buttonOn;

		void Start(){

			// get the image of the button child
			image = transform.Find("button").GetComponent<Image>();

			image.sprite = buttonOff;
		}



		public virtual void OnDrag(PointerEventData ped){
			OnPointerDown (ped);
		}

		public virtual void OnPointerDown(PointerEventData ped) {
			input = true;
			image.sprite = buttonOn;
		}

		public virtual void OnPointerUp(PointerEventData ped) {
			input = false;
			image.sprite = buttonOff;
		}
	}
}

