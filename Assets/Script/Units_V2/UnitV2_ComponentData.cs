using Unity.Entities;

using Unity.Mathematics;
[GenerateAuthoringComponent]
public struct UnitV2_ComponentData : IComponentData
{
    public Entity Prefab;
    public int Health;
    public int CombatMelee;
    public int CombatRange;
    public float Speed;
    public float3 Position;
    public int RegimentId;
}
