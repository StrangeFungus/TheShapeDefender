namespace ShapeDefender
{
    namespace HealthSystem
    {
        using UnityEngine;
        using ShapeDefender.UI;

        public class HealthManager : MonoBehaviour
        {
            public static HealthManager Instance;

            private void Awake()
            {
                if (Instance == null)
                {
                    Instance = this;
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            public void TakeDamage(HealthStatContainer targetsHealthStatContainer, float damageAmount)
            {
                if (targetsHealthStatContainer == null) { return; }
                if (targetsHealthStatContainer.IsDead) { return; }

                float totalDamageDealt = 0;
                if (targetsHealthStatContainer.runtimeHealthStats.currentEnergyShields > 0) // IF TARGET HAS ENERGY SHIELDS
                {
                    float currentEnergyShieldsAfterHit = targetsHealthStatContainer.runtimeHealthStats.currentEnergyShields - damageAmount; // HOW MUCH IS LEFT AFTER TAKING DAMAGE?
                    if (currentEnergyShieldsAfterHit < 0) // IF DAMAGE EXCEEDS SHIELDS
                    {
                        totalDamageDealt += targetsHealthStatContainer.runtimeHealthStats.currentEnergyShields; // DAMAGE DEALT WAS ALL OF SHIELDS
                        targetsHealthStatContainer.runtimeHealthStats.currentEnergyShields = 0f;
                    }
                    else
                    {
                        totalDamageDealt = damageAmount;
                        targetsHealthStatContainer.runtimeHealthStats.currentEnergyShields = currentEnergyShieldsAfterHit;
                    }
                }

                damageAmount -= totalDamageDealt;
                if (damageAmount > 0)
                {
                    float currentHealthAfterHit = targetsHealthStatContainer.runtimeHealthStats.currentHealth - damageAmount; // HOW MUCH IS LEFT AFTER TAKING DAMAGE?
                    if (currentHealthAfterHit < 0) // IF DAMAGE EXCEEDS HEALTH
                    {
                        totalDamageDealt += targetsHealthStatContainer.runtimeHealthStats.currentHealth; // DAMAGE DEALT WAS ALL OF HEALTH
                        targetsHealthStatContainer.runtimeHealthStats.currentHealth = 0f;
                    }
                    else
                    {
                        totalDamageDealt += damageAmount;
                        targetsHealthStatContainer.runtimeHealthStats.currentHealth = currentHealthAfterHit;
                    }
                }

                if (totalDamageDealt > 0)
                {
                    targetsHealthStatContainer.UpdateStatusBars();
                    FloatingTextSpawner.Instance.SpawnText(totalDamageDealt.ToString("F2"), Color.red, targetsHealthStatContainer.transform.position);
                }
                else
                {
                    FloatingTextSpawner.Instance.SpawnText(0.ToString("F0"), Color.blue, targetsHealthStatContainer.transform.position);
                }
            }

            public void RestoreHealth(HealthStatContainer targetsHealthStatContainer, float amountToRestore)
            {
                if (targetsHealthStatContainer.IsDead) { return; }
                if (amountToRestore <= 0) { return; }

                float amountRestored;
                float currentHealthAfterHealing = targetsHealthStatContainer.runtimeHealthStats.currentHealth + amountToRestore;
                if (currentHealthAfterHealing > targetsHealthStatContainer.runtimeHealthStats.maximumHealth.StatValue)
                {
                    amountRestored = targetsHealthStatContainer.runtimeHealthStats.maximumHealth.StatValue - targetsHealthStatContainer.runtimeHealthStats.currentHealth;
                    targetsHealthStatContainer.runtimeHealthStats.currentHealth = targetsHealthStatContainer.runtimeHealthStats.maximumHealth.StatValue;
                }
                else
                {
                    amountRestored = amountToRestore;
                    targetsHealthStatContainer.runtimeHealthStats.currentHealth = currentHealthAfterHealing;
                }

                targetsHealthStatContainer.runtimeHealthStats.currentHealthRegenCooldown = targetsHealthStatContainer.runtimeHealthStats.healthRegenCooldown.StatValue;
                FloatingTextSpawner.Instance.SpawnText(amountRestored.ToString("F2"), Color.green, targetsHealthStatContainer.transform.position);
                targetsHealthStatContainer.UpdateStatusBars();
            }

            public void RestoreEnergyShields(HealthStatContainer targetsHealthStatContainer, float amountToRestore)
            {
                if (targetsHealthStatContainer.IsDead) { return; }
                if (amountToRestore <= 0) { return; }

                float amountRestored;
                float currentEnergyShieldAfterRestoring = targetsHealthStatContainer.runtimeHealthStats.currentEnergyShields + amountToRestore;
                if (currentEnergyShieldAfterRestoring > targetsHealthStatContainer.runtimeHealthStats.maximumEnergyShields.StatValue)
                {
                    amountRestored = targetsHealthStatContainer.runtimeHealthStats.maximumEnergyShields.StatValue - targetsHealthStatContainer.runtimeHealthStats.currentEnergyShields;
                    targetsHealthStatContainer.runtimeHealthStats.currentEnergyShields = targetsHealthStatContainer.runtimeHealthStats.maximumEnergyShields.StatValue;
                }
                else
                {
                    amountRestored = amountToRestore;
                    targetsHealthStatContainer.runtimeHealthStats.currentEnergyShields = currentEnergyShieldAfterRestoring;
                }

                targetsHealthStatContainer.runtimeHealthStats.currentEnergyShieldsRegenCooldown = targetsHealthStatContainer.runtimeHealthStats.energyShieldsRegenCooldown.StatValue;
                FloatingTextSpawner.Instance.SpawnText(amountRestored.ToString("F2"), Color.cyan, targetsHealthStatContainer.transform.position);
                targetsHealthStatContainer.UpdateStatusBars();
            }
        }
    }
}