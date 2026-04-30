# API System {#api_page}

# How‑To: Using the Ability System API

The AbilitySystemAPI is the central access point for interacting with the Tactics Ability System at runtime.
It provides a clean, unified interface for:

    - Activating abilities
    - Checking resource requirements
    - Applying and removing statuses
    - Applying and removing buffs
    - Processing turn‑based logic
    - Dispatching global events

This chapter explains:

    - What the API does
    - How abilities are executed
    - How supplies are evaluated
    - How statuses and buffs are applied
    - How turn start / turn end logic works
    - All available events
    - Best practices

## 1. Understanding the AbilitySystemAPI

The API is a static class that exposes all high‑level operations.
It is designed to be:

    - Simple — one‑line calls for common actions
    - Consistent — same pattern for buffs, statuses, and abilities
    - Decoupled — units only need to implement IUnit, IStatusContainer, and/or IBuffContainer
    - Event‑driven — UI and VFX should subscribe to API events

The API does not store state.
It orchestrates the systems that do.
## 2. Ability Activation
Checking if an ability can activate
```csharp

bool canActivate = AbilitySystemAPI.IsActivatable(abilityInstance);
```
This checks:

    - All supplies (resource costs)
    - Ability cooldown
    - Any custom conditions you add

Executing an ability
```csharp

AbilitySystemAPI.ExecuteAbility(user, target, abilityInstance);
```
Execution performs:

    - Supply consumption
    - Effect activation
    - Cooldown assignment
    - Event dispatch

Execution Flow
```csharp

foreach (Supply currentSupplies in aInstance.ability.supplies)
    currentSupplies.Use();

foreach (Effect currentEffect in aInstance.ability.plugInEffects)
    currentEffect.Activate(aUser, aTarget);

aInstance.currentCooldown = aInstance.ability.turnCooldown;

AbilityExecuted?.Invoke(aTarget, aInstance);
```
## 3. Status API

Statuses are applied and removed through the API.
Apply a Status
```csharp

AbilitySystemAPI.ApplyStatus(target, statusData);
```
Remove a Status by Instance
```csharp

AbilitySystemAPI.RemoveStatus(target, statusInstance);
```
Remove a Status by Data
```csharp

AbilitySystemAPI.RemoveStatus(target, statusData);
```
Status Events

    - StatusApplied(IStatusContainer, StatusInstance)
    - StatusRemoved(IStatusContainer, StatusInstance)
    - StatusActivated(IStatusContainer, StatusInstance)

Use these for:

    - UI icons
    - VFX
    - Audio
    - Combat logs

## 4. Buff API

Buffs are also applied and removed through the API.
Apply a Buff
```csharp

AbilitySystemAPI.ApplyBuff(target, buffData);
```
Remove a Buff by Instance
```csharp

AbilitySystemAPI.RemoveBuff(target, buffInstance);
```
Remove a Buff by Data
```csharp

AbilitySystemAPI.RemoveBuff(target, buffData);
```
Buff Events

    - BuffApplied(IBuffContainer, BuffInstance)
    - BuffRemoved(IBuffContainer, BuffInstance)

Use these for:

    - Stat UI updates
    - Buff icons
    - Audio cues

## 5. Turn Control

Turn control is one of the most important parts of the API.
It processes:

    - Status activation
    - Status expiration
    - Buff expiration
    - Ability cooldown reduction

Turn Start (Statuses)
```csharp

AbilitySystemAPI.OnTurnStart(IStatusContainer container);
```
This:

    - Calls OnTurnStart on each StatusInstance
    - Dispatches StatusActivated events

Turn Start (Buffs)
```csharp

AbilitySystemAPI.OnTurnStart(IBuffContainer container);
```
This:

    - Reduces Buff durations
    - Removes expired buffs
    - Dispatches BuffRemoved events

Turn Start (Full Unit)
```csharp

AbilitySystemAPI.OnTurnStart(IUnit unit);
```
This calls both:

    - OnTurnStart(IStatusContainer)
    - OnTurnStart(IBuffContainer)

## 6. Turn End
Turn End (Statuses)
```csharp

AbilitySystemAPI.OnTurnEnd(IStatusContainer container);
```
This:

    - Calls OnTurnEnd on each StatusInstance
    - Dispatches StatusActivated events
    - Reduces status durations
    - Removes expired statuses

Turn End (Full Unit)
```csharp

AbilitySystemAPI.OnTurnEnd(IUnit unit);
```
This:

    - Processes status end‑of‑turn logic
    - Reduces ability cooldowns
    - Dispatches AbilityCooldownReduced events

## 7. Ability Cooldown API

Cooldowns are reduced automatically during:
```csharp

AbilitySystemAPI.OnTurnEnd(IUnit unit);
```
When a cooldown decreases:
```csharp

AbilityCooldownReduced?.Invoke(unit, abilityInstance);
```
Use this to:

    - Update ability UI
    - Play ready‑sound effects
    - Trigger animations

## 8. API Events (Full List)
Ability Events
| Event | Description |
| --- | --- |
| **AbilityExecuted(IUnit target, AbilityInstance instance)** | Fired when an ability finishes executing |
| **AbilityCooldownReduced(IUnit unit, AbilityInstance instance)** | Fired when a cooldown decreases |
Status Events
| Event | Description |
| --- | --- |
| **StatusApplied(IStatusContainer, StatusInstance)** | Fired when a status is applied |
| **StatusRemoved(IStatusContainer, StatusInstance)** | Fired when a status expires or is removed |
| **StatusActivated(IStatusContainer, StatusInstance)** | Fired when a status activates on turn start/end |
Buff Events
| Event | Description |
| --- | --- |
| **BuffApplied(IBuffContainer, BuffInstance)** | Fired when a buff is applied |
| **BuffRemoved(IBuffContainer, BuffInstance)** | Fired when a buff expires or is removed |
## 9. Integrating the API With Your Game
Turn‑Based Games

Call:
```csharp

AbilitySystemAPI.OnTurnStart(unit);
AbilitySystemAPI.OnTurnEnd(unit);
```
every time a unit begins or ends its turn.
Real‑Time Games

You can:

    - Call turn events on a timer
    - Call turn events on ability use
    - Call turn events on animation events

The system is flexible.
UI Integration

Subscribe to events:
```csharp

AbilitySystemAPI.StatusApplied += OnStatusApplied;
AbilitySystemAPI.BuffRemoved += OnBuffRemoved;
AbilitySystemAPI.AbilityCooldownReduced += OnCooldownTick;
```
VFX / Audio Integration

Use the same events to:

    - Spawn particles
    - Play sounds
    - Trigger animations

## 10. Best Practices

    - Use the API for all ability, buff, and status interactions
    - Never modify BuffManager or StatusManager directly
    - Use events for UI and VFX
    - Keep turn logic centralized
    - Keep abilities stateless; let the API handle execution
    - Keep buffs and statuses backend‑only
    - Use DataReferences for dynamic resource values

## Thank You For Reading 

### `Suggested Readings`
@ref Installation "How to install the package"

[Credits](https://ryanellisdale.github.io/ModularArchitecture/md__documentation_2_credits.html)

[Third Party Notices](https://ryanellisdale.github.io/ModularArchitecture/md__third_01_party_01_notices.html)

[Licensing](https://ryanellisdale.github.io/ModularArchitecture/md__l_i_c_e_n_s_e.html)
