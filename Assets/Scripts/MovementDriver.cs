using UnityEngine;

[System.Serializable]
public class MovementDriver : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 0f;

    private Rigidbody2D hostsRigidbody2D;

    private void Awake()
    {
        hostsRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 movementInputs)
    {
        hostsRigidbody2D.linearVelocity = movementInputs * movementSpeed;

        if (movementInputs != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movementInputs.y, movementInputs.x) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(hostsRigidbody2D.rotation, targetAngle - 90f, movementSpeed * Time.fixedDeltaTime);
            hostsRigidbody2D.SetRotation(angle);
        }
    }
}
