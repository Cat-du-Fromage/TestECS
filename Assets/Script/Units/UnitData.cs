using Unity.Entities;
using UnityEngine;

public struct UnitData : IComponentData
{
    public Entity Prefab;
}

public class SimpleMeshRenderer : IComponentData
{
    public Mesh Mesh;
    public Material Material;
}
