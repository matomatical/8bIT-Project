/*
 * Global controller object for managing the replaying process.
 * Reads a recording object and dispatches its recorded states to
 * replayer objets each frame, so that they can follow along
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using System.Linq;
using UnityEngine;
using Tiled2Unity;
using xyz._8bITProject.cooperace;

namespace xyz._8bITProject.cooperace.recording {

	public class ReplayingController : MonoBehaviour {

		public TiledMap level;
		public ClockController timer;

		DynamicReplayer[] dynamics;
		StaticReplayer[] statics;
		
		Recording recording;

		bool isReplaying = false, hasStarted = false;

		void Start(){

			// find the clock and level if not assigned
			if (timer == null) {
				timer = FindObjectOfType<ClockController> ();
			}
			if (level == null) {
				level = FindObjectOfType<TiledMap> ();
			}

			// get all replayables in this level, filtered by
			// enabled-ness, and sorted by name (using System.Linq)

			dynamics = level.GetComponentsInChildren<DynamicReplayer> ();
			dynamics = dynamics.Where (
				gameObject => gameObject.enabled).ToArray ();
			dynamics = dynamics.OrderBy(
				gameObject => gameObject.name ).ToArray();

			statics = level.GetComponentsInChildren<StaticReplayer> ();
			statics = statics.Where (
				gameObject => gameObject.enabled).ToArray ();
			statics = statics.OrderBy(
				gameObject => gameObject.name ).ToArray();
			
		}

		/// start replaying if we haven't already
		public void StartReplaying(){
			
			if(!hasStarted) {

				this.recording = SceneManager.opts.recording;
				
				isReplaying = true;
				hasStarted = true;
			}
		}

		public void PauseReplaying(){
			if(hasStarted){
				isReplaying = false;	
			}
		}

		public void ContinueReplaying(){
			if(hasStarted){
				isReplaying = true;	
			}
		}

		public void EndReplaying(){
			if(hasStarted){
				isReplaying = false;
				hasStarted = false;	
			}
		}

		void FixedUpdate(){

			if(isReplaying){

				recording.ApplyFrame(timer, dynamics, statics);
			}
		}
	}
}