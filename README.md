# README #

Welcome to team 8-bIT Project's IT Project Project repository.

## What is this repository for? ##

We're building a cooperative platformer game for Android 4.1, using Unity 5.4. The game's working title is 'co-operace' (a portmanteau of 'co-operation' and 'race'). You can read more about it by checking out the Requirements doc and Design doc over on our [shared Google Drive](https://drive.google.com/drive/u/2/folders/0B-X6kHzx5k4TRGJ2dXlnTGR2aFU).

## How do I get set up? ##

#### Dependencies

Before checking out the project, you'll want to have installed the following software:

* Unity 5.4
* Android 4.1 SDK (available through [Android Studio](https://developer.android.com/studio/index.html)'s SDK Manager)

#### Deployment Instructions

Once you have those installed, you'll be able to follow these steps to get the project from the bitbucket repository and into Unity.

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
used.

Go to `File > Build Settings > Player Settings > Publishing Settings`. Then
create a keystore file (or use the android debug key).

##### Deploying to Android Device Manually

If you want to build and install the application directly on your device, follow these steps.

* Go to `File > Build Settings > Build` (`cmd+B`/`ctrl+B`), and select a location to save your build.
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

#### Testing

In order to run all tests

1. Open the project in unity.
1. Open the unity scene Assets/App/Tests/GameplayTests.
1. Go to the `Unity Test Tools > Integration Test Runner`.
1. Click the `Run All` button.

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

In general, if you're adding new code, a rule of thumb is to make sure that it conforms with the code around it.


## Who do I talk to? ##

If you have any questions, email anyone of the following:

* Matt at farrugiam@student.unimelb.edu.au

* Mariam at mariams@student.unimelb.edu.au

* Sam at sbeyer@student.unimelb.edu.au

* Athir at isaleem@student.unimelb.edu.au

* Li at lcheng3@student.unimelb.edu.au