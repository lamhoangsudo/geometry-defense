using Unity.Entities;
using Unity.Mathematics;

public struct RotationComponentData : IComponentData
{
    public float3 vectorDirection;
    public float rotationSpeed;
}
