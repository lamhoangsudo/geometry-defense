using Unity.Entities;
using UnityEngine;

public class LayerShareComponentDataAuthoring : MonoBehaviour
{
    public int LayerIndex;
}

public class LayerShareComponentDataBaker : Baker<LayerShareComponentDataAuthoring>
{
    public override void Bake(LayerShareComponentDataAuthoring authoring)
    {
        var e = GetEntity(authoring, TransformUsageFlags.None);
        AddSharedComponent(e, new LayerShareComponentData
        {
            LayerIndex = authoring.LayerIndex,
        });
    }
}