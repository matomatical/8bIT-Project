/*
 * Mariam Shaid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using System;

namespace _8bITProject.cooperace.multiplayer
{
	public interface IObservable<T>
	{
		void Subscribe(IListener<T> o);
	}
}

