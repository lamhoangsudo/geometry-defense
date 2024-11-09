using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct MovementJob : IJobEntity
{
    public float deltatime;
    public void Execute(ref LocalTransform localTransform, in MovementComponentData movementComponentData)
    {
        localTransform.Position += movementComponentData.movementVector * movementComponentData.moveSpeed * deltatime;
    }
}
