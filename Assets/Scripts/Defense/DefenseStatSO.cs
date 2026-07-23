namespace ShapeDefender
{
    namespace DefenseSystem
    {
        using UnityEngine;

        [CreateAssetMenu(menuName = "Defense Stats", fileName = "New Defense Stats")]
        public class DefenseStatSO : ScriptableObject
        {
            [Header("Parry")]
            public StatEntry parryChance;
            public StatEntry parryCooldown;
            [HideInInspector] public float currentParryCooldown;
            public StatEntry counterAttackChance;

            [Header("Block")]
            public StatEntry blockChance;
            public StatEntry blockCooldown;
            [HideInInspector] public float currentBlockCooldown;
            public StatEntry blockAmount;
            public StatEntry reflectAttackChance;
            public StatEntry reflectAttackAngle;

            [Header("Dodge")]
            public StatEntry dodgeChance;
            public StatEntry dodgeCooldown;
            [HideInInspector] public float currentDodgeCooldown;

            [Header("Armor")]
            public StatEntry armorValue;
            public StatEntry thornsAmount;

            [Header("Resistances")]
            public StatEntry criticalHitChanceResist;
            public StatEntry criticalHitDamageResist;
            public StatEntry statusEffectResist;
            public StatEntry physicalDamageResist;
            public StatEntry magicalDamageResist;
            public StatEntry energyDamageResist;
        }
    }
}