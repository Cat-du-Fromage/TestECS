using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class PrefabComponentSystem : ComponentSystem
{
    //EntityManager _entityManager;
    protected override void OnCreate()
    {

    }
    //ATTENTION will be call just by his own existence(this is why all command are on commented)
    protected override void OnStartRunning()
    {
        //Debug.Log("ON LA FAIT 333");
    }
    //ATTENTION will be call just by his own existence(this is why all command are on commented)
    protected override void OnUpdate()
    {
        // Get all entities from the current world
        /*
        EntityManager _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        Entity spawnedPrefab = _entityManager.Instantiate(PrefabMonoBehavior.prefabEntity);
        _entityManager.SetComponentData(spawnedPrefab, new Translation { Value = new float3(27, 5, 27) });
        */

    }
}
