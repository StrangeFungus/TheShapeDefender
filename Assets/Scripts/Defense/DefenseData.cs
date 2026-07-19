namespace ShapeDefender
{
    namespace DefenseSystem
    {
        using UnityEngine;

        [CreateAssetMenu(menuName = "Shape Defender Systems/Defense Data", fileName = "New Defense Data")]
        public class DefenseData : ScriptableObject
        {
            [Header("Parry -- Default Values")]
            [SerializeField] private float defaultParryChance = 0f;
            [SerializeField] private float defaultParryCooldown = 0f;
            [SerializeField] private float defaultCounterAttackChance = 0f;

            [Header("Parry -- Level Up Values")]
            [SerializeField] private float parryChancePerLevelUp = 0f;
            [SerializeField] private float parryCooldownPerLevelUp = 0f;
            [SerializeField] private float counterAttackChancePerLevelUp = 0f;

            [Header("Parry -- Stats Levels")]
            public int parryChanceLevel = 1;
            public int parryCooldownLevel = 1;
            public int counterAttackChanceLevel = 1;

            [Header("Parry -- Stat Level Up Costs")]
            public float parryChanceUnlockCost = 0f;
            public float counterAttackChanceUnlockCost = 0f;
            
            public float parryChanceExpCost = 0f;
            public float parryCooldownExpCost = 0f;
            public float counterAttackChanceExpCost = 0f;

            [Header("Parry -- Level Up Bools")]
            public bool canLevelUpParryChance = true;
            public bool canLevelUpCounterAttackChance = true;

            public float ParryChance { get { if (!canLevelUpParryChance) { return 0f; } else { return defaultParryChance + (parryChancePerLevelUp * parryChanceLevel); } } }
            public float ParryCooldown { get { if (!canLevelUpParryChance) { return 10000f; } else { return defaultParryCooldown + (parryCooldownPerLevelUp * parryCooldownLevel); } } }
            public float CounterAttackChance { get { if (!canLevelUpCounterAttackChance) { return 0f; } else { return defaultCounterAttackChance + (counterAttackChancePerLevelUp * counterAttackChanceLevel); } } }

            [Header("Block -- Default Values")]
            [SerializeField] private float defaultBlockChance = 0f;
            [SerializeField] private float defaultBlockCooldown = 0f;
            [SerializeField] private float defaultBlockAmount = 0f;
            [SerializeField] private float defaultReflectAttackChance = 0f;
            [SerializeField] private float defaultReflectAttackAngle = 0f;

            [Header("Block -- Level Up Values")]
            [SerializeField] private float blockChancePerLevelUp = 0f;
            [SerializeField] private float blockCooldownPerLevelUp = 0f;
            [SerializeField] private float blockAmountPerLevelUp = 0f;
            [SerializeField] private float reflectAttackChancePerLevelUp = 0f;
            [SerializeField] private float reflectAttackAnglePerLevelUp = 0f;

            [Header("Block -- Stats Levels")]
            public int blockChanceLevel = 1;
            public int blockCooldownLevel = 1;
            public int blockAmountLevel = 1;
            public int reflectAttackChanceLevel = 1;
            public int reflectAttackAngleLevel = 1;

            [Header("Block -- Stat Level Up Costs")]
            public float blockChanceUnlockCost = 0f;
            public float blockAmountUnlockCost = 0f;
            public float reflectAttackChanceUnlockCost = 0f;
            public float reflectAttackAngleUnlockCost = 0f;
            
            public float blockChanceExpCost = 0f;
            public float blockCooldownExpCost = 0f;
            public float blockAmountExpCost = 0f;
            public float reflectAttackChanceExpCost = 0f;
            public float reflectAttackAngleExpCost = 0f;

            [Header("Block -- Level Up Bools")]
            public bool canLevelUpBlockChance = true;
            public bool canLevelUpBlockAmount = true;
            public bool canLevelUpReflectAttackChance = true;
            public bool canLevelUpReflectAttackAngle = true;

            public float BlockChance { get { if (!canLevelUpBlockChance) { return 0f; } else { return defaultBlockChance + (blockChancePerLevelUp * blockChanceLevel); } } }
            public float BlockCooldown { get { if (!canLevelUpBlockChance) { return 10000f; } else { return defaultBlockCooldown + (blockCooldownPerLevelUp * blockCooldownLevel); } } }
            public float BlockAmount { get { if (!canLevelUpBlockAmount) { return 0f; } else { return defaultBlockAmount + (blockAmountPerLevelUp * blockAmountLevel); } } }
            public float ReflectAttackChance { get { if (!canLevelUpReflectAttackChance) { return 0f; } else { return defaultReflectAttackChance + (reflectAttackChancePerLevelUp * reflectAttackChanceLevel); } } }
            public float ReflectAttackAngle { get { if (!canLevelUpReflectAttackAngle) { return 0f; } else { return defaultReflectAttackAngle + (reflectAttackAnglePerLevelUp * reflectAttackAngleLevel); } } }

            [Header("Dodge -- Default Values")]
            [SerializeField] private float defaultDodgeChance = 0f;
            [SerializeField] private float defaultDodgeCooldown = 0f;

            [Header("Dodge -- Level Up Values")]
            [SerializeField] private float dodgeChancePerLevelUp = 0f;
            [SerializeField] private float dodgeCooldownPerLevelUp = 0f;

            [Header("Dodge -- Stats Levels")]
            public int dodgeChanceLevel = 1;
            public int dodgeCooldownLevel = 1;

            [Header("Dodge -- Stat Level Up Costs")]
            public float dodgeChanceUnlockCost = 0f;
             
            public float dodgeChanceExpCost = 0f;
            public float dodgeCooldownExpCost = 0f;

            [Header("Dodge -- Level Up Bools")]
            public bool canLevelUpDodgeChance = true;

            public float DodgeChance { get { if (!canLevelUpDodgeChance) { return 0f; } else { return defaultDodgeChance + (dodgeChancePerLevelUp * dodgeChanceLevel); } } }
            public float DodgeCooldown { get { if (!canLevelUpDodgeChance) { return 10000f; } else { return defaultDodgeCooldown + (dodgeCooldownPerLevelUp * dodgeCooldownLevel); } } }

            [Header("Armor -- Default Values")]
            [SerializeField] private float defaultArmorValue = 0f;
            [SerializeField] private float defaultThornsAmount = 0f;

            [Header("Armor -- Level Up Values")]
            [SerializeField] private float armorValuePerLevelUp = 0f;
            [SerializeField] private float thornsAmountPerLevelUp = 0f;

            [Header("Armor -- Stats Levels")]
            public int armorValueLevel = 1;
            public int thornsAmountLevel = 1;

            [Header("Armor -- Stat Level Up Costs")]
            public float armorValueUnlockCost = 0f;
            public float thornsAmountUnlockCost = 0f;
            
            public float armorValueExpCost = 0f;
            public float thornsAmountExpCost = 0f;

            [Header("Armor -- Level Up Bools")]
            public bool canLevelUpArmorValue = true;
            public bool canLevelUpThornsAmount = true;

            public float ArmorValue { get { if (!canLevelUpArmorValue) { return 0f; } else { return defaultArmorValue + (armorValuePerLevelUp * armorValueLevel); } } }
            public float ThornsAmount { get { if (!canLevelUpThornsAmount) { return 0f; } else { return defaultThornsAmount + (thornsAmountPerLevelUp * thornsAmountLevel); } } }

            [Header("Resistances -- Default Values")]
            [SerializeField] private float defaultCriticalHitChanceResist = 0f;
            [SerializeField] private float defaultCriticalHitDamageResist = 0f;
            [SerializeField] private float defaultStatusEffectResist = 0f;
            [SerializeField] private float defaultPhysicalDamageResist = 0f;
            [SerializeField] private float defaultMagicalDamageResist = 0f;
            [SerializeField] private float defaultEnergyDamageResist = 0f;

            [Header("Resistances -- Level Up Values")]
            [SerializeField] private float criticalHitChanceResistPerLevelUp = 0f;
            [SerializeField] private float criticalHitDamageResistPerLevelUp = 0f;
            [SerializeField] private float statusEffectResistPerLevelUp = 0f;
            [SerializeField] private float physicalDamageResistPerLevelUp = 0f;
            [SerializeField] private float magicalDamageResistPerLevelUp = 0f;
            [SerializeField] private float energyDamageResistPerLevelUp = 0f;

            [Header("Resistances -- Stats Levels")]
            public int criticalHitChanceResistLevel = 1;
            public int criticalHitDamageResistLevel = 1;
            public int statusEffectResistLevel = 1;
            public int physicalDamageResistLevel = 1;
            public int magicalDamageResistLevel = 1;
            public int energyDamageResistLevel = 1;

            [Header("Resistances -- Stat Level Up Costs")]
            public float criticalHitChanceResistUnlockCost = 0f;
            public float criticalHitDamageResistUnlockCost = 0f;
            public float statusEffectResistUnlockCost = 0f;
            public float physicalDamageResistUnlockCost = 0f;
            public float magicalDamageResistUnlockCost = 0f;
            public float energyDamageResistUnlockCost = 0f;
            
            public float criticalHitChanceResistExpCost = 0f;
            public float criticalHitDamageResistExpCost = 0f;
            public float statusEffectResistExpCost = 0f;
            public float physicalDamageResistExpCost = 0f;
            public float magicalDamageResistExpCost = 0f;
            public float energyDamageResistExpCost = 0f;

            [Header("Resistances -- Level Up Bools")]
            public bool canLevelUpCriticalHitChanceResist = true;
            public bool canLevelUpCriticalHitDamageResist = true;
            public bool canLevelUpStatusEffectResist = true;
            public bool canLevelUpPhysicalDamageResist = true;
            public bool canLevelUpMagicalDamageResist = true;
            public bool canLevelUpEnergyDamageResist = true;

            public float CriticalHitChanceResist { get { if (!canLevelUpCriticalHitChanceResist) { return 0f; } else { return defaultCriticalHitChanceResist + (criticalHitChanceResistPerLevelUp * criticalHitChanceResistLevel); } } }
            public float CriticalHitDamageResist { get { if (!canLevelUpCriticalHitDamageResist) { return 0f; } else { return defaultCriticalHitDamageResist + (criticalHitDamageResistPerLevelUp * criticalHitDamageResistLevel); } } }
            public float StatusEffectResist { get { if (!canLevelUpStatusEffectResist) { return 0f; } else { return defaultStatusEffectResist + (statusEffectResistPerLevelUp * statusEffectResistLevel); } } }
            public float PhysicalDamageResist { get { if (!canLevelUpPhysicalDamageResist) { return 0f; } else { return defaultPhysicalDamageResist + (physicalDamageResistPerLevelUp * physicalDamageResistLevel); } } }
            public float MagicalDamageResist { get { if (!canLevelUpMagicalDamageResist) { return 0f; } else { return defaultMagicalDamageResist + (magicalDamageResistPerLevelUp * magicalDamageResistLevel); } } }
            public float EnergyDamageResist { get { if (!canLevelUpEnergyDamageResist) { return 0f; } else { return defaultEnergyDamageResist + (energyDamageResistPerLevelUp * energyDamageResistLevel); } } }
        }
    }
}