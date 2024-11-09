using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class GameInputSysterm : MonoBehaviour
{
    public static GameInputSysterm Instance;
    private GameInput inputActions;
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        inputActions = new();
        inputActions.Player.Enable();
    }
    public float2 GetVectorMovementNormalized()
    {
        return inputActions.Player.Movement.ReadValue<Vector2>().normalized;
    }
    private void OnDestroy()
    {
        inputActions.Dispose();
    }
}
