namespace ShapeDefender
{
    namespace HealthSystem
    {
        using UnityEngine;
        using ShapeDefender.UI;

        public class HealthDataController : MonoBehaviour
        {
            [Header("Health Data")]
            [SerializeField] private HealthData healthDataTemplate;
            private HealthData runtimeHealthData;
            public HealthData RuntimeHealthData { get { return runtimeHealthData; } }

            [Header("Health Bar")]
            [SerializeField] private GameObject healthBarPrefab;
            private GameObject runtimeHealthBar;
            [SerializeField] private Vector3 healthBarOffset = new Vector3(0.0f, 4.0f, 0.0f);

            [Header("Energy Shield Bar")]
            [SerializeField] private GameObject energyShieldBarPrefab;
            private GameObject runtimeEnergyShieldBar;
            [SerializeField] private Vector3 energyShieldBarOffset = new Vector3(0.0f, 8.0f, 0.0f);
            private float currentHealthRegenCooldown = 0f;
            private float currentEnergyShieldRegenCooldown = 0f;

            private FloatingTextSpawner floatingTextSpawner;

            private void Awake()
            {
                if (healthDataTemplate != null)
                {
                    runtimeHealthData = Instantiate(healthDataTemplate);
                    runtimeHealthData.currentHealth = runtimeHealthData.MaximumHealth;
                    runtimeHealthData.currentEnergyShields = runtimeHealthData.MaximumEnergyShields;

                    if (healthBarPrefab != null) { runtimeHealthBar = Instantiate(healthBarPrefab); }
                    if (energyShieldBarPrefab != null) { runtimeEnergyShieldBar = Instantiate(energyShieldBarPrefab); }
                }

                currentHealthRegenCooldown = runtimeHealthData.HealthRegenCooldown;
                currentEnergyShieldRegenCooldown = runtimeHealthData.EnergyShieldRegenCooldown;
                floatingTextSpawner = GameObject.Find("FloatingTextSpawner").GetComponent<FloatingTextSpawner>();
            }

            private void OnEnable()
            {
                if (runtimeHealthData == null) { return; }
                ToggleStatusBars(true);
            }

            private void OnDisable()
            {
                if (runtimeHealthData == null) { return; }
                ToggleStatusBars(false);
            }

            private void Update()
            {
                if (runtimeHealthData == null) { return; }
                UpdateStatusBarsLocation(transform.position);

                if (runtimeHealthData.IsDead)
                {
                    gameObject.SetActive(false);
                }

                currentHealthRegenCooldown -= Time.deltaTime;
                currentEnergyShieldRegenCooldown -= Time.deltaTime;

                if (currentHealthRegenCooldown < 0)
                {
                    RestoreHealth(runtimeHealthData.HealthRegenAmount);
                }

                if (currentEnergyShieldRegenCooldown < 0)
                {
                    RestoreEnergyShields(runtimeHealthData.EnergyShieldRegenAmount);
                }
            }

            public void TakeDamage(float damageAmount)
            {
                if (runtimeHealthData == null) { return; }
                if (runtimeHealthData.IsDead) { return; }

                float totalDamageDealt = 0;
                if (runtimeHealthData.currentEnergyShields > 0) // IF TARGET HAS ENERGY SHIELDS
                {
                    float currentEnergyShieldsAfterHit = runtimeHealthData.currentEnergyShields - damageAmount; // HOW MUCH IS LEFT AFTER TAKING DAMAGE?
                    if (currentEnergyShieldsAfterHit < 0) // IF DAMAGE EXCEEDS SHIELDS
                    {
                        totalDamageDealt += runtimeHealthData.currentEnergyShields; // DAMAGE DEALT WAS ALL OF SHIELDS
                        runtimeHealthData.currentEnergyShields = 0f;
                    }
                    else
                    {
                        totalDamageDealt = damageAmount;
                        runtimeHealthData.currentEnergyShields = currentEnergyShieldsAfterHit;
                    }
                }

                damageAmount -= totalDamageDealt;
                if (damageAmount > 0)
                {
                    float currentHealthAfterHit = runtimeHealthData.currentHealth - damageAmount; // HOW MUCH IS LEFT AFTER TAKING DAMAGE?
                    if (currentHealthAfterHit < 0) // IF DAMAGE EXCEEDS HEALTH
                    {
                        totalDamageDealt += runtimeHealthData.currentHealth; // DAMAGE DEALT WAS ALL OF HEALTH
                        runtimeHealthData.currentHealth = 0f;
                    }
                    else
                    {
                        totalDamageDealt += damageAmount;
                        runtimeHealthData.currentHealth = currentHealthAfterHit;
                    }
                }

                if (totalDamageDealt > 0)
                {
                    UpdateStatusBars();
                    floatingTextSpawner.SpawnText(totalDamageDealt.ToString("F2"), Color.red, transform.position);
                }
                else
                {
                    floatingTextSpawner.SpawnText(0.ToString("F0"), Color.blue, transform.position);
                }
            }

            public void RestoreHealth(float amountToRestore)
            {
                if (runtimeHealthData.IsDead) { return; }
                if (amountToRestore <= 0) { return; }

                float amountRestored;
                float currentHealthAfterHealing = runtimeHealthData.currentHealth + amountToRestore;
                if (currentHealthAfterHealing > runtimeHealthData.MaximumHealth)
                {
                    amountRestored = runtimeHealthData.MaximumHealth - runtimeHealthData.currentHealth;
                    runtimeHealthData.currentHealth = runtimeHealthData.MaximumHealth;
                }
                else
                {
                    amountRestored = amountToRestore;
                    runtimeHealthData.currentHealth = currentHealthAfterHealing;
                }

                currentHealthRegenCooldown = runtimeHealthData.HealthRegenCooldown;
                floatingTextSpawner.SpawnText(amountRestored.ToString("F2"), Color.green, transform.position);
                UpdateStatusBars();
            }

            public void RestoreEnergyShields(float amountToRestore)
            {
                if (runtimeHealthData.IsDead) { return; }
                if (amountToRestore <= 0) { return; }

                float amountRestored;
                float currentEnergyShieldAfterRestoring = runtimeHealthData.currentEnergyShields + amountToRestore;
                if (currentEnergyShieldAfterRestoring > runtimeHealthData.MaximumEnergyShields)
                {
                    amountRestored = runtimeHealthData.MaximumEnergyShields - runtimeHealthData.currentEnergyShields;
                    runtimeHealthData.currentEnergyShields = runtimeHealthData.MaximumEnergyShields;
                }
                else
                {
                    amountRestored = amountToRestore;
                    runtimeHealthData.currentEnergyShields = currentEnergyShieldAfterRestoring;
                }

                currentEnergyShieldRegenCooldown = runtimeHealthData.EnergyShieldRegenCooldown;
                floatingTextSpawner.SpawnText(amountRestored.ToString("F2"), Color.cyan, transform.position);
                UpdateStatusBars();
            }

            public void UpdateStatusBars()
            {
                if (runtimeHealthBar != null)
                {
                    if (runtimeHealthBar.activeSelf)
                    {
                        float healthPercentLeft = -(1 - (runtimeHealthData.currentHealth / runtimeHealthData.MaximumHealth));
                        runtimeHealthBar.transform.GetChild(0).transform.localPosition = new Vector3(healthPercentLeft, 0f, 0f);
                    }
                }

                if (runtimeEnergyShieldBar != null)
                {
                    if (runtimeEnergyShieldBar.activeSelf)
                    {
                        float energyShieldPercentLeft = -(1 - (runtimeHealthData.currentEnergyShields / runtimeHealthData.MaximumEnergyShields));
                        runtimeEnergyShieldBar.transform.GetChild(0).transform.localPosition = new Vector3(energyShieldPercentLeft, 0f, 0f);
                    }
                }
            }

            public void ToggleStatusBars(bool toggleToSet)
            {
                if (runtimeHealthBar != null)
                {
                    runtimeHealthBar.SetActive(toggleToSet);
                }

                if (runtimeEnergyShieldBar != null)
                {
                    if (runtimeHealthData.MaximumEnergyShields > 0f)
                    {
                        runtimeEnergyShieldBar.SetActive(toggleToSet);
                    }
                    else
                    {
                        runtimeEnergyShieldBar.SetActive(false);
                    }
                }
            }

            public void UpdateStatusBarsLocation(Vector3 targetLocation)
            {
                if (runtimeHealthBar != null)
                {
                    runtimeHealthBar.transform.position = targetLocation + healthBarOffset;
                }

                if (runtimeEnergyShieldBar != null)
                {
                    runtimeEnergyShieldBar.transform.position = targetLocation + energyShieldBarOffset;
                }
            }

            public void UnlockHealthRegenAmount(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeHealthData.healthRegenAmountUnlockCost)
                {
                    callersExperiencePoints -= runtimeHealthData.healthRegenAmountUnlockCost;
                    runtimeHealthData.canLevelUpHealthRegen = true;
                }
            }

            public void UnlockMaximumEnergyShield(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeHealthData.maximumEnergyShieldUnlockCost)
                {
                    callersExperiencePoints -= runtimeHealthData.maximumEnergyShieldUnlockCost;
                    runtimeHealthData.canLevelUpMaximumEnergyShield = true;
                }
            }

            public void UnlockEnergyShieldRegenAmount(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints >= runtimeHealthData.energyShieldsRegenAmountUnlockCost)
                {
                    callersExperiencePoints -= runtimeHealthData.energyShieldsRegenAmountUnlockCost;
                    runtimeHealthData.canLevelUpEnergyShieldRegen = true;
                }
            }

            public void LevelUpMaximumHealth(ref float callersExperiencePoints)
            {
                if (callersExperiencePoints < runtimeHealthData.maximumHealthExpCost) { return; }
                else 
                {
                    callersExperiencePoints -= runtimeHealthData.maximumHealthExpCost;
                    runtimeHealthData.maximumHealthLevel++;
                }
            }

            public void LevelUpHealthRegenAmount(ref float callersExperiencePoints)
            {
                if (!runtimeHealthData.canLevelUpHealthRegen || callersExperiencePoints < runtimeHealthData.healthRegenAmountExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeHealthData.healthRegenAmountExpCost;
                    runtimeHealthData.healthRegenAmountLevel++;
                }
            }

            public void LevelUpHealthRegenCooldown(ref float callersExperiencePoints)
            {
                if (!runtimeHealthData.canLevelUpHealthRegen || callersExperiencePoints < runtimeHealthData.healthRegenCooldownExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeHealthData.healthRegenCooldownExpCost;
                    runtimeHealthData.healthRegenCooldownLevel++;
                }
            }

            public void LevelUpMaximumEnergyShield(ref float callersExperiencePoints)
            {
                if (!runtimeHealthData.canLevelUpMaximumEnergyShield || callersExperiencePoints < runtimeHealthData.maximumEnergyShieldExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeHealthData.maximumEnergyShieldExpCost;
                    runtimeHealthData.maximumEnergyShieldsLevel++;
                }
            }

            public void LevelUpEnergyShieldRegenAmount(ref float callersExperiencePoints)
            {
                if (!runtimeHealthData.canLevelUpEnergyShieldRegen || callersExperiencePoints < runtimeHealthData.energyShieldsRegenAmountExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeHealthData.energyShieldsRegenAmountExpCost;
                    runtimeHealthData.energyShieldRegenAmountLevel++;
                }
            }

            public void LevelUpEnergyShieldRegenCooldown(ref float callersExperiencePoints)
            {
                if (!runtimeHealthData.canLevelUpEnergyShieldRegen || callersExperiencePoints < runtimeHealthData.energyShieldsRegenCooldownExpCost) { return; }
                else
                {
                    callersExperiencePoints -= runtimeHealthData.energyShieldsRegenCooldownExpCost;
                    runtimeHealthData.energyShieldRegenCooldownLevel++;
                }
            }
        }
    }
}