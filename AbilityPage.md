# Ability System {#ability_page}

# How‑To: Abilities

Abilities are the core building blocks of the Tactics Ability System Package.
They define what a unit can do in gameplay — dealing damage, applying statuses, healing, buffing, and more.

This chapter explains:

    - What an AbilityData is
    - What an AbilityInstance is
    - How to create abilities in the inspector
    - How to assign abilities to units
    - How to execute abilities in gameplay
    - How cooldowns, conditions, supplies, and effects work together
    - How to integrate abilities into your turn system

## 1. Understanding Abilities

Abilities in this system are split into two layers:
AbilityData (Permanent Data)

A ScriptableObject that defines:

    - Effects
    - Conditions
    - Supplies (resource costs)
    - Cooldown
    - Range
    - Categorization
    - Audio clips
    - Damage type

This is the design-time representation of an ability.
AbilityInstance (Runtime State)

A lightweight class that stores:

    - A reference to the AbilityData
    - The current cooldown

Each unit holds a list of AbilityInstances.

## 2. Creating an Ability

Create a new ability via:
Code

Right‑click → Create → Abilities → Create New Ability

This generates an AbilityData asset.

### Ability Fields
- id A unique string identifier for referencing the ability.
- audioClips Optional audio clips developers can use when the ability is executed.
- plugInEffects A list of Effect ScriptableObjects that define what the ability does.
- Effects are executed in order when the ability is activated.
- damageType / abilityCategory User-defined enums for classification.
- range a simple integer value representing maximum targeting distance.

Important:  

    The system does not enforce range.

    Developers must implement their own targeting logic and read this value manually.

These are not enforced by the system and are intended for:

    UI

    AI

    Damage pipelines

    Filtering abilities

- conditions A list of ConditionReference objects.
All conditions must evaluate to true for the ability to be usable.

- supplies A list of Supply objects representing resource costs.
All supplies must be affordable before the ability can be used.

- turnCooldown A DataReference<int> defining how many turns must pass before reuse.

## 3. Creating Ability Instances

Abilities are not used directly.
Units use AbilityInstances, which store runtime state such as cooldown.

You can create an instance using the constructor:
```csharp

var instance = new AbilityInstance(fireballData);
```

### Recommended Workflow

Because units differ between projects, the system does not enforce how abilities are stored.

You may:

#### A) Assign AbilityData in the inspector and create instances at runtime
```csharp

[SerializeField] private List<AbilityData> startingAbilities;

private void Awake()
{
    foreach (var data in startingAbilities)
        _abilityInstances.Add(new AbilityInstance(data));
}

```

#### B) Create and assign abilities entirely in code
```csharp

_abilityInstances.Add(new AbilityInstance(fireball));
_abilityInstances.Add(new AbilityInstance(heal));

```

#### C) Mix both approaches
This flexibility is intentional.

## 4. Storing Abilities on Units

Your unit class must implement IUnit, which requires:
```csharp
public List<AbilityInstance> abilityInstances { get; }
```

A typical implementation:
```csharp
[SerializeField] private List<AbilityInstance> _abilityInstances = new();
public List<AbilityInstance> abilityInstances => _abilityInstances;
```
This allows the system to:

    - Reduce cooldowns
    - Execute abilities
    - Query ability availability

## 5. Executing an Ability

Abilities are executed through the AbilitySystemAPI.
### Basic Example
```csharp

AbilitySystemAPI.ExecuteAbility(user, target, abilityInstance);
```
### Checking if an Ability Can Be Used
```csharp

if (AbilitySystemAPI.IsActivatable(abilityInstance))
{
    AbilitySystemAPI.ExecuteAbility(user, target, abilityInstance);
}
```
### What Happens During Execution

When an ability is executed:

    - Conditions are checked
    - Supplies are evaluated
    - Supplies are consumed
    - Effects are activated in order
    - Cooldown is applied
    - AbilityExecuted event is fired

This is all handled internally by:
```csharp

AbilitySystemAPI.ExecuteAbility(...)
```
## 6. Integrating Abilities Into Gameplay
### Turn Start / Turn End

Your turn system must call:
```csharp

AbilitySystemAPI.OnTurnStart(unit);
AbilitySystemAPI.OnTurnEnd(unit);
```
This handles:

    - Status ticking
    - Buff ticking
    - Cooldown reduction
    - Status activation events

### Targeting

The system does not include targeting logic.

Developers must:

     - Determine valid targets
    - Check range manually
    - Pass the chosen target into ExecuteAbility

Example:
```csharp

if (distance <= abilityInstance.ability.range)
{
    AbilitySystemAPI.ExecuteAbility(user, target, abilityInstance);
}
```
## 7. Example: Creating a Simple Ability
### Step 1 — Create an Effect
```csharp

[CreateAssetMenu(menuName="Abilities/Effects/Damage")]
public class DamageEffect : Effect
{
    [SerializeField] private float amount;

    public override void Activate(IUnit parent, IUnit target)
    {
        target.UpdateHealth(-amount);
    }
}
```
### Step 2 — Create an AbilityData

    - Add the DamageEffect to plugInEffects
    - Set a cooldown
    - Add a range
    - Add supplies (optional)
    - Add conditions (optional)

### Step 3 — Assign Ability to a Unit
```csharp

_abilityInstances.Add(new AbilityInstance(fireballData));
```
### Step 4 — Execute It
```csharp

AbilitySystemAPI.ExecuteAbility(user, target, fireballInstance);
```
## 8. Best Practices

    - Keep abilities small and composable
    - Use multiple Effects instead of one large one
    - Use Conditions to restrict ability usage
    - Use Supplies to enforce resource costs
    - Use DataReferences for dynamic cooldowns or scaling values
    - Keep targeting logic outside the ability system
    - Use events to trigger UI, VFX, and SFX

## 9. Summary

Abilities in the Tactics Ability System Package are:

    - Modular
    - Data‑driven
    - Extensible
    - Backend‑only
    - Designed for turn‑based tactics games

They combine Effects, Conditions, Supplies, and Cooldowns into a flexible system that developers can shape to fit their own gameplay.

## Thank You For Reading 

### `Suggested Readings`
@ref Installation "How to install the package"

[Credits](https://ryanellisdale.github.io/ModularArchitecture/md__documentation_2_credits.html)

[Third Party Notices](https://ryanellisdale.github.io/ModularArchitecture/md__third_01_party_01_notices.html)

[Licensing](https://ryanellisdale.github.io/ModularArchitecture/md__l_i_c_e_n_s_e.html)
