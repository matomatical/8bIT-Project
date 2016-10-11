/*
 * The interface which should be implemented by classes which are to be an observable in the observer pattern
 * T is the type of message exchanged between the observable and subscriber
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using System;

namespace xyz._8bITProject.cooperace.multiplayer
{
	public interface IObservable<T>
	{
		// Should add o to the list of subscribers so when an update occurs o will be notified
		void Subscribe(IListener<T> o);
	}
}