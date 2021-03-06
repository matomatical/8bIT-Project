/*
 * This class contains the static methods for setting up a game
 * for watching a recording and playing against a recording game
 * in the background, and also setting up a level so that it can
 * be recorded and the recording can be saved afterwards.
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using Tiled2Unity;

namespace xyz._8bITProject.cooperace.recording {
	public class RecordingInit : MonoBehaviour {

		public static void ReplayingAwake(GameObject level) {

			// set up objects and players for replaying

			EnableReplayingComponents (level);


			// set the camera up to follow either player. just find an
			// enabled physics controller!

			foreach (ArcadePhysicsController apc in level.GetComponentsInChildren<ArcadePhysicsController> ()) {
				if (apc.enabled) {
					InitializeLevel.instance.cam.target = apc;
					break;
				}
			}

			// also, there's no need for the controls or chat
			// in a replay

			InitializeLevel.instance.gui.OnScreenControls.SetActive (false);
			InitializeLevel.instance.gui.ChatButton.SetActive (false);

			// enable the replayer object itself

			ReplayingController replayer = FindObjectOfType<ReplayingController>();
			replayer.enabled = true;
			replayer.level = level.GetComponent<TiledMap>();


			// start playing straight away! also start the clock

			replayer.StartReplaying();
			FindObjectOfType<ClockController> ().StartTiming ();


			// the finish lines in the main level should end
			// the replaying, and also exit the game

			foreach (ReplayingEnder ender in level.GetComponentsInChildren<ReplayingEnder>()) {
				ender.enabled = true;

				ender.terminal = true;
			}
		}


		// helper method

		static void EnableReplayingComponents(GameObject level){

			// enable dynamic replayers, and the lerping physics
			// controllers on these objects

			foreach (DynamicReplayer rep in level.GetComponentsInChildren<DynamicReplayer>()) {

				// enable replaying

				rep.enabled = true;

				// enable remote control with lerping

				LerpingPhysicsController lpc = rep.GetComponent<LerpingPhysicsController>();
				lpc.enabled = true;
			}


			// enable static replayers, too

			foreach (StaticReplayer rep in level.GetComponentsInChildren<StaticReplayer>()) {

				// enable replaying

				rep.enabled = true;
			}
		}



		public static void PlayagainstAwake(GameObject ghost, GameObject level) {

			// prepare the ghost level visually

			PrepareGhostLevel (ghost, level);


			// set up the components in the ghost level to track a recording

			EnableReplayingComponents(ghost);


			// enable the replayer object itself

			ReplayingController replayer = FindObjectOfType<ReplayingController>();
			replayer.enabled = true;
			replayer.level = ghost.GetComponent<TiledMap>();

			// but don't start playing just yet! Wait till someone crosses
			// the start lines in the first level, actually!


			// make sure the start line in the main level
			// will start the recording in the ghost level

			foreach (ReplayingStarter starter in level.GetComponentsInChildren<ReplayingStarter>()) {
				starter.enabled = true;

				// link this starter directly to the replayer in the ghost level
				starter.replayer = replayer;
			}


			// the finish lines in the ghost level should not exit the game,
			// or stop the clock! they should just end the replaying being replayed

			foreach (FinishLine finish in ghost.GetComponentsInChildren<FinishLine>()) {

				// turn off clock-stopping

				finish.enabled = false;

				// turn on replaying ending but not level-leaving

				ReplayingEnder ender = finish.GetComponent<ReplayingEnder> ();
				ender.enabled = true;
				ender.terminal = false;
			}

			// also, the level boundaries should not exit the game! they should be deactivated,
			// because they are active by default

			foreach (LevelBoundary exit in ghost.GetComponentsInChildren<LevelBoundary>()) {
				exit.enabled = false;
			}
		}


		static void PrepareGhostLevel(GameObject ghost, GameObject level){

			// shrink level behind real level

			float scale = 0.8f;

			ghost.transform.localScale = new Vector3 (
				ghost.transform.localScale.x * scale,
				ghost.transform.localScale.y * scale,
				ghost.transform.localScale.z
			);

			// shade and order level

			float color = 0.6f;

			Renderer[] renderers = ghost.GetComponentsInChildren<Renderer> ();
			foreach (Renderer renderer in renderers) {
				renderer.sortingLayerName = Magic.SortingLayers.BEFORE_BGR;
				renderer.material.color = new Color (color, color, color);
			}

			// make ghost level scroll along with camera and real level

			BackgroundLevelScroller scroller = ghost.gameObject.AddComponent<BackgroundLevelScroller> ();

			scroller.cam = InitializeLevel.instance.cam.GetComponent<Camera>();
			scroller.level = level.GetComponent<TiledMap>();

			// arrange objects by z depth

			level.transform.position += 2*Vector3.forward;
			ghost.transform.position += Vector3.forward;
		}


		public static void RecordingAwake(GameObject level) {

			// enable static recorders

			foreach (StaticRecorder rec in
				level.GetComponentsInChildren<StaticRecorder>()) {
				rec.enabled = true;
			}

			// enable dynamic recorders

			foreach (DynamicRecorder rec in
				level.GetComponentsInChildren<DynamicRecorder>()) {
				rec.enabled = true;
			}

			// enable the recorder and start the recording

			RecordingController recorder = FindObjectOfType<RecordingController>();
			recorder.enabled = true;
			recorder.level = level.GetComponent<TiledMap>();


			// make sure the start line will start the recording

			foreach (RecordingStarter starter in level.GetComponentsInChildren<RecordingStarter>()) {
				starter.enabled = true;
			}

			// make the finish line finish the recording

			foreach (RecordingEnder ender in level.GetComponentsInChildren<RecordingEnder>()) {
				ender.enabled = true;
			}


			// actually, we should also set up the finish and start lines to light up

			foreach (StartLineLighter lighter in level.GetComponentsInChildren<StartLineLighter>()) {
				lighter.enabled = true;
			}

			foreach (FinishLineLighter lighter in level.GetComponentsInChildren<FinishLineLighter>()) {
				lighter.enabled = true;
			}
		}

	}
}