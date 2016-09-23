using UnityEngine;
using System.Collection.Generic;

namespace xyz._8bITProject.cooperace {

	public class Recording {

		public static int version = 1;
		public int fps = 30;

		private string levelName;

		private List<Frame> frames;

		Recording (string levelName) {

			this.levelName = levelName;

			frames = new List<Frame>();

		}

		void AddFrame (List<State> states) {

			Frame frame = new Frame(states);

			frames.Add(frame);
		}

		private class Frame {

			public State[] states;

			public Frame (List<State> statesList){
				states = states.ToArray();
			}
		}
	}
}