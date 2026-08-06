namespace ShapeDefender
{
    namespace AttackUpgradeSystem
    {
        using System.Collections.Generic;
        using ShapeDefender.AttackSystem;
        using UnityEngine;

        public class AttackUpgradeManager : MonoBehaviour
        {
            public static AttackUpgradeManager Instance;
            [SerializeField] private List<AttackStatSO> attackStatSOTemplates;
            private Dictionary<AttackName, AttackStatSO> runtimeAttackStatSOTemplates = new();
            public Dictionary<AttackName, AttackStatSO> RuntimeAttackStatSOTemplates { get { return runtimeAttackStatSOTemplates; } }

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

                if (attackStatSOTemplates != null)
                {
                    foreach (AttackStatSO attackStatSO in attackStatSOTemplates)
                    {
                        runtimeAttackStatSOTemplates.Add(attackStatSO.attacksName, attackStatSO);
                    }
                }
            }

            public void AddNewAttacks(List<AttackName> attackNames, AttackContainer attacksContainer)
            {
                foreach (var attack in attackNames)
                {
                    AddNewAttack(attack, attacksContainer);
                }
            }

            public void AddNewAttack(AttackName attacksName, AttackContainer attacksContainer)
            {
                if (runtimeAttackStatSOTemplates.ContainsKey(attacksName))
                {
                    AttackStatSO newAttackStatSO = Instantiate(runtimeAttackStatSOTemplates[attacksName]);
                    attacksContainer.runtimeAttackStatSO.Add(newAttackStatSO.attacksName, newAttackStatSO);
                    newAttackStatSO.currentAttacksCooldown = newAttackStatSO.attackCooldown.StatValue;
                    attacksContainer.runtimeAttackStatSO[attacksName].canLevelUp = true;
                }
            }

            public bool AttemptToUnlockAttack(ref float callersExperiencePoints, AttackName attacksName, AttackContainer attacksContainer)
            {
                if (!runtimeAttackStatSOTemplates.ContainsKey(attacksName)) { return false; }
                if (callersExperiencePoints >= runtimeAttackStatSOTemplates[attacksName].expCostToUnlock)
                {
                    callersExperiencePoints -= runtimeAttackStatSOTemplates[attacksName].expCostToUnlock;
                    AddNewAttack(attacksName, attacksContainer);
                    return true;
                }

                return false;
            }

            public bool AttemptToLevelUpAttack(ref float callersExperiencePoints, AttackName attacksName, AttackContainer attacksContainer, int purchaseMultiplier)
            {
                if (!attacksContainer.runtimeAttackStatSO.ContainsKey(attacksName)) { return false; }

                if (callersExperiencePoints >= attacksContainer.runtimeAttackStatSO[attacksName].CalculateExpCostToLevelUp(purchaseMultiplier))
                {
                    callersExperiencePoints -= attacksContainer.runtimeAttackStatSO[attacksName].CalculateExpCostToLevelUp(purchaseMultiplier);
                    attacksContainer.runtimeAttackStatSO[attacksName].attacksLevel++;
                    LevelUpAttacksStats(attacksContainer.runtimeAttackStatSO[attacksName], purchaseMultiplier);
                    return true;
                }

                return false;
            }

            private void LevelUpAttacksStats(AttackStatSO callersAttackStatSO, int purchaseMultiplier)
            {
                callersAttackStatSO.attackRangeMinimum.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.attackRangeMaximum.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.attackCooldown.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.attackAccuracyAngle.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.projectileCount.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.multistrikeChance.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.multistrikeMaxHitsCombo.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.targetPiercingQuantity.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.areaOfEffectRadius.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.maximumSummonsLimit.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.summonSpawnCooldown.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.projectilesDamageStats.minimumDamage.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.projectilesDamageStats.maximumDamage.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.projectilesDamageStats.criticalHitChance.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.projectilesDamageStats.criticalHitDamage.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.projectilesDamageStats.splashDamageRadius.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.projectilesDamageStats.splashDamageFalloffRate.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.projectilesDamageStats.knockbackDistance.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.projectilesDamageStats.knockbackDamage.IncreaseStatsLevel(purchaseMultiplier);
                callersAttackStatSO.projectilesDamageStats.ignoreArmorAmount.IncreaseStatsLevel(purchaseMultiplier);
            }
        }
    }
}