/*
 * Global controller object for managing the recording process.
 * Builds a recording object by collecting states from recording 
 * objects each frame
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using System.Linq;
using UnityEngine;
using Tiled2Unity;
using xyz._8bITProject.cooperace;

namespace xyz._8bITProject.cooperace.recording {

	public class RecordingController : MonoBehaviour {

		public TiledMap level;
		public ClockController timer;

		DynamicRecorder[] dynamics;
		StaticRecorder[] statics;

		Recording recording;
		bool isRecording = false, hasStarted = false;

		/// How many fixed updates should pass between each frame
		/// that is recorded? Setting this indirectly controls the
		/// fps of the recording producted
		public const int fixedUpdatesPerFrame = 2;
		private int fixedUpdatesSinceLastFrame = 0;

		/// determined by fixed updates per frame
		/// fps = (int)(1 / (fixedUpdatesPerFrame*Time.fixedDeltaTime));
		/// (unfortunately this can't be statically evaluated)
		/// for example, if you want 25 fps, use 2 fixed updates per
		/// frame (unity's default fixed update rate is 50fps)
		private int fps;


		void Start(){

			// find the clock and level if not assigned
			if (timer == null) {
				timer = FindObjectOfType<ClockController> ();
			}
			if (level == null) {
				level = FindObjectOfType<TiledMap> ();
			}

			// calculate fps

			fps = (int)(1 / (fixedUpdatesPerFrame*Time.fixedDeltaTime));


			// get all recordables in this level, filtered by
			// enabled-ness, and sorted by name (using System.Linq)

			dynamics = level.GetComponentsInChildren<DynamicRecorder> ();
			dynamics = dynamics.Where (
				gameObject => gameObject.enabled).ToArray ();
			dynamics = dynamics.OrderBy(
				gameObject => gameObject.name ).ToArray ();
			
			statics = level.GetComponentsInChildren<StaticRecorder> ();
			statics = statics.Where (
				gameObject => gameObject.enabled).ToArray ();
			statics = statics.OrderBy(
				gameObject => gameObject.name ).ToArray ();
			
		}

		/// start recording if we haven't already
		public void StartRecording(){

			if(!hasStarted){
				recording = new Recording(SceneManager.opts.level, this.fps);
				hasStarted = true;
				isRecording = true;

				// make sure we'll record the first frame!
				fixedUpdatesSinceLastFrame = fixedUpdatesPerFrame - 1;
			}
		}

		public void PauseRecording(){
			if(hasStarted){
				isRecording = false;
			}
		}

		public void ContinueRecording(){
			if(hasStarted){
				isRecording = true;
			}
		}

		public void EndRecording(){
			if(hasStarted){
				isRecording = false;
				hasStarted = false;
			}
		}

		void FixedUpdate(){

			// record a frame if we're recording at the moment
			if(isRecording){

				// count another frame

				fixedUpdatesSinceLastFrame = fixedUpdatesSinceLastFrame + 1;

				// we're going to 

				if(fixedUpdatesSinceLastFrame == fixedUpdatesPerFrame){

					// THIS is one of the updates where we add a frame
					// to the recording :)

					recording.AddFrame(timer, dynamics, statics);	

					fixedUpdatesSinceLastFrame = 0;
				}

				
			}
		}

		public Recording GetRecording () {
			return recording;
		}
	}
}