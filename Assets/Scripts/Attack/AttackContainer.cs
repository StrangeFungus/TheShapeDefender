namespace ShapeDefender
{
    namespace AttackSystem
    {
        using System.Collections;
        using System.Collections.Generic;
        using ShapeDefender.DamageSystem;
        using ShapeDefender.Tools;
        using UnityEngine;
        using static UnityEngine.GraphicsBuffer;

        [System.Serializable]
        public class AttackContainer : MonoBehaviour
        {
            [SerializeField] private List<AttackStatSO> attackStatSOTemplates;
            private List<AttackStatSO> runtimeAttackStatSO = new List<AttackStatSO>();
            private bool canUseAttacks = true;

            private void Awake()
            {
                if (attackStatSOTemplates != null)
                {
                    foreach (AttackStatSO attackStatSO in attackStatSOTemplates)
                    {
                        AttackStatSO newAttackStatSO = Instantiate(attackStatSO);
                        runtimeAttackStatSO.Add(newAttackStatSO);
                        newAttackStatSO.currentAttacksCooldown = newAttackStatSO.attackCooldown.StatValue;
                    }
                }
            }

            public void AttemptToUseAttacks()
            {
                if (!canUseAttacks) { return; }

                foreach (var attack in runtimeAttackStatSO)
                {
                    attack.currentAttacksCooldown -= Time.deltaTime;
                    GameObject target = FindTarget.FindTargetInRange(gameObject, attack.attackRangeMinimum.StatValue, attack.attackRangeMaximum.StatValue);

                    if (attack.currentAttacksCooldown <= 0.0f && target != null)
                    {
                        SpawnNewAttack(attack, target);
                    }
                }
            }

            public void AttemptToCounterAttack(GameObject callingObject)
            {
                if (!canUseAttacks) { return; }

                foreach (var attack in runtimeAttackStatSO)
                {
                    GameObject target = FindTarget.FindTargetInRange(callingObject, attack.attackRangeMinimum.StatValue, attack.attackRangeMaximum.StatValue);

                    if (target != null)
                    {
                        SpawnNewAttack(attack, target);
                    }
                }
            }

            public void AttemptToReflectAttack(GameObject callingObject, GameObject attackToReflect, float reflectAttackAngle)
            {
                if (!attackToReflect.TryGetComponent<AttackController>(out AttackController attacksController)) { return; }

                GameObject target = FindTarget.FindTargetInRange(callingObject, attacksController.runtimeAttackStatSO.attackRangeMinimum.StatValue, attacksController.runtimeAttackStatSO.attackRangeMaximum.StatValue);
                attacksController.SetTarget(target);
                attacksController.SetSpawnPosition();
            }

            private void SpawnNewAttack(AttackStatSO attackStatSO, GameObject target)
            {
                GameObject newAttackSpawn = Instantiate(attackStatSO.projectilePrefab, transform.position, Quaternion.identity);
                AttackController newAttacksController = newAttackSpawn.GetComponent<AttackController>();
                newAttacksController.runtimeAttackStatSO = Instantiate(attackStatSO);
                newAttacksController.runtimeDamageStatSO = Instantiate(attackStatSO.projectilesDamageStatSOTemplate);
                newAttacksController.SetTarget(target);
                attackStatSO.currentAttacksCooldown = attackStatSO.attackCooldown.StatValue;
            }
        }
    }
}