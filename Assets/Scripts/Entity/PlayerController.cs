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
                    gameObject.SetActive(false);
                }
                if (Keyboard.current.numpad1Key.wasPressedThisFrame)
                {
                    Time.timeScale = 1.0f;
                }
                if (Keyboard.current.numpad3Key.wasPressedThisFrame)
                {
                    Time.timeScale = 3.0f;
                }
                if (Keyboard.current.numpad5Key.wasPressedThisFrame)
                {
                    Time.timeScale = 5.0f;
                }
                if (Keyboard.current.numpad9Key.wasPressedThisFrame)
                {
                    Time.timeScale = 9.0f;
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