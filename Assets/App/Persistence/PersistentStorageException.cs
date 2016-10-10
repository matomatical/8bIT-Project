/*
 * Simple wrapper exception for all possible errors from PersistentStorage.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using System.IO;
using UnityEngine;

namespace xyz._8bITProject.cooperace.persistence {

	public class PersistentStorageException : System.Exception {

			public PersistentStorageException(System.Exception e) : base(e.Message) {}

	}
}