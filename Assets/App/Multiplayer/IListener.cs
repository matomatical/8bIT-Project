using System.Collections.Generic;

namespace _8bITProject.cooperace.multiplayer
{
	public interface IListener<T>
	{
		void Notify(T message);
	}
}

