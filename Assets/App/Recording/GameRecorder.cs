using UnityEngine;
using System.Collection.Generic;

namespace xyz._8bITProject.cooperace {

	public class GameRecorder : MonoBehaviour {

		List<ObjectRecorder> objects;

		Recording recording;

		void Start(){

			// get all recordables in this level

			objects = new List<ObjectRecorder> ();


			// and start the actual recording

			recording = new Recording("level name");

		}

		void Update(){ // are we recording at the moment?

			// FPS???

			// get all states

			List<State> states = new List<State> ();

			foreach (ObjectRecorder recorder in objects) {

				states.Add(recorder.GetState());

			}

			recording.AddFrame(states);

		}

		Recording GetRecording () {

			return recording;

		}

	}
}