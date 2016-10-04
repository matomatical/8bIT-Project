using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.recording {

	[System.Serializable]
	public class Recording {

		private static int version = 1;
		
		private int fps;
		private string level;

		private List<Frame> frames;

		public Recording (string level, int fps) {

			this.level = level;
			this.fps = fps;

			frames = new List<Frame>();
		}

		public void AddFrame (TimeRecorder timer,
			DynamicRecorder[] dynamics, StaticRecorder[] statics) {

			frames.Add(new Frame(timer, dynamics, statics));
		}
	}

	[System.Serializable]
	class Frame {

		public float time;
		public PositionVelocityState[] dynamics;
		public BooleanDeltaState[] statics;

		public Frame (TimeRecorder timer, DynamicRecorder[] dynamics,
				StaticRecorder[] statics){
			
			this.time = timer.GetTime();

			// record dynamic states

			this.dynamics = new PositionVelocityState[dynamics.Length];

			for (int i = 0; i < dynamics.Length; i++) {
				this.dynamics[i] =
					new PositionVelocityState(
						dynamics[i].GetState().position,
						dynamics[i].GetState().velocity);
			}


			// record static object states using delta compression

			// how many states have changed?

			int numChanged = 0;

			for (int i = 0; i < statics.Length; i++) {
				if (statics[i].StateHasChanged()) {
					numChanged++;
				}
			}

			// record those changes

			this.statics = new BooleanDeltaState[numChanged];

			for (int i = 0, j = 0; i < statics.Length; i++) {
				if (statics[i].StateHasChanged()) {
					this.statics[j++] =
						new BooleanDeltaState(i, statics[i].GetState());
				}
			}
		}
	}

	[System.Serializable]
	struct PositionVelocityState {

		public float positionX, positionY;
		public float velocityX, velocityY;
		
		public PositionVelocityState (Vector2 position, Vector2 velocity) {
			positionX = position.x;
			positionY = position.y;
			velocityX = velocity.x;
			velocityY = velocity.y;
		}
	}

	[System.Serializable]
	struct BooleanDeltaState {

		public int index; // id for delta decompression
		public bool state;
		
		public BooleanDeltaState(int index, bool state){
			this.index = index;
			this.state = state;
		}
	}
}


/* JSON spec 

recording : {
	fps : int,
	level : string,
	frames : [frame]
}

frame : {
	time : float,
	dynamics : [dynamicState], // objects for which full state is recorded
	statics  :  [staticState] // objects that use delta compression
}

dynamicState : { // we're storing all in order, so theres no need to store index
	positionX : float,
	positionY : float,
	velocityX : float,
	velocityY : float
}

staticState : {
	index : int, // we're only storing states with changes, so we need an index
	state : bool
}

*/