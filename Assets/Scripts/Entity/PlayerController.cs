namespace ShapeDefender
{
    namespace EntitySystem
    {
        using ShapeDefender.UI;
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

                if (Keyboard.current.oKey.wasPressedThisFrame)
                {
                    RespawnScreenManager.Instance.ToggleRespawnScreen();
                }
            }

            private void OnDisable()
            {
                RespawnScreenManager.Instance.ToggleRespawnScreen();
            }

            private void FixedUpdate()
            {
                entitiesMovementStatContainer.Move(movementInput);
            }
        }
    }
}