#!/bin/sh
cd bin/
echo "starting server"
# while true; do
	java -cp ".:../jar/boon.jar:../jar/hamcrest.jar:../jar/junit.jar" xyz._8bITProject.cooperace.leaderboards.Server
	echo "re-starting server"
# done
cd ../
