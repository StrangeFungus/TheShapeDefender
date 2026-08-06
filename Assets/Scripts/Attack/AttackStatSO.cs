namespace ShapeDefender
{
    public enum AttackName
    {
        Default,
        NormalArrow,
        SimpleRam,
        SmallBomb,
        WindStrike,
    }

    namespace AttackSystem
    {
        using ShapeDefender.DamageSystem;
        using ShapeDefender.Tools;
        using UnityEngine;

        [CreateAssetMenu(menuName = "Attack Stats", fileName = "New Attack Stats")]
        public class AttackStatSO : ScriptableObject
        {
            public AttackName attacksName;
            public GameObject projectilePrefab;
            public bool homesOntoTarget = false;

            public StatEntry projectileSpeed;
            public StatEntry attackRangeMinimum;
            public StatEntry attackRangeMaximum;
            public StatEntry attackCooldown;
            [HideInInspector] public float currentAttacksCooldown = 0f;
            public StatEntry attackAccuracyAngle;

            public StatEntry projectileCount;
            public StatEntry multistrikeChance;
            public StatEntry multistrikeMaxHitsCombo;

            public StatEntry targetPiercingQuantity;
            public StatEntry areaOfEffectRadius;

            [HideInInspector] public int currentSummonsAmount;
            public StatEntry maximumSummonsLimit;
            public StatEntry summonSpawnCooldown;

            public DamageStats projectilesDamageStats;

            public bool canLevelUp;
            public int attacksLevel = 1;

            public float expCostToUnlock;

            public float CalculateExpCostToLevelUp(int purchaseMultiplier)
            {
                float totalCost = 0f;
                totalCost += CalculateCost.StatExpCost(attacksLevel, projectileSpeed.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, attackRangeMinimum.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, attackRangeMaximum.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, attackCooldown.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, attackAccuracyAngle.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, projectileCount.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, multistrikeChance.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, multistrikeMaxHitsCombo.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, targetPiercingQuantity.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, areaOfEffectRadius.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, maximumSummonsLimit.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, summonSpawnCooldown.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, projectilesDamageStats.minimumDamage.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, projectilesDamageStats.maximumDamage.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, projectilesDamageStats.criticalHitChance.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, projectilesDamageStats.criticalHitDamage.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, projectilesDamageStats.splashDamageRadius.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, projectilesDamageStats.splashDamageFalloffRate.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, projectilesDamageStats.knockbackDistance.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, projectilesDamageStats.knockbackDamage.expCostToLevel, purchaseMultiplier);
                totalCost += CalculateCost.StatExpCost(attacksLevel, projectilesDamageStats.ignoreArmorAmount.expCostToLevel, purchaseMultiplier);
                return totalCost;
            }
        }
    }
}