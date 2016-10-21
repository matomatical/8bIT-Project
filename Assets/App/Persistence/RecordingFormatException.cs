/*
 * For errors that occur processing Recording files
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace xyz._8bITProject.cooperace.persistence
{
	class RecordingFormatException : System.Exception
	{
		public RecordingFormatException(string message) : base(message) { }
	}

}
