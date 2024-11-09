using Unity.Entities;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GameEntityManagerSysterm : MonoBehaviour
{
    public static GameEntityManagerSysterm Instance;
    public EntityManager entityManager {  get; private set; }
    private EntityQuery entityQuery;
    private Entity playerEntity;
    private Entity playerGunEntity;
    private void Awake()
    {
        if(Instance == null) Instance = this;
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }
    private void Start()
    {
        playerEntity = GetEntitiesByComponentData(typeof(MovementComponentData))[0];
        playerGunEntity = GetEntitiesByComponentData(typeof(RotationComponentData))[0];
    }
    public Entity[] GetEntitiesByQuery(EntityQueryBuilder entityQueryBuilder)
    {
        entityQuery = entityManager.CreateEntityQuery(entityQueryBuilder);
        Entity[] entities = entityQuery.ToEntityArray(Unity.Collections.Allocator.Temp).ToArray();
        if (entities.IsUnityNull() || entities.Length == 0) return null;
        return entities;
    }
    public Entity[] GetEntitiesByComponentData(params ComponentType[] componentTypes)
    {
        entityQuery = entityManager.CreateEntityQuery(componentTypes);
        Entity[] entities = entityQuery.ToEntityArray(Unity.Collections.Allocator.Temp).ToArray();
        if (entities.IsUnityNull() || entities.Length == 0) return null;
        return entities;
    }
    private void Update()
    {
        if(playerEntity == null) return;
        UpdatePlayerDataMovementCompomentData(GameInputSysterm.Instance.GetVectorMovementNormalized());
        if(playerGunEntity == null) return;
        UpdateUpdatePlayerDataRotationCompomentData(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    private void UpdatePlayerDataMovementCompomentData(Vector2 vectorMovement)
    {
        MovementComponentData movementComponentData = entityManager.GetComponentData<MovementComponentData>(playerEntity);
        movementComponentData.movementVector = new(vectorMovement.x, vectorMovement.y, 0);
        entityManager.SetComponentData(playerEntity, movementComponentData);
    }
    private void UpdateUpdatePlayerDataRotationCompomentData(Vector3 mouseWorldPosition)
    {
        RotationComponentData rotationComponentData = entityManager.GetComponentData<RotationComponentData>(playerGunEntity);
        rotationComponentData.vectorDirection = new float3(mouseWorldPosition.x, mouseWorldPosition.y, 0);
        entityManager.SetComponentData(playerGunEntity, rotationComponentData);
    }
}
