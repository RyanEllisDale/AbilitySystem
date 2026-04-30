# Status System {#status_page}

# How‑To: Status Effects

Statuses are long‑lasting effects applied to units during gameplay.
They can heal over time, deal damage over time, apply buffs, combine into new effects, or trigger logic at the start or end of each turn.

This chapter explains:

    - What a StatusData is
    - What a StatusInstance is
    - How statuses are applied and removed
    - How turn‑based activation works
    - How status mixing works
    - How to create custom statuses
    - How to use built‑in statuses
    - How to integrate statuses into your turn system
    - How to connect statuses to UI, VFX, and audio
    - Best practices

## 1. Understanding Statuses

Statuses in this system are split into two layers:
StatusData (Permanent Data)

A ScriptableObject that defines:

    - id
    - description
    - duration
    - components (for mixing)
    - CreateInstance()

This is the design‑time representation of a status.
StatusInstance (Runtime State)

A lightweight class that stores:

    - A reference to the StatusData
    - The current duration
    - Activation hooks:

        - OnApply
        - OnRemove
        - OnTurnStart
        - OnTurnEnd

Each unit holds a list of StatusInstances through its StatusManager.
Key Concepts

    - Statuses are turn‑based
    - Statuses are single‑target
    - Statuses are backend‑only
    - Statuses can mix into new statuses
    - Statuses can be composite (multiple sub‑effects)
    - Statuses can apply buffs

Statuses do not handle UI, VFX, or audio.
Developers should use AbilitySystemAPI events to connect statuses to presentation.
## 2. StatusData

A StatusData defines the permanent configuration of a status.
```csharp

public abstract class StatusData : ScriptableObject
{
    public string id;
    [Multiline] public string description;
    public int duration = 1;
    public List<StatusData> components = new List<StatusData>();

    public abstract StatusInstance CreateInstance();
}
```

Fields

    - id  A unique identifier for referencing the status.
    - description  A multiline description for UI or debugging.
    - duration  How many turns the status lasts once applied.
    - components  A flattened list of sub‑effects used for mixing logic.

Creating a StatusData :

Assets → Create → Abilities → Status → Create New Status
This produces a ready‑to‑edit StatusData + StatusInstance pair.

## 3. StatusInstance

A StatusInstance stores runtime state and implements turn‑based behavior.
```csharp

public class StatusInstance : IEquatable<StatusInstance>
{
    public StatusData data;
    public int currentDuration = 0;

    public virtual bool OnTurnStart(IStatusContainer target) { return false; }
    public virtual bool OnTurnEnd(IStatusContainer target) { return false; }
    public virtual void OnApply(IStatusContainer target) { }
    public virtual void OnRemove(IStatusContainer target) { }
}
```
Activation Hooks

    - OnApply  Called when the status is first applied.
    - OnRemove  Called when the status expires or is removed.
    - OnTurnStart  Called at the start of each turn.
    - OnTurnEnd  Called at the end of each turn.


**IEquatable** : Two StatusInstances are considered equal if they share the same StatusData.

## 4. StatusManager

Each unit implementing IStatusContainer owns a StatusManager.
```csharp

public class StatusManager
{
    public List<StatusInstance> activeEffects = new();
    public IStatusContainer unitOwner => _owner as IStatusContainer;
}
```
Responsibilities

    - Applying statuses
    - Removing statuses
    - Mixing statuses
    - Preventing component conflicts
    - Triggering OnApply / OnRemove
    - Managing active status instances

Applying a Status
```csharp

StatusInstance ApplyEffect(StatusData newEffect)
```
This method:

    - Prevents adding component effects of active combined statuses
    - Attempts to mix the new effect with active effects
    - Attempts to mix pending effects with each other
    - Applies the final combined effect
    - Calls OnApply

Removing a Status
```csharp

RemoveEffect(StatusData effect)
RemoveEffect(StatusInstance instance)
```
Both remove the status and call OnRemove.
## 5. Status Mixing

Statuses can combine into new statuses using MixRules.
MixRule
```csharp

public class MixRule
{
    public StatusData effectA;
    public StatusData effectB;
    public StatusData result;
}
```
StatusEffectMixingDatabase
A ScriptableObject containing all mix rules.
Right‑click → Create → Abilities → Status → Mixing Database
StatusEffectMixer Singleton

```csharp
StatusEffectMixer.instance.TryMix(a, b)
```
How Mixing Works

When applying a new status:

    - Check if it mixes with any active status
    - If so, remove the active status
    - Replace both with the mixed result
    - Repeat until no more mixes occur
    - Apply the final result

Component Lists

MixRules automatically generate flattened component lists so the system knows:

    - Which statuses are “ingredients”
    - Which statuses should not be re‑applied
    - How to prevent circular mixes

## 6. Built‑In Statuses

The package includes several ready‑to‑use statuses.
Built‑In Status Summary
| Status Name | Description |
| --- | --- |
| **Regeneration** | Restores health at the start of each turn |
| **DamageOverTime** | Deals damage at the end of each turn |
| **Composite** | A status made of multiple sub‑statuses |
| **BuffStatus** | Applies a BuffData as a status |

Restores health at the start of each turn.
```csharp

public override bool OnTurnStart(IStatusContainer target)
{
    target.ApplyStatusDamage(-_regenData.healthPerTurn);
    return true;
}
```
Damage‑Over‑Time

Deals damage at the end of each turn.
```csharp

public override bool OnTurnEnd(IStatusContainer target)
{
    target.ApplyStatusDamage(_dotData.damagePerTurn);
    return true;
}
```
Composite Status

A status that contains multiple sub‑statuses.

    - Forwards OnTurnStart
    - Forwards OnTurnEnd
    - Forwards OnApply
    - Forwards OnRemove

BuffStatus

Applies a BuffData as a status.
```csharp

AbilitySystemAPI.ApplyBuff(statsContainer, _buffStatusData.data);
```
## 7. Creating Custom Statuses

Use the script template:

Assets → Create → Abilities → Status → Create New Status

This generates:

    - <New Status>StatusData
    - <New Status>StatusInstance

Example: Stun
```csharp

public override bool OnApply(IStatusContainer target)
{
    // Disable movement or actions
    return true;
}
```
Example: Shield
```csharp

public override bool OnApply(IStatusContainer target)
{
    // Add temporary HP
}
```
Example: Slow
```csharp

public override bool OnTurnStart(IStatusContainer target)
{
    // Reduce movement range
    return true;
}
```
## 8. Integrating Statuses Into Gameplay
Applying a Status
```csharp

AbilitySystemAPI.ApplyStatus(unit, statusData);
```
Removing a Status
```csharp

AbilitySystemAPI.RemoveStatus(unit, statusData);
```
Turn Start / Turn End

Your turn system must call:
```csharp

AbilitySystemAPI.OnTurnStart(unit);
AbilitySystemAPI.OnTurnEnd(unit);
```
This handles:

    - Status ticking
    - Status activation
    - Duration reduction
    - Status expiration

UI, VFX, and Audio

Statuses do not handle presentation.

Use events:

    - StatusApplied
    - StatusRemoved
    - StatusActivated

These allow you to:

    - Show icons
    - Play animations
    - Spawn VFX
    - Play audio
    - Update UI

## 9. Best Practices

    - Keep statuses small and focused
    - Use mixing for emergent gameplay
    - Use Composite for multi‑effect statuses
    - Avoid putting UI/VFX logic inside statuses
    - Use events for presentation
    - Keep StatusData immutable
    - Use StatusInstance for runtime state only
    - Use components to prevent re‑applying ingredient statuses

## 10. Summary

Statuses in the Tactics Ability System Package are:

    - Modular
    - Turn‑based
    - Extensible
    - Mixable
    - Backend‑only
    - Designed for tactics games


## Thank You For Reading 

### `Suggested Readings`
@ref Installation "How to install the package"

[Credits](https://ryanellisdale.github.io/ModularArchitecture/md__documentation_2_credits.html)

[Third Party Notices](https://ryanellisdale.github.io/ModularArchitecture/md__third_01_party_01_notices.html)

[Licensing](https://ryanellisdale.github.io/ModularArchitecture/md__l_i_c_e_n_s_e.html)
