using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

class PlayerRotationAth : MonoBehaviour
{
    public float rotationSpeed;
}

class PlayerRotationAthBaker : Baker<PlayerRotationAth>
{
    public override void Bake(PlayerRotationAth authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new RotationComponentData
        {
            rotationSpeed = authoring.rotationSpeed,
            vectorDirection = float3.zero,
        });
    }
}
