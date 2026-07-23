namespace ShapeDefender
{
    namespace AttackSystem
    {
        using ShapeDefender.DamageSystem;
        using UnityEngine;

        [CreateAssetMenu(menuName = "Attack Stats", fileName = "New Attack Stats")]
        public class AttackStatSO : ScriptableObject
        {
            public GameObject projectilePrefab;
            public bool homesOntoTarget = false;

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

            public DamageStatSO projectilesDamageStatSOTemplate;
        }
    }
}