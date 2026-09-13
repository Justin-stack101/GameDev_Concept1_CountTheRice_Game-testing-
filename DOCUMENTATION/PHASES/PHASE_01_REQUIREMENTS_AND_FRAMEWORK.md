# Phase 1: Requirements Analysis & Unity 6 Architecture

**Course:** IT2107 - Game Development  
**Lab:** Laboratory Activity 1 - FPS Microgame  
**Student:** Justin Nolasco (`Justin-stack101`)  
**Unity Version:** 6000.5.10f1  

---

## 1. Objective & Lab Rubric
The goal of this lab activity is to demonstrate fundamental game development competency inside Unity:
1. **Task 1: Playtest** — Modify player spawn and verify movement, camera look, and shooting mechanics in Play mode.
2. **Task 2: Add a Room** — Construct a second functional, connected room with corridors or doorways.
3. **Task 3: Edit Colors** — Create and assign custom materials with distinct color palettes to the environment.
4. **Task 4: Add Enemy Bot** — Implement an AI agent with detection, pathfinding, and attack behavior.
5. **Task 5: Import Asset** — Import an external 3D asset into the scene.
6. **Deliverables:** Standalone Windows `.exe` build, ≤60s demonstration video, and public GitHub repository.

---

## 2. The Unity 6 Compatibility Dilemma
* **Problem:** The assignment references the classic Unity "FPS Microgame" template. However, Unity 6 (6000.5.10f1) removed built-in support for older 2021/2022 microgame templates.
* **Exploration of Alternatives:**
  * Searched Unity Asset Store for free first-person controllers.
  * Encountered paid packages (e.g. Easy Peasy, First Person Controller Pro) or deprecated assets lacking Unity 6 support.
* **Strategic Decision:**
  * Rather than relying on fragile or obsolete third-party templates, we decided to **build a modular, lightweight FPS from scratch** using Unity primitives and native C# scripts.
  * **Advantages:** Full ownership of code, zero asset store dependencies, high performance, and directly demonstrative of core game programming knowledge.
