using System;

namespace _8bITProject.cooperace.multiplayer
{
	public interface IObservable<T>
	{
		void Subscribe(IListener<T> o);
	}
}

