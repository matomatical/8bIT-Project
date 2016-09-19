﻿/* 
 * The interface common to all serializers working for the multiplayer side of the game
 * A compenent should implement this and attatch to a game object
 * 
 * Mariam Shaid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 * 
*/

using System;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer
{
	public interface ISerializer<T> : IListener<List<byte>>
	{
		List<byte> Serialize (T information);
		T Deserialize (List<byte> data);
	}
}
	