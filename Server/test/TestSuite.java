import org.junit.runner.RunWith;
import org.junit.runners.Suite;

import xyz._8bITProject.cooperace.leaderboards.tests.*;
import xyz._8bITProject.cooperace.leaderboards.protocol.tests.*;

@RunWith(Suite.class)
@Suite.SuiteClasses({
	// tests here
	ScoreTest.class,
	LeaderboardTest.class,
	LeaderboardsManagerTest.class,
	
	PositionResponseTest.class,
	SubmissionBodyTest.class,
	LeadersResponseTest.class,
	RequestBodyTest.class,
	MessageTest.class
})

public class TestSuite {
	// empty, used only as a holder for the above annotations
}