using System;

namespace Team8bITProject
{
	public interface IObservable<T>
	{
		void Subscribe(T o);
	}
}

