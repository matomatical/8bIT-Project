/*
 * Simple wrapper exception for all possible errors from PersistentStorage.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using System;
using UnityEngine;

namespace xyz._8bITProject.cooperace.persistence {

	public class PersistentStorageException : Exception {

			public PersistentStorageException(Exception e) : base(e.Message) {}

	}
}