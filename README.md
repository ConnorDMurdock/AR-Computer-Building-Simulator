# AR Computer Building Simulator
**Connor Murdock**

**Dr. Cindy Robertson**

**Georgia Gwinnett College**

## What is AR Computer Building Simulator?
  AR Computer Building Simulator is a mobile device application that uses augmented reality technology to guide users through an interactive experience that will teach them about various computer components and how to put them together to build a functional computer. This application was developed in Unity (2021.1.16f1) and works with Android devices that support augemented reality (requires Android 8.1 (API 27) or later). This project was developed for Georgia Gwinnett College through the STEC 4500 - Undergraduate Research Project program under the guidance of Dr. Cindy Robertson.
  
![Screenshot from the app of the computer case with some hardware parts installed.](/Photos/ARPreview.png)

## Why create this app?
  In an online class environment, some topics are much harder to teach effectively. One such topic is computer hardware in ITEC 1001 classes. Traditionally, students participate in a computer hardware lab, where they get hands-on experience with computer hardware. Students who take the class online don't have access to the same equipment at home, the experience is replaced with a lecture-style method which cannot provide the same level of engagement.
  
  Students at home may not have access to physical computer hardware, but almost every student has access to a mobile device. Thus, the idea for this app was born. This application seeks to bridge the gap between physical labs and digital lectures to provide an experience as close to the real thing as possible without relying on physical hardware. Additionally, usage of the app can save money for the school by no longer requiring the schools keep computer hardware on campus for the physical labs.

## Where does the project stand?
  This project is currently still a work in progress, though significant progress has been made thus far. Currently, the implemented features include:
  - **Digital Instructor:** Students are taught all of the necessary information through the app by their robotic assistant, the "Computer-building Assistant and Robotic Lecturer" or C.A.R.L. for short.
  - **Step-by-Step Instruction:** The app takes students through the lab one computer hardware part at a time, so they won't feel overwhelmed by the amount of information presented to them.
  - **Save Feature:** Students can close the app and resume at any time by loading their saved game. No more lost progress!
  - **Badge Collecting:** As the student successfully installs computer parts, they will earn badges to reward their progress. The student can see their badge progress at any time through the options menu.
  - **Interactive Installation System:** Students are shown the computer parts in AR space, with the ability to view them from every angle. Parts are installed with animations to show the proper install technique and location in the computer.
  - **No Anchors Required:** Many AR apps rely on anchor points (usually an image or QR code) to operate and maintain location in real world space. This app does not rely on physical anchors, and instead dynamically tracks the world's surfaces.

![Screenshot from the app of the badges collected screen, where the user has earned all of the badges by installing all of the computer parts.](/Photos/BadgeScreen.png)

## What's Next?
  The remaining features that need to be implemented in the application are the following:
  - Currently only some of the parts have a complete install sequence. The remaining parts need to be set up with the full install sequence (attach the correct scripts, add sounds and animations, etc.).
  - Porting over to Apple devices. I currently don't own or have access to a Mac computer, so I am unable to build or test a version of the app for Apple devices. It's important that the app function on all kinds of devices to maintain a high level of accessability for students.
  - Support for cables in computer parts and animations. Cabling is an important part of computer building, and the current version of the app does not support cables in animations. Future support will need to be added so that students can have a complete experience.
  - General polish. More sounds and more animations to maintain engagement with students and make the most enjoyable experience possible.
