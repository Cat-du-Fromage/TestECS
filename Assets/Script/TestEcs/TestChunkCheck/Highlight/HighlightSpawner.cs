using Unity.Entities;
using UnityEngine;
using System;

[GenerateAuthoringComponent]
public struct HighlightSpawner : IComponentData
{
    public Entity Prefab;
}
