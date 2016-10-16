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
	public class ChatMessageTest {
		[Test]
		public void EqualsNullShouldReturnFalse () {
			ChatMessage test = new ChatMessage ("Hello, world!", true);
			Assert.IsFalse (test.Equals (null));
		}

		[Test]
		public void EqualsItselfShouldReturnTrue () {
			ChatMessage test = new ChatMessage ("Hello, world!", true);
			Assert.That (test.Equals (test));
		}

		[Test]
		public void EqualsACopyShouldReturnTrue () {
			ChatMessage test0 = new ChatMessage ("Hello, world!", true);
			ChatMessage test1 = new ChatMessage ("Hello, world!", true);

			Assert.That (test0.Equals (test1));
		}
		[Test]
		public void EqualsDifferentIDShouldReturnFalse () {
			ChatMessage test0 = new ChatMessage ("Hello, world!", true);
			ChatMessage test1 = new ChatMessage ("Hello, world!1!", true);

			Assert.That (!test0.Equals (test1));
		}
		[Test]
		public void EqualsDifferentStateShouldReturnFalse () {
			ChatMessage test0 = new ChatMessage ("Hello, world!", true);
			ChatMessage test1 = new ChatMessage ("Hello, world!", false);

			Assert.That (!test0.Equals (test1));
		}
		[Test]
		public void EqualsBothDifferentShouldReturnFalse () {
			ChatMessage test0 = new ChatMessage ("Hello, world!", true);
			ChatMessage test1 = new ChatMessage ("Hello, world!1!", false);

			Assert.That (!test0.Equals (test1));
		}
	}
}

