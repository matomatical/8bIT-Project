@echo off

if not exist bin mkdir bin
javac -cp "./src/;./jar/boon.jar;./jar/hamcrest.jar;./jar/junit.jar;./jar/matomatical.jar" -d bin/ src/xyz/_8bITProject/cooperace/leaderboards/Server.java
echo build complete
