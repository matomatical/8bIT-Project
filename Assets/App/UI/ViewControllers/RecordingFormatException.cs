/*
 * Recordings menu logic.
 *
 * Athir Saleem    <isaleem@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using xyz._8bITProject.cooperace.recording;
using xyz._8bITProject.cooperace.persistence;

namespace xyz._8bITProject.cooperace.recording
{
	class RecordingFormatException : System.Exception
	{
		public RecordingFormatException(string message) : base(message) { }
	}

}
