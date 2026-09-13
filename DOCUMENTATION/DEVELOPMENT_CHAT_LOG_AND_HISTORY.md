# IT2107 Lab 1 — Development Chat Log & Decision History

**Course:** IT2107 - Game Development  
**Student:** Justin Nolasco (`Justin-stack101`)  
**Project:** FPS Demonstration Unity Project  
**Repository:** [Justin-stack101/GameDev_Concept1_CountTheRice_Game-testing-](https://github.com/Justin-stack101/GameDev_Concept1_CountTheRice_Game-testing-.git)  
**Unity Version:** 6000.5.10f1 (Unity 6)  
**Date:** September 13, 2026  

---

## Executive Summary & Session Overview
This document records the complete chronological development conversation, technical decisions, troubleshooting steps, and conceptual discussions that took place during the implementation of **IT2107 Lab Activity 1 (FPS Microgame)**.

Due to the absence of the legacy Unity FPS Microgame template in Unity 6, the team opted to construct a clean, modular FPS demonstration from scratch using Unity primitives, custom C# scripts, and automated editor tooling.

---

## Chronological Conversation Log & Milestones

### Phase 1: Problem Definition & Unity 6 Compatibility
* **User Context:** Student needed to complete the 5 required tasks for IT2107 Lab 1:
  1. Playtest and adjust player spawn
  2. Add a new connected room
  3. Edit room colors/materials
  4. Add an enemy bot
  5. Import an asset and build a standalone executable with a demo video
* **Discovery:** The official Unity "FPS Microgame" template was deprecated and unavailable for Unity 6 (6000.5.10f1). Various third-party Asset Store controllers were either paid or incompatible.
* **Decision:** Build a lightweight, custom first-person shooter directly in Unity using native primitives and C# scripts. This satisfies 100% of the rubric while keeping the codebase transparent, modular, and performant.

### Phase 2: Core Gameplay Architecture & Scripting
Five foundational C# scripts were authored in `Assets/Scripts/`:
1. `FPSController.cs`: First-person player controller implementing WASD movement, mouse look, jumping, and shooting via `linearVelocity` (Unity 6 standard).
2. `EnemyAI.cs`: Bot that detects player distance, chases the player via NavMesh, and attacks within close range.
3. `EnemyHealth.cs`: Handles enemy damage taking, flash feedback, and destruction upon reaching 0 HP.
4. `Bullet.cs`: Physics projectile that inflicts damage on contact with `EnemyHealth` targets and cleans itself up.
5. `PlayerHealth.cs`: Player health management and automatic scene reset upon player defeat.

### Phase 3: GitHub Initialization & Backup
* Local git repository initialized at: `C:\Users\justi\Downloads\School Files\MainProjectCollection\FPS_Demonstration_Unity`.
* Remote configured to: `https://github.com/Justin-stack101/GameDev_Concept1_CountTheRice_Game-testing-.git`.
* Standard Unity `.gitignore` and `README.md` committed.

### Phase 4: Manual Level Design & The Stretched Walls Bug
* **User Question:** *"again on the plate what is the transform ? setup? and the cube part aswell?"*
* **Reference Values Provided:**
  * Plane (Floor): Position `(0, 0, 0)`, Scale `(3, 1, 3)` (or 5 for larger play areas).
  * North Wall Cube: Position `(0, 1.5, 7.5)`, Scale `(15, 3, 0.5)`.
* **Issue Observed via Screenshot:** In Unity's Hierarchy, the 4 wall cubes (`Wall_North`, `Wall_South`, `Wall_East`, `Wall_West`) were nested *inside* `Plane` as child GameObjects.
* **Technical Cause:** In Unity, child objects inherit their parent's Scale (Local Space to World Space transformation). Placing a wall with scale `15` under a Plane with scale `3` caused the wall to scale to `45` units, producing stretched white beams across the floor.

### Phase 5: Automation via Custom Editor Tooling
* **User Question:** *"can you do all of the changes? or its important that i need to manually do this?"*
* **Solution Created:** Developed `Assets/Editor/FPSSceneBuilder.cs`.
  * Added custom menu command: `[MenuItem("FPS Lab/Build Complete FPS Scene")]`.
  * Automatically creates:
    * Room 1 (Spawn room with 4 walls, doorway)
    * Corridor (Connecting hallway)
    * Room 2 (Lab Task 2: Second room with cover pillars)
    * Materials & Colors (Lab Task 3: Contrasting blue/dark palettes)
    * Player GameObject with First-Person Camera, FirePoint, and linked components (Lab Task 1)
    * Bullet Prefab with physics
    * Enemy AI Bot with NavMeshAgent (Lab Task 4)
    * Automatic NavMesh baking and Build Settings registration.

### Phase 6: Deep Dive — Game Dev Fundamentals & Career Insights
* **Discussion 1: Is manual placement important for a future game developer?**
  * Key takeaway: Understanding Parent-Child coordinate spaces, Component Architecture, and Prefabs is vital. However, professional developers don't hand-type numbers; they use grid snapping, modular kits, and custom editor tools.
* **Discussion 2: Transition from Full-Stack Web Dev to Game Dev as Creative Work:**
  * Web development provides rock-solid system logic, state management, and event handling.
  * Game development provides the creative outlet for physics, pacing, audio, and visual atmosphere.
  * Full-stack skills also empower developers to build multiplayer networking, web leaderboards, and WebGL browser exports.
* **Discussion 3: Feasibility, Limitations, Scoping & Planning:**
  * #1 risk in game development is Scope Creep.
  * The "Greyboxing" principle: test mechanics with gray cubes first.
  * The "Vertical Slice / MVP" approach: polish 1 room with 1 gun and 1 enemy before scaling out.

### Phase 7: Troubleshooting & Bug Fixes
* **Bug 1: Compiler Error CS0234 (`PlayerHealth.cs`)**
  * *Error:* `The type or namespace name 'UI' does not exist in the namespace 'UnityEngine'`.
  * *Fix:* Removed unused `using UnityEngine.UI;` from `PlayerHealth.cs`.
* **Bug 2: NavMeshAgent `ResetPath()` Runtime Error**
  * *Error:* `"ResetPath" can only be called on an active agent that has been placed on a NavMesh`.
  * *Fix:* Added `agent.isOnNavMesh` check to `EnemyAI.cs`, plus an automatic fallback so the enemy smoothly translates towards the player even if the NavMesh has not yet been baked.
* **Deprecation Warnings:** Silenced obsolete Unity 6 CS0618 warnings in the editor builder.

### Phase 8: Git Commit & Remote Push
* Checkpoint 1 committed:
  * 28 files added/modified (+4299 insertions).
  * Included complete scene `Assets/Scenes/FPS_Level.unity`, materials, prefabs, editor scripts, and project settings.
* Successfully pushed to `origin/main` on GitHub.

---

## Technical Checklist & Lab Task Mapping

| Lab Task | Requirement | Status | Implementation Details |
| :--- | :--- | :---: | :--- |
| **Task 1** | Playtest FPS | ✅ Done | WASD movement, mouse pitch/yaw, jump, shooting via `FPSController.cs`. |
| **Task 2** | Add a Room | ✅ Done | Room 1 (Spawn) connected via hallway to Room 2 (Enemy Zone) generated by `FPSSceneBuilder.cs`. |
| **Task 3** | Edit Colors | ✅ Done | Custom materials (`Mat_Floor`, `Mat_Room1_Wall`, `Mat_Room2_Wall` in blue theme) generated in `Assets/Materials/`. |
| **Task 4** | Add Enemy Bot | ✅ Done | `Enemy_Bot` capsule with `NavMeshAgent`, `EnemyAI.cs`, and `EnemyHealth.cs`. |
| **Task 5** | Import Asset & Build | 🔄 Ready | Free 3D prop import and Standalone Windows `.exe` build ready to trigger. |

---

*This document serves as permanent project documentation and development proof for IT2107.*
