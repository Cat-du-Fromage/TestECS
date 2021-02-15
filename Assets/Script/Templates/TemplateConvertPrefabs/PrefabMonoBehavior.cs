using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class PrefabMonoBehavior : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    //allow to Use it on different Script
    public static Entity prefabEntity;

    public GameObject prefabUnitGameObject;
    //private EntityManager _entityManager;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
