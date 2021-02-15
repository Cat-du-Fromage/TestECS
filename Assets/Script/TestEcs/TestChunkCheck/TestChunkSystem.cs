using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Mathematics;

public struct CompA : IComponentData
{
    public int x;
    public int z;
}

public struct CompB : IComponentData
{
    public int x;
    public int y;
}
/// <summary>
/// HERE we test how to iterate through chunk which contain a certain archetype of Entity
/// </summary>
public class TestChunkSystem : SystemBase
{
    EntityManager _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    private EntityQuery query;

    protected override void OnCreate()
    {
        base.OnCreate();

    }

    protected override void OnStartRunning()
    {
        //Create Entity with components
        _entityManager.CreateEntity(typeof(CompA), typeof(CompB));
        //Create a "dataBase"/query of entities containing the components CompA and CompB
        query = _entityManager.CreateEntityQuery(
            ComponentType.ReadWrite<CompA>(),
            ComponentType.ReadWrite<CompB>()
            );


    }

    // Update is called once per frame
    protected override void OnUpdate()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            // Create a temporary Array of chunks containing the query
            NativeArray<ArchetypeChunk> chunks = query.CreateArchetypeChunkArray(Allocator.TempJob);
            //REMPLACE ArchetypeChunkComponentType,  get the component in entityManager and set if we can write or not;
            ComponentTypeHandle<CompA> aType = _entityManager.GetComponentTypeHandle<CompA>(false);
            //Go through the array of chunks created before(which contains the archetype we want)
            foreach (ArchetypeChunk chunk in chunks)
            {
                //Get the current array of the chunk so. we SHALL NOT DISPOSE THEM!
                NativeArray<CompA> aVals = chunk.GetNativeArray<CompA>(aType);
                for (int i = 0; i < chunk.Count; i++)
                {
                    CompA a = aVals[i];
                    a.x = 7;
                    aVals[i] = a;
                }
            }
            chunks.Dispose();
        }

        //Test to Verify if we change only the z value Of CompA if the previous CompA.x value is reset to 0 or not
        //SPOILER IT DONT!! hooray
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Create a temporary Array of chunks containing the query
            NativeArray<ArchetypeChunk> chunks = query.CreateArchetypeChunkArray(Allocator.TempJob);
            //REMPLACE ArchetypeChunkComponentType,  get the component in entityManager and set if we can write or not;
            ComponentTypeHandle<CompA> aType = _entityManager.GetComponentTypeHandle<CompA>(false);
            //Go through the array of chunks created before(which contains the archetype we want)
            foreach (ArchetypeChunk chunk in chunks)
            {
                //Get the current array of the chunk so. we SHALL NOT DISPOSE THEM!
                NativeArray<CompA> aVals = chunk.GetNativeArray<CompA>(aType); //CAUTION UNITY.COLLETION NEEDED FOR NATIVEARRAY!!
                for (int i = 0; i < chunk.Count; i++)
                {
                    CompA a = aVals[i];
                    a.z = 10;
                    aVals[i] = a;
                }
            }
            chunks.Dispose();
        }
    }

}
