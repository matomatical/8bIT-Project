cd test/

# Build Tests
echo "building tests..."
javac -cp ".:../bin:../jar/junit.jar" TestSuite.java
echo "built!"

# Run Tests
echo "running tests..."
java -cp ".:../bin/:../jar/boon.jar:../jar/junit.jar:../jar/hamcrest.jar" org.junit.runner.JUnitCore TestSuite
echo "testing complete"

cd ../