# Contribution guidelines

Thank you for taking your time to contribute to this project!

The following is a set of guidelines which you should read to make a great contribution. These are mostly guidelines, not rules. Use your best judgment, and feel free to propose changes to this document in a pull request.

#### Table of contents

- [Reporting issues](#reporting-issues)
- [Working from source](#working-from-source)
- [Creating pull requests](#creating-pull-requests)
  - [Fixing bugs](#fixing-bugs)
  - [Adding new features](#adding-new-features)
- [Coding conventions](#coding-conventions)

## Reporting issues

If you encounter any problems with kRPC.MechJeb, please [open a new issue](https://github.com/Genhis/KRPC.MechJeb/issues/new/choose) and follow guidelines in the given template. Please make sure you have an up-to-date version of kRPC.MechJeb, older versions are not supported.

## Working from source

Besides general Visual Studio setup, you need to create lib folder in the root project directory and copy there four DLLs:
- **Assembly-CSharp.dll** and **UnityEngine.dll** (found in `Kerbal Space Program\KSP_x64_Data\Managed`)
- **KRPC.dll** and **KRPC.SpaceCenter.dll** (found in kRPC distribution package or `Kerbal Space Program\GameData\kRPC` if you have the mod installed)

## Creating pull requests

New features are developed in separate branches, and later merged to master and released. Please check if the feature you want to implement isn't already in one of the branches, so that you wouldn't waste your time implementing it again.

Pull requests are merged with squashing or rebasing to keep commit history clean. It is therefore recommended to **create a new branch for each pull request** and not to use `master` branch. It is up to you how you name your new branch (e.g. `issue-4`, `smart-rcs`, etc.).

Before you start coding, please read our [coding conventions](#coding-conventions). After you make changes to the code or add a new functionality, add an entry to the changelog file stating what has been changed with your pull request ID in brackets. If your pull request fixes an existing issue, use the issue ID instead.

Sample changelog entry for PR #4:
```
### Fixed
- ValueError when setting enum properties ([#4](https://github.com/Genhis/KRPC.MechJeb/pull/4))
```

### Fixing bugs

When your pull request fixes a bug, **please attach a code snippet** (ideally in Python) which can reproduce the bug.

### Adding new features

**All added or removed kRPC properties and methods must be reflected in `docs/order.txt` file.** Classes in this file are alphabetically ordered and separated with a blank line. Properties and methods in each class are ordered as they appear in MechJeb windows in the game.

**For new MechJeb modules, please create a new `.tmpl`** (template, formatted as reStructuredText) file in `docs` directory named after the module class (e.g. for `SmartRCS` the name would be `smart-rcs.tmpl`) with the following content (it may need to be adjusted to your module):

```
.. default-domain:: {{ domain.sphinxname }}
.. highlight:: {{ domain.highlight }}
{{ domain.currentmodule("MechJeb") }}
{% import domain.macros as macros with context %}

SmartRCS
========

{{ macros.class(services["MechJeb"].classes["SmartRCS"]) }}
{{ macros.enumeration(services["MechJeb"].enumerations["SmartRCSMode"]) }}
```

Then, add your module to `index.tmpl` file to a suitable category.  Again, entries are alphabetically ordered.

## Coding conventions

These coding conventions are used throughout the project. Please be consistent with existing code and use them in your pull requests.

For your convenience, we created a `.editorconfig` file which takes care of the automatic formatting in Visual Studio. Here are some conventions enforced or suggested by the config file:

- **Use tabs instead of spaces for indentation**
- **Put opening brackets on the same line**
- **Use lambdas and expression-bodied members where possible**
- **Try to minimize number of brackets in expressions** (use your best judgement to leave it readable, contributions won't be rejected because of that)
- For anything which is not mentioned here, **adhere to the conventions used in existing code and general C# coding conventions**

Lastly, thank you for reading all of this and thanks again for contributing. All improvements to the project are welcome, even if incomplete; we can finish them together. If you have any questions regarding this document or this project, feel free to open a new issue.
