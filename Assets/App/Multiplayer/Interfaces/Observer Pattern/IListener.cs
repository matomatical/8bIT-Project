/*
 * The interface which should be implemented by classes which are to be a subscriber in the observer pattern
 * T is the type of message exchanged between the observable and subscriber
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer
{
	public interface IListener<T>
	{
		// To be called by an IObservable<T> when an update. It passes information through message.
		void Notify(T message);
	}
}