namespace ShapeDefender
{
    namespace HealthSystem
    {
        using UnityEngine;

        [System.Serializable]
        [CreateAssetMenu(menuName = "Shape Defender Systems/Health Data", fileName = "New Health Data")]
        public class HealthData : ScriptableObject
        {
            [Header("Health -- Default Values")]
            [SerializeField] private float defaultMaximumHealth = 0f;
            [SerializeField] private float defaultHealthRegenAmount = 0f;
            [SerializeField] private float defaultHealthRegenCooldown = 0f;

            [Header("Health -- Level Up Values")]
            [SerializeField] private float maximumHealthPerLevelUp = 0f;
            [SerializeField] private float healthRegenAmountPerLevelUp = 0f;
            [SerializeField] private float healthRegenCooldownPerLevelUp = 0f;

            [Header("Health -- Stats Levels")]
            public int maximumHealthLevel = 0;
            public int healthRegenAmountLevel = 0;
            public int healthRegenCooldownLevel = 0;

            [Header("Health -- Stat Level Up Costs")]
            public float healthRegenAmountUnlockCost = 0f;
            
            public float maximumHealthExpCost = 0f;
            public float healthRegenAmountExpCost = 0f;
            public float healthRegenCooldownExpCost = 0f;

            [Header("Health -- Level Up Bools")]
            public bool canLevelUpHealthRegen = true;
            public float MaximumHealth { get { return defaultMaximumHealth + (maximumHealthPerLevelUp * maximumHealthLevel); } }
            public float HealthRegenAmount { get { if (!canLevelUpHealthRegen) { return 0f; } else { return defaultHealthRegenAmount + (healthRegenAmountPerLevelUp * healthRegenAmountLevel); } } }
            public float HealthRegenCooldown { get { if (!canLevelUpHealthRegen) { return 0f; } else { return defaultHealthRegenCooldown + (healthRegenCooldownPerLevelUp * healthRegenCooldownLevel); } } }
            [HideInInspector] public float currentHealth = 0f;
            public bool IsDead => currentHealth <= 0f;

            [Header("Energy Shield -- Default Values")]
            [SerializeField] private float defaultMaximumEnergyShield = 0f;
            [SerializeField] private float defaultEnergyShieldRegenAmount = 0f;
            [SerializeField] private float defaultEnergyShieldRegenCooldown = 0f;

            [Header("Energy Shield -- Level Up Values")]
            [SerializeField] private float maximumEnergyShieldPerLevelUp = 0f;
            [SerializeField] private float energyShieldsRegenAmountPerLevelUp = 0f;
            [SerializeField] private float energyShieldsRegenCooldownPerLevelUp = 0f;

            [Header("Energy Shield -- Stats Levels")]
            public int maximumEnergyShieldsLevel = 0;
            public int energyShieldRegenAmountLevel = 0;
            public int energyShieldRegenCooldownLevel = 0;

            [Header("Energy Shield -- Stat Level Up Costs")]
            public float maximumEnergyShieldUnlockCost = 0f;
            public float energyShieldsRegenAmountUnlockCost = 0f;
            
            public float maximumEnergyShieldExpCost = 0f;
            public float energyShieldsRegenAmountExpCost = 0f;
            public float energyShieldsRegenCooldownExpCost = 0f;

            [Header("Energy Shield -- Level Up Bools")]
            public bool canLevelUpMaximumEnergyShield = true;
            public bool canLevelUpEnergyShieldRegen = true;

            public float MaximumEnergyShields { get { if (!canLevelUpMaximumEnergyShield) { return 0f; } else { return defaultMaximumEnergyShield + (maximumEnergyShieldPerLevelUp * maximumEnergyShieldsLevel); } } }
            public float EnergyShieldRegenAmount { get { if (!canLevelUpEnergyShieldRegen) { return 0f; } else { return defaultEnergyShieldRegenAmount + (energyShieldsRegenAmountPerLevelUp * energyShieldRegenAmountLevel); } } }
            public float EnergyShieldRegenCooldown { get { if (!canLevelUpEnergyShieldRegen) { return 0f; } else { return defaultEnergyShieldRegenCooldown + (energyShieldsRegenCooldownPerLevelUp * energyShieldRegenCooldownLevel); } } }
            [HideInInspector] public float currentEnergyShields = 0f;
        }
    }
}