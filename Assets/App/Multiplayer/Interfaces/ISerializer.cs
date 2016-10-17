/* 
 * The interface common to all serializers working for the multiplayer side of the game
 * A compenent should implement this and attatch to a game object
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 * 
*/

using System;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer
{
	public interface ISerializer<T> : IListener<List<byte>>
	{
		/// Serialise information, so that it is appropriately formatted to be sent
		/// across the network
		List<byte> Serialize (T information);

		/// Take raw data and convert it into meaningful information
		T Deserialize (List<byte> data);
	}
}