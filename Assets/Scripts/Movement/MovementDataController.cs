namespace ShapeDefender
{
    namespace MovementSystem
    {
        using UnityEngine;

        public class MovementDataController : MonoBehaviour
        {
            [Header("Movement Data")]
            [SerializeField] private MovementData movementDataTemplate;
            private MovementData runtimeMovementData;
            public MovementData RuntimeMovementData { get { return runtimeMovementData; } }

            private Rigidbody2D hostsRigidbody2D;

            private float currentMovementSpeed = 0f;
            private float accelerationSpeed = 0f;
            private float turningSpeed = 0f;
            private float brakingSpeed = 0f;

            public enum ZoneMovementType
            {
                Ground, SurfaceWater, Underwater, Flying, Space
            }

            private ZoneMovementType zoneMovementType;

            private void Awake()
            {
                if (movementDataTemplate != null)
                {
                    runtimeMovementData = Instantiate(movementDataTemplate);
                }

                hostsRigidbody2D = GetComponent<Rigidbody2D>();
                SetNewZoneMovementType(ZoneMovementType.Ground);
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
                        currentMovementSpeed = runtimeMovementData.GroundSpeed;
                        accelerationSpeed = runtimeMovementData.GroundAccelerationSpeed;
                        turningSpeed = runtimeMovementData.GroundTurningSpeed;
                        brakingSpeed = runtimeMovementData.GroundBrakingSpeed;
                        break;
                    case ZoneMovementType.SurfaceWater:
                        currentMovementSpeed = runtimeMovementData.SurfaceWaterSpeed;
                        accelerationSpeed = runtimeMovementData.SurfaceWaterAccelerationSpeed;
                        turningSpeed = runtimeMovementData.SurfaceWaterTurningSpeed;
                        brakingSpeed = runtimeMovementData.SurfaceWaterBrakingSpeed;
                        break;
                    case ZoneMovementType.Underwater:
                        currentMovementSpeed = runtimeMovementData.UnderwaterSpeed;
                        accelerationSpeed = runtimeMovementData.UnderwaterAccelerationSpeed;
                        turningSpeed = runtimeMovementData.UnderwaterTurningSpeed;
                        brakingSpeed = runtimeMovementData.UnderwaterBrakingSpeed;
                        break;
                    case ZoneMovementType.Flying:
                        currentMovementSpeed = runtimeMovementData.FlyingSpeed;
                        accelerationSpeed = runtimeMovementData.FlyingAccelerationSpeed;
                        turningSpeed = runtimeMovementData.FlyingTurningSpeed;
                        brakingSpeed = runtimeMovementData.FlyingBrakingSpeed;
                        break;
                    case ZoneMovementType.Space:
                        currentMovementSpeed = runtimeMovementData.SpaceTravelSpeed;
                        accelerationSpeed = runtimeMovementData.SpaceTravelAccelerationSpeed;
                        turningSpeed = runtimeMovementData.SpaceTravelTurningSpeed;
                        brakingSpeed = runtimeMovementData.SpaceTravelBrakingSpeed;
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

            public void UnlockGroundSpeed(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeMovementData.groundSpeedUnlockCost)
                {
                    callersExperiencePoints -= runtimeMovementData.groundSpeedUnlockCost;
                    runtimeMovementData.canLevelUpGroundSpeedStats = true;
                }
            }

            public void UnlockSurfaceWaterSpeed(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeMovementData.surfaceWaterSpeedUnlockCost)
                {
                    callersExperiencePoints -= runtimeMovementData.surfaceWaterSpeedUnlockCost;
                    runtimeMovementData.canLevelUpSurfaceWaterSpeedStats = true;
                }
            }

            public void UnlockUnderwaterSpeed(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeMovementData.underwaterSpeedUnlockCost)
                {
                    callersExperiencePoints -= runtimeMovementData.underwaterSpeedUnlockCost;
                    runtimeMovementData.canLevelUpUnderwaterSpeedStats = true;
                }
            }

            public void UnlockFlyingSpeed(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeMovementData.flyingSpeedUnlockCost)
                {
                    callersExperiencePoints -= runtimeMovementData.flyingSpeedUnlockCost;
                    runtimeMovementData.canLevelUpFlyingSpeedStats = true;
                }
            }

            public void UnlockSpaceTravelSpeed(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeMovementData.spaceTravelSpeedUnlockCost)
                {
                    callersExperiencePoints -= runtimeMovementData.spaceTravelSpeedUnlockCost;
                    runtimeMovementData.canLevelUpSpaceTravelSpeedStats = true;
                }
            }

            public void LevelUpGroundSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpGroundSpeedStats || callersExperiencePoints < runtimeMovementData.groundSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.groundSpeedExpCost;
                    runtimeMovementData.groundSpeedLevel++;
                }
            }

            public void LevelUpGroundTurningSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpGroundSpeedStats || callersExperiencePoints < runtimeMovementData.groundTurningSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.groundTurningSpeedExpCost;
                    runtimeMovementData.groundTurningSpeedLevel++;
                }
            }

            public void LevelUpGroundAccelerationSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpGroundSpeedStats || callersExperiencePoints < runtimeMovementData.groundAccelerationSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.groundAccelerationSpeedExpCost;
                    runtimeMovementData.groundAccelerationSpeedLevel++;
                }
            }

            public void LevelUpGroundBrakingSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpGroundSpeedStats || callersExperiencePoints < runtimeMovementData.groundBrakingSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.groundBrakingSpeedExpCost;
                    runtimeMovementData.groundBrakingSpeedLevel++;
                }
            }

            public void LevelUpSurfaceWaterSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpSurfaceWaterSpeedStats || callersExperiencePoints < runtimeMovementData.surfaceWaterAccelerationSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.surfaceWaterAccelerationSpeedExpCost;
                    runtimeMovementData.surfaceWaterAccelerationSpeedLevel++;
                }
            }

            public void LevelUpSurfaceWaterTurningSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpSurfaceWaterSpeedStats || callersExperiencePoints < runtimeMovementData.SurfaceWaterTurningSpeed) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.SurfaceWaterTurningSpeed;
                    runtimeMovementData.surfaceWaterTurningSpeedLevel++;
                }
            }

            public void LevelUpSurfaceWaterAccelerationSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpSurfaceWaterSpeedStats || callersExperiencePoints < runtimeMovementData.surfaceWaterAccelerationSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.surfaceWaterAccelerationSpeedExpCost;
                    runtimeMovementData.surfaceWaterAccelerationSpeedLevel++;
                }
            }

            public void LevelUpSurfaceWaterBrakingSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpSurfaceWaterSpeedStats || callersExperiencePoints < runtimeMovementData.surfaceWaterBrakingSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.surfaceWaterBrakingSpeedExpCost;
                    runtimeMovementData.surfaceWaterBrakingSpeedLevel++;
                }
            }

            public void LevelUpUnderwaterSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpUnderwaterSpeedStats || callersExperiencePoints < runtimeMovementData.underwaterSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.underwaterSpeedExpCost;
                    runtimeMovementData.underwaterSpeedLevel++;
                }
            }

            public void LevelUpUnderwaterTurningSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpUnderwaterSpeedStats || callersExperiencePoints < runtimeMovementData.underwaterTurningSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.underwaterTurningSpeedExpCost;
                    runtimeMovementData.underwaterTurningSpeedLevel++;
                }
            }

            public void LevelUpUnderwaterAccelerationSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpUnderwaterSpeedStats || callersExperiencePoints < runtimeMovementData.underwaterAccelerationSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.underwaterAccelerationSpeedExpCost;
                    runtimeMovementData.underwaterAccelerationSpeedLevel++;
                }
            }

            public void LevelUpUnderwaterBrakingSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpUnderwaterSpeedStats || callersExperiencePoints < runtimeMovementData.underwaterBrakingSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.underwaterBrakingSpeedExpCost;
                    runtimeMovementData.underwaterBrakingSpeedLevel++;
                }
            }

            public void LevelUpFlyingSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpFlyingSpeedStats || callersExperiencePoints < runtimeMovementData.flyingSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.flyingSpeedExpCost;
                    runtimeMovementData.flyingSpeedLevel++;
                }
            }

            public void LevelUpFlyingTurningSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpFlyingSpeedStats || callersExperiencePoints < runtimeMovementData.flyingTurningSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.flyingTurningSpeedExpCost;
                    runtimeMovementData.flyingTurningSpeedLevel++;
                }
            }

            public void LevelUpFlyingAccelerationSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpFlyingSpeedStats || callersExperiencePoints < runtimeMovementData.flyingAccelerationSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.flyingAccelerationSpeedExpCost;
                    runtimeMovementData.flyingAccelerationSpeedLevel++;
                }
            }

            public void LevelUpFlyingBrakingSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpFlyingSpeedStats || callersExperiencePoints < runtimeMovementData.flyingBrakingSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.flyingBrakingSpeedExpCost;
                    runtimeMovementData.flyingBrakingSpeedLevel++;
                }
            }

            public void LevelUpSpaceTravelSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpSpaceTravelSpeedStats || callersExperiencePoints < runtimeMovementData.spaceTravelSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.spaceTravelSpeedExpCost;
                    runtimeMovementData.spaceTravelSpeedLevel++;
                }
            }

            public void LevelUpSpaceTravelTurningSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpSpaceTravelSpeedStats || callersExperiencePoints < runtimeMovementData.spaceTravelTurningSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.spaceTravelTurningSpeedExpCost;
                    runtimeMovementData.spaceTravelTurningSpeedLevel++;
                }
            }

            public void LevelUpSpaceTravelAccelerationSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpSpaceTravelSpeedStats || callersExperiencePoints < runtimeMovementData.spaceTravelAccelerationSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.spaceTravelAccelerationSpeedExpCost;
                    runtimeMovementData.spaceTravelAccelerationSpeedLevel++;
                }
            }

            public void LevelUpSpaceTravelBrakingSpeed(ref float callersExperiencePoints)
            {
                if (!runtimeMovementData.canLevelUpSpaceTravelSpeedStats || callersExperiencePoints < runtimeMovementData.spaceTravelBrakingSpeedExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeMovementData.spaceTravelBrakingSpeedExpCost;
                    runtimeMovementData.spaceTravelBrakingSpeedLevel++;
                }
            }
        }
    }
}