/*
 * Tests for PlayerInformation. Tests the Equals method.
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

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
			DynamicObjectInformation test = new DynamicObjectInformation (new Vector2 (), new Vector2 ());
			Assert.That (!test.Equals (null));
		}

		[Test]
		public void EqualsItself () {
			DynamicObjectInformation test = new DynamicObjectInformation (new Vector2 (), new Vector2 ());
			Assert.That (test.Equals (test));
		}

		[Test]
		public void EqualsCopy () {
			DynamicObjectInformation test0 = new DynamicObjectInformation (new Vector2 (), new Vector2 ());
			DynamicObjectInformation test1 = new DynamicObjectInformation (new Vector2 (), new Vector2 ());

			Assert.That (test0.Equals (test1));
		}

		[Test]
		public void NotEqualsDiffernet () {
			DynamicObjectInformation test0 = new DynamicObjectInformation (new Vector2 (0, 0), new Vector2 (0, 0));
			DynamicObjectInformation test1 = new DynamicObjectInformation (new Vector2 (1, 1), new Vector2 (1, 1));

			Assert.That (!test0.Equals (test1));
		}
	}
}

