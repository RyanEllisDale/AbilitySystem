# Effect System {#effect_page}

# How‑To: Effects

Effects are the fundamental action units of the Tactics Ability System Package.
Each Effect represents one atomic gameplay action performed when an ability activates — such as healing, applying a status, pushing a unit, or raising an event.

This chapter explains:

    - What an Effect is
    - How Effects integrate with abilities
    - How to create new Effects
    - How to use parent/target references
    - How to chain multiple Effects
    - Built‑in Effects included in the package
    - How to create custom Effects
    - How to integrate Effects with your own UI, VFX, and audio
    - Best practices

## 1. Understanding Effects

Effects are ScriptableObjects that define a single action performed when an ability is executed.

Every Effect inherits from the base class:
```csharp

public abstract class Effect : ScriptableObject
{
    public abstract void Activate(IUnit parent, IUnit target);
}
```

Key Concepts

    - parent — the unit activating the ability
    - target — the unit receiving the effect
    - Effects are single‑target by design
    - Effects are backend‑only (no UI, VFX, or audio)
    - Effects are executed in order as listed in the ability’s plugInEffects list

Effects do not handle visuals or presentation.
Developers are encouraged to use AbilitySystemAPI events to trigger UI, animations, VFX, and audio.

## 2. Creating an Effect

Create a new Effect via:
Code

Right‑click → Create → Abilities → Effects → Effect : <Type>

This generates a ScriptableObject that can be added to any AbilityData.
Parent and Target

Every Effect receives:

```csharp
Activate(IUnit parent, IUnit target)
```

    - parent is the caster
    - target is the chosen target
    - Both are provided automatically by the ability execution pipeline


## 3. How Effects Integrate With Abilities

Effects are executed inside:
```csharp

foreach (Effect currentEffect in aInstance.ability.plugInEffects)
{
    currentEffect.Activate(aUser, aTarget);
}
```

This means:

    - Effects run in sequence
    - Each Effect is independent
    - Effects can be chained to create complex behaviors
    - Effects do not need to know about each other

Example chain:

    - DamageEffect
    - ApplyStatusEffect
    - PushEffect
    - CallEvent

## 4. Built‑In Effects

| Effect Name | Description |
| --- | --- |
| **ApplyStatusEffect** | Applies a StatusData to the target |
| **ApplyBuff** | Applies a BuffData to the target |
| **HealEffect** | Restores health to the target |
| **PushEffect** | Moves the target by a grid offset |
| **PrintEffect** | Logs a message to the console |
| **CallEvent** | Raises a Modular Architecture GameEvent |

Below are the full details for each.
ApplyStatusEffect
```csharp

public class ApplyStatusEffect : Effect
{
    [SerializeField] private StatusData _statusData;

    public override void Activate(IUnit parent, IUnit target)
    {
        AbilitySystemAPI.ApplyStatus(target, _statusData);
    }
}
```

Applies a StatusData to the target unit.
ApplyBuff
```csharp

public class ApplyBuff : Effect
{
    [SerializeField] private BuffData _buffData;

    public override void Activate(IUnit parent, IUnit target)
    {
        AbilitySystemAPI.ApplyBuff(target, _buffData);
    }
}
```

Applies a BuffData to the target unit.
HealEffect
```csharp

public class HealEffect : Effect
{
    [SerializeField] private DataReference<float> _healAmount;

    public override void Activate(IUnit parent, IUnit target)
    {
        target.UpdateHealth(_healAmount);
    }
}
```

Restores health to the target.
PushEffect
```csharp

public class PushEffect : Effect
{
    [SerializeField] private Vector2Int _pushForce;

    public override void Activate(IUnit parent, IUnit target)
    {
        target.MoveUnit(_pushForce);
    }
}
```

Attempts to move the target by a grid offset.
PrintEffect
```csharp

public class PrintEffect : Effect
{
    [SerializeField] private string _message;

    public override void Activate(IUnit parent, IUnit target)
    {
        Debug.Log(_message);
    }
}
```

Prints a message to the console.
CallEvent
```csharp

public class CallEvent : Effect
{
    [SerializeField] private GameEvent _gameEvent;

    public override void Activate(IUnit parent, IUnit target)
    {
        _gameEvent?.Raise();
    }
}
```

Raises a Modular Architecture GameEvent.
## 5. Creating Custom Effects

Effects are intentionally simple and easy to extend.

Example: Damage Effect

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

Example: Conditional Effect
```csharp
public class ConditionalEffect : Effect
{
    [SerializeField] private ConditionReference _condition;
    [SerializeField] private Effect _effect;

    public override void Activate(IUnit parent, IUnit target)
    {
        if (_condition.value.Evaluate())
            _effect.Activate(parent, target);
    }
}
```

Example: Multi‑Step Effect
```csharp

public class MultiEffect : Effect
{
    [SerializeField] private List<Effect> _effects;

    public override void Activate(IUnit parent, IUnit target)
    {
        foreach (var effect in _effects)
            effect.Activate(parent, target);
    }
}
```

## 6. Chaining Effects

Effects are naturally chainable because:

    - Abilities execute Effects in order
    - Effects do not depend on each other
    - Effects can modify the game state for later Effects

Example chain:

    - DamageEffect
    - ApplyStatusEffect
    - PushEffect
    - CallEvent

This allows for complex ability behaviors without writing monolithic code.
## 7. Integrating Effects With UI, VFX, and Audio

Effects do not trigger visuals, animations, or UI.

This is intentional.

Developers should use AbilitySystemAPI events:

    - AbilityExecuted
    - StatusApplied
    - BuffApplied
    - StatusActivated
    - BuffRemoved

These events allow you to:

    - Play animations
    - Spawn VFX
    - Play audio
    - Update UI
    - Trigger camera shakes
    - Drive AI reactions

This keeps the Effect system clean, backend‑only, and presentation‑agnostic.
## 8. Best Practices

    - Keep Effects small and focused
    - Use multiple Effects instead of one large one
    - Use DataReferences for dynamic values
    - Use API events for presentation logic
    - Avoid hard‑coding visuals or UI inside Effects
    - Use chaining to build complex behaviors
    - Keep Effects stateless whenever possible

## 9. Summary

Effects in the Tactics Ability System Package are:

    - Modular
    - Composable
    - Easy to extend
    - Backend‑only
    - Executed in sequence
    - Single‑target (for now)

They form the foundation of ability behavior and allow developers to build complex gameplay actions through simple, reusable components.

## Thank You For Reading 

### `Suggested Readings`
@ref Installation "How to install the package"

[Credits](https://ryanellisdale.github.io/ModularArchitecture/md__documentation_2_credits.html)

[Third Party Notices](https://ryanellisdale.github.io/ModularArchitecture/md__third_01_party_01_notices.html)

[Licensing](https://ryanellisdale.github.io/ModularArchitecture/md__l_i_c_e_n_s_e.html)
