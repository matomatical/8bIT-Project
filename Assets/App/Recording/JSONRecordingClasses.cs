/*
 * The classes that make up a serialisable recording object are all found
 * inside this file.
 * Recording - stores a sequence of frames and some header information
 * Frame - stored object states in a specified order for each level
 * PositionVelocityState - the storage format of a dynamic object's two-vector 
 * state, stored with delta compression (only stored if state is different
 * from previous recorded state)
 * BooleanDeltaState - the storage format of a static object's boolean state,
 * stored with delta compression (only storing changes)
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections.Generic;
using xyz._8bITProject.cooperace;

namespace xyz._8bITProject.cooperace.recording {

	/// stores a sequence of frames and some header information
	[System.Serializable]
	public class Recording {
		
		/// The recording file format's version number
		[SerializeField] public static int version = 1;

		/// The recording's target framerate
		[SerializeField] public int fps;
		/// The recording's level's name
		[SerializeField] public string level;

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
		public void AddFrame (ClockController timer,
			DynamicRecorder[] dynamics, StaticRecorder[] statics) {

			// create and store a new frame

			Frame frame = new Frame(timer.GetTime(), dynamics, statics);
			
			frames.Add(frame);

			// optional: print the frame's json to the console

			// Debug.Log(JsonUtility.ToJson(frame));
		}


		/// which frame are we up to in a replay?
		private int head = 0, prev = -1;

		/// Apply frame n's information to these replayer objects
		/// so that they match the frame
		public void ApplyFrame(ClockController timer,
			DynamicReplayer[] dynamics, StaticReplayer[] statics) {

			// spin up to the frame right before the current time

			float currentTime = timer.GetTime();

			while(frames[head].time <= currentTime){
				head++;
				if(head >= frames.Count){
					// we've reached the end of the recording, and should
					// return the last frame
					head--;
					break;
				}
			}

			// that makes frames[head] the first frame before (or at) the 
			// current time

			// if this frame hasn't need rendered yet, we should play it

			if(head > prev){
				frames[head].Apply(dynamics, statics);
				prev = head;
			}
		}
	}

	/// stored object states in a specified order for each level
	[System.Serializable]
	class Frame {

		/// the time on the clock during this frame
		public float time;

		/// all of the dynamic objects' states
		public DynamicDeltaState[] dynamics;

		/// those of the static objects' states which changed this frame
		public BooleanDeltaState[] statics;

		/// create a new frame for a bunch of recordables
		public Frame (float time, DynamicRecorder[] dynamics,
				StaticRecorder[] statics){
			
			// record time this frame

			this.time = time;

			// record dynamic states using delta compression

			this.dynamics = RecordDynamicStates(dynamics);

			// record static object states using delta compression

			this.statics = RecordStaticStates(statics);

		}

		/// formulate array of changed dynamic recorder object states
		/// for storage with delta compression
		private DynamicDeltaState[] RecordDynamicStates(
				DynamicRecorder[] dynamics){

			// how many states have changed?

			int numChanged = 0;

			for (int i = 0; i < dynamics.Length; i++) {

				dynamics[i].CheckForChanges();

				if (dynamics[i].StateHasChanged()) {
					numChanged++;
				}
			}

			// record those changes

			DynamicDeltaState[] deltas = new DynamicDeltaState[numChanged];

			for (int i = 0, j = 0; i < dynamics.Length; i++) {
				
				if(dynamics[i].StateHasChanged()){
					DynamicState state = dynamics[i].LastState();
					deltas[j++] = 
						new DynamicDeltaState(i, state.position, state.velocity);

				}
			}

			return deltas;
		}

		/// formulate array of changed static (boolean) recorder object
		/// states for storage with delta compression
		private BooleanDeltaState[] RecordStaticStates(
				StaticRecorder[] statics){

			// how many states have changed?

			int numChanged = 0;

			for (int i = 0; i < statics.Length; i++) {

				statics[i].CheckForChanges();

				if (statics[i].StateHasChanged()) {
					numChanged++;
				}
			}

			// record those changes

			BooleanDeltaState[] deltas = new BooleanDeltaState[numChanged];

			for (int i = 0, j = 0; i < statics.Length; i++) {
				if (statics[i].StateHasChanged()) {
					deltas[j++] =
						new BooleanDeltaState(i, statics[i].LastState());
				}
			}

			return deltas;
		}

		/// Apply this frame's information to these replayer objects
		/// so that they match the frame
		public void Apply(
			DynamicReplayer[] dynamics, StaticReplayer[] statics) {
			
			// apply dynamic states from this frame
			
			for (int i = 0; i < this.dynamics.Length; i++) {
				
				DynamicDeltaState state = this.dynamics[i];

				dynamics[state.index].SetState(new DynamicState(
						new Vector2(state.positionX, state.positionY),
						new Vector2(state.velocityX, state.velocityY)
					));
			}

			// apply static states from this frame

			for (int i = 0; i < this.statics.Length; i++) {
				
				BooleanDeltaState state = this.statics[i];
				
				statics[state.index].SetState(state.state);
			}
		}
	}

	/// the storage format of a dynamic object's two-vector state,
	/// stored with delta compression (only storing changes)
	[System.Serializable]
	struct DynamicDeltaState {

		/// index into replayer array for delta DEcompression
		public int index;

		/// position components
		public float positionX, positionY;

		/// velocity components
		public float velocityX, velocityY;
		
		/// create a position velocity state from two vectors for a given index
		public DynamicDeltaState (int index,
				Vector2 position, Vector2 velocity) {

			this.index = index;
			
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

		/// index into replayer array for delta DEcompression
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
	dynamics : [dynamicDeltaState],	// two-vector states
	statics  : [booleanDeltaState]	// boolean states
}

dynamicDeltaState : {
	index : int,
	positionX : float,
	positionY : float,
	velocityX : float,
	velocityY : float
}

booleanDeltaState : {
	index : int,
	state : bool
}

*/