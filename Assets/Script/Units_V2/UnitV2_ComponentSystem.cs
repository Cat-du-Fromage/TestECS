using Unity.Entities;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

public class UnitV2_ComponentSystem : SystemBase
{
    BeginInitializationEntityCommandBufferSystem ei_ECB;

    EntityManager _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    
    EntityArchetype RegimentArchetype;

    protected override void OnCreate()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        ei_ECB = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();

        RegimentArchetype = _entityManager.CreateArchetype(
            typeof(RenderMesh),
            typeof(Rotation),
            typeof(Translation),
            typeof(RenderBounds),
            typeof(LocalToWorld)
            );
    }
    // Start is called before the first frame update
    protected override void OnStartRunning()
    {

        Entity regimentTest = _entityManager.CreateEntity(RegimentArchetype);
        _entityManager.AddComponentData(regimentTest, new RegimentData());

        _entityManager.SetName(regimentTest, "RegimentTest");

        var ecb = ei_ECB.CreateCommandBuffer().AsParallelWriter();
        Entities
            .WithBurst(synchronousCompilation: true)
            .ForEach((Entity e, int entityInQueryIndex, in UnitV2_ComponentData uic, in LocalToWorld ltw) =>
            {
                for (int i = 0; i < 4; i++)
                {
                    Entity defEntity = ecb.Instantiate(entityInQueryIndex, uic.Prefab);
                    float3 _position = new float3(20 + i * 2, 5, 20 + i);
                    ecb.SetComponent(entityInQueryIndex, defEntity, new Translation { Value = _position });
                    ecb.AddComponent<UnitV2_ComponentData>(entityInQueryIndex, defEntity);
                    ecb.SetComponent(entityInQueryIndex, defEntity, new UnitV2_ComponentData {Health = 100, CombatMelee = 20, CombatRange = 30, RegimentId = regimentTest.Index, Speed = 1f, Position = _position, Prefab = uic.Prefab });
                }
                ecb.DestroyEntity(entityInQueryIndex, e);
            }
            ).ScheduleParallel();
        ei_ECB.AddJobHandleForProducer(Dependency);

        EntityQuery m_Group = GetEntityQuery(typeof(UnitV2_ComponentData));
        int num = m_Group.CalculateEntityCount();
        _entityManager.SetComponentData(regimentTest, new RegimentData { Health = num });
    }

    // Update is called once per frame
    protected override void OnUpdate()
    {

    }

    public void MergeEntitiesTogether(Entity parent, Entity child)
    {
        if (!_entityManager.HasComponent(child, typeof(Parent)))
        {
            _entityManager.AddComponentData(child, new Parent { Value = parent });
            _entityManager.AddComponentData(child, new LocalToParent());

            DynamicBuffer<LinkedEntityGroup> buf = _entityManager.GetBuffer<LinkedEntityGroup>(parent);
            buf.Add(child);
        }
    }
}

