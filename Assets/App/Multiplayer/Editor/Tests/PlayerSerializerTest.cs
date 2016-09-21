using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.multiplayer.tests
{
	[TestFixture]
	public class PlayerSerializerTest
	{
		[Test]
		public void SerializeDserialize () {
			PlayerSerializer serializer = new PlayerSerializer ();

			// Serialize some information
			PlayerInformation info = new PlayerInformation (0, 0, PlayerInformation.PlayerState.MOVING);
			List<byte> data = serializer.Serialize (info);

			// Deserialize the result
			PlayerInformation resultingInfo = serializer.Deserialize (data);

			// Check the information is the same as the original
			Assert.That(resultingInfo.Equals(info));
		}
	}
}

