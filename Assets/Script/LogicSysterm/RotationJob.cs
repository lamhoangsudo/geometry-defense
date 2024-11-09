using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct RotationJob : IJobEntity
{
    public float deltatime;
    public LocalTransform gunTransform;
    public void Execute(ref LocalTransform localTransform, in RotationComponentData rotationComponentData)
    {
        float3 vectorDirection = rotationComponentData.vectorDirection - localTransform.Position;
        if (vectorDirection.Equals(float3.zero)) return;
        float angle = Mathf.Atan2(rotationComponentData.vectorDirection.x, rotationComponentData.vectorDirection.y) * Mathf.Rad2Deg;
        localTransform.Rotation = Quaternion.Lerp(localTransform.Rotation, Quaternion.Euler(0, 0, -angle), deltatime * rotationComponentData.rotationSpeed);
    }
}
