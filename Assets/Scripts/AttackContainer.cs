using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackContainer : MonoBehaviour
{
    [SerializeField] private List<AttackData> attackDataTemplates;
    private List<AttackData> runtimeAttackData = new List<AttackData>();

    private float attacksShortestMinRange = 1f;
    private float attackShortestMaxRange = 1f;
    private bool canUseAttacks = true;

    private void Awake()
    {
        if (attackDataTemplates != null && attackDataTemplates.Count > 0)
        {
            foreach (AttackData attackData in attackDataTemplates)
            {
                runtimeAttackData.Add(Instantiate(attackData));
            }
        }

    }
    public void AttemptToUseAttacks()
    {
        if (!canUseAttacks) { return; }

        float smallestMinRange = float.MaxValue;
        float smallestMaxRange = float.MaxValue;

        GameObject target = null;
        if (gameObject.CompareTag("Enemy"))
        {
            target = GameObject.Find("Player");
        }

        foreach (var attack in runtimeAttackData)
        {
            attack.ReduceCurrentCooldownTime(Time.deltaTime);
            if (smallestMinRange <= attack.AttackRangeMinimum) { smallestMinRange = attack.AttackRangeMinimum; }
            if (smallestMaxRange <= attack.AttackRangeMaximum) { smallestMinRange = attack.AttackRangeMaximum; }

            if (target == null) // Or is already dead?
            {
                target = FindTarget.FindTargetInRange(gameObject, attack.AttackRangeMinimum, attack.AttackRangeMaximum);
            }

            if (attack.AttackCooldownRemaining <= 0.0f && target != null)
            {
                GameObject newAttackSpawn = Instantiate(attack.ProjectilePrefab, transform.position, attack.ProjectilePrefab.transform.rotation);
                AttackData newAttackDataCopy = Instantiate(attack);
                AttackController newAttacksController = newAttackSpawn.GetComponent<AttackController>();
                newAttacksController.RuntimeAttackData = newAttackDataCopy;
                newAttacksController.SetTarget(target);
                attack.ResetCooldownTimer();
            }
        }
    }
}
