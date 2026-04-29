# Spawner System — Take-by-Take Recording Plan

**How to use this:** Record one TAKE at a time. Each take has three sections — **Pre-Take Steps** (do before pressing Record), **In-Take Steps** (do while recording, synced with the voiceover), and the **Voiceover** (read this verbatim). After Stop Recording, set up the next take's pre-steps and continue.

**~16 takes. Each 15-60 seconds. Recording time with retakes: ~45-65 min.**

**File naming:** save each as `take-NN-name.mov` in `Samples~/SpawnerSystemSample/Video/raw-takes/`. If you retake, use `take-14a.mov`, `take-14b.mov`, etc.

---

## Phase A — One-Time Setup (do ONCE before any take)

### A1. Environment
- [ ] Close Slack, browsers (except one PR tab), Spotify, anything that pings
- [ ] Silence phone, put it face-down
- [ ] Mac: System Settings → Notifications → Do Not Disturb ON
- [ ] Mac: System Settings → Focus → enable for 2 hours

### A2. Unity Editor setup
- [ ] Open `assignment/UnityMechanicsFramework/` in Unity
- [ ] Open `Samples~/SpawnerSystemSample/Assets/Scenes/DemoScene.unity`
- [ ] Edit → Preferences → UI Scaling → 125% (so text reads on 1080p)
- [ ] Window → Layout → Default (predictable panel positions)
- [ ] Visible panels: Hierarchy, Scene, Game, Inspector, Project, Console
- [ ] Maximize on Play: OFF (right-click Game tab → uncheck)
- [ ] Console: clear, "Collapse" off, "Clear on Play" ON, "Error Pause" OFF

### A3. IDE setup
- [ ] Open the project in VS Code or Rider
- [ ] Open all 7 files in `Runtime/World/` as tabs in this order:
      `SpawnProfile_UMFOSS.cs`, `SpawnPoint_UMFOSS.cs`, `SpawnHelper_UMFOSS.cs`, `SpawnerEvents_UMFOSS.cs`, `WaveSpawner_UMFOSS.cs`, `TimedSpawner_UMFOSS.cs`, `ProximitySpawner_UMFOSS.cs`
- [ ] Bump font: Cmd+= a few times (or Settings → font size 16+)
- [ ] Hide minimap and sidebars to maximize code area

### A4. Recording
- [ ] QuickTime → File → New Screen Recording → set audio source to your mic
- [ ] Test: record 5 seconds, play back, check audio level (no clipping, no hiss)
- [ ] Resolution target: 1920x1080 @ 30fps
- [ ] Mic ~30cm from mouth, away from keyboard
- [ ] Create folder: `Samples~/SpawnerSystemSample/Video/raw-takes/`

### A5. Pre-stage assets needed later (saves time during recording)
- [x] `RuntimeSetup.cs` already created at `SpawnerProject/Assets/Scripts/RuntimeSetup.cs` — uses public `Configure()` API (added to SpawnPoint_UMFOSS and TimedSpawner_UMFOSS in this prep step):

```csharp
using System.Collections.Generic;
using UnityEngine;
using GameplayMechanicsUMFOSS.World;

public class RuntimeSetup : MonoBehaviour
{
    public SpawnProfile_UMFOSS profile;

    void Start()
    {
        var spGo = new GameObject("SpawnPoint");
        spGo.transform.position = Vector3.zero;
        var sp = spGo.AddComponent<SpawnPoint_UMFOSS>();
        sp.Configure(SpawnShape.Circle, radius: 3f);

        var tsGo = new GameObject("RuntimeTimedSpawner");
        var ts = tsGo.AddComponent<TimedSpawner_UMFOSS>();
        ts.Configure(
            profile: profile,
            spawnPoints: new List<SpawnPoint_UMFOSS> { sp },
            interval: 1.5f,
            maxActive: 5,
            spawnOnStart: true);
    }
}
```
- [x] Compile verified — zero errors

### A6. Pre-take ritual (do before EVERY take)
1. Read the voiceover out loud once cold
2. Execute the Pre-Take Steps for that take
3. Take a breath
4. Press Record
5. Wait 1 full second of silence
6. Speak the voiceover, performing the In-Take Steps
7. After last word, wait 1 full second of silence
8. Stop Recording
9. Save file as `take-NN-name.mov`

That 1-second silence padding makes editing painless.

---

## Phase B — The Takes

---

### TAKE 1 — Intro hook (20s)

**Pre-Take Steps**
1. Open browser, navigate to `https://github.com/vijit101/UnityMechanicsFramework/pull/38`
2. Scroll so the PR title is visible
3. Make browser fullscreen (Cmd+Ctrl+F)

**In-Take Steps**
- Stay on the PR page, no clicks needed
- Speak clearly, look at the title

**Voiceover**
> "Hi, I'm Satish Rathod. This is the walkthrough for the Spawner System mechanic I've contributed to UnityMechanicsFramework as part of Issue twenty-five."

**Stop Recording. Save as `take-01-intro.mov`.**

---

### TAKE 2 — Agenda (20s)

**Pre-Take Steps**
1. Cmd+Tab to Unity
2. Click on Scene tab so DemoScene is visible
3. Frame the scene: F key after clicking any spawner GameObject in Hierarchy

**In-Take Steps**
- No clicks during the take, just speak with Scene view visible

**Voiceover**
> "In the next ten minutes I'll cover four things — an end-to-end demo, a walkthrough of every script, how the data flows, and finally I'll build a spawner from scratch using only scripts. No editor tooling. Let's go."

**Stop Recording. Save as `take-02-agenda.mov`.**

---

### TAKE 3 — End-to-end demo (50s)

**Pre-Take Steps**
1. Click Game tab so Game view is in focus
2. Player positioned AWAY from the proximity trigger zone
3. Game view framed wide so all three spawners are visible
4. Hand on movement keys (WASD/arrows), other hand near Ctrl+P

**In-Take Steps**
- 1 second in: press Play (Ctrl+P)
- Speak through the spawner list while scene runs
- At "as I move the player" — move player toward proximity zone
- At "fires" — proximity should trigger
- After the closing line, press Ctrl+P to stop Play

**Voiceover**
> "This is DemoScene. Three spawners running at once — WaveSpawner on the left in rounds, TimedSpawner in the center on a fixed interval, and ProximitySpawner on the right. As I move the player into the trigger radius… it detects via Physics2D.OverlapCircle and fires. All three share the same SpawnProfile data layer — only the trigger mechanism differs. That's the core idea."

**Stop Recording. Save as `take-03-demo.mov`.**

**Stop Recording. Save as `take-07-console-stop.mov`.**

---

### TAKE 4 — Architecture: folder structure (25s)

**Pre-Take Steps**
1. Click Project tab
2. Navigate to `Runtime/World/`
3. Expand the folder so all 7 scripts are visible
4. Make sure no files are selected (click empty area)

**In-Take Steps**
- Move cursor down the file list as you mention each layer
- "Data layer" → hover SpawnProfile
- "Scene layer" → hover SpawnPoint
- "Trigger layer" → hover the three spawner files

**Voiceover**
> "Everything lives in Runtime slash World. Seven scripts in three layers. Data layer — SpawnProfile, a ScriptableObject. Scene layer — SpawnPoint, a MonoBehaviour with a shape. Trigger layer — the three spawners."

**Stop Recording. Save as `take-04-folder-structure.mov`.**

---

### TAKE 5 — Architecture: helpers and events (20s)

**Pre-Take Steps**
1. Same Project window view
2. Hover over SpawnHelper file ready to point

**In-Take Steps**
- Hover SpawnHelper at "SpawnHelper handles"
- Hover SpawnerEvents at "SpawnerEvents defines"

**Voiceover**
> "Two helpers tie it together — SpawnHelper handles the actual instantiate-or-pool call, and SpawnerEvents defines the EventBus events. Communication is event-driven — no direct references between systems."

**Stop Recording. Save as `take-05-helpers-events.mov`.**

---

### TAKE 6 — Script: SpawnProfile (40s)

**Pre-Take Steps**
1. Cmd+Tab to IDE
2. Click `SpawnProfile_UMFOSS.cs` tab
3. Scroll to top so `[CreateAssetMenu]` line is visible
4. Place cursor in margin (out of the way)

**In-Take Steps**
- "ScriptableObject" → highlight `: ScriptableObject` line
- "CreateAssetMenu attribute" → highlight that attribute line
- "WeightedEntry" → scroll to and highlight the WeightedEntry struct
- "GetRandomPrefab" → scroll to that method
- "GetCountForWave" → scroll to that method

**Voiceover**
> "SpawnProfile is a ScriptableObject — encounters live as assets, not in code. The CreateAssetMenu attribute lets you make new profiles from the Project window. Inside, a list of WeightedEntry — prefab plus weight. GetRandomPrefab does weighted random selection. GetCountForWave reads the difficulty curve. Designers tune the curve in the Inspector — no code change."

**Stop Recording. Save as `take-06-spawnprofile.mov`.**

---

### TAKE 7 — Script: SpawnPoint (40s)

**Pre-Take Steps**
1. Click `SpawnPoint_UMFOSS.cs` tab
2. Scroll to the `Shape` enum at top
3. Cursor in margin

**In-Take Steps**
- "Shape enum" → highlight enum
- "Point", "Circle", "Rectangle" → highlight each enum value as you say it
- "GetSpawnPosition" → scroll to that method, show the switch
- "OnDrawGizmos" → scroll to that method
- "if-UNITY-EDITOR" → highlight the `#if UNITY_EDITOR` line

**Voiceover**
> "SpawnPoint is a MonoBehaviour that gives a position when asked. The Shape enum decides how — Point returns the transform, Circle returns a random point in a radius, Rectangle returns a random point in a 2D box. The OnDrawGizmos block draws the shape in the Scene view, wrapped in if-UNITY-EDITOR so it stays out of builds."

**Stop Recording. Save as `take-07-spawnpoint.mov`.**

---

### TAKE 8 — Script: SpawnHelper (25s)

**Pre-Take Steps**
1. Click `SpawnHelper_UMFOSS.cs` tab
2. Scroll to the static `Spawn` method
3. Cursor in margin

**In-Take Steps**
- "static class" → highlight class declaration
- "ObjectPoolManager" → highlight the pool lookup line
- "Instantiate" → highlight the fallback line

**Voiceover**
> "SpawnHelper is a static class with one job — turn a prefab plus position into a live GameObject. It asks the ObjectPoolManager first. If pooling is set up, you reuse — no garbage. Otherwise it falls back to Instantiate. Every spawner uses this."

**Stop Recording. Save as `take-08-spawnhelper.mov`.**

---

### TAKE 9 — Script: SpawnerEvents (25s)

**Pre-Take Steps**
1. Click `SpawnerEvents_UMFOSS.cs` tab
2. Scroll to top so all 5 event structs fit on screen if possible
3. Cursor in margin

**In-Take Steps**
- Hover or briefly highlight each event struct as you name it
- "EnemySpawnedEvent and EnemyDiedEvent" → highlight those two
- "WaveStartedEvent, WaveClearedEvent, AllWavesCompletedEvent" → highlight those three

**Voiceover**
> "SpawnerEvents is just data — five event structs. EnemySpawnedEvent and EnemyDiedEvent track active counts. WaveStartedEvent, WaveClearedEvent, AllWavesCompletedEvent are for the wave system. Any other system — UI, audio, score — can subscribe via EventBus without knowing the spawner exists."

**Stop Recording. Save as `take-09-spawnerevents.mov`.**

---

### TAKE 10 — Script: WaveSpawner (50s)

**Pre-Take Steps**
1. Click `WaveSpawner_UMFOSS.cs` tab
2. Scroll to `StartWaves()` method
3. Cursor in margin

**In-Take Steps**
- "StartWaves kicks off" → highlight `StartWaves` method signature
- "spawns the count, waits for the clear condition" → scroll to wave loop, highlight wait logic
- "all dead, timed end, or manual" → scroll to `WaveClearCondition` enum, highlight the three values
- "EventBus events, not null checks" → scroll to where EnemyDiedEvent is subscribed and highlight that line

**Voiceover**
> "WaveSpawner runs round-based encounters. You assign a list of WaveProfiles in the Inspector — each wave has its own profile, count, and clear condition. StartWaves kicks off the loop. For each wave it spawns the count, waits for the clear condition — all dead, timed end, or manual — then cooldowns and starts the next. Active count is tracked via EventBus events, not null checks. That matters because pooled enemies are deactivated, not destroyed. Null-checking would lie."

**Stop Recording. Save as `take-10-wavespawner.mov`.**

---

### TAKE 11 — Script: TimedSpawner (40s)

**Pre-Take Steps**
1. Click `TimedSpawner_UMFOSS.cs` tab
2. Scroll to `StartSpawning` method
3. Cursor in margin

**In-Take Steps**
- "TimerUtility" → scroll to and highlight the TimerUtility usage line
- "maxActive" → scroll to that field declaration
- "respawnOnDeath" → scroll to that field declaration

**Voiceover**
> "TimedSpawner fires on a fixed interval. It uses TimerUtility instead of an Update countdown — that means it pauses when the game pauses, no extra wiring. Two options matter — maxActive caps how many can be alive at once, and respawnOnDeath refires immediately when an enemy dies instead of waiting for the next interval. Together they give you patrol-style and respawn-style spawning from the same script."

**Stop Recording. Save as `take-11-timedspawner.mov`.**

---

### TAKE 12 — Script: ProximitySpawner (40s)

**Pre-Take Steps**
1. Click `ProximitySpawner_UMFOSS.cs` tab
2. Scroll to `Update` method showing `Physics2D.OverlapCircle`
3. Cursor in margin

**In-Take Steps**
- "Physics2D.OverlapCircle" → highlight that line
- "one-shot" → scroll to one-shot branch
- "cooldown" → scroll to cooldown branch
- "ForceSpawn" → scroll to ForceSpawn public method

**Voiceover**
> "ProximitySpawner triggers when something on the player layer enters its radius. I'm using Physics2D.OverlapCircle instead of OnTriggerEnter deliberately — OverlapCircle works without a Rigidbody and without a Collider on the spawner. Lighter, simpler. Two modes — one-shot fires once and disables, or cooldown re-arms after a delay. There's also a ForceSpawn public method for scripted events."

**Stop Recording. Save as `take-12-proximityspawner.mov`.**

---

### TAKE 13 — From-scratch: empty scene (25s)

**Pre-Take Steps**
1. Cmd+Tab to Unity
2. File → New Scene → Empty (Built-in)
3. Save as `RuntimeBuildScene.unity` in `Samples~/SpawnerSystemSample/Assets/Scenes/`
4. Hierarchy should show only `Main Camera` (or completely empty if you remove camera)
5. Project window in `Samples~/SpawnerSystemSample/Assets/`

**In-Take Steps**
- Hover Hierarchy to show it's empty
- No clicks needed during voiceover

**Voiceover**
> "Reviewer asked specifically — show prefab setup from scratch using only scripts, no editor tool. So here's an empty scene. No prefab. No spawner GameObject. Just a fresh scene."

**Stop Recording. Save as `take-13-empty-scene.mov`.**

---

### TAKE 14 — From-scratch: build and Play (60s)

**Pre-Take Steps (do BEFORE recording — all already-staged so the take is just a tour)**
1. `RuntimeProfile.asset` already created in `SpawnerProject/Assets/` with `Enemy_Basic` prefab and weight 1
2. `RuntimeSetup.cs` already exists in `SpawnerProject/Assets/Scripts/` (created in A5)
3. `RuntimeBuildScene.unity` open from take 13
4. `Bootstrap` GameObject already in Hierarchy with `RuntimeSetup` component added — Profile field intentionally LEFT EMPTY (you assign it on camera)
5. IDE has `RuntimeSetup.cs` open as the front tab
6. Project window in `SpawnerProject/Assets/` showing `RuntimeProfile` and `Scripts/RuntimeSetup`

**In-Take Steps**
- "the data" → click `RuntimeProfile` in Project to highlight it (Inspector shows entry list)
- "this script" → Cmd+Tab to IDE, scroll the `RuntimeSetup.cs` file once top-to-bottom (~5s)
- "drop and assign" → Cmd+Tab back to Unity, click `Bootstrap` in Hierarchy, drag `RuntimeProfile` from Project into the empty Profile field
- "hit Play" → press Ctrl+P
- Wait 4-5s, watch enemies spawn in Game view
- Press Ctrl+P at end of voiceover to stop Play

**Voiceover**
> "Step one — the data. RuntimeProfile asset, one enemy prefab, weight one. Step two — this RuntimeSetup script. In Start, it creates a SpawnPoint, configures it as a Circle with radius three. Then a TimedSpawner, configured with the profile, that spawn point, interval one point five, max active five, spawnOnStart true. Step three — drop the script on a Bootstrap GameObject, assign the profile, hit Play. Enemies spawning every one and a half seconds in a three-unit circle. Zero editor wiring. The same pattern works for WaveSpawner and ProximitySpawner — every public field is settable from code."

**Stop Recording. Press Ctrl+P to stop Play. Save as `take-14-build-and-play.mov`.**

---

### TAKE 15 — Recap (25s)

**Pre-Take Steps**
1. Open browser, navigate to README on GitHub or local file preview
2. Scroll to the Spawner System section (#3)
3. Make browser fullscreen

**In-Take Steps**
- Stay scrolled at the Spawner System card
- No clicks needed

**Voiceover**
> "Quick recap. Three spawners — Wave, Timed, Proximity — sharing one data layer via SpawnProfile. EventBus for communication. TimerUtility for pause-aware timing. SpawnHelper for pooling. Every script has a line-by-line explainer next to it."

**Stop Recording. Save as `take-15-recap.mov`.**

---

### TAKE 16 — Outro (15s)

**Pre-Take Steps**
1. Cmd+Tab to Unity
2. Project window on `Runtime/World/`
3. All 7 scripts AND all 7 `_scriptExplainer.txt` files visible

**In-Take Steps**
- Hover over the explainer files briefly while speaking

**Voiceover**
> "Demo scene is in Samples, Spawner System Sample. Thanks for watching."

**Stop Recording. Save as `take-16-outro.mov`.**

---

## Phase C — Stitching & Editing

### C1. Import
- [ ] Open iMovie / DaVinci Resolve / CapCut
- [ ] New project, 1920x1080 @ 30fps
- [ ] Drop all 16 takes into the timeline in number order
- [ ] If you have multiple versions (take-14a, 14b, 14c), pick the best one

### C2. Trim
- [ ] For each clip, trim ~0.5s of silence at start and end
- [ ] Aim for ~0.2s of silence between clips for natural pacing

### C3. Audio polish
- [ ] Add a 0.2s audio crossfade between every clip pair
- [ ] Normalize: Resolve → Fairlight → Loudness → target -16 LUFS
- [ ] Listen end-to-end on headphones for any pops or volume jumps

### C4. Visual polish (optional but recommended)
- [ ] Add a 3s title card at start: "UnityMechanicsFramework — Spawner System — by Satish Rathod"
- [ ] Add a 3s end card: "Issue #25 — PR #38"

### C5. Export
- [ ] Format: MP4, codec H.264, 1920x1080, 30fps, ~10-15 Mbps
- [ ] Save as `Samples~/SpawnerSystemSample/Video/SpawnerSystemTutorial.mp4` (overwrite the old one)
- [ ] Verify file size <500MB (GitHub limit is 100MB for direct push, may need Git LFS or a release attachment)

### C6. Sanity check
- [ ] Watch the full export on phone screen — text readable? Audio clear?
- [ ] If any take is broken, redo just that take and re-stitch

### C7. Ship
- [ ] Commit the new video to the branch
- [ ] Push
- [ ] Comment on PR #38: "Re-recorded the video. Covers end-to-end demo, every script walkthrough, and from-scratch prefab setup via code as requested."

---

## If you flub a take

Stop recording mid-take if you slip up. Save what you have as `take-NN-flub.mov`, then start a new take with the same number plus a letter suffix: `take-14a.mov`, `take-14b.mov`, `take-14c.mov`. Pick the best in editing.

## If your voice gets tired

Take a break. Drink water. Don't push through — tired voice = retake later. Aim for ~5 takes per session, then break for 10 min.
