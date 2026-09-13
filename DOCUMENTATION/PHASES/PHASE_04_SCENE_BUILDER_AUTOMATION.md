# Phase 4: Custom Editor Tooling & Scene Builder Automation

**Course:** IT2107 - Game Development  
**Lab:** Laboratory Activity 1 - FPS Microgame  
**Student:** Justin Nolasco (`Justin-stack101`)  

---

## 1. Motivation for Automation
Manual coordinate entry for multi-room level layouts, material creation, prefab generation, and component wiring is prone to user error and tedious repetition. 

To streamline production and eliminate transform mistakes, we built a custom Unity Editor extension:
* **Script Location:** `Assets/Editor/FPSSceneBuilder.cs`
* **Menu Command:** `[MenuItem("FPS Lab/Build Complete FPS Scene")]`

---

## 2. Automated Pipeline Operations
When invoked from Unity's top menu bar, the tool executes:

1. **Asset Architecture Creation:**
   * Creates directories: `Assets/Materials`, `Assets/Prefabs`, `Assets/Scenes`.
   * Generates materials: `Mat_Floor` (dark slate), `Mat_Room1_Wall` (light stone), `Mat_Room2_Wall` (cyan blue — Lab Task 3), `Mat_Enemy` (red/orange), `Mat_Bullet` (gold).
2. **Prefab Generation:**
   * Creates a sphere with `Rigidbody` (continuous collision), `SphereCollider`, and `Bullet.cs`.
   * Serializes it to `Assets/Prefabs/Bullet.prefab`.
3. **Environment Generation:**
   * **Room 1 (Spawn):** 20m × 20m room with south, west, east walls and north wall with a 4m doorway.
   * **Corridor:** 8m long × 6m wide hallway connecting Room 1 to Room 2.
   * **Room 2 (Enemy Zone):** 20m × 20m room (Lab Task 2) with cover pillars and blue-themed materials (Lab Task 3).
4. **Player Assembly:**
   * Creates `Player` GameObject tagged `"Player"`.
   * Adds `CharacterController`, `PlayerHealth.cs`, and `FPSController.cs`.
   * Repositions Main Camera to player head height `(0, 0.65, 0)` and creates `FirePoint`.
   * Links `bulletPrefab`, `cameraTransform`, and `firePoint` automatically.
5. **Enemy Bot Placement:**
   * Instantiates `Enemy_Bot` in Room 2 with `NavMeshAgent`, `EnemyAI.cs`, and `EnemyHealth.cs`.
6. **Navigation & Persistence:**
   * Sets static navigation flags and invokes NavMesh generation.
   * Saves the scene to `Assets/Scenes/FPS_Level.unity` and inserts it into `EditorBuildSettings`.
