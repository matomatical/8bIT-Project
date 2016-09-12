using System;
using System.Collections.Generic;

namespace Team8bITProject
{
	public interface ISerializer : IObservable<IListener<List<byte>>>
	{
		void Deserialize (List<byte> update);
		List<byte> Serialize();
	}
}
	