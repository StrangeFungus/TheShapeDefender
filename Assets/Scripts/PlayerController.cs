using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerController : BaseEntity
{
    [SerializeField] private InputAction movementAction;
    private Vector2 movementInput;

    [SerializeField] private float experienceGainMultiplier = 0;
    [SerializeField] private float constructionPointsMultiplier = 0;

    private new void Awake()
    {
        movementAction.Enable();
        base.Awake();
    }

    private new void Update()
    {
        movementInput = movementAction.ReadValue<Vector2>();
        base.Update();
    }

    private void FixedUpdate()
    {
        movementDriver.Move(movementInput);
    }
}
