using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

class PlayerMovementAth : MonoBehaviour
{
    public float moveSpeed; 
}

class PlayerMovementAthBaker : Baker<PlayerMovementAth>
{
    public override void Bake(PlayerMovementAth authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new MovementComponentData
        {
            moveSpeed = authoring.moveSpeed,
            movementVector = float3.zero,
        });
    }
}
