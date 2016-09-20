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
			IPointerDownHandler {

		private bool input = false;

//		void Start(){
//			SpriteRenderer
//		}

		public bool Input(){
			return input;
		}

		public virtual void OnPointerDown(PointerEventData ped) {
			input = true;
		}

		public virtual void OnPointerUp(PointerEventData ped) {
			input = false;
		}
	}
}

