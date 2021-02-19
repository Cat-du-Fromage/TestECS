using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Mathematics;

public class Unit : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public static Entity prefabEntity;

    public GameObject prefabUnitGameObject;

    public static List<Entity> ListUnits;
    //private EntityManager _entityManager;

    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        
        Entity prefabEntity = conversionSystem.GetPrimaryEntity(prefabUnitGameObject);
        Unit.prefabEntity = prefabEntity;
        
        UnitData _UnitData = new UnitData
        {
            Prefab = prefabEntity
        };
        entityManager.AddComponentData(entity, _UnitData);
        

    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(prefabUnitGameObject);
    }

    public static Entity InitUnit()
    {
        
            EntityManager _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            Entity spawnedEntity = _entityManager.Instantiate(prefabEntity);
            _entityManager.SetComponentData(spawnedEntity, new Translation { Value = new float3(27, 5, 27) });
        _entityManager.AddComponent<IUnitComponent>(spawnedEntity);
        return spawnedEntity;


    }
    
    /*
    private void Awake()
    {
    
        //Entity spawnedEntity = _entityManager.Instantiate(prefabEntity);
        //_entityManager.SetComponentData(spawnedEntity, new IUnitComponent { combatMelee = 20, combatRange = 10, LifePoint = 100 });
        //_entityManager.SetComponentData(spawnedEntity, new Translation { Value = new float3(27, 5, 27) });
        //_entityManager.AddComponent<RenderBounds>(spawnedEntity);
        //_entityManager.AddComponent<LocalToWorld>(spawnedEntity);
        //_entityManager.AddComponent<RenderMesh>(spawnedEntity);
        /*
        GameObjectConversionSettings settingsPrefab = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        Entity entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefabUnitGameObject, settingsPrefab);

        Entity entityUnitInstance = _entityManager.Instantiate(entity);

        _entityManager.SetComponentData(entityUnitInstance, new Translation { Value = new float3(27, 5, 27) });
        _entityManager.AddComponent<RenderBounds>(entityUnitInstance);
        _entityManager.SetComponentData(entityUnitInstance, new IUnitComponent { name = "BritishInfantry", combatMelee = 20, combatRange = 10, LifePoint = 100 });

    EntityManager _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    Entity spawnedEntity = _entityManager.Instantiate(Unit.prefabEntity);
    _entityManager.SetComponentData(spawnedEntity, new Translation { Value = new float3(27, 5, 27) });
    

}
    */   
            
}