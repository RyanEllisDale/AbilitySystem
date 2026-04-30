# Buff System {#buff_page}

# How‑To: Buffs

Buffs are temporary stat‑modifying effects applied to units.
They increase or decrease numerical values such as attack, defense, speed, or any custom stat your game defines.

This chapter explains:

    - What a BuffData is
    - What a BuffInstance is
    - How stat modifiers work
    - How BuffManager stores and applies buffs
    - How buffs tick and expire each turn
    - How to apply and remove buffs via the AbilitySystemAPI
    - How to integrate buffs with units and stats
    - How to create custom buffs
    - Best practices

## 1. Understanding Buffs

Buffs in this system are:

    - Data‑driven — defined as ScriptableObjects
    - Runtime‑tracked — via BuffInstance
    - Stat‑focused — they only modify stats on IBuffContainer
    - Turn‑based — they have a duration in turns
    - Backend‑only — they do not handle UI, VFX, or audio directly

Buffs are the “stat layer” of the ability system.
Statuses are the “effect layer”.
Statuses can also apply buffs using BuffStatusData.
## 2. BuffData

BuffData defines the permanent configuration of a buff.
Creating a Buff : Right‑click → Create → Abilities → Buffs → Create New Buff
```csharp

[CreateAssetMenu(fileName = "New Buff",  menuName ="Abilities/Buffs/Create New Buff", order = 0)]
public class BuffData : ScriptableObject
{
    public string id;
    [Multiline] public string description;

    public int duration = 1;
    [SerializeField] private StatModifier[] _modifiers;
    public StatModifier[] modifiers => _modifiers;
}
```

Fields

    - id  A unique identifier for referencing the buff.
    - description  A multiline description for UI or debugging.
    - duration  How many turns the buff remains active once applied.
    - modifiers  An array of StatModifier rules that define how this buff affects stats.



Configure:

    - id
    - description
    - duration
    - modifiers (stat ID, value, type)

## 3. Stat Modifiers

Stat modifiers define how a buff changes stats on an IBuffContainer.
```csharp

public enum StatModifierType : byte
{ 
    Add,
    Multiply
}

[System.Serializable]
public struct StatModifier
{
    public string targetStatID; // targetStatID is a string key that must match whatever your IBuffContainer uses internally to identify stats.
    public float modifierValue;
    public StatModifierType modifierType;
}
```
Modifier Types

    - Add  Adds (or subtracts) a flat value to the stat.
    - Multiply  Multiplies (or divides) the stat by a value.

## 4. BuffInstance

BuffInstance is the runtime representation of a BuffData.
```csharp

[System.Serializable]   
public class BuffInstance : IEquatable<BuffInstance>
{
    [SerializeField] private BuffData _data;
    public BuffData data => _data;
    public int currentDuration = 1;

    public BuffInstance(BuffData data)
    {
        this._data = data;
        this.currentDuration = _data.duration;
    }
}
```
Responsibilities

    - Stores a reference to BuffData
    - Tracks remaining duration
    - Applies stat changes on apply
    - Reverses stat changes on expire

Applying Modifiers
```csharp

public void OnApply(IBuffContainer target)
{
    foreach (StatModifier currentModifier in _data.modifiers)
    { 
        target.ModifyStat(currentModifier.targetStatID,
                          currentModifier.modifierValue,
                          currentModifier.modifierType);
    }
}
```
Expiring Modifiers
```csharp

public void OnExpire(IBuffContainer target)
{
    foreach(StatModifier currentModifier in _data.modifiers)
    {
        float removalValue = currentModifier.modifierType == StatModifierType.Add
            ? -currentModifier.modifierValue
            : 1f / currentModifier.modifierValue;

        target.ModifyStat(currentModifier.targetStatID,
                          removalValue,
                          currentModifier.modifierType);
    }
}
```
BuffInstance uses the inverse of the original modification to restore stats when the buff expires.
## 5. IBuffContainer

Any object that wants to receive buffs must implement IBuffContainer.
```csharp

public interface IBuffContainer
{
    public BuffManager buffManager { get; }

    float? GetStat(string statID);
    float? ModifyStat(string statID, float modifyAmount, StatModifierType modifyType);
}
```
Responsibilities

    - Expose a BuffManager
    - Provide access to stats via string IDs
    - Implement stat modification logic

Typical usage:

    - Store stats in fields or a dictionary
    - Map statID to the correct stat
    - Apply Add or Multiply logic in ModifyStat

## 6. BuffManager

BuffManager manages all active BuffInstances on a single IBuffContainer.
```csharp

[System.Serializable]
public class BuffManager
{
    private IBuffContainer _buffContainer;
    [SerializeField] private List<BuffInstance> _buffInstances = new();
    public List<BuffInstance> buffInstances => _buffInstances;
}
```
Adding a Buff
```csharp

public BuffInstance AddBuff(BuffData buffData)
{
    BuffInstance buffInstance = new BuffInstance(buffData);

    _buffInstances.Add(buffInstance);
    buffInstance.OnApply(_buffContainer);
            
    return buffInstance;
}
```
Removing a Buff by Instance
```csharp

public BuffInstance RemoveBuff(BuffInstance buffInstance)
{
    if (_buffInstances.Contains(buffInstance) == false)
        return null;

    _buffInstances.Remove(buffInstance);
    buffInstance.OnExpire(_buffContainer);

    return buffInstance;
}
```
Removing a Buff by Data
```csharp

public BuffInstance RemoveBuff(BuffData buffData)
{
    BuffInstance buffInstance = _buffInstances.Find(b => b.data == buffData);

    if (buffInstances == null)
        return null;
            
    _buffInstances.Remove(buffInstance);
    buffInstance.OnExpire(_buffContainer);

    return buffInstance;
}
```
BuffManager does not tick durations — that is handled by the AbilitySystemAPI turn logic.
## 7. Turn‑Based Buff Ticking

Buff durations are reduced during OnTurnStart.
```csharp

public static void OnTurnStart(IBuffContainer aCurrentStatsContainer)
{
    List<BuffInstance> Instances = new List<BuffInstance>(aCurrentStatsContainer.buffManager.buffInstances);

    foreach (BuffInstance CurrentInstance in Instances)
    {
        CurrentInstance.currentDuration--;

        if (CurrentInstance.currentDuration <= 0)
        {
            RemoveBuff(aCurrentStatsContainer, CurrentInstance);
        }
    }
}
```
Summary of Turn Behavior

    - Buffs tick at the start of the unit’s turn
    - Duration decreases by 1
    - When duration reaches 0:
        - Buff is removed
        - Stat modifiers are reversed
        - BuffRemoved event fires

## 8. Applying and Removing Buffs via the API
Applying a Buff
```csharp

AbilitySystemAPI.ApplyBuff(target, buffData);
```
Removing a Buff by Instance
```csharp

AbilitySystemAPI.RemoveBuff(target, buffInstance);
```
Removing a Buff by Data
```csharp

AbilitySystemAPI.RemoveBuff(target, buffData);
```
Buff Events
```csharp

public static event Action<IBuffContainer, BuffInstance> BuffApplied;
public static event Action<IBuffContainer, BuffInstance> BuffRemoved;
```
Use these to:

    - Update UI
    - Play VFX/SFX
    - Trigger animations

## 9. Integrating Buffs With Units

A typical unit that supports buffs:
```csharp

public class UnitScript : MonoBehaviour, IBuffContainer
{
    [SerializeField] private BuffManager _buffManager;
    public BuffManager buffManager => _buffManager;

    [SerializeField] private float attack;
    [SerializeField] private float defense;

    public float? GetStat(string statID)
    {
        return statID switch
        {
            "Attack" => attack,
            "Defense" => defense,
            _ => null
        };
    }

    public float? ModifyStat(string statID, float modifyAmount, StatModifierType modifyType)
    {
        switch (statID)
        {
            case "Attack":
                attack = modifyType == StatModifierType.Add ? attack + modifyAmount : attack * modifyAmount;
                return attack;

            case "Defense":
                defense = modifyType == StatModifierType.Add ? defense + modifyAmount : defense * modifyAmount;
                return defense;
        }

        return null;
    }
}
```
You are free to implement stat storage however you like.
## 10. Creating Custom Buffs

Buffs are purely data‑driven. To create a new buff:

    - Create a new BuffData asset
    - Set id, description, duration
    - Add one or more StatModifiers

Examples
Attack Up

    - targetStatID: "Attack"
    - modifierType: Add
    - modifierValue: +5

Defense Up (Percent)

    - targetStatID: "Defense"
    - modifierType: Multiply
    - modifierValue: 1.25

Slow

    - targetStatID: "MoveSpeed"
    - modifierType: Multiply
    - modifierValue: 0.5

Buff behavior is entirely defined by how your IBuffContainer interprets stat IDs.

## 11. Best Practices

    - Keep buffs focused on stats only
    - Use clear, consistent stat IDs
    - Prefer multiplicative modifiers for scaling effects
    - Use additive modifiers for flat bonuses
    - Avoid putting UI/VFX logic inside buffs
    - Use BuffApplied and BuffRemoved events for presentation
    - Let the AbilitySystemAPI handle duration ticking
    - Use buffs for predictable, numeric changes; use statuses for more complex logic

## 12. Summary

Buffs in the Tactics Ability System Package are:

    - Data‑driven
    - Stat‑focused
    - Turn‑based
    - Backend‑only
    - Integrated with the AbilitySystemAPI
    - Designed to work alongside statuses and abilities

## Thank You For Reading 

### `Suggested Readings`
@ref Installation "How to install the package"

[Credits](https://ryanellisdale.github.io/ModularArchitecture/md__documentation_2_credits.html)

[Third Party Notices](https://ryanellisdale.github.io/ModularArchitecture/md__third_01_party_01_notices.html)

[Licensing](https://ryanellisdale.github.io/ModularArchitecture/md__l_i_c_e_n_s_e.html)
