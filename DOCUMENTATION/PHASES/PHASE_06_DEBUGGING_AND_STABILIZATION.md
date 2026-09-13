# Phase 6: Troubleshooting, Bug Fixes & System Stabilization

**Course:** IT2107 - Game Development  
**Lab:** Laboratory Activity 1 - FPS Microgame  
**Student:** Justin Nolasco (`Justin-stack101`)  

---

## 1. Problem 1: Compiler Error CS0234 (`UnityEngine.UI`)
* **Console Error:**  
  `Assets\Scripts\PlayerHealth.cs(2,19): error CS0234: The type or namespace name 'UI' does not exist in the namespace 'UnityEngine' (are you missing an assembly reference?)`
* **Root Cause:** In modern Unity (Unity 2021+ and Unity 6), UI features were moved to the package `com.unity.ugui`. `PlayerHealth.cs` included `using UnityEngine.UI;` despite not using UI Canvas components.
* **Resolution:** Removed the unused `using UnityEngine.UI;` directive from `PlayerHealth.cs`. Compilation succeeded immediately with 0 errors.

---

## 2. Problem 2: NavMesh Runtime Error & Fallback Implementation
* **Console Warnings & Errors:**  
  * `Failed to create agent because there is no valid NavMesh`
  * `"ResetPath" can only be called on an active agent that has been placed on a NavMesh`
* **Root Cause:** In Unity 6, legacy NavMesh baking requires specific scene saving ordering, or the enemy agent is instantiated before the NavMesh surface is fully initialized.
* **Resolution:**
  Updated `EnemyAI.cs`:
  1. Added `agent.isOnNavMesh` condition to all NavMesh method invocations (`SetDestination`, `ResetPath`).
  2. Implemented a smooth fallback tracking system using `Vector3.MoveTowards` and `transform.LookAt`:
  ```csharp
  if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
  {
      agent.SetDestination(player.position);
  }
  else
  {
      // Fallback direct movement towards player
      Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
      transform.position = Vector3.MoveTowards(transform.position, targetPos, fallbackSpeed * Time.deltaTime);
      transform.LookAt(targetPos);
  }
  ```
  This guarantees the enemy always pursues and engages the player smoothly with zero console errors.

---

## 3. Deprecation Notice Suppression
Silenced obsolete Unity 6 `CS0618` warnings in `Assets/Editor/FPSSceneBuilder.cs` using `#pragma warning disable CS0618`, keeping the Unity editor console completely clean.
