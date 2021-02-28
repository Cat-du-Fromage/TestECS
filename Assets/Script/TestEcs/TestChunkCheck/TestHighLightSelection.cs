using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

public class TestHighLightSelection : SystemBase
{
    private MonoHighlightPrefab _highLightMono;
    private EntityQuery _QueryselectedUnit;
    private EntityQuery _QueryHighLightSelection;
    private EntityManager _entityManager;
    protected override void OnCreate()
    {
        //Get both Highlight and unit Selected on queries
        _QueryHighLightSelection = GetEntityQuery(typeof(HighlightSpawner));
        _QueryselectedUnit = GetEntityQuery(typeof(UnitV2_ComponentData), typeof(SelectedUnit));
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    protected override void OnStartRunning()
    {
        base.OnStartRunning();
    }

    protected override void OnUpdate()
    {
        /*
        Entities
            .WithStructuralChanges()
            .WithAll<SelectedUnit>()
            .ForEach((Entity selectedUnit, ref Translation translation) =>
        {
            float3 unitPosition = translation.Value + new float3(0f, -0.5f, 0f);
            Entity prefab = _entityManager.GetComponentData<HighlightSpawner>(_QueryHighLightSelection.GetSingletonEntity()).Prefab;
            Entity entity = _entityManager.Instantiate(MonoHighlightPrefab.prefabEntity);

            _entityManager.AddComponent<Parent>(entity);
            _entityManager.SetComponentData(entity, new Parent { Value = selectedUnit});
            _entityManager.AddComponent<LocalToParent>(entity);
            _entityManager.AddComponent<Child>(selectedUnit);

            //exemple DynamicBuffer POUR REGIMENT
            DynamicBuffer<Child> linkedEntities = EntityManager.AddBuffer<Child>(selectedUnit);
            linkedEntities.Add(new Child { Value = selectedUnit });
            linkedEntities.Add(new Child { Value = entity });
            
            //REmove component tag select
            _entityManager.SetComponentData(selectedUnit, new Translation {Value = new float3(3,0,5) + translation.Value });
            _entityManager.RemoveComponent(selectedUnit, typeof(SelectedUnit));
        })
            .WithoutBurst()
            .Run();
        
        */
    }
        
}
