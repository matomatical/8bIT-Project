# README #

Welcome to team 8-bIT Project's IT Project Project repository.

### What is this repository for? ###

We're building a cooperative platformer game for Android 4.1, using Unity 5. The game is currently unnamed. You can read more about it by checking out the [shared Google Drive](https://drive.google.com/drive/u/2/folders/0B-X6kHzx5k4TRGJ2dXlnTGR2aFU).

### How do I get set up? ###

#### Dependencies

Before checking out the project, you'll want to have installed the following software:

* Unity 5
* Android 4.1 SDK (available through [Android Studio](https://developer.android.com/studio/index.html)'s SDK Manager)

#### Deployment Instructions

Once you have those installed, you'll be able to follow these steps to get the project from the bitbucket repository and into Unity.

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

In order to run all tests,
1. Open the project in unity.
2. Go to the "Unity Test Tools > Integration Test Runner".
3. Click the "Run All" button.

### Contribution guidelines ###

Coming soon.

### Who do I talk to? ###

If you have any questions, email anyone of the following:

* Matt at farrugiam@student.unimelb.edu.au

* Mariam at mariams@student.unimelb.edu.au

* Sam at sbeyer@student.unimelb.edu.au

* Athir at isaleem@student.unimelb.edu.au

* Li at lcheng3@student.unimelb.edu.au
