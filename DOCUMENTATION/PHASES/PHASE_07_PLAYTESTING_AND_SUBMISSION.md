# Phase 7: Playtesting, Build & Submission Preparation

**Course:** IT2107 - Game Development  
**Lab:** Laboratory Activity 1 - FPS Microgame  
**Student:** Justin Nolasco (`Justin-stack101`)  

---

## 1. Playtesting Checklist (Lab Task 1)
In Unity Editor Play Mode:
* [x] **Player Movement:** WASD movement operates smoothly at 6 m/s.
* [x] **Mouse Aim:** Mouse look responds across X and Y axes, vertical look clamped between -80° and 80°.
* [x] **Jump Mechanics:** Spacebar jumps and gravity returns player to ground.
* [x] **Shooting:** Left Click spawns yellow physics projectiles traveling forward at 35 m/s.
* [x] **Enemy Reaction:** Enemy detects player when within 18m, changes color to aggressive red, pursues, and attacks in close quarters (15 damage per hit).
* [x] **Combat Feedback:** Enemy flashes white upon bullet impact; dies upon taking 50 damage (2 shots).

---

## 2. Build Instructions for Standalone Executable
1. Open Unity: **File → Build Settings**.
2. Confirm `Assets/Scenes/FPS_Level.unity` is checked in the Scenes in Build list.
3. Platform: **Windows, Mac, Linux** (Target Platform: **Windows**, Architecture: **x86_64**).
4. Click **Build** and choose output folder `Builds/`.
5. Unity produces `FPS_Demonstration_Unity.exe`.

---

## 3. Video Demonstration Checklist (≤60s)
* Tool: Windows Xbox Game Bar (`Win + G`) or OBS Studio.
* Sequence to record:
  1. Walk around Room 1 (Spawn).
  2. Walk through the Corridor into Room 2 (demonstrating custom blue wall materials and cover pillars).
  3. Engage the Enemy Bot (show bot chasing and shooting bullets to defeat it).
  4. Show the imported 3D prop.
* Video length: Under 60 seconds (per rubric guidelines).
