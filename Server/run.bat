@echo off

:: default port

if "%1"=="" (
    set port=2693
) else (
    set port=%1
)

:: navigate to directory
cd bin/

echo starting server once on port %port%

java -cp ".;../jar/boon.jar;../jar/hamcrest.jar;../jar/junit.jar" xyz._8bITProject.cooperace.leaderboards.Server %port%

cd ../
