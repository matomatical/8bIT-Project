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

# run the server
if $forever; then
	
	echo "starting server perpetually on port $port";

	while true; do
		java -cp "./bin:./jar/boon.jar:./jar/hamcrest.jar:./jar/junit.jar:./jar/matomatical.jar" xyz._8bITProject.cooperace.leaderboards.Server $port
		echo "re-starting server"
	done

else

	echo "starting server once on port $port";

	java -cp "./bin:./jar/boon.jar:./jar/hamcrest.jar:./jar/junit.jar:./jar/matomatical.jar" xyz._8bITProject.cooperace.leaderboards.Server $port

fi;
