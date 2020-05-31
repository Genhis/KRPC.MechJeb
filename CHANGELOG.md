# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]
### Added
- `Visible` property to AirplaneAutopilot, AscentAutopilot, DockingAutopilot, LandingAutopilot, RendezvousAutopilot, SmartASS, SmartRCS, Translatron

## [0.6.0] - 2020-05-25
### Added
- `LaunchMode` property to AscentAutopilot to determine whether there is an ongoing timed launch
- `AbortTimedLaunch()` method to AscentAutopilot to cancel any known timed launch before it starts
- **`MakeNodes()` method to ManeuverPlanner operations** returning a list of nodes
- Popup windows in main menu and flight screens showing initialization errors and unavailable API methods
### Changed
- AscentClassic property `AutoPathPerc` was renamed to `AutoTurnPercent`
- AscentClassic property `AutoPathSpeedFactor` was renamed to `AutoTurnSpeedFactor`
- **All object instances** (modules, windows, controllers and operations) **are now permanent** until you reset KSP, so you can cache them and reuse them for multiple flights which may be useful if your program switches between vessels
### Deprecated
- `MakeNode()` in ManeuverPlanner operations was replaced with `MakeNodes()`
### Fixed
- AscentClassic properties - `AutoPathPerc` and `AutoPathSpeedFactor`
- ManeuverPlanner operations were not working in MechJeb v2.9.2.0+ ([#10](https://github.com/Genhis/KRPC.MechJeb/issues/10))
- Translatron mode getter raised an exception

## [0.5.1] - 2020-02-16
### Fixed
- `LaunchToRendezvous()` and `LaunchToTargetPlane()` methods in AscentAutopilot ([#8](https://github.com/Genhis/KRPC.MechJeb/issues/8))

## [0.5.0] - 2019-06-11
### Added
- **SmartASS** window
- **Translatron** window ([#5](https://github.com/Genhis/KRPC.MechJeb/pull/5))
### Fixed
- ValueError when setting enum properties ([#4](https://github.com/Genhis/KRPC.MechJeb/pull/4))
- OperationEllipticize apoapsis field pointed to periapsis instead

## [0.4.1] - 2019-05-11
### Fixed
- `AutostageLimit` property of StageController wasn't saved properly ([#2](https://github.com/Genhis/KRPC.MechJeb/issues/2))

## [0.4.0] - 2019-03-31
### Added
- Auto-hotstaging support in StageController
### Changed
- **AscentPEG** module overhaul (renamed to **AscentPVG**)
- **OperationTransfer** changed from Hohmann to Bi-impulsive transfer
### Fixed
- AscentAutopilot raised an exception during initialization phase ([#1](https://github.com/Genhis/KRPC.MechJeb/issues/1)) due to AscentPEG being renamed to AscentPVG ([MuMech/MechJeb2@61c0ada](https://github.com/MuMech/MechJeb2/commit/61c0adae6bea4f2f4e9b02b86534d4f1993b9e8))
- DeployableController raised an exception during initialization phase

## [0.3.0] - 2018-09-15
### Added
- **DeployableController** for antennas and solar panels
- **SmartRCS** window
- **ThrustController** containing most of the settings from the Utilities window
- `LaunchToRendezvous()` and `LaunchToTargetPlane()` methods to AscentAutopilot
- `AutostagingOnce` property to StagingController
### Removed
- Setters of `AutoTurnStartAltitude`, `AutoTurnStartVelocity`, `AutoTurnEndAltitude` in **AscentClassic** - these properties should be read-only
### Fixed
- `AscentAutopilot#DesiredInclination` property didn't do anything
- If ascent path editor was open while changing `AscentPathIndex`, the old window would not close and would report null ascent path

## [0.2.0] - 2018-09-07
### Added
- Missing `CorrectiveSteering` property in AscentAutopilot
### Changed
- `Operation#MakeNode()` returns a Node object from SpaceCenter service
### Removed
- `Enabled` property is no longer accessible for:
  - **AscentClassic**, **AscentGT** and **AscentPEG** - it is handled internally when you set your ascent path for AscentAutopilot
  - **NodeExecutor** - only GET is accessible, so that you can test whether the module is still running
  - **RCSController** - it hasn't been fully implemented yet and it would have no or unknown effect
  - **TargetController** - the module is always enabled
### Fixed
- NullReferenceException was thrown when trying to set `AscentPathIndex` in AscentAutopilot
- AscentAutopilot was not working properly when GUI wasn't visible
- MechJeb instance was referring to wrong vessel in certain situations e.g. changing focus to another vessel in a close proximity or reverting a flight to launch

## [0.1.0] - 2018-08-26
### Added
- **AirplaneAutopilot**
- **AscentAutopilot** with 3 ascent path options:
  - **AscentClassic:** Classic Ascent Profile
  - **AscentGT:** Stock-style GravityTurn™
  - **AscentPEG:** Powered Explicit Guidance (RSS/RO)
- **DockingAutopilot**
- **LandingAutopilot**
- **ManeuverPlanner** with 16 operations:
  - **OperationApoapsis**
  - **OperationCircularize**
  - **OperationCourseCorrection:** Fine tune closest approach to target
  - **OperationEllipticize:** Change both periapsis and apoapsis
  - **OperationInclination**
  - **OperationInterplanetaryTransfer**
  - **OperationKillRelVel:** Match velocities with target
  - **OperationLambert:** Intercept target at chosen time
  - **OperationLan:** Change longitude of ascending node
  - **OperationLongitude:** Change surface longitude of apsis
  - **OperationMoonReturn**
  - **OperationPeriapsis**
  - **OperationPlane:** Match planes with target
  - **OperationResonantOrbit**
  - **OperationSemiMajor**
  - **OperationTransfer:** Hohmann transfer to target
  - **TimeSelector** class to set starting time of maneuvers
- **NodeExecutor**
- **RCSController** (basic implementation)
- **RendezvousAutopilot**
- **StagingController**
- **TargetController** to get information about the current target

[Unreleased]: https://github.com/Genhis/KRPC.MechJeb/compare/v0.6.0...HEAD
[0.6.0]: https://github.com/Genhis/KRPC.MechJeb/compare/v0.5.1...v0.6.0
[0.5.1]: https://github.com/Genhis/KRPC.MechJeb/compare/v0.5.0...v0.5.1
[0.5.0]: https://github.com/Genhis/KRPC.MechJeb/compare/v0.4.1...v0.5.0
[0.4.1]: https://github.com/Genhis/KRPC.MechJeb/compare/v0.4.0...v0.4.1
[0.4.0]: https://github.com/Genhis/KRPC.MechJeb/compare/v0.3.0...v0.4.0
[0.3.0]: https://github.com/Genhis/KRPC.MechJeb/compare/v0.2.0...v0.3.0
[0.2.0]: https://github.com/Genhis/KRPC.MechJeb/compare/v0.1.0...v0.2.0
[0.1.0]: https://github.com/Genhis/KRPC.MechJeb/commit/6fafaaa41df39a60933d75cfd9c765c5aa8691f7
