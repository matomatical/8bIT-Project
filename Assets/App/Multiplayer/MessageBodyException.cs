/*
 * Exception thrown when something goes wrong
 * deserialising a message body
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 * Matt Farrugia < farrugiam@student.unimelb.edu.au >
 */

using System;

namespace xyz._8bITProject.cooperace.multiplayer
{
	public class MessageBodyException : Exception
	{
		public MessageBodyException (string m) : base(m) { }
	}
}

