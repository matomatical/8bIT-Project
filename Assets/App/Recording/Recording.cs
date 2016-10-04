/*
 * The classes that make up a serialisable recording object are all found
 * inside this file.
 * Recording - stores a sequence of frames and some header information
 * Frame - stored object states in a specified order for each level
 * PositionVelocityState - the storage format of a dynamic object's two-vector 
 * state, stored in a fixed array (no need to store index along with state,
 * as long as order is fixed)
 * BooleanDeltaState - the storage format of a static object's boolean state,
 * stored with delta compression (only storing changes)
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.recording {

	/// stores a sequence of frames and some header information
	[System.Serializable]
	public class Recording {

 		// TODO: remove
 		// for temporary passing of a json string between levels
		public static string json;

		/// The recording file format's version number
		[SerializeField] public static int version = 1;

		/// The recording's target framerate
		[SerializeField] private int fps;
		/// The recording's level's name
		[SerializeField] private string level;

		/// The recording: a sequence of frames
		[SerializeField] private List<Frame> frames;

		/// Create a new recording for a certain level and fps
		public Recording (string level, int fps) {

			this.level = level;
			this.fps = fps;

			frames = new List<Frame>();
		}

		/// Add the information from these recordable objects to
		/// a new frame
		public void AddFrame (TimeRecorder timer,
			DynamicRecorder[] dynamics, StaticRecorder[] statics) {

			Frame frame = new Frame(timer, dynamics, statics);

			// optional: print the frame's json to the console
			// Debug.Log(JsonUtility.ToJson(frame));

			frames.Add(frame);
		}

		/// Apply frame n's information to these replayer objects
		/// so that they match the frame
		public void ApplyFrame(int n, TimeReplayer timer,
			DynamicReplayer[] dynamics, StaticReplayer[] statics) {

			// is this a valid frame number?

			if(n >= frames.Count || n < 0){
				return;
			}

			// good. now, we can get the frame in question

			frames[n].Apply(timer, dynamics, statics);
		}
	}

	/// stored object states in a specified order for each level
	[System.Serializable]
	class Frame {

		/// the time on the clock during this frame
		public float time;

		/// all of the dynamic objects' states
		public PositionVelocityState[] dynamics;

		/// those of the static objects' states which changed this frame
		public BooleanDeltaState[] statics;

		/// create a new frame for a bunch of recordables
		public Frame (TimeRecorder timer, DynamicRecorder[] dynamics,
				StaticRecorder[] statics){
			
			// record time this frame

			this.time = timer.GetTime();


			// record dynamic states

			this.dynamics = new PositionVelocityState[dynamics.Length];

			for (int i = 0; i < dynamics.Length; i++) {
				
				DynamicState state = dynamics[i].GetState();
				this.dynamics[i] =
					new PositionVelocityState(state.position, state.velocity);
			}

			// record static object states using delta compression

			// how many states have changed?

			int numChanged = 0;

			for (int i = 0; i < statics.Length; i++) {

				statics[i].CheckForChanges();

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


		/// Apply this frame's information to these replayer objects
		/// so that they match the frame
		public void Apply(TimeReplayer timer,
			DynamicReplayer[] dynamics, StaticReplayer[] statics) {

			// apply time from this frame

			timer.SetTime(frame.time);

			
			// apply dynamic states from this frame
			
			for (int i = 0; i < dynamics.Length; i++) {
				
				PositionVelocityState state = frame.dynamics[i];
				
				dynamics[i].SetState(new DynamicState(
						new Vector2(state.positionX, state.positionY),
						new Vector2(state.velocityX, state.velocityY)
					));
			}


			// apply static states from this frame

			for (int i = 0; i < frame.statics.Length; i++) {
				
				BooleanDeltaState state = frame.statics[i];
				
				statics[state.index].SetState(state.state);
			}
		}
	}


	/// the storage format of a dynamic object's two-vector state, stored in a 
	/// fixed array (no need to store index along with state, as order is fixed)
	[System.Serializable]
	struct PositionVelocityState {

		/// position components
		public float positionX, positionY;

		/// velocity components
		public float velocityX, velocityY;
		
		/// create a position velocity state from two vectors
		public PositionVelocityState (Vector2 position, Vector2 velocity) {
			positionX = position.x;
			positionY = position.y;
			velocityX = velocity.x;
			velocityY = velocity.y;
		}
	}

	/// the storage format of a static object's boolean state,
 	/// stored with delta compression (only storing changes)
	[System.Serializable]
	struct BooleanDeltaState {

		/// index into replayer arracy for delta DEcompression
		public int index;

		/// the state resulting from the change
		public bool state;
		
		/// create a new delta state for a given index and new state
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