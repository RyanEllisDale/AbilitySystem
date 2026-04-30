# Documentation

Full API documentation is available here:

- [API Reference](https://ryanellisdale.github.io/ModularArchitecture/)

# Tactics Ability System Package

The Tactics Ability System Package is a small Unity framework designed to provide modular, reusable, data‑driven gameplay components for turn‑based tactics games. Unity’s built‑in MonoBehaviour‑centric workflow is simple and familiar, but it often leads to tightly coupled systems and duplicated logic. This package embraces a more modular, compositional approach using ScriptableObjects and runtime instances to create clean, scalable gameplay architecture.

### Features

- Modular Ability Composition (ScriptableObject‑driven abilities and effects)
- Data‑Driven Architecture (serialized data + runtime instances)
- Effect System (single‑target effects that can be composed into complex abilities)
- Resource & Supply System (generic, safe cost evaluation for tactics resources)
- Status Effects (turn‑based statuses, mixing logic, composite statuses)
- Buff System (additive/multiplicative stat modifiers, debuffs, timed buffs)
- Turn Integration Hooks (turn start / turn end entry points)
- Event‑Driven Architecture (clean API events for UI, VFX, and audio)
- Backend‑Only by Design (no UI or visuals included — bring your own presentation layer)
- Custom property drawers and editor scripts
- Samples and demos
- MIT Licensed