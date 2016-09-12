using System;

namespace Team8bITProject
{
	public interface IListener<T>
	{
		void notify(T message);
	}
}

