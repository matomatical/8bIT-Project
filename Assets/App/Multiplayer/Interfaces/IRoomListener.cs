/*
 * A simple interface for the waiting room UI
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

namespace xyz._8bITProject.cooperace.multiplayer
{
	public interface IRoomListener
	{
		/// The status of the room
		void SetRoomStatusMessage(string message);

		/// Hide the room
		void HideRoomStatusMessage();

		/// Called on completion of connection
		void OnConnectionComplete();
	}
}