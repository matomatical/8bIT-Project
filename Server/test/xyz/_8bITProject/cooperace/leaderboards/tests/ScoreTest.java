package xyz._8bITProject.cooperace.leaderboards.tests;

import org.junit.Assert;
import org.junit.Test;

import xyz._8bITProject.cooperace.leaderboards.InvalidScoreException;
import xyz._8bITProject.cooperace.leaderboards.Score;

public class ScoreTest {
	
	@Test
	public void validationShouldRejectLonFirstNames() {
		try{
			// must throw an exception:
			Score score = new Score(1, "long name", "abc");
			score.validate();
			Assert.fail("Long names (>3) should not be accepted");
		} catch (InvalidScoreException e) {
			// success! pass the test
		}
	}
	
	@Test
	public void validationShouldRejectLongSecondNames() {
		try{
			// must throw an exception:
			Score score = new Score(1, "abc", "abcDE");
			score.validate();
			Assert.fail("Long names (>3) should not be accepted");
		} catch (InvalidScoreException e) {
			// success! pass the test
		}
	}
	
	@Test
	public void validationShouldRejectShortFirstNames() {
		try{
			// must throw an exception:
			Score score = new Score(1, "a", "bcd");
			score.validate();
			Assert.fail("Short names (<3) should not be accepted");
		} catch (InvalidScoreException e) {
			// success! pass the test
		}
	}
	
	@Test
	public void validationShouldRejectShortSecondNames() {
		try{
			// must throw an exception:
			Score score = new Score(1, "acd", "b");
			score.validate();
			Assert.fail("Short names (<3) should not be accepted");
		} catch (InvalidScoreException e) {
			// success! pass the test
		}
	}
	
	@Test
	public void validationShouldRejectNullFirstNames() {
		try{
			// must throw an exception:
			Score score = new Score(1, null, "---");
			score.validate();
			Assert.fail("Missing names should not be accepted");
		} catch (InvalidScoreException e) {
			// success! pass the test
		}
	}
	
	@Test
	public void validationShouldRejectNullSecondNames() {
		try{
			// must throw an exception:
			Score score = new Score(1, "---", null);
			score.validate();
			Assert.fail("Missing names should not be accepted");
		} catch (InvalidScoreException e) {
			// success! pass the test
		}
	}
	
	@Test
	public void comparingToGreaterTimeShouldGiveNegative() {
		
		Score score1 = new Score(1, "ANY", "STR");
		Score score2 = new Score(2, "STR", "STR");
		
		int result = score1.compareTo(score2);
		
		Assert.assertTrue(result < 0);
	}
	
	@Test
	public void comparingToSmallerTimeShouldGivePositive() {
		
		Score score1 = new Score(1, "ANY", "STR");
		Score score2 = new Score(2, "STR", "STR");
		
		int result = score2.compareTo(score1);
		
		Assert.assertTrue(result > 0);
	}
	
	@Test
	public void comparingToEqualTimeShouldGiveZero() {
		
		Score score1 = new Score(1, "BUT", "NOT");
		Score score2 = new Score(1, "THE", "STR");
		
		int result = score2.compareTo(score1);
		
		Assert.assertEquals(0, result);
	}
	
	@Test
	public void equalsShouldAcceptSameScore() {
		
		Score score = new Score(100, "ABC", "DEF");
		
		boolean result = score.equals(score);
		
		Assert.assertEquals(true, result);
	}
	
	@Test
	public void equalsShouldAcceptIdenticalScore() {
		
		Score score1 = new Score(100, "ABC", "DEF");
		Score score2 = new Score(100, "ABC", "DEF");
		
		boolean result = score1.equals(score2);
		
		Assert.assertEquals(true, result);
	}
	
	@Test
	public void equalsShouldRejectNullScore() {
		
		Score score = new Score(100, "ABC", "DEF");
		
		boolean result = score.equals(null);
		
		Assert.assertEquals(false, result);
	}
	
	@Test
	public void equalsShouldRejectNonScore() {
		
		Score score = new Score(100, "ABC", "DEF");
		
		boolean result = score.equals(new Object());
		
		Assert.assertEquals(false, result);
	}
	
	@Test
	public void equalsShouldRejectDifferentTime() {
		
		Score score1 = new Score(100, "ABC", "DEF");
		Score score2 = new Score(101, "ABC", "DEF");
		
		boolean result = score1.equals(score2);
		
		Assert.assertEquals(false, result);
	}
	
	@Test
	public void equalsShouldRejectDifferentFirstName() {
		
		Score score1 = new Score(100, "ABC", "DEF");
		Score score2 = new Score(100, "ABD", "DEF");
		
		boolean result = score1.equals(score2);
		
		Assert.assertEquals(false, result);
	}
	
	@Test
	public void equalsShouldRejectDifferentSecondName() {
		
		Score score1 = new Score(100, "ABC", "DEF");
		Score score2 = new Score(100, "ABC", "DEG");
		
		boolean result = score1.equals(score2);
		
		Assert.assertEquals(false, result);
	}
	
	@Test
	public void equalsShouldRejectDifferentNameOrder() {
		
		Score score1 = new Score(100, "ABC", "DEF");
		Score score2 = new Score(100, "DEF", "ABC");
		
		boolean result = score1.equals(score2);
		
		Assert.assertEquals(false, result);
	}
	
	@Test
	public void validationShouldAcceptDefaultScore() {
		try{
			// must NOT throw an exception:
			Score score = Score.newDefaultScore();
			score.validate();
		} catch (InvalidScoreException e) {
			Assert.fail("Missing names should be accepted");
		}
	}
}
