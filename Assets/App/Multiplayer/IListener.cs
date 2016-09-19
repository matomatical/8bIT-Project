/*
 * Mariam Shaid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using System.Collections.Generic;

namespace _8bITProject.cooperace.multiplayer
{
	public interface IListener<T>
	{
		void Notify(T message);
	}
}

