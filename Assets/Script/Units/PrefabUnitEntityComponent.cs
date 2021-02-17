using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
/*
public class PrefabUnitEntityComponent : ComponentSystem
{
    Entity entity;
    EntityManager _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    //EntityManager _entityManager;
    protected override void OnCreate()
    {

    }

    protected override void OnStartRunning()
    {
        for (int i = 0; i < 3; i++)
        {   
            Entity e =  Unit.InitUnit();
            _entityManager.SetComponentData(e, new IUnitComponent{ combatMelee = i});
            entity = e;
        }
        Debug.Log("ON LA FAIT");
    }

    protected override void OnUpdate()
    {

        if(Input.GetKeyDown(KeyCode.S))
        {
            var u = _entityManager.GetComponentData<IUnitComponent>(entity);
            u.combatRange = 200;
            u.LifePoint += 15;
            _entityManager.SetComponentData<IUnitComponent>(entity, u);
        }
    }
}
*/



