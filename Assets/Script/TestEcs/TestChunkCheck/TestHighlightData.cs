using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

[GenerateAuthoringComponent]
public class MaterialChanger : IComponentData
{
    public UnityEngine.Material material0;
    public UnityEngine.Material material1;
    /*
    public uint frequency;
    public uint frame;
    public uint active;
    */
}

public class MaterialChangerSystem : SystemBase
{
    EntityManager _entityManager;
    protected override void OnCreate()
    {
        base.OnCreate();
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }
    protected override void OnUpdate()
    {
        Entities.WithStructuralChanges().
            ForEach((Entity ent, MaterialChanger changer, in RenderMesh mesh) =>
            {
                RenderMesh modifiedMesh = mesh;
                modifiedMesh.material = _entityManager.HasComponent<SelectedUnit>(ent) == false ? changer.material0 : changer.material1;
                _entityManager.SetSharedComponentData<RenderMesh>(ent, modifiedMesh);
            }).Run();
    }
}