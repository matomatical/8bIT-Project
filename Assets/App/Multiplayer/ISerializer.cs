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
	public interface ISerializer : IListener<List<byte>>
	{

	}
}
	