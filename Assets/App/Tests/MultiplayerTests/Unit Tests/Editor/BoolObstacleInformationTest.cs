/*
 * Tests for BoolObstacleInformation. Tests the Equals method.
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

using System.Collections.Generic;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.multiplayer.tests {
	public class BoolObstacleInformationTest {
		[Test]
		public void EqualsNullFalse () {
			BoolObstacleInformation test = new BoolObstacleInformation (0, true);
			Assert.That (!test.Equals (null));
		}

		[Test]
		public void EqualsItself () {
			BoolObstacleInformation test = new BoolObstacleInformation (0, true);
			Assert.That (test.Equals (test));
		}

		[Test]
		public void EqualsCopy () {
			BoolObstacleInformation test0 = new BoolObstacleInformation (0, true);
			BoolObstacleInformation test1 = new BoolObstacleInformation (0, true);

			Assert.That (test0.Equals (test1));
		}

		[Test]
		public void NotEqualsDiffernet () {
			BoolObstacleInformation test0 = new BoolObstacleInformation (0, true);
			BoolObstacleInformation test1 = new BoolObstacleInformation (1, false);

			Assert.That (!test0.Equals (test1));
		}
	}
}
