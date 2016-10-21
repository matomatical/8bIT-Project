/*
 * Finish line light-up logic. Light up all finish lines
 * when one is passed
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class FinishLineLighter : MonoBehaviour {

		static bool stopped = false;
		static Color onColor = new Color(1, 1, 1);
		static Color offColor = new Color(0.75f, 0.75f, 0.75f);

		/// The sprite renderer to light up when we pass the line
		SpriteRenderer sprite;

		void Start(){

			// reset static state between games
			stopped = false;

			// link components

			sprite = GetComponent<SpriteRenderer> ();

		}

		void OnTriggerEnter2D (Collider2D other) {
			if (ArcadePhysics.SameWorld(this, other)) {

				// only trigger if this component is on
				if (enabled) {

					if (!stopped) {
						
						stopped = true;
					}

				}
			}
		}

		void Update() {
			if (stopped) {
				sprite.color = onColor;
			} else {
				sprite.color = offColor;
			}
		}
	}
}
