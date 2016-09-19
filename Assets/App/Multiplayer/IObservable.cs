/*
 * Mariam Shaid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using System;

namespace xyz._8bITProject.cooperace.multiplayer
{
	public interface IObservable<T>
	{
		void Subscribe(IListener<T> o);
	}
}

