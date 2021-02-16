using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using Unity.Jobs;

public class TestSelectionEntity : SystemBase
{
    private float3 startPosition;
    //private float3 endPosition;

    public Ray ray;
    // Start is called before the first frame update
    protected override void OnCreate()
    {
        base.OnCreate();
    }

    protected override void OnStartRunning()
    {
        base.OnStartRunning();
    }

    // Update is called once per frame
    protected override void OnUpdate()
    {


        if(Input.GetMouseButtonDown(0))
        {
            startPosition = TestUtils.GetMouseWorldPosition();
            Debug.Log(startPosition);
        }

        if(Input.GetMouseButtonUp(0))
        {
            float3 endPosition = TestUtils.GetMouseWorldPosition();

            float3 lowerLeftPosition = new float3(math.min(startPosition.x, endPosition.x), 0, math.min(startPosition.z, endPosition.z));
            float3 upperRightPosition = new float3(math.max(startPosition.x, endPosition.x), 0, math.max(startPosition.z, endPosition.z));
            Debug.Log(endPosition);
            Entities.ForEach((Entity entity, ref Translation translation) =>
            {
                float3 entityPosition = translation.Value;
                if (entityPosition.x >= lowerLeftPosition.x &&
                   entityPosition.z >= lowerLeftPosition.z &&
                   entityPosition.x <= upperRightPosition.x &&
                   entityPosition.z <= upperRightPosition.z)
                {
                    Debug.Log(entity);
                }

            })
                .WithoutBurst()
                //.Run();
                .ScheduleParallel();

        }
    }
}
