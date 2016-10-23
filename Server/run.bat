@echo off

:: default port

if "%1"=="" (
    set port=2693
) else (
    set port=%1
)

echo starting server once on port %port%

java -cp "./bin;./jar/boon.jar;./jar/hamcrest.jar;./jar/junit.jar;./jar/matomatical.jar" xyz._8bITProject.cooperace.leaderboards.Server %port%
