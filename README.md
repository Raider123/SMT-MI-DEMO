# 🎮 VR Motor Imagery Training Game

A virtual reality **serious game** designed to support **motor imagery (MI) training** for brain-computer interface (BCI) users.  
The game focuses on **motor priming**, **embodiment**, and **motivating training structures** to prepare users for MI-based BCI tasks.

---

## 🧠 Motivation

Motor imagery BCIs require **practice** — but traditional training protocols are often repetitive and mentally exhausting.  
This VR game provides a **more engaging training environment**, built around:

- **Physical movement priming** before MI tasks
- **Natural hand tracking** and **eye tracking**
- **Reward-based engagement mechanisms**
- **Immersive spatial context** to strengthen body ownership

---

## 🎛️ System Overview

| Component | Purpose |
|---------|---------|
| **Unity (C#)** | Game logic, interaction, UI |
| **Meta Quest Pro** | VR environment + eye + hand tracking |
| **Player** | Actively performs priming → later imagines movement |
| **Future Integration** | Online MI-based closed-loop BCI feedback |

All gameplay scripts and logic are located in: /Assets/

---

## 🏁 Start Screen

Players begin stationary behind a table and can choose between the tutorial and the game mode.

![Start Screen](assets/screenshots/start.png)

---

## 🎓 In-Game Tutorial (Hand Tracking)

The tutorial explains how to use **hand gestures** to interact with the game — no controllers required.

![Hand Gesture Tutorial](assets/screenshots/tutorial.png)

---

## 🎮 Gameplay Flow

The game alternates between **motor priming** and **motor imagery** phases:

- **MI Cue** → Display of the hand to be imagined
- **MI Task** → Physical or imagined hand movement
- **Feedback** → Score and reward feedback
- **Rest** → Short break before the next trial

![Gameplay Flow](assets/screenshots/game_flow.png)

---

## 🧩 Game Modes

| Mode | Purpose |
|------|--------|
| **Training Mode (Motor Priming)** | Player performs real movements to strengthen motor activation |
| **MI Mode (Motor Imagery)** | Player repeats the same task *mentally*, without movement |

This structure follows neuroscientific evidence that **physical priming improves MI learning**.

---

## 📁 Folder Structure
├── Assets/ # Game scripts, scene files, UI, models
├── Packages/
├── ProjectSettings/
└── assets/screenshots/ # Images used in README


## 🚀 Build Instructions

1. Open the project in **Unity**
2. Switch platform → **Android (Meta Quest Pro)**
3. Enable:
   - Hand Tracking Support
   - Eye Tracking Permissions
4. Build & Run using:
   - Meta Quest Link **or**
   - ADB USB deployment

---

## 🔮 Future Work

- Integration with **live BCI feedback**
- Adaptive difficulty
- VR embodiment enhancements (self-avatar / IK refinement)

---

## 📜 License

For research and educational use.
