using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using Unity.Jobs;
using Unity.Physics.Systems;
using Unity.Physics;

public class TestSelectionEntity : SystemBase
{
    private float3 startPosition;
    private float3 endPosition;

    public bool dragSelect;

    private EntityQuery query;

    /// <summary>
    /// ECS RAYCAST BASIC Construction
    /// </summary>
    /// <param name="fromPosition"></param>
    /// <param name="toPosition"></param>
    /// <returns></returns>
    private Entity Raycast(float3 fromPosition, float3 toPosition)
    {
        //WTF IS THIS??!!
        BuildPhysicsWorld buildPhysicsWorld = World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>();
        //AND THIS??!!
        CollisionWorld collisionWorld = buildPhysicsWorld.PhysicsWorld.CollisionWorld;

        RaycastInput raycastInput = new RaycastInput
        {
            Start = fromPosition,
            End = toPosition,
            //Layer filter
            Filter = new CollisionFilter
            {
                BelongsTo = ~0u, //belongs to all layers
                CollidesWith = ~0u, //collides with all layers
                GroupIndex = 0,
            }
        };
        //throw a raycast
        Unity.Physics.RaycastHit raycastHit = new Unity.Physics.RaycastHit();
        if(collisionWorld.CastRay(raycastInput, out raycastHit))
        {
            //Return the entity hit
            Entity hitEntity = buildPhysicsWorld.PhysicsWorld.Bodies[raycastHit.RigidBodyIndex].Entity;
            return hitEntity;
        }
        else
        {
            return Entity.Null;
        }

    }

    // Start is called before the first frame update
    protected override void OnCreate()
    {
        base.OnCreate();
    }

    protected override void OnStartRunning()
    {
        dragSelect = false;
    }

    // Update is called once per frame
    protected override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //startPosition = TestUtils.GetMouseWorldPosition();
            startPosition = Input.mousePosition;
            //Debug.Log(startPosition);
        }

        if(Input.GetMouseButton(0))
        {
            dragSelect = math.length(startPosition - (float3)Input.mousePosition) > 10 ? true : false;
        }

        //USING classic unityEngineRay and it seems it doesn't work....
        if(Input.GetMouseButtonUp(0))
        {
            //Simple Click without drag
            if(dragSelect == false)
            {
                //UnityEngine.Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
                UnityEngine.Ray ray = Camera.main.ScreenPointToRay(startPosition);
                Debug.Log(Raycast(ray.origin, ray.direction * 50000f));
                Debug.Log(startPosition);
                
                //SET "Unit Selected" add component(shareComponent)
                //find all his regiment and add component to them too
            }
            else
            {
                endPosition = Input.mousePosition;
                Debug.Log(startPosition);
                Debug.Log(endPosition);
                //WHY not the z vector???!!
                float3 lowerLeftPosition = new float3(math.min(startPosition.x, endPosition.x), math.min(startPosition.y, endPosition.y), 0);
                float3 upperRightPosition = new float3(math.max(startPosition.x, endPosition.x), math.max(startPosition.y, endPosition.y), 0);
                Entities
                    .WithAll<UnitV2_ComponentData>()
                    .ForEach((Entity entity, ref Translation translation) =>
                {

                    float3 entityPosition = translation.Value;
                    float3 screenPos = Camera.main.WorldToScreenPoint(entityPosition);
                    if (screenPos.x >= lowerLeftPosition.x &&
                       screenPos.y >= lowerLeftPosition.y &&
                       screenPos.x <= upperRightPosition.x &&
                       screenPos.y <= upperRightPosition.y)
                    {
                        Debug.Log(entity);
                    }

                })
                    .WithoutBurst()
                    .Run();
                    //.ScheduleParallel();
            }


        }
    }


}
