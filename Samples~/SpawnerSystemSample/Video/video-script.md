# Spawner System — Video Walkthrough Script

**Target length:** ~10-12 minutes
**Resolution:** 1080p, 30fps
**Audio:** clear voice, no background music
**Tools:** OBS (or QuickTime + Audacity), Unity Editor open on DemoScene

---

## Section 1 — Intro (30s)

### Screen
- Show the README.md `Spawner System` section in browser/IDE
- Cut to Unity Editor with `Samples~/SpawnerSystemSample/Assets/Scenes/DemoScene.unity` open

### Voiceover
> "Hi, I'm Satish Rathod. This is the walkthrough for the Spawner System mechanic I've contributed to UnityMechanicsFramework as part of Issue #25.
>
> In the next 10 minutes, I'll cover four things — first, an end-to-end demo of the working scene; second, a walkthrough of every script and how they connect; third, how the data flows; and finally, I'll build a spawner prefab from scratch using only scripts — no editor tooling.
>
> Let's get into it."

---

## Section 2 — End-to-End Demo (1m 30s)

### Screen
- Press Play in Unity
- Show all 3 spawners running in the demo scene
- Move player into proximity zone — show ProximitySpawner triggering
- Wait for a wave to clear — show WaveSpawner advancing
- Show TimedSpawner pumping out enemies on interval
- Open the Console — point at `[WaveSpawner] Wave 1 started` / `EnemyDiedEvent` logs
- Stop Play

### Voiceover
> "This is `DemoScene`. We have three spawners active at once — a WaveSpawner on the left, a TimedSpawner in the center, and a ProximitySpawner on the right.
>
> Watch the WaveSpawner — it's spawning enemies in rounds. When the current wave clears, it waits for the configured cooldown, then starts the next wave. Difficulty scales per wave via an AnimationCurve.
>
> The TimedSpawner here is firing on a fixed interval — every two seconds. It uses our `TimerUtility_UMFOSS` so it respects the global pause system.
>
> Now watch the right side. As I move the player into the trigger radius… *(move player)* …the ProximitySpawner detects via `Physics2D.OverlapCircle` and fires once.
>
> All three spawners share the same data layer — `SpawnProfile` ScriptableObjects. The only thing that differs is the trigger mechanism. That's the core idea behind this system."

---

## Section 3 — Folder Structure & Architecture (1m)

### Screen
- Open Project window, expand `Runtime/World/`
- List the 7 scripts on screen
- Open a quick draw or whiteboard overlay (or just point at scripts in order)

### Voiceover
> "Everything lives in `Runtime/World/`. There are seven scripts and they fall into three layers.
>
> The **data layer** is one file — `SpawnProfile_UMFOSS`. That's a ScriptableObject. It holds what spawns, how many, weights, and difficulty scaling.
>
> The **scene layer** is `SpawnPoint_UMFOSS` — a MonoBehaviour you drop on a transform. It has a shape — Point, Circle, or Rectangle — and gives a random position inside that shape when asked.
>
> The **trigger layer** is the three spawners — `WaveSpawner`, `TimedSpawner`, `ProximitySpawner`. They all read the same `SpawnProfile` and the same `SpawnPoint`s. They only differ in *when* they decide to spawn.
>
> Two helpers tie it together — `SpawnHelper_UMFOSS` does the actual instantiate-or-pool call, and `SpawnerEvents_UMFOSS` defines the events that fire — `EnemySpawnedEvent`, `EnemyDiedEvent`, `WaveStartedEvent`, `WaveClearedEvent`, `AllWavesCompletedEvent`.
>
> Communication between systems is through `EventBus` — no direct references."

---

## Section 4 — Script-by-Script Walkthrough (4m 30s)

### Screen
For each script, open it in IDE / Unity, scroll through key sections, and highlight the lines you're describing. Keep each script under 45s.

---

#### 4.1 — `SpawnProfile_UMFOSS.cs` (40s)

### Screen
- Open file, scroll to `[CreateAssetMenu]` line, then `WeightedEntry` struct, then `GetRandomPrefab` / `GetCountForWave` methods

### Voiceover
> "`SpawnProfile` is a ScriptableObject — that means encounters live as assets, not in code. You see the `[CreateAssetMenu]` attribute — that's how you create new profiles from the Project window.
>
> Inside, it holds a list of `WeightedEntry` — each entry has a prefab and a weight. When you call `GetRandomPrefab`, it does weighted random selection — heavier weights spawn more often.
>
> `GetCountForWave` reads the difficulty curve and returns how many enemies that wave should spawn. Designers tune the curve in the Inspector — no code change needed."

---

#### 4.2 — `SpawnPoint_UMFOSS.cs` (40s)

### Screen
- Show `Shape` enum (Point/Circle/Rectangle)
- Show `GetSpawnPosition()` switch
- Show `OnDrawGizmos` for editor visualization

### Voiceover
> "`SpawnPoint` is a MonoBehaviour you place in your scene. Its job is simple — give a position when asked.
>
> The `Shape` enum decides how the position is computed. `Point` returns the transform's position. `Circle` returns a random point inside a radius. `Rectangle` returns a random point inside a 2D box.
>
> The `OnDrawGizmos` block draws the shape in the Scene view so designers can see exactly where enemies will land. This is editor-only code wrapped in `#if UNITY_EDITOR` to keep it out of builds."

---

#### 4.3 — `SpawnHelper_UMFOSS.cs` (30s)

### Screen
- Show the static `Spawn` method — pulls from pool if available, else `Instantiate`

### Voiceover
> "`SpawnHelper` is a small static class with one job — turn a prefab plus a position into a live GameObject.
>
> It first asks the `ObjectPoolManager` for a pooled instance. If pooling is set up, you get reuse — no garbage. If not, it falls back to `Instantiate`. Every spawner uses this — it keeps spawning logic in one place."

---

#### 4.4 — `SpawnerEvents_UMFOSS.cs` (25s)

### Screen
- Show the event struct definitions

### Voiceover
> "`SpawnerEvents` is just data — five event structs. `EnemySpawnedEvent` and `EnemyDiedEvent` track active counts. `WaveStartedEvent`, `WaveClearedEvent`, and `AllWavesCompletedEvent` are for the wave system.
>
> Any other mechanic — UI, audio, score — can subscribe via `EventBus.Subscribe<EnemyDiedEvent>` without ever knowing the spawner exists."

---

#### 4.5 — `WaveSpawner_UMFOSS.cs` (50s)

### Screen
- Show `StartWaves()` entry point
- Show wave loop — spawn count, wait for clear condition, cooldown, next wave
- Show the `WaveClearCondition` enum (AllDead / TimedEnd / Manual)

### Voiceover
> "`WaveSpawner` runs round-based encounters. You assign a list of `WaveProfile`s in the Inspector — each wave has its own profile, count, and clear condition.
>
> Calling `StartWaves` kicks off the loop. For each wave it spawns the configured count, then waits — either for all enemies to die, or for a fixed timer, or for an explicit `EndWaveManually` call. Then it cooldowns and starts the next wave.
>
> Active enemy count is tracked via `EnemySpawnedEvent` and `EnemyDiedEvent` on the EventBus — not by null-checking, because pooled enemies are deactivated, not destroyed. That's a real footgun if you don't use events."

---

#### 4.6 — `TimedSpawner_UMFOSS.cs` (45s)

### Screen
- Show `StartSpawning()`
- Show interval logic via `TimerUtility_UMFOSS`
- Show max-active cap and respawn-on-death

### Voiceover
> "`TimedSpawner` fires on a fixed interval. It uses `TimerUtility_UMFOSS` instead of an `Update` countdown, which means it integrates with our pause system automatically — pause the game, the timer pauses too, no extra wiring.
>
> Two configuration options matter — `maxActive` caps how many enemies can be alive at once, and `respawnOnDeath` makes the spawner refire immediately when an enemy dies instead of waiting for the next interval. Together they give you patrol-style and respawn-style spawning from the same script."

---

#### 4.7 — `ProximitySpawner_UMFOSS.cs` (40s)

### Screen
- Show `Update` doing `Physics2D.OverlapCircle`
- Show one-shot vs cooldown branch
- Show `ForceSpawn()` API

### Voiceover
> "`ProximitySpawner` triggers when something on the player layer enters its radius. I'm using `Physics2D.OverlapCircle` instead of `OnTriggerEnter` deliberately — `OverlapCircle` works without a Rigidbody and without a Collider on the spawner itself. It's lighter and simpler for ambush spawners.
>
> Two modes — one-shot, which fires once and disables itself, or cooldown, which re-arms after a delay so the player can trigger it again. There's also a `ForceSpawn` public method for scripted events."

---

## Section 5 — Build a Spawner From Scratch via Script (2m 30s)

### Screen
- Create an **empty scene** (File → New Scene → Empty)
- In Project window, right-click → Create → ScriptableObjects → Spawner → SpawnProfile, name it `RuntimeProfile`
- Drag enemy prefab into the WeightedEntry list, weight 1
- Now create a new C# script `RuntimeSetup.cs` and paste:

```csharp
using UnityEngine;
using GameplayMechanicsUMFOSS.World;

public class RuntimeSetup : MonoBehaviour
{
    public SpawnProfile_UMFOSS profile;
    public GameObject enemyPrefab;

    void Start()
    {
        // 1. Build a SpawnPoint
        var spGo = new GameObject("SpawnPoint");
        spGo.transform.position = new Vector3(0, 0, 0);
        var sp = spGo.AddComponent<SpawnPoint_UMFOSS>();
        sp.shape = SpawnPoint_UMFOSS.Shape.Circle;
        sp.radius = 3f;

        // 2. Build a TimedSpawner that uses it
        var tsGo = new GameObject("RuntimeTimedSpawner");
        var ts = tsGo.AddComponent<TimedSpawner_UMFOSS>();
        ts.profile = profile;
        ts.spawnPoints = new[] { sp };
        ts.interval = 1.5f;
        ts.maxActive = 5;
        ts.spawnOnStart = true;
    }
}
```

- Drop it on an empty GameObject, assign `RuntimeProfile`, press Play
- Show enemies spawning every 1.5s in a 3-unit circle

### Voiceover
> "Reviewer asked specifically — show the prefab setup from scratch using only scripts, no editor build tool. So here's an empty scene, no prefab, no spawner GameObject.
>
> Step one — create the data. I right-click in Project, Create → SpawnProfile, drag in an enemy prefab, set weight one. That's the only asset I need.
>
> Step two — this `RuntimeSetup` script. I'm building everything in `Start`. First, I make a new GameObject, add a `SpawnPoint_UMFOSS` component, set its shape to Circle and radius to three.
>
> Then I make a second GameObject, add `TimedSpawner_UMFOSS`, plug in the profile, plug in the spawn point, set interval and max active. `spawnOnStart = true` makes it auto-fire.
>
> I drop `RuntimeSetup` on an empty GameObject, assign the profile, hit Play.
>
> *(press play)*
>
> Enemies spawning every 1.5 seconds inside a 3-unit circle. Zero editor wiring beyond dropping the script and the profile. The same pattern works for `WaveSpawner` and `ProximitySpawner` — every public field is settable from code."

---

## Section 6 — Recap (30s)

### Screen
- Cut back to README's `Spawner System` section
- Show the file list in `Runtime/World/`

### Voiceover
> "Quick recap. Three spawners — Wave, Timed, Proximity — sharing one data layer via `SpawnProfile`. Communication via `EventBus`, pause-aware via `TimerUtility`, pool-friendly via `SpawnHelper`. Every script has a `_scriptExplainer.txt` next to it with line-by-line code explanations. Demo scene is in `Samples~/SpawnerSystemSample`. Thanks for watching."

---

## Recording Checklist

- [ ] Mic levels tested — speak ~30cm from mic, no clipping
- [ ] Unity in Maximize-on-Play OFF so Console stays visible during demo
- [ ] Console cleared before recording each section
- [ ] Hide irrelevant Unity panels (Lighting, Profiler) — keep Hierarchy, Inspector, Project, Console, Scene/Game
- [ ] Editor zoom: increase font size in Preferences → UI Scaling so code is readable on 1080p
- [ ] Cursor highlighter on (OBS source or macOS accessibility) so viewer can follow clicks
- [ ] Final pass: re-watch on phone-screen size — if you can't read it on a phone, redo
- [ ] Export as `.mp4` H.264, place in `Samples~/SpawnerSystemSample/Video/SpawnerSystemTutorial.mp4` (overwrite existing)
- [ ] Commit + push, then comment on PR #38 with new video link
