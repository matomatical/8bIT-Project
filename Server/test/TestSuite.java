import org.junit.runner.RunWith;
import org.junit.runners.Suite;
import org.junit.BeforeClass;

import com.matomatical.util.Log;

import xyz._8bITProject.cooperace.leaderboards.tests.*;
import xyz._8bITProject.cooperace.leaderboards.protocol.tests.*;

@RunWith(Suite.class)
@Suite.SuiteClasses({
	ScoreTest.class,
	LeaderboardTest.class,
	LeaderboardsManagerTest.class,
	
	PositionResponseTest.class,
	SubmissionBodyTest.class,
	LeadersResponseTest.class,
	RequestBodyTest.class,
	MessageTest.class,
	
	// INSERT YOUR TESTS HERE, SEPARARTED BY COMMAS
	
	EmptyTest.class // final test class has no comma
})

/** TestSuite; junit test class to run all unit tests
 * @author Matt <farrugiam@student.unimelb.edu.au>
 */
public class TestSuite {

	// redirect test logging to test.log
	// and disable stdout logging to not interfere with test results
	@BeforeClass
	public static void setupLogging() {
		Log.setPath("./test.log", true);
		Log.setStdoutLogging(false);
	}

}
