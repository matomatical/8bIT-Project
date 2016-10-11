using System;
using NUnit.Framework;
using UnityEngine;

namespace xyz._8bITProject.cooperace.multiplayer.tests
{
	[TestFixture]
	public class PlayerInformationTest
	{
		[Test]
		public void EqualsNullFalse () {
			PlayerInformation test = new PlayerInformation (new Vector2 (), new Vector2 ());
			Assert.That (!test.Equals (null));
		}

		[Test]
		public void EqualsItself () {
			PlayerInformation test = new PlayerInformation (new Vector2 (), new Vector2 ());
			Assert.That (test.Equals (test));
		}

		[Test]
		public void EqualsCopy () {
			PlayerInformation test0 = new PlayerInformation (new Vector2 (), new Vector2 ());
			PlayerInformation test1 = new PlayerInformation (new Vector2 (), new Vector2 ());

			Assert.That (test0.Equals (test1));
		}

		[Test]
		public void NotEqualsDiffernet () {
			PlayerInformation test0 = new PlayerInformation (new Vector2 (0, 0), new Vector2 (0, 0));
			PlayerInformation test1 = new PlayerInformation (new Vector2 (1, 1), new Vector2 (1, 1));

			Assert.That (!test0.Equals (test1));
		}
	}
}

