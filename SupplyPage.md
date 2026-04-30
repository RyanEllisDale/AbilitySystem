# Supply System {#supply_page}

# How‑To: Supplies

Supplies represent resource costs required to activate an ability.
They allow abilities to consume mana, stamina, ammo, energy, or any custom resource your game defines.

This chapter explains:

    - What a Supply is
    - How supplies integrate with abilities
    - How Evaluate and Use work
    - How to create supplies in the inspector
    - How to connect supplies to your resource system
    - Best practices

## 1. Understanding Supplies

Supplies are simple data objects that define:

    - Which resource an ability consumes
    - How much of that resource is required
    - Whether the user has enough to activate the ability

Supplies are evaluated automatically by:
```csharp

AbilitySystemAPI.IsActivatable(abilityInstance)
```
And consumed automatically during:
```csharp

AbilitySystemAPI.ExecuteAbility(...)
```
Supplies do not store runtime state.
They simply reference external resource values through DataReference<float>.
## 2. Supply Class
```csharp

[System.Serializable]
public class Supply
{
    public string id;
    [Multiline] public string description;
    [SerializeField] private DataReference<float> _resource;
    [SerializeField] private DataReference<float> _cost;

    public bool Evaluate() { return _resource.value >= _cost.value; }
    public void Use() { _resource.value = _resource.value - _cost.value; }
}
```
Fields

    - id  A unique identifier for referencing the supply.
    - description A multiline description for UI or debugging.
    - _resource A DataReference<float> pointing to the current amount of the resource.
    - _cost  A DataReference<float> defining how much resource is consumed.

Evaluate()

Checks whether the ability can afford the cost.
```csharp

public bool Evaluate()
{
    return _resource.value >= _cost.value;
}

Use()
```
Consumes the resource.
```csharp

public void Use()
{
    _resource.value -= _cost.value;
}
```
Supplies assume that Evaluate() has already passed before Use() is called.
## 3. Integrating Supplies With Abilities

Supplies are added directly to an AbilityData asset.

Example:

    - Mana cost
    - Stamina cost
    - Ammo cost
    - Custom resource cost

When an ability is executed:

    - All supplies are evaluated
    - If any fail, the ability cannot activate
    - If all pass, each supply’s Use() method is called
    - Resources are deducted

This is handled internally by:
```csharp

foreach (Supply currentSupplies in aInstance.ability.supplies)
{
    currentSupplies.Use();
}
```
## 4. Creating Supplies in the Inspector

Supplies are configured inside an AbilityData asset.

Each supply requires:

    - A resource reference (DataReference<float>)
    - A cost reference (DataReference<float>)

Example setup:
| Field | Example |
| --- | --- |
| id | ``"ManaCost"`` |
| description | ``"Consumes ``mana ``to ``cast ``spells"`` |
| resource | ``playerMana`` (DataReference<float>) |
| cost | ``fireballManaCost`` (DataReference<float>) |

This allows:

    - Designers to tune costs without touching code
    - Dynamic resource values (DataReference) to update at runtime
    - Abilities to share resource pools

## 5. Connecting Supplies to Your Resource System

Supplies rely on DataReference<float>, which means:

    - Resources can be stored anywhere
    - Resources can be shared between units
    - Resources can be modified by buffs, statuses, or gameplay events
    - Resources can be displayed in UI easily

Common patterns:
Pattern A — Unit‑Local Resources

Each unit has its own DataReference<float> for:

    - Mana
    - Stamina
    - Energy

Pattern B — Global Resources

Shared resources such as:

    - Team energy
    - Shared ammo pool
    - Global cooldown meter

Pattern C — Hybrid

Some resources are local, others global.

Supplies work with all patterns.
## 6. Example: Mana Cost
```csharp

[SerializeField] private DataReference<float> mana;
[SerializeField] private DataReference<float> manaCost;

public Supply manaSupply = new Supply
{
    id = "Mana",
    description = "Consumes mana to cast spells",
    _resource = mana,
    _cost = manaCost
};
```
Add this supply to an AbilityData’s supplies list.
## 7. Example: Ammo Cost
```csharp

[SerializeField] private DataReference<float> ammo;
[SerializeField] private DataReference<float> ammoCost;

public Supply ammoSupply = new Supply
{
    id = "Ammo",
    description = "Consumes ammo when firing",
    _resource = ammo,
    _cost = ammoCost
};
```
## 8. Best Practices

    - Keep supplies simple and focused
    - Use DataReferences for dynamic resource values
    - Use multiple supplies for multi‑resource abilities
    - Use Evaluate() before Use()
    - Keep resource logic outside the ability system
    - Use supplies for numeric costs; use conditions for logical requirements

## 9. Summary

Supplies in the Tactics Ability System Package are:

    - Lightweight
    - Data‑driven
    - Easy to configure
    - Integrated with abilities
    - Designed for resource‑based gameplay

They provide a clean, modular way to enforce ability costs without embedding resource logic into abilities themselves.

### `Suggested Readings`
[How-To-Install](https://ryanellisdale.github.io/AbilitySystem/installation.html)

[Credits](https://ryanellisdale.github.io/ModularArchitecture/md__documentation_2_credits.html)

[Third Party Notices](https://ryanellisdale.github.io/ModularArchitecture/md__third_01_party_01_notices.html)

[Licensing](https://ryanellisdale.github.io/ModularArchitecture/md__l_i_c_e_n_s_e.html)