namespace ShapeDefender
{
    namespace LevelUpSystem
    {
        using ShapeDefender.DefenseSystem;
        using ShapeDefender.HealthSystem;
        using ShapeDefender.MovementSystem;
        using UnityEngine;

        [System.Serializable]
        public class LevelUpManager : MonoBehaviour
        {
            public static LevelUpManager Instance;

            private void Awake()
            {
                if (Instance == null)
                {
                    Instance = this;
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            // HEALTH RELATED UNLOCK AND LEVEL UP FUNCTIONS
            public void UnlockHealthRegenAmount(ref float callersExperiencePoints, HealthDataController callersHealthDataController)
            {
                callersHealthDataController.UnlockHealthRegenAmount(ref callersExperiencePoints);
            }

            public void UnlockMaximumEnergyShield(ref float callersExperiencePoints, HealthDataController callersHealthDataController)
            {
                callersHealthDataController.UnlockMaximumEnergyShield(ref callersExperiencePoints);
            }

            public void UnlockEnergyShieldRegenAmount(ref float callersExperiencePoints, HealthDataController callersHealthDataController)
            {
                callersHealthDataController.UnlockEnergyShieldRegenAmount(ref callersExperiencePoints);
            }

            public void LevelUpMaximumHealth(ref float callersExperiencePoints, HealthDataController callersHealthDataController)
            {
                callersHealthDataController.LevelUpMaximumHealth(ref callersExperiencePoints);
            }

            public void LevelUpHealthRegenAmount(ref float callersExperiencePoints, HealthDataController callersHealthDataController)
            {
                callersHealthDataController.LevelUpHealthRegenAmount(ref callersExperiencePoints);
            }

            public void LevelUpHealthRegenCooldown(ref float callersExperiencePoints, HealthDataController callersHealthDataController)
            {
                callersHealthDataController.LevelUpHealthRegenCooldown(ref callersExperiencePoints);
            }

            public void LevelUpMaximumEnergyShield(ref float callersExperiencePoints, HealthDataController callersHealthDataController)
            {
                callersHealthDataController.LevelUpMaximumEnergyShield(ref callersExperiencePoints);
            }

            public void LevelUpEnergyShieldRegenAmount(ref float callersExperiencePoints, HealthDataController callersHealthDataController)
            {
                callersHealthDataController.LevelUpEnergyShieldRegenAmount(ref callersExperiencePoints);
            }

            public void LevelUpEnergyShieldRegenCooldown(ref float callersExperiencePoints, HealthDataController callersHealthDataController)
            {
                callersHealthDataController.LevelUpEnergyShieldRegenCooldown(ref callersExperiencePoints);
            }

            // DEFENSE RELATED UNLOCK AND LEVEL UP FUNCTIONS
            public void UnlockParryChance(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.UnlockParryChance(ref callersExperiencePoints);
            }

            public void UnlockCounterAttackChance(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.UnlockCounterAttackChance(ref callersExperiencePoints);
            }

            public void UnlockBlockChance(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.UnlockBlockChance(ref callersExperiencePoints);
            }

            public void UnlockBlockAmount(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.UnlockBlockAmount(ref callersExperiencePoints);
            }

            public void UnlockReflectAttackChance(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.UnlockReflectAttackChance(ref callersExperiencePoints);
            }

            public void UnlockReflectAttackAngle(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.UnlockReflectAttackAngle(ref callersExperiencePoints);
            }

            public void UnlockDodgeChance(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.UnlockDodgeChance(ref callersExperiencePoints);
            }

            public void UnlockArmorValue(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.UnlockArmorValue(ref callersExperiencePoints);
            }

            public void UnlockThornsAmount(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.UnlockThornsAmount(ref callersExperiencePoints);
            }

            public void UnlockCriticalHitChanceResist(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.UnlockCriticalHitChanceResist(ref callersExperiencePoints);
            }

            public void UnlockCriticalHitDamageResist(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.UnlockCriticalHitDamageResist(ref callersExperiencePoints);
            }

            public void UnlockStatusEffectResist(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.UnlockStatusEffectResist(ref callersExperiencePoints);
            }

            public void UnlockPhysicalDamageResist(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.UnlockPhysicalDamageResist(ref callersExperiencePoints);
            }

            public void UnlockMagicalDamageResist(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.UnlockMagicalDamageResist(ref callersExperiencePoints);
            }

            public void UnlockEnergyDamageResist(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.UnlockEnergyDamageResist(ref callersExperiencePoints);
            }

            public void LevelUpParryChance(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpParryChance(ref callersExperiencePoints);
            }

            public void LevelUpParryCooldown(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpParryCooldown(ref callersExperiencePoints);
            }

            public void LevelUpCounterAttackChance(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpCounterAttackChance(ref callersExperiencePoints);
            }

            public void LevelUpBlockChance(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpBlockChance(ref callersExperiencePoints);
            }

            public void LevelUpBlockCooldown(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpBlockCooldown(ref callersExperiencePoints);
            }

            public void LevelUpBlockAmount(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpBlockAmount(ref callersExperiencePoints);
            }

            public void LevelUpReflectAttackChance(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpReflectAttackChance(ref callersExperiencePoints);
            }

            public void LevelUpReflectAttackAngle(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpReflectAttackAngle(ref callersExperiencePoints);
            }

            public void LevelUpDodgeChance(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpDodgeChance(ref callersExperiencePoints);
            }

            public void LevelUpDodgeCooldown(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpDodgeCooldown(ref callersExperiencePoints);
            }

            public void LevelUpArmorValue(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpArmorValue(ref callersExperiencePoints);
            }

            public void LevelUpThornsAmount(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpThornsAmount(ref callersExperiencePoints);
            }

            public void LevelUpCriticalHitChanceResist(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpCriticalHitChanceResist(ref callersExperiencePoints);
            }

            public void LevelUpCriticalHitDamageResist(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpCriticalHitDamageResist(ref callersExperiencePoints);
            }

            public void LevelUpStatusEffectResist(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpStatusEffectResist(ref callersExperiencePoints);
            }

            public void LevelUpPhysicalDamageResist(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpPhysicalDamageResist(ref callersExperiencePoints);
            }

            public void LevelUpMagicalDamageResist(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpMagicalDamageResist(ref callersExperiencePoints);
            }

            public void LevelUpEnergyDamageResist(ref float callersExperiencePoints, DefenseDataController callersDefenseDataController)
            {
                callersDefenseDataController.LevelUpEnergyDamageResist(ref callersExperiencePoints);
            }

            // MOVEMENT RELATED UNLOCK AND LEVEL UP FUNCTIONS
            public void UnlockGroundSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.UnlockGroundSpeed(ref callersExperiencePoints);
            }

            public void UnlockSurfaceWaterSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.UnlockSurfaceWaterSpeed(ref callersExperiencePoints);
            }

            public void UnlockUnderwaterSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.UnlockUnderwaterSpeed(ref callersExperiencePoints);
            }

            public void UnlockFlyingSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.UnlockFlyingSpeed(ref callersExperiencePoints);
            }

            public void UnlockSpaceTravelSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.UnlockSpaceTravelSpeed(ref callersExperiencePoints);
            }

            public void LevelUpGroundSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpGroundSpeed(ref callersExperiencePoints);
            }

            public void LevelUpGroundTurningSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpGroundTurningSpeed(ref callersExperiencePoints);
            }

            public void LevelUpGroundAccelerationSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpGroundAccelerationSpeed(ref callersExperiencePoints);
            }

            public void LevelUpGroundBrakingSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpGroundBrakingSpeed(ref callersExperiencePoints);
            }

            public void LevelUpSurfaceWaterSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpSurfaceWaterSpeed(ref callersExperiencePoints);
            }

            public void LevelUpSurfaceWaterTurningSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpSurfaceWaterTurningSpeed(ref callersExperiencePoints);
            }

            public void LevelUpSurfaceWaterAccelerationSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpSurfaceWaterAccelerationSpeed(ref callersExperiencePoints);
            }

            public void LevelUpSurfaceWaterBrakingSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpSurfaceWaterBrakingSpeed(ref callersExperiencePoints);
            }

            public void LevelUpUnderwaterSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpUnderwaterSpeed(ref callersExperiencePoints);
            }

            public void LevelUpUnderwaterTurningSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpUnderwaterTurningSpeed(ref callersExperiencePoints);
            }

            public void LevelUpUnderwaterAccelerationSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpUnderwaterAccelerationSpeed(ref callersExperiencePoints);
            }

            public void LevelUpUnderwaterBrakingSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpUnderwaterBrakingSpeed(ref callersExperiencePoints);
            }

            public void LevelUpFlyingSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpFlyingSpeed(ref callersExperiencePoints);
            }

            public void LevelUpFlyingTurningSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpFlyingTurningSpeed(ref callersExperiencePoints);
            }

            public void LevelUpFlyingAccelerationSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpFlyingAccelerationSpeed(ref callersExperiencePoints);
            }

            public void LevelUpFlyingBrakingSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpFlyingBrakingSpeed(ref callersExperiencePoints);
            }

            public void LevelUpSpaceTravelSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpSpaceTravelSpeed(ref callersExperiencePoints);
            }

            public void LevelUpSpaceTravelTurningSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpSpaceTravelTurningSpeed(ref callersExperiencePoints);
            }

            public void LevelUpSpaceTravelAccelerationSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpSpaceTravelAccelerationSpeed(ref callersExperiencePoints);
            }

            public void LevelUpSpaceTravelBrakingSpeed(ref float callersExperiencePoints, MovementDataController callersMovementDataController)
            {
                callersMovementDataController.LevelUpSpaceTravelBrakingSpeed(ref callersExperiencePoints);
            }
        }
    }
}