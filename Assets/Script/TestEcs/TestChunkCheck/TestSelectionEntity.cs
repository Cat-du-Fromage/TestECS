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
    public float3 startPosition;
    public float3 endPosition;

    public bool dragSelect;
    private float widthBoxSelect;
    private float heightBoxSelect;

    private EntityManager _entityManager;

    #region RAYCAST ECS
    //==========================================================================================================================
    /// <summary>
    /// ECS RAYCAST BASIC Construction
    /// </summary>
    /// <param name="fromPosition"></param>
    /// <param name="toPosition"></param>
    /// <returns></returns>
    private Entity Raycast(float3 fromPosition, float3 toPosition)
    {
        BuildPhysicsWorld buildPhysicsWorld = World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>(); //Seems to connect physics to the current world
        CollisionWorld collisionWorld = buildPhysicsWorld.PhysicsWorld.CollisionWorld;//Seems to connect methods uses for collision to the physics world we created

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
    //==========================================================================================================================
    #endregion RAYCAST ECS
    // Start is called before the first frame update
    protected override void OnCreate()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        //UnityEngine.Material materialSelect = Resources.Load("Material/Highlight.mat", typeof(UnityEngine.Material)) as UnityEngine.Material;
    }

    protected override void OnStartRunning()
    {
        dragSelect = false;
        UnityEngine.Material materialSelect = Resources.Load("Material/Highlight.mat", typeof(UnityEngine.Material)) as UnityEngine.Material;
    }

    // Update is called once per frame
    protected override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            TestSelectionMonoPart.instance.selectionBox.gameObject.SetActive(true); //SelectionBox SHOW
        }

        if(Input.GetMouseButton(0))
        {
            dragSelect = math.length(startPosition - (float3)Input.mousePosition) > 10 ? true : false;

            widthBoxSelect = Input.mousePosition.x - startPosition.x;
            heightBoxSelect = Input.mousePosition.y - startPosition.y;

            TestSelectionMonoPart.instance.selectionBox.sizeDelta = new float2(math.abs(widthBoxSelect), math.abs(heightBoxSelect));
            TestSelectionMonoPart.instance.selectionBox.anchoredPosition = new float2(startPosition.x, startPosition.y) + new float2(widthBoxSelect/2, heightBoxSelect/2);
        }

        if(Input.GetMouseButtonUp(0))
        {
            
            TestSelectionMonoPart.instance.selectionBox.gameObject.SetActive(false); //SelectionBox HIDE
            //Simple Click without drag
            if (dragSelect == false)
            {
                UnityEngine.Ray ray = Camera.main.ScreenPointToRay(startPosition);
                Entity _entHit = Raycast(ray.origin, ray.direction * 50000f);
                if (_entityManager.HasComponent<UnitV2_ComponentData>(_entHit))
                {
                    //ADD SELECTION COMPONENT
                    _entityManager.RemoveComponent<SelectedUnit>(_entHit); // REMOVE component SELECTIONUNIT; NOTE: Unit with removed component goes in the same chunk
                    Debug.Log(_entHit);
                }
                
                //SET "Unit Selected" add component(shareComponent)
                //find all his regiment and add component to them too
            }
            else
            {
                /// <summary>
                ///     CHANGE TO MAKE IN FUTURE PROJECT
                ///     * USE QUERY INSTEAD OF ".WithAll<UnitV2_ComponentData>()"
                ///     * ALLOW BURST COMPILE
                /// </summary>
                endPosition = Input.mousePosition;
                //WHY not the z vector???!!
                float3 lowerLeftPosition = new float3(math.min(startPosition.x, endPosition.x), math.min(startPosition.y, endPosition.y), 0);
                float3 upperRightPosition = new float3(math.max(startPosition.x, endPosition.x), math.max(startPosition.y, endPosition.y), 0);
                ComponentTypeHandle<MeshRenderer> RenderMesh = _entityManager.GetComponentTypeHandle<MeshRenderer>(false);
                Entities
                    .WithStructuralChanges() // allow to use MainThread structural change , CAREFULL this does not allow BURST COMPILE
                    .WithAll<UnitV2_ComponentData>()//Select Only Entities wit at least this component
                    .ForEach((Entity entity, ref Translation translation) =>
                {

                    float3 entityPosition = translation.Value;
                    float3 screenPos = Camera.main.WorldToScreenPoint(entityPosition);
                    if (screenPos.x >= lowerLeftPosition.x &&
                       screenPos.y >= lowerLeftPosition.y &&
                       screenPos.x <= upperRightPosition.x &&
                       screenPos.y <= upperRightPosition.y)

                    {
                        _entityManager.SetComponentData<UnitV2_ComponentData>(entity, new UnitV2_ComponentData { CombatMelee = 100 });// ATTENTION: no need for ".WithStructuralChanges()" 
                        _entityManager.AddComponent<SelectedUnit>(entity); // Add SelectionComponent : ATTENTION: NEED ".WithStructuralChanges()" to work
                    }

                })
                    .WithoutBurst()
                    .Run();
                    //.ScheduleParallel(); ATTENTION pas de burst ici , car Camera.main n'est pas une fonction ECS
                    // Tout variable ou methods NON-ECS bloque burst?
            }


        }

    }
}

