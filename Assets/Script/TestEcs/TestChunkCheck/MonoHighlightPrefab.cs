using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

public class MonoHighlightPrefab : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
    /*
    [SerializeField] private Mesh _highlightMesh;
    [SerializeField] private Material _highlightMaterial;
    */
    public GameObject HighlightPrefab;

    public static Entity prefabEntity;
    /*
    private EntityManager _entityManager;
    private EntityArchetype _highLightArchetype;
    */
    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(HighlightPrefab);
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        Entity HighLPrefab = conversionSystem.GetPrimaryEntity(HighlightPrefab);
        MonoHighlightPrefab.prefabEntity = HighLPrefab;
        //We also test if we can juste use ONE component to convert all prefab: Plot Twist NO....
        var _highLightData = new HighlightSpawner
        {
            Prefab = HighLPrefab
        };
        dstManager.AddComponentData(entity, _highLightData);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //setHighlight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    //FAIL TEST TO MAKE A FULL ESC entity mesh generation
    public Entity setHighlight()
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
        Entity entity = _entityManager.CreateEntity(_highLightArchetype);
        _entityManager.SetName(entity, "HighlightTest");
        _entityManager.AddComponentData(entity, new Translation
        {
            Value = new float3(2f, 8f, 4f)
        });

        _entityManager.AddComponentData(entity, new NonUniformScale
        {
            Value = new float3(1f,0.125f,1f)
        });

        _entityManager.AddSharedComponentData(entity, new RenderMesh
        {
            mesh = _highlightMesh,
            material = _highlightMaterial
        });

        return entity;
    }
    */
}
