@mainpage Modular Architecture - Home

## Overview

Modular architecture is a small Unity package designed to provide structured re-usable data-driven pieces of code that can be used and expanded upon in your own Unity projects. Unity's codebase is inherently quite tightly coupled with the reign of Mono Behaviour and whilst it works and the workflow is very simple it has shortcomings. Throughout the years more and more Unity developers have been finding success with encorporating more modular and composite based designs into their Unity work-flows. 

Some Excellent Resources and the inspiration behind this package : 
- [Game Architecture with Scriptable Objects](https://www.youtube.com/watch?v=raQ3iHhE_Kk&t=1530s)
- [Overthrowing the MonoBehaviour Tyranny](https://www.youtube.com/watch?v=6vmRwLYWNRo )

### The Benefits to Modularity In Unity

- Fewer dependencies — MonoBehaviour‑heavy setups often create tangled scene references; modular systems stay clean and isolated
- Safer iteration — swapping or updating modules (input, UI, audio, etc.) won’t impact unrelated features
- Easier testing — data‑driven modules avoid scene‑reset issues and reduce dependency headaches
- Better organization — Unity’s asset workflow pairs naturally with ScriptableObjects and persistent data
- Memory efficiency — multiple objects can read from a single data asset, avoiding unnecessary duplication

### Features

- Create-able data assets for IComparable Data Types 
- Modular data game event system
- Modular data conditions system ( return a `operator` b )  
- Extendable Enums (Cory Koseck) and Extendable Enum Data Sets
- Custom property drawers
- Samples and Demos

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


@page Installation

The modular architecture package can be added to your Unity project in three ways. The first two use the Package Manager, while the third is a manual fallback if the Package Manager is unavailable.

## Package Manager 

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
Use : https://github.com/RyanEllisDale/ModularArchitecture.git


You can also find this URL on the project’s GitHub page:  
https://github.com/RyanEllisDale/ModularArchitecture

Installing the package this way prevents editing the package directly inside your project, but allows you to update it at any time through the Package Manager.

<details>
<summary>Resources:</summary>
- https://docs.unity3d.com/2020.1/Documentation/Manual/upm-ui-giturl.html
- https://github.com/RyanEllisDale/ModularArchitecture.git
- https://github.com/RyanEllisDale/ModularArchitecture
</details>

## Manual Install

Manual installation is also possible if the Package Manager is unavailable or not preferred.  
To install the package manually, download and extract it, then place the extracted folder inside your project's `Packages/` directory (located in the project root). If the `Packages/` folder does not exist, create it manually.

Ensure that you copy only the package’s root folder — the one that directly contains the `package.json` file.


## Thank You For Reading 

### `Suggested Readings`

@ref How-To-Use "How to use the package"

[Credits](https://ryanellisdale.github.io/ModularArchitecture/md__documentation_2_credits.html)

[Third Party Notices](https://ryanellisdale.github.io/ModularArchitecture/md__third_01_party_01_notices.html)

[Licensing](https://ryanellisdale.github.io/ModularArchitecture/md__l_i_c_e_n_s_e.html)

@page How-To-Use 

Modular architecture comes with 4 main features :
- Data Variables / References
- Modular Conditions
- Extendable Enums
- Events

In this page I will briefly talk about how these can be used. 
It is reccomended you have been through the Installation Guide ( @ref Installation "Here" ) before reading through this. 

## Samples

Each of the 4 Main features of Modular architecture has it's own sample piece / demostration that shows off the functionality / theory behind it. These samples can be installed by opening up the package-manager window in your Unity project, finding the Modular Architecture package, moving to the samples view and importing the relevant sample(s) you want. All the samples are designed to be small in scope and easy to follow along with.

## Conditions

The `Condition` system allows you to create simple logic checks without writing code by comparing two values using a chosen operator.

### Setup

1. Choose a **Value Type** (Int, Float, String, or Bool).
2. Assign a **Subject** (the current value).
3. Assign a **Target** (the value to compare against).
4. Select a **Comparison** type (e.g. Equal, Greater, Less).

### Example

To check if a player’s health is low:

- Type: `Float`
- Subject: `PlayerHealth` (Data Reference)
- Target: `30`
- Comparison: `Less`

This represents: `PlayerHealth < 30`

### Usage

Call: condition.Evaluate()
This returns `true` or `false` depending on whether the condition is met.

### Common Use Cases

- Triggering events (e.g. player health is low)
- Unlocking actions (e.g. score ≥ required amount)
- AI decision making
- UI updates (e.g. show warning states)

Conditions are fully data-driven, meaning they can be configured in the Inspector and reused across systems without modifying code.

## Data

## How to Use Data References

The data system allows you to use either **constant values** or **shared data assets** interchangeably, without changing your code.

### Setup

Choose how the value is provided:
   - **Constant** → uses a fixed value set in the Inspector
   - **Variable** → references a `DataContainer` asset

### Example

```csharp
public FloatReference speed;
```

Set `Use Constant` = true → speed = 5
OR
Set `Use Constant` = false → assign a `Float Variable` asset

### Usage

References / Variables are just data values, you can reference them directly with `[Reference].value` or implicity `=[Reference]`,
These values are also modifiable even when in the data asset ( data variable ).

## Events

## How to Use Game Events

The event system allows different parts of your game to communicate without being directly connected.

A `GameEvent` is a reusable asset that can be triggered from anywhere, and all registered listeners will respond automatically.

### Setup

1. Create a `GameEvent` asset (e.g. `PlayerDied`, `ButtonPressed`)
2. Add a `GameEventListener` to any object that should react
3. Define a response using a `UnityEvent` or method call
4. Link the listener to the event

### Raising an Event

Trigger the event from code or inspector:
gameEvent.Raise();

This will notify all subscribed listeners.

### Listening to an Event

A listener automatically subscribes when enabled:

- OnEnable → subscribes to the event  
- OnDisable → unsubscribes from the event  

When the event is raised, the listener executes its response.

### Example

- `PlayerDied` → show game over UI, play sound, pause game  
- `DoorOpened` → play animation, unlock area  
- `ButtonPressed` → start level or trigger action  

### Usage

Game Events are used to:

- Decouple systems (UI, gameplay, audio, etc.)
- Trigger multiple reactions from a single action
- Remove direct references between objects
- Build flexible event-driven gameplay flow

### Summary

Game Events act as global signals that systems can react to. Instead of calling each other directly, objects simply respond when an event is raised, making the project more modular and scalable.

## Enums

## How to Use Extendable Enums

The enum system allows traditional C# enums to be represented as ScriptableObject assets, making them more flexible and designer-friendly.
Instead of being hardcoded in code, enum values can be created, referenced, and swapped like data.

### Setup

Right click in your project asset browser, navigate to the create menu, in there you will find Modular Architecture>Enums>Create New Enum.
This is how we define new enums, when you've created an enum it will be compiled into that same context asset browser. 


This behaves like a normal enum but is stored as data.

### Example Use Cases

- Game states (Menu, Gameplay, Paused)
- Enemy types (Melee, Ranged, Boss)
- Item categories (Weapon, Armor, Consumable)
- AI modes or behavior types

### Benefits

- Enums can be extended without changing code
- Designers can add or modify values through assets
- Systems can reference enum data instead of hardcoded values
- Works well with data-driven architectures (conditions, events, references)

## Thank You For Reading 

### `Suggested Readings`
@ref Installation "How to install the package"

[Credits](https://ryanellisdale.github.io/ModularArchitecture/md__documentation_2_credits.html)

[Third Party Notices](https://ryanellisdale.github.io/ModularArchitecture/md__third_01_party_01_notices.html)

[Licensing](https://ryanellisdale.github.io/ModularArchitecture/md__l_i_c_e_n_s_e.html)