namespace ShapeDefender
{
    namespace AttackSystem
    {
        using System.Collections.Generic;
        using ShapeDefender.AttackUpgradeSystem;
        using ShapeDefender.Tools;
        using UnityEngine;

        [System.Serializable]
        public class AttackContainer : MonoBehaviour
        {
            public List<AttackName> defaultAttacks;
            public Dictionary<AttackName, AttackStatSO> runtimeAttackStatSO = new Dictionary<AttackName, AttackStatSO>();
            private bool canUseAttacks = true;

            private void Awake()
            {
                if (defaultAttacks != null)
                {
                    AttackUpgradeManager.Instance.AddNewAttacks(defaultAttacks, this);
                }
            }

            public void AttemptToUseAttacks()
            {
                if (!canUseAttacks) { return; }

                foreach (var attack in runtimeAttackStatSO)
                {
                    attack.Value.currentAttacksCooldown -= Time.deltaTime;
                    GameObject target = FindTarget.FindTargetInRange(gameObject, attack.Value.attackRangeMinimum.StatValue, attack.Value.attackRangeMaximum.StatValue);

                    if (attack.Value.currentAttacksCooldown <= 0.0f && target != null)
                    {
                        SpawnNewAttack(attack.Value, target);
                        attack.Value.currentAttacksCooldown = attack.Value.attackCooldown.StatValue;
                    }
                }
            }

            public void AttemptToCounterAttack(GameObject callingObject)
            {
                if (!canUseAttacks) { return; }

                foreach (var attack in runtimeAttackStatSO)
                {
                    GameObject target = FindTarget.FindTargetInRange(callingObject, attack.Value.attackRangeMinimum.StatValue, attack.Value.attackRangeMaximum.StatValue);

                    if (target != null)
                    {
                        SpawnNewAttack(attack.Value, target);
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
                Vector2 direction = (target.transform.position - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

                GameObject newAttackSpawn = Instantiate(attackStatSO.projectilePrefab, transform.position, rotation);

                AttackController controller = newAttackSpawn.GetComponent<AttackController>();
                controller.runtimeAttackStatSO = Instantiate(attackStatSO);
                controller.SetTarget(target);
            }
        }
    }
}