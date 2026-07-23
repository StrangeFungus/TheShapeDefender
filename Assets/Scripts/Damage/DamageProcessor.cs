namespace ShapeDefender
{
    namespace DamageSystem
    {
        using ShapeDefender.AttackSystem;
        using ShapeDefender.DefenseSystem;
        using ShapeDefender.HealthSystem;
        using UnityEngine;

        public static class DamageProcessor
        {
            public static bool ProcessDamageToTarget(GameObject targetHit, GameObject attacksObject, DamageStatSO incomingRuntimeDamageStatSO)
            {
                if (targetHit == null) { return false; }
                if (incomingRuntimeDamageStatSO == null) { return false; }
                if (!targetHit.TryGetComponent<HealthStatContainer>(out HealthStatContainer targetsHealthStatContainer)) { return false; }

                // What are we doing here:
                // We calculate the damage, calculate the critical hit chance and damage and return both the normal damage and the bonus damage.
                // If the target hit has defensive components we can then attempt to defend that damage before it gets applied.
                // Apply said damage to target.

                float minDamage = incomingRuntimeDamageStatSO.minimumDamage.StatValue;
                float maxDamage = incomingRuntimeDamageStatSO.maximumDamage.StatValue;
                if (minDamage > maxDamage)
                {
                    minDamage = maxDamage;
                }
                float damage = Random.Range(minDamage, maxDamage);

                float bonusDamage = 0f;
                float critHitChance = incomingRuntimeDamageStatSO.criticalHitChance.StatValue;
                float critDamagePercent = incomingRuntimeDamageStatSO.criticalHitDamage.StatValue;
                if (Random.Range(1f, 100f) <= critHitChance)
                {
                    bonusDamage = (damage * critDamagePercent) - damage;
                }

                bool wasAttackReflected = false;
                AttemptToDefendDamage(targetHit, attacksObject, ref damage, ref bonusDamage, ref wasAttackReflected);

                HealthManager.Instance.TakeDamage(targetsHealthStatContainer, damage + bonusDamage);
                return wasAttackReflected;
            }

            private static void AttemptToDefendDamage(GameObject targetHit, GameObject attacksObject, ref float damage, ref float bonusDamage, ref bool wasAttackReflected)
            {
                if (!targetHit.TryGetComponent<DefenseStatContainer>(out DefenseStatContainer targetsDefenseStatContainer))
                {
                    return;
                }

                bool wasAbleToDefend = AttemptToParry(targetsDefenseStatContainer, attacksObject);

                if (!wasAbleToDefend) { wasAbleToDefend = AttemptToBlock(targetsDefenseStatContainer, attacksObject, ref damage, ref bonusDamage, ref wasAttackReflected); }
                if (!wasAbleToDefend) { AttemptToDodge(targetsDefenseStatContainer, ref damage, ref bonusDamage); }
            }

            private static bool AttemptToParry(DefenseStatContainer targetsDefenseStatContainer, GameObject attacksObject)
            {
                float parryChance = targetsDefenseStatContainer.runtimeDefenseStats.parryChance.StatValue;
                if (targetsDefenseStatContainer.runtimeDefenseStats.currentParryCooldown <= 0f && Random.Range(1f, 100f) <= parryChance)
                {
                    targetsDefenseStatContainer.runtimeDefenseStats.currentParryCooldown = targetsDefenseStatContainer.runtimeDefenseStats.parryCooldown.StatValue;
                    float counterAttackChance = targetsDefenseStatContainer.runtimeDefenseStats.counterAttackChance.StatValue;
                    if (Random.Range(1f, 100f) <= counterAttackChance && targetsDefenseStatContainer.gameObject.TryGetComponent<AttackContainer>(out AttackContainer targetsAttackContainer))
                    {
                        targetsAttackContainer.AttemptToCounterAttack(targetsDefenseStatContainer.gameObject);
                    }

                    return true;
                }

                return false;
            }

            private static bool AttemptToBlock(DefenseStatContainer targetsDefenseStatContainer, GameObject attackObject, ref float damage, ref float bonusDamage, ref bool wasAttackReflected)
            {
                float blockChance = targetsDefenseStatContainer.runtimeDefenseStats.blockChance.StatValue;
                if (targetsDefenseStatContainer.runtimeDefenseStats.currentBlockCooldown <= 0f && Random.Range(1f, 100f) <= blockChance)
                {
                    targetsDefenseStatContainer.runtimeDefenseStats.currentBlockCooldown = targetsDefenseStatContainer.runtimeDefenseStats.blockCooldown.StatValue;

                    float blockDamageValue = targetsDefenseStatContainer.runtimeDefenseStats.blockAmount.StatValue;
                    float amountBlocked = bonusDamage - blockDamageValue;
                    if (amountBlocked > 0f) { damage -= amountBlocked; }
                    if (damage < 0f) { damage = 0f; }

                    float reflectAttackChance = targetsDefenseStatContainer.runtimeDefenseStats.reflectAttackChance.StatValue;
                    if (Random.Range(1f, 100f) <= reflectAttackChance && targetsDefenseStatContainer.gameObject.TryGetComponent<AttackContainer>(out AttackContainer targetsAttackContainer))
                    {
                        wasAttackReflected = true;
                        float reflectAttackAngle = targetsDefenseStatContainer.runtimeDefenseStats.reflectAttackAngle.StatValue;
                        targetsAttackContainer.AttemptToReflectAttack(targetsDefenseStatContainer.gameObject, attackObject, reflectAttackAngle);
                    }

                    return true;
                }

                return false;
            }

            private static bool AttemptToDodge(DefenseStatContainer targetsDefenseStatContainer, ref float damage, ref float bonusDamage)
            {
                float dodgeChance = targetsDefenseStatContainer.runtimeDefenseStats.dodgeChance.StatValue;
                if (targetsDefenseStatContainer.runtimeDefenseStats.currentDodgeCooldown <= 0f && Random.Range(1f, 100f) <= dodgeChance)
                {
                    targetsDefenseStatContainer.runtimeDefenseStats.currentDodgeCooldown = targetsDefenseStatContainer.runtimeDefenseStats.dodgeCooldown.StatValue;

                    damage = 0f;
                    bonusDamage = 0f;
                    return true;
                }

                return false;
            }
        }
    }
}