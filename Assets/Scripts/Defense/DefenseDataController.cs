namespace ShapeDefender
{
    namespace DefenseSystem
    {
        using UnityEngine;

        public class DefenseDataController : MonoBehaviour
        {
            [Header("Defense Data")]
            [SerializeField] private DefenseData defenseDataTemplate;
            private DefenseData runtimeDefenseData;
            public DefenseData RuntimeDefenseData { get { return runtimeDefenseData; } }

            private float currentParryCooldown = 0f;
            private float currentBlockCooldown = 0f;
            private float currentDodgeCooldown = 0f;

            private void Awake()
            {
                if (defenseDataTemplate != null)
                {
                    runtimeDefenseData = Instantiate(defenseDataTemplate);
                    currentParryCooldown = runtimeDefenseData.ParryCooldown;
                    currentBlockCooldown = runtimeDefenseData.BlockCooldown;
                    currentDodgeCooldown = runtimeDefenseData.DodgeCooldown;
                }
            }

            private void Update()
            {
                if (runtimeDefenseData == null) { return; }

                currentParryCooldown -= Time.deltaTime;
                currentBlockCooldown -= Time.deltaTime;
                currentDodgeCooldown -= Time.deltaTime;
            }

            public void ResetParryCooldown()
            {
                currentParryCooldown = runtimeDefenseData.ParryCooldown;
            }

            public void ResetBlockCooldown()
            {
                currentBlockCooldown = runtimeDefenseData.BlockCooldown;
            }

            public void ResetDodgeCooldown()
            {
                currentDodgeCooldown = runtimeDefenseData.DodgeCooldown;
            }

            public void UnlockParryChance(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeDefenseData.parryChanceUnlockCost)
                {
                    callersExperiencePoints -= runtimeDefenseData.parryChanceUnlockCost;
                    runtimeDefenseData.canLevelUpParryChance = true;
                    ResetParryCooldown();
                }
            }

            public void UnlockCounterAttackChance(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeDefenseData.counterAttackChanceUnlockCost)
                {
                    callersExperiencePoints -= runtimeDefenseData.counterAttackChanceUnlockCost;
                    runtimeDefenseData.canLevelUpCounterAttackChance = true;
                }
            }

            public void UnlockBlockChance(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeDefenseData.blockChanceUnlockCost)
                {
                    callersExperiencePoints -= runtimeDefenseData.blockChanceUnlockCost;
                    runtimeDefenseData.canLevelUpBlockChance = true;
                    ResetBlockCooldown();
                }
            }

            public void UnlockBlockAmount(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeDefenseData.blockAmountUnlockCost)
                {
                    callersExperiencePoints -= runtimeDefenseData.blockAmountUnlockCost;
                    runtimeDefenseData.canLevelUpBlockAmount = true;
                }
            }

            public void UnlockReflectAttackChance(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeDefenseData.reflectAttackChanceUnlockCost)
                {
                    callersExperiencePoints -= runtimeDefenseData.reflectAttackChanceUnlockCost;
                    runtimeDefenseData.canLevelUpReflectAttackChance = true;
                }
            }

            public void UnlockReflectAttackAngle(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeDefenseData.reflectAttackAngleUnlockCost)
                {
                    callersExperiencePoints -= runtimeDefenseData.reflectAttackAngleUnlockCost;
                    runtimeDefenseData.canLevelUpReflectAttackAngle = true;
                }
            }

            public void UnlockDodgeChance(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeDefenseData.dodgeChanceUnlockCost)
                {
                    callersExperiencePoints -= runtimeDefenseData.dodgeChanceUnlockCost;
                    runtimeDefenseData.canLevelUpDodgeChance = true;
                    ResetDodgeCooldown();
                }
            }

            public void UnlockArmorValue(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeDefenseData.armorValueUnlockCost)
                {
                    callersExperiencePoints -= runtimeDefenseData.armorValueUnlockCost;
                    runtimeDefenseData.canLevelUpArmorValue = true;
                }
            }

            public void UnlockThornsAmount(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeDefenseData.thornsAmountUnlockCost)
                {
                    callersExperiencePoints -= runtimeDefenseData.thornsAmountUnlockCost;
                    runtimeDefenseData.canLevelUpThornsAmount = true;
                }
            }

            public void UnlockCriticalHitChanceResist(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeDefenseData.criticalHitChanceResistUnlockCost)
                {
                    callersExperiencePoints -= runtimeDefenseData.criticalHitChanceResistUnlockCost;
                    runtimeDefenseData.canLevelUpCriticalHitChanceResist = true;
                }
            }

            public void UnlockCriticalHitDamageResist(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeDefenseData.criticalHitDamageResistUnlockCost)
                {
                    callersExperiencePoints -= runtimeDefenseData.criticalHitDamageResistUnlockCost;
                    runtimeDefenseData.canLevelUpCriticalHitDamageResist = true;
                }
            }

            public void UnlockStatusEffectResist(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeDefenseData.statusEffectResistUnlockCost)
                {
                    callersExperiencePoints -= runtimeDefenseData.statusEffectResistUnlockCost;
                    runtimeDefenseData.canLevelUpStatusEffectResist = true;
                }
            }

            public void UnlockPhysicalDamageResist(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeDefenseData.physicalDamageResistUnlockCost)
                {
                    callersExperiencePoints -= runtimeDefenseData.physicalDamageResistUnlockCost;
                    runtimeDefenseData.canLevelUpPhysicalDamageResist = true;
                }
            }

            public void UnlockMagicalDamageResist(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeDefenseData.magicalDamageResistUnlockCost)
                {
                    callersExperiencePoints -= runtimeDefenseData.magicalDamageResistUnlockCost;
                    runtimeDefenseData.canLevelUpMagicalDamageResist = true;
                }
            }

            public void UnlockEnergyDamageResist(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeDefenseData.energyDamageResistUnlockCost)
                {
                    callersExperiencePoints -= runtimeDefenseData.energyDamageResistUnlockCost;
                    runtimeDefenseData.canLevelUpEnergyDamageResist = true;
                }
            }

            public void LevelUpParryChance(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpParryChance || callersExperiencePoints < runtimeDefenseData.parryChanceExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.parryChanceExpCost;
                    runtimeDefenseData.parryChanceLevel++;
                }
            }

            public void LevelUpParryCooldown(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpParryChance || callersExperiencePoints < runtimeDefenseData.parryCooldownExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.parryCooldownExpCost;
                    runtimeDefenseData.parryCooldownLevel++;
                }
            }

            public void LevelUpCounterAttackChance(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpCounterAttackChance || callersExperiencePoints < runtimeDefenseData.counterAttackChanceExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.counterAttackChanceExpCost;
                    runtimeDefenseData.counterAttackChanceLevel++;
                }
            }

            public void LevelUpBlockChance(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpBlockChance || callersExperiencePoints < runtimeDefenseData.blockChanceExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.blockChanceExpCost;
                    runtimeDefenseData.blockChanceLevel++;
                }
            }

            public void LevelUpBlockCooldown(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpBlockChance || callersExperiencePoints < runtimeDefenseData.blockCooldownExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.blockCooldownExpCost;
                    runtimeDefenseData.blockCooldownLevel++;
                }
            }

            public void LevelUpBlockAmount(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpBlockAmount || callersExperiencePoints < runtimeDefenseData.blockAmountExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.blockAmountExpCost;
                    runtimeDefenseData.blockAmountLevel++;
                }
            }

            public void LevelUpReflectAttackChance(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpReflectAttackChance || callersExperiencePoints < runtimeDefenseData.reflectAttackChanceExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.reflectAttackChanceExpCost;
                    runtimeDefenseData.reflectAttackChanceLevel++;
                }
            }

            public void LevelUpReflectAttackAngle(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpReflectAttackAngle || callersExperiencePoints < runtimeDefenseData.reflectAttackAngleExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.reflectAttackAngleExpCost;
                    runtimeDefenseData.reflectAttackAngleLevel++;
                }
            }

            public void LevelUpDodgeChance(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpDodgeChance || callersExperiencePoints < runtimeDefenseData.dodgeChanceExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.dodgeChanceExpCost;
                    runtimeDefenseData.dodgeChanceLevel++;
                }
            }

            public void LevelUpDodgeCooldown(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpDodgeChance || callersExperiencePoints < runtimeDefenseData.dodgeCooldownExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.dodgeCooldownExpCost;
                    runtimeDefenseData.dodgeCooldownLevel++;
                }
            }

            public void LevelUpArmorValue(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpArmorValue || callersExperiencePoints < runtimeDefenseData.armorValueExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.armorValueExpCost;
                    runtimeDefenseData.armorValueLevel++;
                }
            }

            public void LevelUpThornsAmount(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpThornsAmount || callersExperiencePoints < runtimeDefenseData.thornsAmountExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.thornsAmountExpCost;
                    runtimeDefenseData.thornsAmountLevel++;
                }
            }

            public void LevelUpCriticalHitChanceResist(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpCriticalHitChanceResist || callersExperiencePoints < runtimeDefenseData.criticalHitChanceResistExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.criticalHitChanceResistExpCost;
                    runtimeDefenseData.criticalHitChanceResistLevel++;
                }
            }

            public void LevelUpCriticalHitDamageResist(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpCriticalHitDamageResist || callersExperiencePoints < runtimeDefenseData.criticalHitDamageResistExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.criticalHitDamageResistExpCost;
                    runtimeDefenseData.criticalHitDamageResistLevel++;
                }
            }

            public void LevelUpStatusEffectResist(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpStatusEffectResist || callersExperiencePoints < runtimeDefenseData.statusEffectResistExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.statusEffectResistExpCost;
                    runtimeDefenseData.statusEffectResistLevel++;
                }
            }

            public void LevelUpPhysicalDamageResist(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpPhysicalDamageResist || callersExperiencePoints < runtimeDefenseData.physicalDamageResistExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.physicalDamageResistExpCost;
                    runtimeDefenseData.physicalDamageResistLevel++;
                }
            }

            public void LevelUpMagicalDamageResist(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpMagicalDamageResist || callersExperiencePoints < runtimeDefenseData.magicalDamageResistExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.magicalDamageResistExpCost;
                    runtimeDefenseData.magicalDamageResistLevel++;
                }
            }

            public void LevelUpEnergyDamageResist(ref float callersExperiencePoints)
            {
                if (!runtimeDefenseData.canLevelUpEnergyDamageResist || callersExperiencePoints < runtimeDefenseData.energyDamageResistExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeDefenseData.energyDamageResistExpCost;
                    runtimeDefenseData.energyDamageResistLevel++;
                }
            }
        }
    }
}