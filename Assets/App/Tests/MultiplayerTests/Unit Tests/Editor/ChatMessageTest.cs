/*
 * Tests for ChatMessage. Tests the Equals method.
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.multiplayer.tests {

	[TestFixture]
	public class ChatMessageTest
	{
		[Test]
		public void EqualsNullShouldReturnFalse () {
			ChatMessage test = new ChatMessage ("Hello, world!");
			Assert.IsFalse (test.Equals (null));
		}

		[Test]
		public void EqualsItselfShouldReturnTrue () {
			ChatMessage test = new ChatMessage ("Hello, world!");
			Assert.That (test.Equals (test));
		}

		[Test]
		public void EqualsACopyShouldReturnTrue () {
			ChatMessage test0 = new ChatMessage ("Hello, world!");
			ChatMessage test1 = new ChatMessage ("Hello, world!");

			Assert.That (test0.Equals (test1));
		}
		[Test]
		public void EqualsDifferentIDShouldReturnFalse () {
			ChatMessage test0 = new ChatMessage ("Hello, world!");
			ChatMessage test1 = new ChatMessage ("Hello, world!1!");

			Assert.That (!test0.Equals (test1));
		}
	}
}

