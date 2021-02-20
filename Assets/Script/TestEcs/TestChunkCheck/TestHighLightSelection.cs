using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

public class TestHighLightSelection : SystemBase
{
    private EntityQuery _QueryselectedUnit;
    private EntityQuery _QueryHighLightSelection;
    private EntityArchetype _highLightArchetype;
    private EntityManager _entityManager;
    protected override void OnCreate()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        _highLightArchetype = _entityManager.CreateArchetype(
            typeof(RenderMesh),
            typeof(Rotation),
            typeof(Translation),
            typeof(RenderBounds),
            typeof(LocalToWorld),
            typeof(Highlight)
            );

        _QueryselectedUnit = GetEntityQuery(ComponentType.ReadOnly<SelectedUnit>());
        
    }

    protected override void OnStartRunning()
    {
        base.OnStartRunning();
    }

    protected override void OnUpdate()
    {
        
    }
}
