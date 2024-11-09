using Unity.Entities;
using Unity.Mathematics;

public struct MovementComponentData : IComponentData
{
    public float moveSpeed;
    public float3 movementVector;
}
