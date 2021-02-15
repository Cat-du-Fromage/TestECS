using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Mathematics;

public struct CompC : IComponentData
{
    public int Health;
    public int RegimentSize;
}

public struct CompD : IComponentData
{
    public int CombatCC;
    public int CombatCR;
}

public class TestMassEntityCreationIteration : SystemBase
{
    EntityManager _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    // Start is called before the first frame update
    protected override void OnCreate()
    {
        base.OnCreate();
    }

    protected override void OnStartRunning()
    {
        base.OnStartRunning();

        EntityArchetype _archetype = _entityManager.CreateArchetype(typeof(CompC), typeof(CompD));
        Entity _entity = _entityManager.CreateEntity(_archetype);

        NativeArray<Entity> ents = new NativeArray<Entity>(10, Allocator.Persistent);
        //Create an entity with components that use an EntityArchetype.
        _entityManager.CreateEntity(_archetype, ents);

        // Copy an existing entity, including its current data, with Instantiate
        _entityManager.Instantiate(_entity, ents);

        _entityManager.DestroyEntity(ents);

        NativeArray<ArchetypeChunk> chunks = new NativeArray<ArchetypeChunk>(10, Allocator.Persistent);
        //_entityManager.Crea(_archetype, chunks, 200);

        ents.Dispose();
        chunks.Dispose();
    }

    protected override void OnUpdate()
    {

    }
}
