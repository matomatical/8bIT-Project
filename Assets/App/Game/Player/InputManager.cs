using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class InputManager : MonoBehaviour {

		// link these GUI things to provide non-keyboard input controls

		public Joystick joystick;
		public Button jumpButton;

		// inputs struct

		Inputs inputs;
		
		public struct Inputs {
			public bool up, down;
			[Range(-1,1)]
			public float horizontal;

			public void SetInputs(bool up, bool down, float horizontal){
				this.up = up;
				this.down = down;
				this.horizontal = Mathf.Clamp(horizontal, -1, 1);
			}
		}

		// The current state of inputs (a copy, since Inputs is a struct)

		public Inputs GetInputs(){
			return inputs;
		}

		// Refresh the state of inputs each frame

		void Update() {

			// refresh our input

			bool up = UpInput();
			bool down = DownInput();
			float horizontal = HorizontalInput();

			inputs.SetInputs(up, down, horizontal);
		}

		bool UpInput() {

			// resolve button input

			bool buttonInput = jumpButton && jumpButton.Input();

			// resolve key input

			bool spaceKey = Input.GetKey(KeyCode.Space);
			bool upKey = Input.GetKey(KeyCode.UpArrow);

			bool keyInput = spaceKey || upKey;

			// resolve virtual input

			bool virtualInput = (virtualUpCounter > 0);

			// unity inputs

			bool up = buttonInput || keyInput || virtualInput;

			return up;
		}

		bool DownInput() {

			// resolve key input

			bool keyInput = Input.GetKey(KeyCode.DownArrow);

			// resolve virtual input

			bool virtualInput = (virtualDownCounter > 0);

			// (no alternatives to unify with right now)

			bool down = keyInput || virtualInput;

			return down;
		}

		float HorizontalInput() {
			
			// resolve key input

			bool leftKey = Input.GetKey(KeyCode.LeftArrow);
			bool rightKey = Input.GetKey(KeyCode.RightArrow);

			float keyInput = 0;

			if (leftKey == rightKey) { // left and right, or neither
				keyInput = 0;
			} else if (leftKey) {
				keyInput = -1;
			} else { // right only
				keyInput = 1;
			}

			// resolve stick input

			float joystickInput = 0;

			if (joystick) {
				joystickInput = joystick.Input();
			}

			// resolve virtual input

			int virtualRight = (virtualRightCounter > 0) ? 1 : 0;
			int virtualLeft = (virtualLeftCounter > 0) ? 1 : 0;

			int virtualInput = virtualRight - virtualLeft;

			// unify inputs

			float horizontal = Mathf.Clamp(joystickInput + keyInput + virtualInput, -1, 1);

			return horizontal;
		}

		// virtual keypressing

		private int virtualRightCounter;

		public void VirtualKeyRightDown() {
			virtualRightCounter++;
		}

		public void VirtualKeyRightUp() {
			if (virtualRightCounter > 0) {
				virtualRightCounter--;
			}
		}

		private int virtualLeftCounter;

		public void VirtualKeyLeftDown() {
			virtualLeftCounter++;
		}

		public void VirtualKeyLeftUp() {
			if (virtualLeftCounter > 0) {
				virtualLeftCounter--;
			}
		}

		private int virtualUpCounter;

		public void VirtualKeyUpDown() {
			virtualRightCounter++;
		}

		public void VirtualKeyUpUp() {
			if (virtualUpCounter > 0) {
				virtualUpCounter--;
			}
		}

		private int virtualDownCounter;

		public void VirtualKeyDownDown() {
			virtualDownCounter++;
		}

		public void VirtualKeyDownUp() {
			if (virtualDownCounter > 0) {
				virtualDownCounter--;
			}
		}
	}
}