# IN-NAV

## Augmented Reality Navigation App
Welcome to the Augmented Reality Navigation App repository! This project was developed as part of the STAR-PNT internship at IIT Tirupati Technological Innovation Hub. The app aims to provide users with AR-based navigation, guiding them to predefined points by overlaying digital directions on the real world.
Demonstration [Video here](https://youtu.be/yH35KQISpo4)

## Project Overview
The core functionality of this app lies in its ability to integrate Augmented Reality (AR) with real-world navigation. Users can view virtual arrows and paths overlaid on their surroundings, offering an intuitive and interactive navigation experience. The project is currently in the conceptual phase, with ongoing development and improvements.

<img src="Screenshot%202024-09-22%20143442.png">
<img src="Screenshot%202024-09-22%20143506.png">

## Key Features
### AR Navigation
Visual arrows and paths guide users through physical spaces, enhancing traditional navigation methods.
### 3D CAD Model Integration
A detailed 3D CAD model of the Central Instrumentation Facility Building at IIT Tirupati is integrated into the app, providing a realistic overlay of the real-world environment.
### Unity NavMesh
Utilizes Unity's NavMesh system to calculate the most efficient navigation paths, ensuring accurate and smooth guidance.
## Technologies Used
### Unity
For creating the AR experiences and integrating the 3D model.
### AR Foundation
Leveraging Unity's AR Foundation to build AR applications across different devices.
### 3D CAD Modeling
Created using tools like Autodesk Fusion and Blender, then imported into Unity.
## Future Enhancements
### Improved AR Stability 
Enhancing the stability and accuracy of AR overlays.
### Expanded Model Library
Adding more buildings and locations to provide comprehensive navigation aids.
### Enhanced User Interaction
Developing features like voice commands and haptic feedback to improve user experience.
## Installation Guide

Follow these steps to set up and run the AR Navigation app on your local machine:

### Prerequisites
- **Unity 2020.3 LTS or newer** (with AR Foundation and AR Subsystems packages)
- **3D Models** (if using custom models like the Central Instrumentation Facility)
- **Git** (for cloning the repository)
- **A supported AR device** (like an iOS or Android device with AR capabilities)

### Step 1: Clone the Repository
```bash
git clone https://github.com/TheKalbairCo/AR-Indoor-Navigation-App.git
cd AR-Indoor-Navigation-App
```

### Step 2: Open the Project in Unity
1. Launch **Unity Hub**.
2. Click on **Open Project**.
3. Navigate to the cloned repository folder and select it.
4. Wait for the project to load and all dependencies to resolve.
5. Open the "QR_Test" Scene in the project

### Step 3: Install Dependencies
Ensure the following Unity packages are installed:
1. Go to **Window > Package Manager**.
2. Search for and install:
   - **AR Foundation**
   - **AR Subsystems**
   - **AR Core XR Plugin** (for Android)
   - **ARKit XR Plugin** (for iOS)

### Step 4: Import 3D Models
1. Import the 3D CAD model (e.g., the Central Instrumentation Facility) into Unity.
2. Ensure the model is properly scaled and positioned for the AR overlay.

### Step 5: Build and Run on Device
1. Connect your AR-compatible device.
2. In Unity, go to **File > Build Settings**.
3. Select your target platform (Android/iOS) and click **Switch Platform**.
4. Ensure **AR Core/ARKit Support** is enabled in **Player Settings**.
5. Click **Build and Run** to deploy the app to your device.

### Step 6: Test the App
- Once the app is deployed, use your device’s camera to view the real world, and the AR arrows and navigation lines should guide you to predefined points.


## Contributions
I welcome contributions from the community! If you're interested in improving this project, please feel free to fork the repository, make your changes, and submit a pull request. For major changes, please open an issue first to discuss what you would like to change.

## License
This project is licensed under the MIT License - see the LICENSE file for details.

## Contact
For any inquiries or feedback, please contact me at abhimanyuvenu93@gmail.com.

## References
- [Google io 2018 code labs](https://codelabs.developers.google.com/io2018/)
- [Sceneform Quaternion handling for rotational orientation update](https://proandroiddev.com/arcore-cupcakes-4-understanding-quaternion-rotations-f90703f3966e)  
- [Pedometer](http://www.gadgetsaint.com/android/create-pedometer-step-counter-android/#.XPJx8vZuLIU)
- [MLkit text recogniser tutorial](https://www.youtube.com/watch?v=T0273WiUQPI)  
- [Sceneform intro](https://codelabs.developers.google.com/codelabs/sceneform-intro/index.html?index=..%2F..index#0)  
- [Building ARCore apps using sceneform](https://proandroiddev.com/building-arcore-apps-using-sceneform-part-4-9bb8374eaab4)  
- [Sceneform ar using arcore in android studio](https://medium.freecodecamp.org/how-to-build-an-augmented-reality-android-app-with-arcore-and-android-studio-43e4676cb36f)  


 
