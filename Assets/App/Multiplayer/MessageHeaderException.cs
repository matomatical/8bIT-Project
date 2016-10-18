/*
 * General Exception thrown by HeaderManager
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using System;

namespace xyz._8bITProject.cooperace.multiplayer
{
	public class MessageHeaderException : Exception
	{
		public MessageHeaderException (string m) : base(m) { }
	}
}

