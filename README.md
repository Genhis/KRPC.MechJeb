# kRPC.MechJeb Service

*NOTE: This is a temporary fork of the kRPC.MechJeb project, aimed at providing support for MechJeb 2.15.x until the main project catches up.*

Are you tired of manually giving commands to your rockets? Do you want to automate tourist transportation? Do you know basics of programming but don't want to bother with calculating precise steering yourself? Do you like to use MechJeb for controlling your rockets but want more automation? If all of this is true, then this KSP mod is just for you!

**This addon to [kRPC] provides remote procedures to interact with [MechJeb 2].** With [a simple script](https://genhis.github.io/KRPC.MechJeb/tutorials/launch-into-orbit.html), you can configure MechJeb autopilots and remotely control your rocket.

## Installation

1. Install [kRPC 0.7.0](https://github.com/krpc/krpc/releases/tag/v0.7.0) and [MechJeb 2.15.4](https://github.com/MuMech/MechJeb2/releases/tag/2.15.4.0) for KSP 1.12.5.
2. Download [latest release files](https://github.com/darchambault/KRPC.MechJeb/releases).
3. Copy KRPC.MechJeb.dll and KRPC.MechJeb.xml to `Kerbal Space Program/GameData/kRPC` directory.
4. Use a kRPC 0.7.0 client. For C-nano, C++, C# and Java, use the matching MechJeb bindings in the release archive.
5. Happy coding!

If you encounter any issues, please check if your [kRPC] and [MechJeb 2] versions are supported for your chosen [kRPC.MechJeb version](https://github.com/darchambault/KRPC.MechJeb/releases). Newer versions may contain breaking changes, so it may be necessary to downgrade them to the latest supported versions. A popup will notify you about potential issues when you start the game, listing functions/properties which are unavailable. If you don't see the popup, kRPC.MechJeb should work as expected.

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

All contributions to this mod are welcome. If you are interested in contributing, please read our [Contribution guidelines](https://github.com/darchambault/KRPC.MechJeb/blob/master/.github/CONTRIBUTING.md) and [Code of conduct](https://github.com/darchambault/KRPC.MechJeb/blob/master/.github/CODE_OF_CONDUCT.md).

See [Working from source](https://github.com/darchambault/KRPC.MechJeb/blob/master/.github/CONTRIBUTING.md#working-from-source) for more details how to set up the project.

## TO-DO

- Implement `AdvancedTransfer` maneuver operation
- Implement `AttitudeAdjustment` controller
- Implement `RoverAutopilot`
- Implement `SpaceplaneAutopilot` (known as Aircraft Approach & Autoland)

## Not implemented

- **NodeEditor** (replacement in kRPC.SpaceCenter service)
- **WarpHelper**

[Original KRPC.MechJeb Add-on by Genhis]: https://github.com/Genhis/KRPC.MechJeb
[kRPC]: https://krpc.github.io/krpc
[MechJeb 2]: https://github.com/MuMech/MechJeb2
