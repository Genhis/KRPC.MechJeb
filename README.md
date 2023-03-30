# kRPC.MechJeb Service

Are you tired of manually giving commands to your rockets? Do you want to automate tourist transportation? Do you know basics of programming but don't want to bother with calculating precise steering yourself? Do you like to use MechJeb for controlling your rockets but want more automation? If all of this is true, then this KSP mod is just for you!

**This addon to [kRPC] provides remote procedures to interact with [MechJeb 2].** With [a simple script](https://genhis.github.io/KRPC.MechJeb/tutorials/launch-into-orbit.html), you can configure MechJeb autopilots and remotely control your rocket.

## Installation

1. Install [kRPC](https://krpc.github.io/krpc/getting-started.html) and [MechJeb 2](https://www.curseforge.com/kerbal/ksp-mods/mechjeb) mods.
2. Download [latest release files](https://github.com/Genhis/KRPC.MechJeb/releases).
3. Copy KRPC.MechJeb.dll to `Kerbal Space Program/GameData/kRPC` directory.
4. If you are using Python, Lua or similar client libraries, you can start coding right away. For C-nano, C++, C# and Java, find the relevant file in the release archive and link it with your project.
5. Happy coding!

If you encounter any issues, please check if your [kRPC] and [MechJeb 2] versions are supported for your chosen [kRPC.MechJeb version](https://github.com/Genhis/KRPC.MechJeb/releases). Newer versions may contain breaking changes, so it may be necessary to downgrade them to the latest supported versions. A popup will notify you about potential issues when you start the game, listing functions/properties which are unavailable. If you don't see the popup, kRPC.MechJeb should work as expected.

## Getting started

- Documentation for
[C-nano](https://genhis.github.io/KRPC.MechJeb/cnano/),
[C++](https://genhis.github.io/KRPC.MechJeb/cpp/),
[C#](https://genhis.github.io/KRPC.MechJeb/csharp/),
[Java](https://genhis.github.io/KRPC.MechJeb/java/),
[Lua](https://genhis.github.io/KRPC.MechJeb/lua/),
[Python](https://genhis.github.io/KRPC.MechJeb/python/)
- [Tutorials and Examples](https://genhis.github.io/KRPC.MechJeb/tutorials.html)
- [Third-party scripts](https://genhis.github.io/KRPC.MechJeb/third-party-scripts.html)

When you enter the flight scene or switch between vessels, it is **recommended to check whether the API is ready** before you call other operations. Otherwise, it may throw an exception.

## Contributing

All contributions to this mod are welcome. If you are interested in contributing, please read our [Contribution guidelines](https://github.com/Genhis/KRPC.MechJeb/blob/master/.github/CONTRIBUTING.md) and [Code of conduct](https://github.com/Genhis/KRPC.MechJeb/blob/master/.github/CODE_OF_CONDUCT.md).

See [Working from source](https://github.com/Genhis/KRPC.MechJeb/blob/master/.github/CONTRIBUTING.md#working-from-source) for more details how to set up the project.

## TO-DO

- Implement `AdvancedTransfer` maneuver operation
- Implement `AttitudeAdjustment` controller
- Implement `RoverAutopilot`
- Implement `SpaceplaneAutopilot` (known as Aircraft Approach & Autoland)

## Not implemented

- **NodeEditor** (replacement in kRPC.SpaceCenter service)
- **WarpHelper**

[kRPC]: https://krpc.github.io/krpc
[MechJeb 2]: https://github.com/MuMech/MechJeb2
