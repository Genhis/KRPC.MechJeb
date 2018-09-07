# KRPC.MechJeb Service

<span style="color: red">Please bear in mind that the addon is in Alpha stage and may contain bugs.</span>

This addon to [KRPC](https://krpc.github.io/krpc) (a [Kerbal Space Program](https://kerbalspaceprogram.com/) mod) intends to
create a link between [KRPC](https://krpc.github.io/krpc) and [MechJeb2](https://github.com/MuMech/MechJeb2) via reflection.

Huge thanks to the authors of [KRPC](https://krpc.github.io/krpc) and [MechJeb2](https://github.com/MuMech/MechJeb2) for
their amazing mods, and to the author of the original [krpcmj](https://github.com/artwhaley/krpcmj/) addon.

## Installation

1. Download [latest release files](https://github.com/Genhis/KRPC.MechJeb/releases).
2. Copy KRPC.MechJeb.dll to `Kerbal Space Program/GameData/kRPC` directory.
3. If you are using Python, Lua or similar client libraries, you can start coding right away. For C++, C# and Java, find the
relevant file in the release archive and link it with your project.
4. Happy coding!

## Modules

- **AirplaneAutopilot** (known as AircraftGuidance)
- **AscentAutopilot** (known as AscentGuidance)
  - **AscentClassic** (known as Classic Ascent Profile)
  - **AscentGT** (known as Stock-style GravityTurn™)
  - **AscentPEG** (known as Powered Explicit Guidance (RSS/RO))
- **DockingAutopilot**
- **LandingAutopilot** (known as Landing Guidance)
- **ManeuverPlanner** (supports all operations except `AdvancedTransfer`)
- **RendezvousAutopilot**

## Controllers

- **NodeExecutor**
- **RCSController**
- **StagingController**
- **TargetController**

## TO-DO

- Add logging statements for debugging
- Add documentation
- Add code examples
- Implement `AdvancedTransfer` maneuver operation
- Implement `AttitudeAdjustment` controller
- Implement `RoverAutopilot`
- Implement `SmartASS`
- Implement `SmartRCS`? (partial support in RCSController)
- Implement `SolarPanelController`
- Implement `SpaceplaneAutopilot` (known as Aircraft Approach & Autoland)
- Implement `Translation` module?

## Not implemented

- **NodeEditor** (replacement in KRPC.SpaceCenter)
- **ThrustController** (possible replacement in KRPC.SpaceCenter)
- **Utilities** window
- **WarpHelper**
