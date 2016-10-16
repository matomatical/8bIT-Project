/*
 * General Exception thrown by HeaderManager
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using System;

namespace xyz._8bITProject.cooperace.multiplayer
{
	public class HeaderException : Exception
	{
		public HeaderException (string m) : base(m) { }
	}
}

