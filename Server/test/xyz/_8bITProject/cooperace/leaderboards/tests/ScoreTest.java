package xyz._8bITProject.cooperace.leaderboards.tests;

import static org.junit.Assert.*;

import org.junit.Test;

import xyz._8bITProject.cooperace.leaderboards.InvalidScoreException;
import xyz._8bITProject.cooperace.leaderboards.Score;

public class ScoreTest {
	
	@Test
	public void nameValidationShouldRejectLonFirstNames() {
		try{
			// must throw an exception:
			new Score(1, "long name", "abc");
			fail("Long names (>3) should not be accepted");
		} catch (InvalidScoreException e) {
			// success! pass the test
		}
	}
	
	@Test
	public void nameValidationShouldRejectLongSecondNames() {
		try{
			// must throw an exception:
			new Score(1, "abc", "abcDE");
			fail("Long names (>3) should not be accepted");
		} catch (InvalidScoreException e) {
			// success! pass the test
		}
	}
	
	@Test
	public void nameValidationShouldRejectShortFirstNames() {
		try{
			// must throw an exception:
			new Score(1, "a", "bcd");
			fail("Short names (<3) should not be accepted");
		} catch (InvalidScoreException e) {
			// success! pass the test
		}
	}
	
	@Test
	public void nameValidationShouldRejectShortSecondNames() {
		try{
			// must throw an exception:
			new Score(1, "acd", "b");
			fail("Short names (<3) should not be accepted");
		} catch (InvalidScoreException e) {
			// success! pass the test
		}
	}
	
	@Test
	public void nameValidationShouldRejectNullFirstNames() {
		try{
			// must throw an exception:
			new Score(1, null, "---");
			fail("Missing names should not be accepted");
		} catch (InvalidScoreException e) {
			// success! pass the test
		}
	}
	
	@Test
	public void nameValidationShouldRejectNullSecondNames() {
		try{
			// must throw an exception:
			new Score(1, "---", null);
			fail("Missing names should not be accepted");
		} catch (InvalidScoreException e) {
			// success! pass the test
		}
	}
}
