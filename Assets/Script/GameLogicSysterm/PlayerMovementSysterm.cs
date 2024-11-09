using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

partial struct MovementSysterm : ISystem
{
    private EntityManager entityManager;
    private EntityQuery playerEntityQuery;
    private Entity gunEntity;
    private LocalTransform playerGunEntityLocalTransform;
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        entityManager = state.EntityManager;
        playerEntityQuery = entityManager.CreateEntityQuery(
            new EntityQueryBuilder(Allocator.Temp)
            .WithAll<MovementComponentData>()
            .WithAll<RotationComponentData>()
            .WithAllRW<LocalTransform>());
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (gunEntity == null)
        {
            EntityQuery query = entityManager.CreateEntityQuery(new EntityQueryBuilder(Allocator.Temp).WithAllRW<LocalTransform>());
            query.SetSharedComponentFilter(new LayerShareComponentData { LayerIndex = 2 });
            NativeArray<Entity> gunEntitys = playerEntityQuery.ToEntityArray(Allocator.Temp);
            if(gunEntitys.Length == 0) return;
            gunEntity = gunEntitys[0];
            playerGunEntityLocalTransform = entityManager.GetComponentData<LocalTransform>(gunEntity);
            return;
        }
        else
        {
            MovementJob movementJob = new()
            {
                deltatime = SystemAPI.Time.DeltaTime,
            };
            JobHandle movementHandle = movementJob.ScheduleParallel(state.Dependency);
            RotationJob rotationJob = new()
            {
                deltatime = SystemAPI.Time.DeltaTime,
                gunTransform = playerGunEntityLocalTransform,
            };
            state.Dependency = rotationJob.ScheduleParallel(movementHandle);
            
        }
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
