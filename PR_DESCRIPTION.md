# [Mechanic] Update Boomerang Weapon Submission

## Mechanic Name

**Boomerang Weapon System** — `GameplayMechanicsUMFOSS.Combat`

## What changed in this update?

This update focuses on the requested submission fundamentals:

1. The boomerang runtime scripts now live under `Runtime/Mechanic/BoomerangWeapon/Scripts/`
2. The old combined explainer has been replaced with one explainer per runtime script inside `Runtime/Mechanic/BoomerangWeapon/Script_Explainers/`
3. The loose sample `Assets/` content has been removed from `Samples~/BoomerangWeapon/`
4. The original demo clip is still included, and a second walkthrough video ZIP has been added for:
   - script walkthrough
   - prefab setup from scratch
   - end-to-end mechanic behavior
5. README and contribution docs were updated so the boomerang entry matches the current submission format

## How to review it

1. Extract `Samples~/BoomerangWeapon/BoomerangWeaponProject.zip`
2. Open the extracted project in **Unity 2021.3 LTS or later**
3. Review the runtime source in `Runtime/Mechanic/BoomerangWeapon/Scripts/`
4. Review the explainers in `Runtime/Mechanic/BoomerangWeapon/Script_Explainers/`
5. Open `Samples~/BoomerangWeapon/BoomerangWeaponDemoVideo.zip` for the original gameplay demo
6. Open `Samples~/BoomerangWeapon/BoomerangWeaponSetupWalkthrough.zip` for the setup and script walkthrough

## Namespace used

`GameplayMechanicsUMFOSS.Combat`

## Folder structure

```text
Runtime/
└── Mechanic/
    └── BoomerangWeapon/
        ├── Scripts/
        └── Script_Explainers/

Samples~/
└── BoomerangWeapon/
    ├── BoomerangWeaponProject.zip
    ├── BoomerangWeaponDemoVideo.zip
    └── BoomerangWeaponSetupWalkthrough.zip
```

## README entry

The boomerang mechanic entry in `README.md` now points to:
- `Runtime/Mechanic/BoomerangWeapon/Scripts/`
- `Runtime/Mechanic/BoomerangWeapon/Script_Explainers/`
- `Samples~/BoomerangWeapon/BoomerangWeaponProject.zip`
- both in-repo video ZIPs

## Checklist

- [x] Runtime scripts moved into the expected mechanic structure
- [x] One script explainer added per boomerang runtime script
- [x] Loose sample `Assets/` folder removed from the repo sample directory
- [x] Original demo video kept in repo
- [x] New walkthrough video ZIP added in repo
- [x] README boomerang entry updated
- [x] Contribution guidance updated for the current submission format
