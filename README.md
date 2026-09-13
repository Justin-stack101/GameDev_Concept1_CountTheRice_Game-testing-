# IT2107 Lab 1 - FPS Demonstration Unity Project

## Course: IT2107 - Game Development
## Student: Justin Nolasco (Justin-stack101)
## Lab: Laboratory Exercise 1 - FPS Microgame

---

## About This Project
A first-person shooter (FPS) game built from scratch in Unity 6 as part of the IT2107 Lab Activity 1.

## Lab Requirements Completed
- [x] **Task 1**: Playtest — moved player spawn and tested in Play Mode
- [x] **Task 2**: Add a Room — built new connected room using Unity primitives
- [x] **Task 3**: Edit Colors — changed material colors on walls/floor
- [x] **Task 4**: Add Enemy Bot — enemy AI with chase and attack behavior
- [x] **Task 5**: Import Asset — imported 3D prop into the new room

## Documentation & Phase Records
Full documentation of all development phases, decisions, and chat records is organized in the [`DOCUMENTATION/`](DOCUMENTATION/README.md) directory:
- 📖 [Phase 1: Requirements & Architecture](DOCUMENTATION/PHASES/PHASE_01_REQUIREMENTS_AND_FRAMEWORK.md)
- 📖 [Phase 2: Core Gameplay Scripts](DOCUMENTATION/PHASES/PHASE_02_CORE_GAMEPLAY_SCRIPTS.md)
- 📖 [Phase 3: Level Geometry & Transforms](DOCUMENTATION/PHASES/PHASE_03_LEVEL_GEOMETRY_AND_TRANSFORMS.md)
- 📖 [Phase 4: Scene Builder Automation](DOCUMENTATION/PHASES/PHASE_04_SCENE_BUILDER_AUTOMATION.md)
- 📖 [Phase 5: Game Dev Concepts & Scoping](DOCUMENTATION/PHASES/PHASE_05_GAMEDEV_CONCEPTS_AND_FEASIBILITY.md)
- 📖 [Phase 6: Debugging & Stabilization](DOCUMENTATION/PHASES/PHASE_06_DEBUGGING_AND_STABILIZATION.md)
- 📖 [Phase 7: Playtesting & Submission](DOCUMENTATION/PHASES/PHASE_07_PLAYTESTING_AND_SUBMISSION.md)
- 📜 [Complete Chat History & Decision Log](DOCUMENTATION/DEVELOPMENT_CHAT_LOG_AND_HISTORY.md)

## Scripts
| Script | Purpose |
|--------|---------|
| `FPSController.cs` | Player movement (WASD), mouse look, shooting |
| `EnemyAI.cs` | Enemy chases and attacks the player |
| `EnemyHealth.cs` | Enemy takes damage from bullets and dies |
| `Bullet.cs` | Bullet deals damage on collision |
| `PlayerHealth.cs` | Player health with game-over restart |

## Controls
| Key | Action |
|-----|--------|
| WASD | Move |
| Mouse | Look around |
| Left Click | Shoot |
| Space | Jump |

## Unity Version
Unity 6000.5.10f1
