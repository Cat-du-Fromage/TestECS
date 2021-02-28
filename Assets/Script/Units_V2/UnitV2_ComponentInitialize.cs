using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

public class UnitV2_ComponentInitialize
{
    public static EntityArchetype UnitArchetype;
    private static RenderMesh _unitRenderer;
    private static EntityManager _entityManager;

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        UnitArchetype = _entityManager.CreateArchetype(
            typeof(UnitV2_ComponentData),
            typeof(RenderMesh),
            typeof(Rotation),
            typeof(Translation),
            typeof(RenderBounds),
            typeof(LocalToWorld)
            );
    }
    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitializeWithScene()
    {
        //_unitRenderer = 
    }
    /*
    public static MeshRenderer GetLookFromPrototype(string protoName)
    {
        GameObject prototype = GameObject.Find(protoName);
        RenderMesh result = prototype.GetComponent<MeshRenderer>().;
    }
    
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        Entity prefabEntity = conversionSystem.GetPrimaryEntity(prefabUnitGameObject);
        Unit.prefabEntity = prefabEntity;

        PrefabComponentData _PrefabComponentData = new PrefabComponentData
        {
            Prefab = prefabEntity
        };
        entityManager.AddComponentData(entity, _PrefabComponentData);
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(prefabUnitGameObject);
    }
    */
}
