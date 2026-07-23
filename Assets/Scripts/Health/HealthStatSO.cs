namespace ShapeDefender
{
    namespace HealthSystem
    {
        using UnityEngine;

        [CreateAssetMenu(menuName = "Health Stats", fileName = "New Health Stats")]
        public class HealthStatSO : ScriptableObject
        {
            [Header("Health")]
            [HideInInspector] public float currentHealth;
            public StatEntry maximumHealth;
            public StatEntry healthRegenAmount;
            public StatEntry healthRegenCooldown;
            [HideInInspector] public float currentHealthRegenCooldown;

            [Header("Energy Shields")]
            [HideInInspector] public float currentEnergyShields;
            public StatEntry maximumEnergyShields;
            public StatEntry energyShieldsRegenAmount;
            public StatEntry energyShieldsRegenCooldown;
            [HideInInspector] public float currentEnergyShieldsRegenCooldown;
        }
    }
}