@mainpage Tactics Ability System

## Overview

The Tactics Ability System Package is a modular, data‑driven backend framework for creating abilities, buffs, and status effects in turn‑based tactics games. It provides a flexible foundation built around composable ScriptableObject components, allowing developers to design complex gameplay behaviors without rewriting boilerplate logic. This package focuses entirely on gameplay logic.
It does not include UI, animations, VFX, or targeting visuals — instead, it exposes clean event hooks so developers can connect their own presentation layer. Designed for solo developers and small teams, the system aims to accelerate prototyping and production by offering a robust, extensible ability architecture that integrates cleanly with existing unit, grid, and turn systems.

### Features

- Modular Ability Composition ( Scriptable Objects + Conditional Building Blocks )
- Data‑Driven Architecture ( Serialized Data Classes and Instancing )
- Effect System ( Single-target abilities that can be composited to make new abilities )
- Resource & Supply System ( Generic safe cost system for tactics' resources )
- Status Effects ( Status combinations, expendable interfaces and functionality )
- Buff System ( Debuffs, Active Buffs, Data-driven )
- Turn Integration Hooks ( Entry Points )
- Event‑Driven Architecture ( API Exposure )
- Backend‑Only by Design
- Custom property drawers / Editor Scripts
- Samples and Demos
- MIT Licensed

Turn‑based tactics games rely heavily on abilities, buffs, and statuses — but building a flexible, scalable ability system from scratch is time‑consuming and error‑prone. I built this package from the base of a Modular Architectural package in hopes that It can serve as a clean modular foundational framework for your projects. 

- A clean, modular foundation
- A data‑driven workflow
- Extensible components
- Clear integration points
- Minimal assumptions about your game

## Package Structure 

I created this package from the base of an earlier project I had made called PackageTemplate which can be found at : https://github.com/RyanEllisDale/PackageTemplate in both of these packages I have attempted to follow the established Unity and community standards. 
The resources I used for my packaging layout and code standards can be found at : 

- Unity’s Official Package Layout: https://docs.unity3d.com/6000.3/Documentation/Manual/cus-layout.html
- Unity’s Code Standard Tips: https://unity.com/how-to/naming-and-code-style-tips-c-scripting-unity
- uvasgd’s Unity Documentation Tips: https://uvasgd.github.io/sgd-docs/unity/documentation.html

The changelog follows Unity and *Keep a ChangeLog* conventions which can be found at : 

- Unity Changelog Guidelines: https://docs.unity3d.com/6000.3/Documentation/Manual/cus-changelog.html  
- Keep a Changelog: https://keepachangelog.com/en/1.1.0/

Throughout the whole project, Semantic versioning is used, and a generally consistent Markdown styling is kept, 
A guide to both can be found at : 

- Semantic Versioning: https://semver.org/spec/v2.0.0.html  
- Markdown Syntax Guide: https://markdownguide.offshoot.io/basic-syntax/ 

## Documentation 

The documentation for this package is generated using Doxygen, styled with the doxygen‑awesome theme, and built directly from the XML comments within the codebase. This ensures that the documentation stays accurate, up‑to‑date, and closely aligned with the implementation. It also allows contributors to improve the docs simply by enhancing the inline comments in the source files. If you find any errors in the codebase feel free to toy around as much as you want and send me your findings, I'd love to hear from you.

- Doxygen : https://www.doxygen.nl/
- doxygen-awesome-css : https://github.com/jothepro/doxygen-awesome-css 

## Thank You For Reading 

### `Suggested Readings`
@ref Installation "How to install the package"

@ref How-To-Use "How to use the package"

[Credits](https://ryanellisdale.github.io/ModularArchitecture/md__documentation_2_credits.html)

[Third Party Notices](https://ryanellisdale.github.io/ModularArchitecture/md__third_01_party_01_notices.html)

[Licensing](https://ryanellisdale.github.io/ModularArchitecture/md__l_i_c_e_n_s_e.html)


@page installation

The Tactics Ability System Package is distributed as a Unity package and requires the Modular Architecture framework to function.
Follow the steps below to install both packages into your project.

# Requirements : 

- Unity 2019.4+ (developed on 2022.3.1, but compatible with older versions)
- **Modular Architecture Package (must be installed first)**

## Modular Architecture ( Dependency / Base )

The Tactics Ability System Package is built on top of the Modular Architecture framework.
You must install it before importing this package.

You can find the full details on installation here : https://ryanellisdale.github.io/ModularArchitecture/_installation.html 
Quick Summary Options : 

### Option A — Install via Git URL (Unity Package Manager)

1. Open **Unity**
2. Go to **Window → Package Manager**
3. Click the **+** button
4. Select **“Add package from Git URL…”**
5. Use: https://github.com/RyanEllisDale/ModularArchitecture.git
6. Press Add

Unity will download and install the package automatically.

### Option B — Manual Installation

1. Download the latest release from GitHub: https://github.com/RyanEllisDale/ModularArchitecture
2. Extract the `.zip` file.
3. Drag the extracted folder into your Unity project’s `Package/` directory.
4. Unity will automatically import the package and compile the scripts.

## Installing the Tactics Ability System Package : 

### Package Manager 

You can access the Package Manager by opening your Unity project, selecting **Window** from the top menu bar, and choosing **Package Manager**. In the top‑left corner of the Package Manager window, the Add button allows you to install external packages.

This package supports two Package Manager installation methods:

- **Add package from disk**  
- **Add package from Git URL**

<details>
<summary>Resources:</summary>
- https://docs.unity3d.com/6000.4/Documentation/Manual/upm-ui.html  
- https://docs.unity3d.com/6000.4/Documentation/Manual/PackagesList.html
</details>

### Package Manager Disk

To install from disk, download and extract the package, place it anywhere on your machine, then select **Add package from disk** and point Unity to the folder. Packages installed this way are *local* and not stored inside the project.  
If the folder is moved or deleted, Unity will lose the reference and you’ll need to re‑add it.

<details>
<summary>Resources:</summary>
- https://docs.unity3d.com/2020.1/Documentation/Manual/upm-ui-local.html
</details>

### Package Manager — Git

This is the simplest way to add the package to your project.  
Open the Package Manager, click the **Add** button, and select **Add package from Git URL**. Unity will prompt you for a valid Git URL. <br/>
Use : https://github.com/RyanEllisDale/AbilitySystem.git


You can also find this URL on the project’s GitHub page:  
https://github.com/RyanEllisDale/AbilitySystem

Installing the package this way prevents editing the package directly inside your project, but allows you to update it at any time through the Package Manager.

<details>
<summary>Resources:</summary>
- https://docs.unity3d.com/2020.1/Documentation/Manual/upm-ui-giturl.html
- https://github.com/RyanEllisDale/AbilitySystem.git
</details>

### Manual Install

Manual installation is also possible if the Package Manager is unavailable or not preferred.  
To install the package manually, download and extract it, then place the extracted folder inside your project's `Packages/` directory (located in the project root). If the `Packages/` folder does not exist, create it manually.

Ensure that you copy only the package’s root folder — the one that directly contains the `package.json` file.

## Verification

After installing both, you should be able to find both `Modular Architecture` and `Ability System` in your project's browser in the package/ section. If not, check to see if you have packages enabled in your project browser, and restart Unity. 

## Thank You For Reading 

### `Suggested Readings`

@ref How-To-Use "How to use the package"

[Credits](https://ryanellisdale.github.io/ModularArchitecture/md__documentation_2_credits.html)

[Third Party Notices](https://ryanellisdale.github.io/ModularArchitecture/md__third_01_party_01_notices.html)

[Licensing](https://ryanellisdale.github.io/ModularArchitecture/md__l_i_c_e_n_s_e.html)
