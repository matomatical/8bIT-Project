/*
 * Tests for BoolObstacleInformation. Tests the Equals method.
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

using System.Collections.Generic;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.multiplayer.tests {

	[TestFixture]
	public class BoolObstacleInformationTest
	{
		[Test]
		public void EqualsNullShouldReturnFalse () {
			BoolObstacleInformation test = new BoolObstacleInformation (0, true);
			Assert.That (!test.Equals (null));
		}

		[Test]
		public void EqualsItselfShouldReturnTrue () {
			BoolObstacleInformation test = new BoolObstacleInformation (0, true);
			Assert.That (test.Equals (test));
		}

		[Test]
		public void EqualsACopyShouldReturnTrue () {
			BoolObstacleInformation test0 = new BoolObstacleInformation (0, true);
			BoolObstacleInformation test1 = new BoolObstacleInformation (0, true);

			Assert.That (test0.Equals (test1));
		}
		[Test]
		public void EqualsDifferentIDShouldReturnFalse () {
			BoolObstacleInformation test0 = new BoolObstacleInformation (0, true);
			BoolObstacleInformation test1 = new BoolObstacleInformation (1, true);

			Assert.That (!test0.Equals (test1));
		}
		[Test]
		public void EqualsDifferentStateShouldReturnFalse () {
			BoolObstacleInformation test0 = new BoolObstacleInformation (0, true);
			BoolObstacleInformation test1 = new BoolObstacleInformation (0, false);

			Assert.That (!test0.Equals (test1));
		}
		[Test]
		public void EqualsBothDifferentShouldReturnFalse () {
			BoolObstacleInformation test0 = new BoolObstacleInformation (0, true);
			BoolObstacleInformation test1 = new BoolObstacleInformation (1, false);

			Assert.That (!test0.Equals (test1));
		}
	}
}
