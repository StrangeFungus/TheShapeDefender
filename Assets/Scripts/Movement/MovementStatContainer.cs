namespace ShapeDefender
{
    public enum ZoneMovementType
    {
        Ground, SurfaceWater, Underwater, Flying, SpaceTravel
    }

    namespace MovementSystem
    {
        using UnityEngine;

        public class MovementStatContainer : MonoBehaviour
        {
            [SerializeField] private MovementStatSO movementStatSOTemplate;
            [HideInInspector] public MovementStatSO runtimeMovementStats;

            private Rigidbody2D hostsRigidbody2D;

            private ZoneMovementType zoneMovementType;
            private float currentMovementSpeed = 0f;
            private float accelerationSpeed = 0f;
            private float turningSpeed = 0f;
            private float brakingSpeed = 0f;

            private void Awake()
            {
                if (movementStatSOTemplate != null)
                {
                    runtimeMovementStats = Instantiate(movementStatSOTemplate);
                }

                hostsRigidbody2D = GetComponent<Rigidbody2D>();
                SetNewZoneMovementType(ZoneMovementType.Ground); // Default Until I get the zones set up to swap it out as well as check if the object has the movement type.
            }

            // We need to have a step before this that will get checked to see if we can even enter into a zone and if not stop all movement at the boarder. 
            // Zone Trigger gets hit by entity, check if can enter, if not stop and bounce away from trigger zone.

            public void SetNewZoneMovementType(ZoneMovementType newZoneMovementType)
            {
                zoneMovementType = newZoneMovementType;
                UpdateMovementStats();
            }

            public void UpdateMovementStats()
            {
                switch (zoneMovementType)
                {
                    case ZoneMovementType.Ground:
                        currentMovementSpeed = runtimeMovementStats.groundSpeed.StatValue;
                        accelerationSpeed = runtimeMovementStats.groundAccelerationSpeed.StatValue;
                        turningSpeed = runtimeMovementStats.groundTurningSpeed.StatValue;
                        brakingSpeed = runtimeMovementStats.groundBrakingSpeed.StatValue;
                        break;
                    case ZoneMovementType.SurfaceWater:
                        currentMovementSpeed = runtimeMovementStats.surfaceWaterSpeed.StatValue;
                        accelerationSpeed = runtimeMovementStats.surfaceWaterAccelerationSpeed.StatValue;
                        turningSpeed = runtimeMovementStats.surfaceWaterTurningSpeed.StatValue;
                        brakingSpeed = runtimeMovementStats.surfaceWaterBrakingSpeed.StatValue;
                        break;
                    case ZoneMovementType.Underwater:
                        currentMovementSpeed = runtimeMovementStats.underwaterSpeed.StatValue;
                        accelerationSpeed = runtimeMovementStats.underwaterAccelerationSpeed.StatValue;
                        turningSpeed = runtimeMovementStats.underwaterTurningSpeed.StatValue;
                        brakingSpeed = runtimeMovementStats.underwaterBrakingSpeed.StatValue;
                        break;
                    case ZoneMovementType.Flying:
                        currentMovementSpeed = runtimeMovementStats.flyingSpeed.StatValue;
                        accelerationSpeed = runtimeMovementStats.flyingAccelerationSpeed.StatValue;
                        turningSpeed = runtimeMovementStats.flyingTurningSpeed.StatValue;
                        brakingSpeed = runtimeMovementStats.flyingBrakingSpeed.StatValue;
                        break;
                    case ZoneMovementType.SpaceTravel:
                        currentMovementSpeed = runtimeMovementStats.spaceTravelSpeed.StatValue;
                        accelerationSpeed = runtimeMovementStats.spaceTravelAccelerationSpeed.StatValue;
                        turningSpeed = runtimeMovementStats.spaceTravelTurningSpeed.StatValue;
                        brakingSpeed = runtimeMovementStats.spaceTravelBrakingSpeed.StatValue;
                        break;
                }
            }

            public void Move(Vector2 movementInput)
            {
                if (hostsRigidbody2D == null) return;

                float targetSpeed = movementInput.magnitude * currentMovementSpeed;
                float currentSpeed = hostsRigidbody2D.linearVelocity.magnitude;
                float accelRate = (movementInput.sqrMagnitude > 0.01f) ? accelerationSpeed : brakingSpeed;
                currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, accelRate * Time.fixedDeltaTime);

                if (movementInput.sqrMagnitude > 0.01f)
                {
                    float targetAngle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg - 90f;
                    float smoothedAngle = Mathf.LerpAngle(hostsRigidbody2D.rotation, targetAngle, turningSpeed * Time.fixedDeltaTime);
                    hostsRigidbody2D.SetRotation(smoothedAngle);
                }

                Vector2 forwardDirection = hostsRigidbody2D.transform.up;
                hostsRigidbody2D.linearVelocity = forwardDirection * currentSpeed;
            }
        }
    }
}