# Jade Engine Architecture

This document provides a deep dive into the internal mechanisms of the Jade Engine. It is intended for contributors, curious engine developers, and anyone who wants to understand the design rationale behind the engine's architecture.

## 1. Core Architectural Principles

The engine is built on three core principles that guide every design decision.

* **Data-Oriented Design**: The absolute priority is the efficient layout and processing of data. The archetype and chunk-based architecture follows a strict **Structure of Arrays (SoA)** pattern to maximize CPU cache hits during iteration, which is the cornerstone of the engine's performance.
* **Predictable Performance**: The engine aims for a "zero-allocation" policy within the main game loop. The pervasive use of `struct`, `ref`, `Span<T>`, and manually managed native memory is fundamental to eliminating unpredictable stalls caused by the .NET Garbage Collector.
* **Safe & Ergonomic API**: Low-level complexity is intentionally abstracted away behind clean, expressive, and type-safe facades. The goal is to provide a powerful and intuitive API (`EntityCommands`, `Query<T>`, `EventWriter<T>`) without exposing the unsafe, performance-critical code that underpins it.

## 2. The ECS Core: Data Storage & Lifecycle

This is the heart of the engine, where data lives and is manipulated.

### Entity Lifecycle Management

An `Entity` is not an object; it is a lightweight handle used to identify and access its associated data.

* **`Id` (uint)**: A unique identifier that acts as an index into the `World`'s internal arrays.
* **`Version` (ushort)**: A counter that increments each time an `Id` is recycled after an entity is destroyed. This mechanism solves the "ABA problem" by ensuring that old, stale handles to a destroyed entity cannot be used to accidentally access a new entity that has recycled the same `Id`. An operation is only valid if both the `Id` and `Version` match.
* **ID Recycling**: When an entity is destroyed, its `Id` is pushed into a `Queue<uint>` of free IDs. The `World` will draw from this queue before incrementing its global ID counter, ensuring dense packing of entity data over time.

### The Archetype Model

An `Archetype` is a unique data layout defined by a specific set of components. All entities that share the exact same component signature reside in the same `Archetype`.

#### Archetype Chunks: The Memory Foundation

To ensure data locality, each `Archetype` is composed of a list of `ArchetypeChunk` objects.

* **Fixed-Size Native Memory**: An `ArchetypeChunk` is a 16KB block of manually-allocated, 64-byte aligned native memory. This provides direct control over memory layout and bypasses the GC.
* **Structure of Arrays (SoA)**: Within a chunk, components are not grouped by entity. Instead, all components of the same type are stored together in contiguous arrays.

    *Example memory layout for a chunk containing `Position` and `Velocity` components:*
    ```
    // Chunk Memory
    [Ent1, Ent2, ... EntN] [Pos1, Pos2, ... PosN] [Vel1, Vel2, ... VelN]
    |<----Entities---->| |<----Positions----->| |<---Velocities--->|
    ```
    This layout is the secret to high-speed queries. When a system iterates over all `Position` components, the CPU can load a single, continuous block of homogeneous data into its cache, avoiding costly cache misses.

#### The `MoveEntity` Process: A Structural Change

When a component is added to or removed from an entity, the entity's fundamental data signature changes. It can no longer live in its old `Archetype` and must be moved. This is a critical and carefully orchestrated process.

1.  **Find or Create Destination**: The `World` calculates the new `ComponentMask` for the entity and finds the corresponding destination `Archetype`. If it doesn't exist, a new one is created.
2.  **Add & Copy Data**: The entity is added to a chunk in the destination `Archetype`. Then, the data for all of its shared components is copied from the source chunk to the new chunk.
3.  **Perform Swap-Remove**: To fill the now-empty slot in the source `Archetype`, the **very last entity** in that archetype is moved into the empty slot.
4.  **Update Indexes**: The `World`'s central `_entityIndex` is updated for the entity that was moved to fill the gap, pointing it to its new location. Finally, the index for the original entity is updated to point to its new home in the destination `Archetype`.

This *swap-remove* technique is vital. It ensures that archetypes remain tightly packed with no "holes," maintaining the benefits of contiguous data without the massive performance cost of shifting all subsequent elements in memory.

## 3. System Scheduling and Execution

### The Deterministic Game Loop

The `SystemStage` enum (`Startup`, `Update`, `PostUpdate`, etc.) enforces a clear and predictable execution flow for the entire game loop. This structure is fundamental to writing reliable and maintainable game logic.

### The Dependency Graph & Topological Sort

The order of system execution is not left to chance. Developers can specify explicit execution constraints using attributes:
* `[RunAfter<T>]`: Guarantees a system will run after another within the same stage.
* `[RunBefore<T>]`: Guarantees a system will run before another.

The `SystemStorage` class consumes these attributes to build a dependency graph. When systems are added or changed, it performs a **Topological Sort** on this graph to compute a linear, deterministic execution plan. This process also inherently detects circular dependencies (e.g., A after B, and B after A), throwing an exception to prevent infinite loops at runtime.

## 4. Advanced Subsystems

### The Relation System

The engine supports creating arbitrary, directional relationships between entities, which can also hold data. This is far more powerful than simple parent-child hierarchies and can be used to build complex scene graphs or logical connections (e.g., `EntityA` *targets* `EntityB`). The key to its performance is the `RelationStorage`, which maintains separate indexes for relation subjects and targets (`_subjectIndex`, `_targetIndex`), allowing for near-instantaneous lookups without iterating through all existing relations.

### The Event System

For decoupled communication, the engine uses a double-buffered event system. The `EventStorage` maintains two sets of event queues. During a frame, all events are written to the "back buffer." At the end of the `PostUpdate` stage, the buffers are swapped. This ensures that events produced in frame `N` can only be read in frame `N+1`, creating a stable, predictable, and frame-delayed flow of information that prevents race conditions between systems.

## 5. Detailed Architecture Diagram

```mermaid
graph TD

    subgraph "User API & Entry Points"
        direction LR
        UserSystem["<strong>SystemBase</strong><br/><i>(Game Logic)</i>"]
        Plugin["PluginBase"]
        EntityCommands["EntityCommands<br/><i>(Fluent API)</i>"]
        Query["Query&lt;T...&gt;<br/><i>(Query Object)</i>"]
    end

    subgraph "Engine Core: The World"
        World["<strong>World</strong><br/><i>(Facade & Orchestrator)</i>"]

        subgraph "Managed Subsystems"
            direction TB
            SystemStorage["SystemStorage<br/><i>Scheduler</i>"]
            ArchetypeStorage["ArchetypeStorage<br/><i>Data Manager</i>"]
            RelationStorage["RelationStorage"]
            EventStorage["EventStorage"]
            ResourceStorage["ResourceStorage"]
        end
    end

    subgraph "Low-Level Data Storage"
        direction TB
        Archetype["Archetype"]
        ArchetypeChunk["<strong>ArchetypeChunk</strong><br/>(Aligned Native Memory)<br/><i>Structure of Arrays</i>"]
    end
    
    %% --- Configuration Flow ---
    Plugin -- "AddSystem(), AddResource()" --> World
    World -- "delegates to" --> SystemStorage
    World -- "delegates to" --> ResourceStorage

    %% --- Logic Execution Flow ---
    SystemStorage -- "executes" --> UserSystem
    UserSystem -- "Spawn()" --> World
    World -- "creates" --> EntityCommands
    
    UserSystem -- "Query<T>()" --> World
    World -- "creates" --> Query
    
    UserSystem -- "GetResource<T>()" --> World
    
    %% --- Data Modification Flow ---
    EntityCommands -- "AddComponent<T>()<br/>RemoveComponent<T>()" --> World
    World -- "<b>MoveEntity()</b>" --> ArchetypeStorage
    
    %% --- Data Read Flow ---
    Query -- "<b>ForEach()</b>" --> World
    World -- "GetMatchingArchetypes()" --> ArchetypeStorage
    ArchetypeStorage -- "reads from" --> Archetype
    Archetype -- "iterates over" --> ArchetypeChunk
    ArchetypeChunk -- "provides <strong>Span&lt;T&gt;</strong> to" --> Query

    %% --- Ownership & Relations ---
    World -- "owns" --> SystemStorage
    World -- "owns" --> ArchetypeStorage
    World -- "owns" --> RelationStorage
    World -- "owns" --> EventStorage
    World -- "owns" --> ResourceStorage
    
    ArchetypeStorage -- "owns" --> Archetype

    %% --- Styles ---
    classDef default fill:#2d333b,stroke:#58a6ff,stroke-width:1px,color:#c9d1d9
    classDef userApi fill:#0e4429,stroke:#58a6ff
    classDef worldCore fill:#845403,stroke:#f0b95b
    classDef managers fill:#7e4040,stroke:#f88181
    classDef data fill:#003874,stroke:#58a6ff

    class UserSystem,Plugin,EntityCommands,Query userApi;
    class World worldCore;
    class SystemStorage,ArchetypeStorage,RelationStorage,EventStorage,ResourceStorage managers;
    class Archetype,ArchetypeChunk data;