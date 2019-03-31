# KRPC.MechJeb Service

Please bear in mind that the addon is in Alpha stage and may contain bugs. If you encounter a bug, please report it on
GitHub. Feel free to create a pull request if you want.

This addon to [KRPC](https://krpc.github.io/krpc) (a [Kerbal Space Program](https://kerbalspaceprogram.com/) mod) provides
RPCs to interact with [MechJeb2](https://github.com/MuMech/MechJeb2) via reflection.

Huge thanks to the authors of [KRPC](https://krpc.github.io/krpc) and [MechJeb2](https://github.com/MuMech/MechJeb2) for
their amazing mods, and to the author of the original [krpcmj](https://github.com/artwhaley/krpcmj/) addon.

## Installation

1. Download [latest release files](https://github.com/Genhis/KRPC.MechJeb/releases).
2. Copy KRPC.MechJeb.dll to `Kerbal Space Program/GameData/kRPC` directory.
3. If you are using Python, Lua or similar client libraries, you can start coding right away. For C-nano, C++, C# and Java,
find the relevant file in the release archive and link it with your project.
4. Happy coding!

Although all examples have been tested using **kRPC v0.4.8** and **MechJeb v2.8.3.0**, I do support upcoming versions,
so please create a new issue if something doesn't work for you.

## Getting started

- Documentation for
[C-nano](https://genhis.github.io/KRPC.MechJeb/cnano/),
[C++](https://genhis.github.io/KRPC.MechJeb/cpp/),
[C#](https://genhis.github.io/KRPC.MechJeb/csharp/),
[Java](https://genhis.github.io/KRPC.MechJeb/java/),
[Lua](https://genhis.github.io/KRPC.MechJeb/lua/),
[Python](https://genhis.github.io/KRPC.MechJeb/python/)
- [Tutorials and Examples](https://genhis.github.io/KRPC.MechJeb/tutorials.html)

## TO-DO

- Add logging statements for debugging
- Implement `AdvancedTransfer` maneuver operation
- Implement `AttitudeAdjustment` controller
- Implement `RoverAutopilot`
- Implement `SmartASS`
- Implement `SpaceplaneAutopilot` (known as Aircraft Approach & Autoland)
- Implement `Translation` module?

## Not implemented

- **NodeEditor** (replacement in KRPC.SpaceCenter)
- **WarpHelper**
