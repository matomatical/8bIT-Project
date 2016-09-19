/*
 * An interface for objects that are linked based on their address value.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	public interface IAddressLinkedObject {

		string GetAddress();
		void SetAddress(string address);

	}
}