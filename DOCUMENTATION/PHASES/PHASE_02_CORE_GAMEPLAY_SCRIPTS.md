# Phase 2: Core Gameplay Architecture & Scripting

**Course:** IT2107 - Game Development  
**Lab:** Laboratory Activity 1 - FPS Microgame  
**Student:** Justin Nolasco (`Justin-stack101`)  

---

## 1. Scripting Overview
To replace the missing microgame template, five custom C# scripts were authored in `Assets/Scripts/`:

| Script | Responsibility | Key Features |
| :--- | :--- | :--- |
| `FPSController.cs` | Player Controller | WASD motion, Mouse Look with vertical clamping (-80° to 80°), Jump physics, Weapon shooting via `linearVelocity`. |
| `EnemyAI.cs` | Bot Artificial Intelligence | Detection radius, NavMesh chasing, melee attack cooldown, visual color change on aggro, fallback direct tracking. |
| `EnemyHealth.cs` | Enemy Damage System | HP tracking, hit feedback (white flash coroutine), object destruction on death. |
| `Bullet.cs` | Projectile Physics | Damage delivery on `OnCollisionEnter`, collision cleanup, auto-destruction timer. |
| `PlayerHealth.cs` | Player Life System | HP clamping, damage logging, automatic scene reload on death. |

---

## 2. Unity 6 Modernization Notes
In `FPSController.cs`, projectile impulse uses:
```csharp
rb.linearVelocity = firePoint.forward * bulletSpeed;
```
This adheres to the Unity 6 API where `rb.velocity` was replaced with `rb.linearVelocity`.
