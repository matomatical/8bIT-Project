import org.junit.runner.RunWith;
import org.junit.runners.Suite;

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
	// empty, used only as a holder for the above annotations
}