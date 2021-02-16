using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Mathematics;

public struct CompC : IComponentData
{
    public int Health;
    public int RegimentSize;
}

public struct CompD : IComponentData
{
    public int CombatCC;
    public int CombatCR;
}

public class TestMassEntityCreationIteration : SystemBase
{
    EntityManager _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    private EntityQuery query2;
    // Start is called before the first frame update
    protected override void OnCreate()
    {
        base.OnCreate();
    }

    protected override void OnStartRunning()
    {
        base.OnStartRunning();

        EntityArchetype _archetype = _entityManager.CreateArchetype(typeof(CompC), typeof(CompD));
        Entity _entity = _entityManager.CreateEntity(_archetype);

        query2 = _entityManager.CreateEntityQuery(
            ComponentType.ReadWrite<CompC>(),
            ComponentType.ReadWrite<CompD>()
            );

        NativeArray<Entity> ents = new NativeArray<Entity>(10, Allocator.Persistent);
        //Create an entity with components that use an EntityArchetype.
        _entityManager.CreateEntity(_archetype, ents);

        // Copy an existing entity, including its current data, with Instantiate
        _entityManager.Instantiate(_entity, ents);
        _entityManager.SetComponentData<CompC>(ents[0], new CompC { Health = 30 });
        //_entityManager.DestroyEntity(ents);

        NativeArray<ArchetypeChunk> chunks = new NativeArray<ArchetypeChunk>(10, Allocator.Persistent);
        //_entityManager.Crea(_archetype, chunks, 200);

        ents.Dispose();
        chunks.Dispose();
    }

    protected override void OnUpdate()
    {
        //Test to Verify if we change only the z value Of CompA if the previous CompA.x value is reset to 0 or not
        //SPOILER IT DONT!! hooray
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Create a temporary Array of chunks containing the query
            NativeArray<ArchetypeChunk> chunks = query2.CreateArchetypeChunkArray(Allocator.TempJob);
            //REMPLACE ArchetypeChunkComponentType,  get the component in entityManager and set if we can write or not;
            ComponentTypeHandle<CompC> aType = _entityManager.GetComponentTypeHandle<CompC>(false);
            //Go through the array of chunks created before(which contains the archetype we want)
            foreach (ArchetypeChunk chunk in chunks)
            {
                //Get the current array of the chunk so. we SHALL NOT DISPOSE THEM!
                NativeArray<CompC> aVals = chunk.GetNativeArray<CompC>(aType); //CAUTION UNITY.COLLETION NEEDED FOR NATIVEARRAY!!
                for (int i = 0; i < chunk.Count; i++)
                {
                    CompC a = aVals[i];
                    if(aVals[i].Health == 30) // select the only entity who has 30 health
                    {
                        Debug.Log("TROUVÉE " + aVals[i].Health + " " + i); //careful "i" don't represent the ID of the entity..
                    }
                    //a.Health = 10;
                    //aVals[i] = a;
                }
            }
            chunks.Dispose();
        }

    }
}
