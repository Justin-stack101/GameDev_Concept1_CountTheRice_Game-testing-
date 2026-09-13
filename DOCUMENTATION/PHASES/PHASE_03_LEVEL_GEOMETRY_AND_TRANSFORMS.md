# Phase 3: Level Geometry, Transforms & The Stretched Walls Diagnosis

**Course:** IT2107 - Game Development  
**Lab:** Laboratory Activity 1 - FPS Microgame  
**Student:** Justin Nolasco (`Justin-stack101`)  

---

## 1. Manual Setup Coordinates Discussed
During initial arena construction, the baseline room coordinates were defined:
* **Plane (Floor):** Position `(0, 0, 0)`, Rotation `(0, 0, 0)`, Scale `(3, 1, 3)` (creates a 30m × 30m plane).
* **Wall North:** Position `(0, 1.5, 7.5)`, Scale `(15, 3, 0.5)`.
* **Wall South:** Position `(0, 1.5, -7.5)`, Scale `(15, 3, 0.5)`.
* **Wall East:** Position `(7.5, 1.5, 0)`, Scale `(0.5, 3, 15)`.
* **Wall West:** Position `(-7.5, 1.5, 0)`, Scale `(0.5, 3, 15)`.

---

## 2. The Stretched Walls Phenomenon
* **Symptom:** In the Unity editor viewport, the walls appeared as massive, distorted beams running across the floor.
* **Root Cause Analysis:**
  * In the Unity Hierarchy, the four wall GameObjects were dragged as **children** beneath the `Plane`.
  * In Unity's Transform hierarchy, a child object's world scale is computed as:
    $$\text{World Scale} = \text{Parent Scale} \times \text{Local Scale}$$
  * Because `Plane` had an X and Z scale of `3` (or `5`), the wall's local length of `15` was multiplied by `3`, stretching it to `45` units and distorting its thickness.
* **Key Learning Takeaway:**
  * Child objects inherit their parent's Transform matrix (Position, Rotation, Scale).
  * Independent architectural geometry (walls, floors) should never be childed to each other. Instead, group them under an unscaled empty GameObject (e.g. `[Environment]`, Scale `(1, 1, 1)`).
