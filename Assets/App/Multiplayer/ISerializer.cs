/* 
 * The interface common to all serializers working for the multiplayer side of the game
 * A compenent should implement this and attatch to a game object
 * 
 * Sam Beyer <sbeyer@student.unimelb.edu.au>
 * 
*/ 

using System;
using System.Collections.Generic;

namespace _8bITProject.cooperace.multiplayer
{
	public interface ISerializer : IObservable<List<byte>>, IListener<List<byte>>
	{
		// Takes an update, turns it into something meaningful and updates the object
		void Deserialize (List<byte> update);
		// Looks at the state of an object and turns it into a list of bytes
		List<byte> Serialize();
	}
}
	