# README #

Welcome to team 8-bIT Project's IT Project Project repository.

## What is this repository for? ##

We're building a cooperative platformer game for Android 4.1, using Unity 5. The game's working title is 'co-operace' (a portmanteau of 'co-operation' and 'race'). You can read more about it by checking out the Requirements doc and Design doc over on our [shared Google Drive](https://drive.google.com/drive/u/2/folders/0B-X6kHzx5k4TRGJ2dXlnTGR2aFU).

The repository also includes our home-made leaders boards server (under `Server/`). It has its own build and test instructions, included later in this README. The leaderboards server is built in Java.

## How do I get set up? ##

### Android Application ###

#### Dependencies

Before checking out the project, you'll want to have installed the following software:

* Unity 5
* Android 4.1 SDK (available through [Android Studio](https://developer.android.com/studio/index.html)'s SDK Manager)

#### Deployment Instructions

Once you have those installed, you'll be able to follow these steps to get the project from the Bitbucket repository and into Unity.

* First you'll want to create a folder for the repository, and then download the repository and put it in that folder.
* Next, you'll want to open up Unity.
* Click on the "Open" button, navigate to and choose the folder you downloaded the repository to.
* If you haven't already, make sure you've told Unity where the Android SDK is on your computer. You can do this by going to Unity preferences, then external tools.
* All done! The project should open up in Unity, no worries!

##### Deploying to Android Device Manually

If you want to build and install the application directly on your device, follow these steps.

* From Unity, navigate to `File > Build Settings` (`cmd+^+B`/`ctrl+^+B`)
* If the selected platform (the one with the unity icon in the platform list) is not Android, select Android and press `Select Platform`.
* Press `Build`, and select a location to save your build.
* After the build succeeds, connect your Android device and copy the resulting apk file to somewhere on your device.
* Navigate to the apk file from your device, and press it to begin installing. You may have to alter your security settings to allow installation of third-party apks. (WARNING: Do not install apk files from sources you do not trust.)
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

#### Testing

In order to run all tests

1. Open the project in unity.
1. Go to the "Unity Test Tools > Integration Test Runner".
1. Click the "Run All" button.

### Server ###

#### Dependencies

The server aspect of the project has no external dependencies! However, the scripts for automated building, testing and running the server assume that the user is working in a unix environment with access to `sh` (the java classpath is specified with colons instead of semicolons, for example, so these might not run in properly on Windows).

#### Build Instructions

To build the Server, simply `cd` into the `Server/` directory and run `sh build.sh`. This may take a moment, but will recompile all of the classes into subfolders of the `bin/` directory, ready to be run.

You can then run the server using `sh run.sh` (making sure you are still within the `Server/` directory). This will launch the server for you on the default ports. You can quit any time using `ctl`+`C`.

You may want to select your own port for the server to listen on for testing purposes. You can do this using the `-p` option like so: `sh run.sh -p 2693` to run on port 2693, for example.

You may also want the server to run perpetually. That is, to start itself up again even if it ever closes. To achieve this, you can use the `-f` (forever) flag: `sh run.sh -f`.

> Note: You'll no longer be able to shut down the script with `ctrl`+`C`; this will now close (and restart) the server. You can end the script with `ctrl`+`Z` followed by `kill PID` where `PID` is the process ID of the script, found using the `ps` command.

If you want to leave the server running while you are away, the simplest way to do this is to background the launch script with `sh run.sh [opts] &`, and then disown it using `disown -h PID` where `PID` is the process ID of the script, found using the `ps` command.

> Note: You'll no longer be able to shut down the script the usual way, you'll have to use `kill PID` (if you don't know the ID at this point, `ps -x` lists IDs of disowned processes). If the server shows up as still running, kill that process too.

> Also: disowning a process nulls its standard input/output streams. If you want to keep these, you should redirect them using `>` and `<`.

##### Configuring the Application to connect to an instance of the Server

Instructions to come.


#### Testing

Once you have compiled the project (see build instructions) and are in the `Server/` directory, you can run the test suite by calling `sh test.sh`. This will re-build all jUnit test classes involved and then

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


