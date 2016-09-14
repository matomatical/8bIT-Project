#!/bin/sh

# parse command options
port="2693";	# default port
forever=false;	# default perpetual-ness

while [[ "$#" > 0 ]]; do
	case $1 in
    	-p) port="$2";shift;;
    	-f) forever=true;;
    	*) break;;
  	esac; shift
done

# navigate to directory
cd bin/

# run the server
if $forever; then
	
	echo "starting server perpetually on port $port";

	while true; do
		java -cp ".:../jar/boon.jar:../jar/hamcrest.jar:../jar/junit.jar" xyz._8bITProject.cooperace.leaderboards.Server
		echo "re-starting server"
	done

else

	echo "starting server once on port $port";

	java -cp ".:../jar/boon.jar:../jar/hamcrest.jar:../jar/junit.jar" xyz._8bITProject.cooperace.leaderboards.Server

fi;

cd ../