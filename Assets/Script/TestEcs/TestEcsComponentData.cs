using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct TestEcsComponentData : IComponentData
{
    public float4x4 Value;
}
