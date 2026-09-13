# Phase 5: Game Dev Fundamentals, Scoping & Career Theory

**Course:** IT2107 - Game Development  
**Lab:** Laboratory Activity 1 - FPS Microgame  
**Student:** Justin Nolasco (`Justin-stack101`)  

---

## 1. The 20/80 Rule in Game Engines
Unity contains thousands of APIs and settings. Professional game developers rely heavily on a core 20% that delivers 80% of functionality:
1. **GameObjects & Components:** Modular entity containers with behavior attachments.
2. **The Update Loop (`Update()`):** Real-time per-frame evaluation of inputs and motion.
3. **Collision Callbacks (`OnCollisionEnter` / `OnTriggerEnter`):** Physics-driven events.
4. **Prefabs:** Blueprint templates allowing dynamic instantiation at runtime.

---

## 2. Synergies: Full-Stack Web Development & Game Development
* **Logic & Architecture:** Web developers bring strong skills in state management, event-driven programming, and debugging.
* **Creative Outlet:** Game dev provides immediate tactile feedback, physics tuning ("game juice"), visual atmosphere, and lighting that enterprise web development rarely touches.
* **Technical Bridge:** Full-stack developers excel at multiplayer networking (WebSockets, HTTP APIs), database-backed leaderboards, and WebGL browser deployments.

---

## 3. Scoping, Feasibility, and Planning Techniques
* **The Danger of Scope Creep:** The #1 reason indie projects fail is over-ambition (attempting MMORPGs or AAA open-worlds as solo developers).
* **Greyboxing:** Testing movement, jumps, combat feel, and level layout with basic primitive geometry before touching art or textures.
* **Vertical Slice / MVP:** Building one single complete room with polished controls, one enemy, and complete win/loss conditions before attempting multiple levels.
* **The 1-Page GDD:** Keeping design documentation concise (Elevator pitch, Core loop, Constraints, Cut list).
