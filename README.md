# kRPC.MechJeb Service

Are you tired of manually giving commands to your rockets? Do you want to automate tourist transportation? Do you know basics of programming but don't want to bother with calculating precise steering yourself? Do you like to use MechJeb for controlling your rockets but want more automation? If all of this is true, then this KSP mod is just for you!

**This addon to [kRPC](https://krpc.github.io/krpc) provides remote procedures to interact with [MechJeb 2](https://github.com/MuMech/MechJeb2).** With [a simple script](https://genhis.github.io/KRPC.MechJeb/tutorials/launch-into-orbit.html), you can configure MechJeb autopilots and remotely control your rocket.

## Installation

1. Install [kRPC](https://krpc.github.io/krpc/getting-started.html) and [MechJeb 2](https://www.curseforge.com/kerbal/ksp-mods/mechjeb) mods.
2. Download [latest release files](https://github.com/Genhis/KRPC.MechJeb/releases).
3. Copy KRPC.MechJeb.dll to `Kerbal Space Program/GameData/kRPC` directory.
4. If you are using Python, Lua or similar client libraries, you can start coding right away. For C-nano, C++, C# and Java, find the relevant file in the release archive and link it with your project.
5. Happy coding!

Although all examples have been tested using **kRPC v0.4.8** and **MechJeb v2.8.3.0**, I do support upcoming versions, so please create a new issue if something doesn't work for you.

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

## Contributing

All contributions to this mod are welcome. If you are interested in contributing, please read our [Contribution guidelines](https://github.com/Genhis/KRPC.MechJeb/blob/master/.github/CONTRIBUTING.md) and [Code of conduct](https://github.com/Genhis/KRPC.MechJeb/blob/master/.github/CODE_OF_CONDUCT.md).

See [Working from source](https://github.com/Genhis/KRPC.MechJeb/blob/master/.github/CONTRIBUTING.md#working-from-source) for more details how to set up the project.

## TO-DO

- Add logging statements for debugging
- Implement `AdvancedTransfer` maneuver operation
- Implement `AttitudeAdjustment` controller
- Implement `RoverAutopilot`
- Implement `SpaceplaneAutopilot` (known as Aircraft Approach & Autoland)

## Not implemented

- **NodeEditor** (replacement in kRPC.SpaceCenter service)
- **WarpHelper**
