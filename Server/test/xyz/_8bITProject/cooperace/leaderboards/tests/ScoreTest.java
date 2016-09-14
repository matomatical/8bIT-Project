package xyz._8bITProject.cooperace.leaderboards;

import static org.junit.Assert.*;

import org.junit.Test;

public class ScoreTest {

	@Test
	public void test() {
		fail("Not yet implemented");
	}

	/* Things to test:
	 * 	- name validation
	 * 	- time validation
	 *  - score comparison
	 */
	
	@Test
	public void nameValidationShouldRejectLongNames() {
		try{
			// must throw an exception:
			new Score(1, "long name", "another long name");
			fail("Long names (>3) should not be accepted");
		} catch (InvalidScoreException e) {
			// success! pass the test
		}
	}
	
	@Test
	public void nameValidationShouldRejectShortNames() {
		try{
			// must throw an exception:
			new Score(1, "a", "b");
			fail("Short names (<3) should not be accepted");
		} catch (InvalidScoreException e) {
			// success! pass the test
		}
	}
	
	@Test
	public void nameValidationShouldRejectNullNames() {
		try{
			// must throw an exception:
			new Score(1, null, null);
			fail("Missing names should not be accepted");
		} catch (InvalidScoreException e) {
			// success! pass the test
		}
	}
}
