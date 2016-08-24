using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	private RectTransform baseImage;
	private RectTransform stickImage;

	private float input = 0;
	public float Input(){
		return input;
	}
	
	private void Start(){

		// firstborn child object is the base
		baseImage = transform.GetChild(0).gameObject
			.GetComponent<Image>().GetComponent<RectTransform>();

		// ITS firstborn child object is the stick
		stickImage = transform.GetChild(0).GetChild(0)
			.GetComponent<Image>().GetComponent<RectTransform>();

		// set it to the original position
		ResetStickImage();
	}


	public virtual void OnDrag(PointerEventData p) {
		
		Vector2 position = Vector2.zero;

		bool inside = RectTransformUtility.ScreenPointToLocalPointInRectangle(
			baseImage, p.position, p.pressEventCamera, out position);

		if(inside) {

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

	private void ResetStickImage(){
		stickImage.anchoredPosition = new Vector2(0, baseImage.sizeDelta.y / 4);
	}
}
