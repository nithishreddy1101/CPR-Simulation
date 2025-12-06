# [Project Name] - VR CPR Simulation


## 🫀 Overview
**[Project Name]** is an immersive Virtual Reality application built with Unity designed to train users in Cardiopulmonary Resuscitation (CPR). This simulation provides a safe, controlled environment for users to practice life-saving techniques, offering real-time feedback on compression rate, depth, and hand placement.

The goal of this project is to gamify first-aid training and make it accessible, engaging, and effective using VR technology.

## ✨ Key Features
* **Realistic Environment:** [e.g., High-fidelity hospital room / Street accident scene].
* **Real-time Feedback:** Visual and audio cues indicating if chest compressions are too fast, too slow, or not deep enough.
* **Guided Tutorial:** Step-by-step voice/text instructions following standard CPR guidelines (e.g., AHA/Red Cross).
* **Interactable Medical Equipment:** [e.g., Functional AED (Automated External Defibrillator) interactions].
* **Hand Tracking / Controller Support:** Fully supports [e.g., Meta Quest Controllers / Hand Tracking].
* **Scoring System:** Users receive a score based on accuracy and timing at the end of the session.

## 🛠 Tech Stack
* **Engine:** Unity [2022.3 LTS]
* **Language:** C#
* **VR Framework:**  XR Interaction Toolkit
* **Hardware Tested:** Meta Quest 2, Meta Quest 3
* **3D Assets:**  Custom models / Blender / Unity Asset Store


## ⚙️ Installation & Setup

### Prerequisites
* Unity Hub and Unity Editor version **[Your Unity Version]**.
* A VR Headset compatible with OpenXR/Oculus Link.

### Steps to Run
1.  **Clone the Repository**
    ```bash
    git clone [https://github.com/](https://github.com/nithishreddy1101/CPR-Simualtion.git
    ```
2.  **Open in Unity**
    * Open Unity Hub.
    * Click "Add" and select the cloned project folder.
    * Open the project (Allow some time for package resolution).
3.  **Configure XR Settings**
    * Go to `Edit > Project Settings > XR Plug-in Management`.
    * Ensure the provider for your headset (e.g., Oculus, OpenXR) is checked.
4.  **Build & Run**
    * Connect your VR headset to the PC.
    * Go to `File > Build Settings`.
    * Select your platform (Android for Standalone Quest / Windows for PCVR).
    * Click **Build and Run**.


## 🤝 Contributing
Contributions are welcome! If you have suggestions for better CPR mechanics or optimization:
1.  Fork the Project
2.  Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3.  Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4.  Push to the Branch (`git push origin feature/AmazingFeature`)
5.  Open a Pull Request
