/*
 * The interface which should be implemented by classes which are to be an observable in the observer pattern
 * T is the type of message exchanged between the observable and subscriber.
 * C is the 'channel' type which allows the subscriber to specify what kind of content
 * to be notified about (it could be an enum of actual channels, or a struct containing
 * subscription preferences, for example)
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 * Matt Farrugia < sbeyer@student.unimelb.edu.au >
 */

using System;

namespace xyz._8bITProject.cooperace.multiplayer
{
	public interface IObservable<T, C>
	{
		/// Subscribe observer to the list of subscribers so when an update occurs observer will be notified
		void Subscribe(IListener<T> observer, C channel);
	}
}