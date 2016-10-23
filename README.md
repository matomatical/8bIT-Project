# README #

Welcome to team 8-bIT Project's IT Project Project repository.

## What is this repository for? ##

We're building a cooperative platformer game for Android 4.1, using Unity 5.4. The game's title is 'co-operace' (a portmanteau of 'co-operation' and 'race'). You can read more about it by checking out the Requirements doc and Design doc over on our [shared Google Drive](https://drive.google.com/drive/u/2/folders/0B-X6kHzx5k4TRGJ2dXlnTGR2aFU).

## How do I get set up? ##

The project has two major parts: an android application and a server that stores high scores. First up, here are the instructions for setting up the android application. The server instructions are a bit further down.

### Android Application ###

#### Dependencies

Before checking out the project, you'll want to have installed the following software:

* Unity 5.4
* Android 4.1 SDK (available through [Android Studio](https://developer.android.com/studio/index.html)'s SDK Manager)

#### Deployment Instructions

Once you have those installed, you'll be able to follow these steps to get the project from the Bitbucket repository and into Unity.

* Download the repository, note which folder it is.
* Next, you'll want to open up Unity.
* Click on the "Open" button, navigate to and choose the folder you downloaded the repository to.
* Reimport all assets to make sure all plugins are properly initialized. (Right-click `Assets` in the `Project` window and select `Reimport`.)
* If you haven't already, make sure you've told Unity where the Android SDK is on your computer as well as the JDK. You can do this by going to `Edit > Preferences`, then external tools.
* From Unity, navigate to `File > Build Settings` (`cmd+shift+B`/`ctrl+shift+B`)
* If the selected platform (the one with the unity icon in the platform list) is not Android, select Android and press `Select Platform`.
* All done! The project should open up in Unity, no worries! You should be able to play a scene in Unity's player.

##### Setting up the Keystore

Before the a build can be made, it is necessary to setup the keystore to be
used. Get in touch if you need access to the keystore and password. Otherwise, go to `File > Build Settings > Player Settings > Publishing Settings`. Then
create a keystore file (or use the android debug key).

##### Deploying to Android Device Manually

If you want to build and install the application directly on your device, follow these steps.

* Go to `File > Build Settings > Build` (`cmd+B`/`ctrl+B`), and select a location to save your build.

> Note: if you would like select debug messages to be logged on the screen (anything logged with the UILogger), select the `development build` option from the `File > Build Settings` right before you press `Build`.

* After the build succeeds, connect your Android device and copy the resulting apk file to somewhere on your device.
* Navigate to the apk file from your device, and press it to begin installing. You may have to alter your security settings to allow installation of third-party apks. 

> (WARNING: Do not install apk files from sources you do not trust.)

* After the application has been installed, launch it from your device.

##### Deploying to Android Device Remotely

If you want to avoid having to manually install the application on your device, you can build and run it remotely using the Unity Remote 5 app.

To do this, you will first need to configure your Android device:

* Install Unity Remote 5 from the Google Play Store.
* Enable USB debugging in your device’s settings (you may need to [unlock developer options](http://www.greenbot.com/article/2457986/how-to-enable-developer-options-on-your-android-phone-or-tablet.html) first)

After configuring your device, follow these steps to deploy your application directly from Unity.

* Connect your device to your computer using a USB cable, and ensure that USB debugging is enabled over the connection.
* Start the Unity 5 Remote on your device.
* In Unity, navigate to `Edit > Project Settings > Editor`, and select 'Any Android Device' for the Device field in the Unity Remote section.
* Now, when you run your project within Unity (by pressing the `Play` button), after a short delay you should be able to control the game using your device.

Note: If the remote doesn't begin to show the game on your device, try restarting Unity.

##### Deploying to Android Emulator

* Open android studio project and hit `Run`.
* Further error or warning message resolutions can be found [here](https://developer.android.com/studio/run/emulator.html)

#### Integration Testing

Integration tests are located in `Assets/App/Tests` and sub folders. As it stands we have a gameplay testing scene `Assets/App/Tests/Gameplay Tests/Gameplay Tests.unity` to test game mechanics and a multiplayer testing scene `Assets/App/Tests/MultiplayerTests/MultiplayerTests.unity` to test sending and receiving updates. To run these tests:

1. Open the project in unity.
1. Open the scene containing the tests you would like to run.
1. Go to the `Unity Test Tools > Integration Test Runner`.
1. Click the `Run All` button.

#### Unit Testing

Unit tests are located in `Assets/App/Tests` and sub folders. To run these tests:

1. Ensure you have a local instance of the leaderboards server running (see Server section below). Some leaderboards tests will fail otherwise.
1. Open the project in Unity.
1. Navigate to `Window > Editor Tests Runner`.
1. In the windows which pops up, click `Run All`.

### Server ###

#### Dependencies

The server component of the project has no external dependencies! (Aside from Java).

However, the scripts for automated building, testing and running the server *assume that the user is working in a unix environment* with access to `sh`, because the server is intended to be run on a unix VM.

> To build, test and run on Windows, support is limited. However, you can give it a go. Simply replace `build.sh`, `test.sh`, and `run.sh` in the following instructions with `build.bat`, `test.bat`, and `run.bat`, respectively. These scripts are modified to use Windows-Java style classpaths, in which paths are separated by semicolons rather than colons.
> Note that `run.bat` unfortunately does not support running forever.

#### Build Instructions

To build the Server, simply `cd` into the `Server/` directory and run `sh build.sh`. This may take a moment, but will recompile all of the classes into subfolders of the `bin/` directory, ready to be run.

You can then run the server using `sh run.sh` (making sure you are still within the `Server/` directory). This will launch the server for you on the default ports. You can quit any time using `ctl`+`C`.

You may want to select your own port for the server to listen on for testing purposes. You can do this using the `-p` option like so: `sh run.sh -p 2693` to run on port 2693, for example.

You may also want the server to run perpetually. That is, to start itself up again even if it ever closes. To achieve this, you can use the `-f` (forever) flag: `sh run.sh -f`.

> Note: The script is build purposely for the version of unix on the server it is running on. These flags may not all work due to some differences with bash versions. You should be able to run the server at least once on the default port, though.

> Note: You'll no longer be able to shut down the script with `ctrl`+`C`; this will now close (and restart) the server. You can end the script with `ctrl`+`Z` followed by `kill PID` where `PID` is the process ID of the script (`run.sh`), found using the `ps` command.

If you want to leave the server running while you are away, the simplest way to do this is to background the launch script with `sh run.sh [opts] &`, and then disown it using `disown -h PID` where `PID` is the process ID of the script, found using the `ps` command.

> Note: You'll no longer be able to shut down the script the usual way, you'll have to use `kill PID` (if you don't know the ID at this point, `ps -x` lists IDs of disowned processes). If the server shows up as still running, kill that process too.

> Also: disowning a process nulls its standard input/output streams. If you want to keep these, you should redirect them using `>` and `<`.

##### Configuring the Application to connect to an instance of the Server

One thing you might need to do if you want to test the leaderboards server is configure the application to connect to a specific server address. Right now, it's set up to use `localhost:2693` inside the unity editor, and `lb.cooperace.8bitproject.xyz:2693` outside of the editor (on Android builds). This address contains a live running version of the leaderboards server. But, for testing purposes, you may wish to redirect your app to connect to your own instance of the server at your own address. This can be achieved by changing the static strings at the top of the `Leaderboards` class inside `Assets/App/Leaderboards/Leaderboards.cs`. If you set the editor/build addresses to wherever you are running the server, that will work!

#### Unit Testing

Once you have compiled the project (see build instructions) and are in the `Server/` directory, you can run the test suite by calling `sh test.sh`. This will re-build all jUnit test classes involved and then run each of the tests, giving you detailed error messages if any tests fail.

##### Adding New Tests

You can add new jUnit test classes anywhere under `Server/tests/`. A final step is then to add the names of your test classes to the list of tests included in the main test suite. To do this, open `Server/tests/TestSuite.java` and add your classes as shown:

```
import org.junit.runner.RunWith;
import org.junit.runners.Suite;

@RunWith(Suite.class)
@Suite.SuiteClasses({
    ExampleTest1.class,
    ExampleTest2.class,
    ExampleTest3.class,
    // add your tests here, delimited by commas
    ExampleTestN.class // final class has no comma
})

public class TestSuite {
  // empty, used only as a holder for the above annotations
}
```

Once you have finished, running `sh test.sh` again (from within `Server/`) as per the instructions above will compile and run all tests, including your new ones.

#### Integration Tests

More info to come

#### Acceptance Tests

The easiest way to perform end-to-end tests of the server is to use a command line utility such as netcat to open a TCP connection with the server and (e.g. `nc lb.cooperace.8bitproject.xyz 2693`, where you fill in the appropriate name and port number based on where you are running the server), and type/paste JSON messages as if you were the game contacting the server, to see what the replies are. Refer to the design documentation in our [Google Drive](https://drive.google.com/drive/u/2/folders/0B-X6kHzx5k4TRGJ2dXlnTGR2aFU) for info on the format of messages.

In the future, the aim is to provide a series of automated tests for this too.

#### Server Logs

While the server is running, it contributes to a logfile. This logfile can be found at either `Server/bin/logfile.txt` or `Server/logfile.txt`, depending on how the server was started. Check it out to see details of the interactions with connecting clients.

## Contribution guidelines ##

### Developing ###

Every major feature will be developed outside of the repository's master branch, in its own branch. This method will ensure that the master branch will always compile and contain a fully tested, working version of the project. The lifecycle for a feature will follow these steps:

1. Create a new git branch for the feature.
2. Develop the feature in that branch, making regular commits as usual.
3. When the feature is ready for an initial review, or when you'd like feedback or advice on your progress, use Bitbucket's pull request feature to conduct this (but don't merge the branch into master just yet).
4. Finish development of the feature, including all tests.
5. After the team has finished reviewing the code, and confirmed that all necessary tests are present and working the pull request will be accepted.

### Coding Conventions ###

In terms of conventions, we're not fussed about indentation or brace placement. However, code should be properly documented and all names should be descriptive and intuitive, while follow the language's standard conventions for casing.

In general, if you're adding new code, make sure that it conforms with the code around it.

## Who do I talk to? ##

If you have any questions, email anyone of the following:

* Matt at `farrugiam@student.unimelb.edu.au`

* Mariam at `mariams@student.unimelb.edu.au`

* Sam at `sbeyer@student.unimelb.edu.au`

* Athir at `isaleem@student.unimelb.edu.au`

* Li at `lcheng3@student.unimelb.edu.au`