using Unity.Entities;

[GenerateAuthoringComponent]
public struct RegimentData : IComponentData
{
    public int Health;
    public int RegimentSize;
}
