namespace ShapeDefender
{
    namespace HealthSystem
    {
        using UnityEngine;

        public class HealthStatContainer : MonoBehaviour
        {
            [SerializeField] private HealthStatSO healthStatSOTemplate;
            [HideInInspector] public HealthStatSO runtimeHealthStats;

            public bool IsDead => runtimeHealthStats.currentHealth <= 0f;

            [SerializeField] private GameObject healthBarPrefab;
            private GameObject runtimeHealthBar;
            [SerializeField] private Vector3 healthBarOffset = new Vector3(0.0f, 4.0f, 0.0f);

            [SerializeField] private GameObject energyShieldBarPrefab;
            private GameObject runtimeEnergyShieldBar;
            [SerializeField] private Vector3 energyShieldBarOffset = new Vector3(0.0f, 8.0f, 0.0f);

            private void Awake()
            {
                if (healthStatSOTemplate != null)
                {
                    runtimeHealthStats = Instantiate(healthStatSOTemplate);
                    runtimeHealthStats.currentHealth = runtimeHealthStats.maximumHealth.StatValue;
                    runtimeHealthStats.currentEnergyShields = runtimeHealthStats.maximumEnergyShields.StatValue;
                    runtimeHealthStats.currentHealthRegenCooldown = 10000f;
                    runtimeHealthStats.currentEnergyShieldsRegenCooldown = 10000f;
                    if (healthBarPrefab != null) { runtimeHealthBar = Instantiate(healthBarPrefab); }
                    if (energyShieldBarPrefab != null) { runtimeEnergyShieldBar = Instantiate(energyShieldBarPrefab); }
                }
            }

            private void OnEnable()
            {
                if (runtimeHealthStats == null) { return; }
                ToggleStatusBars(true);
            }

            private void OnDisable()
            {
                if (runtimeHealthStats == null) { return; }
                ToggleStatusBars(false);
            }

            private void Update()
            {
                if (runtimeHealthStats == null) { return; }
                UpdateStatusBarsLocation(transform.position);

                if (IsDead)
                {
                    gameObject.SetActive(false);
                }

                if (runtimeHealthStats.healthRegenCooldown.canLevelUp)
                {
                    runtimeHealthStats.currentHealthRegenCooldown -= Time.deltaTime;
                }

                if (runtimeHealthStats.energyShieldsRegenCooldown.canLevelUp)
                {
                    runtimeHealthStats.currentEnergyShieldsRegenCooldown -= Time.deltaTime;
                }

                if (runtimeHealthStats.currentHealthRegenCooldown <= 0)
                {
                    HealthManager.Instance.RestoreHealth(this, runtimeHealthStats.healthRegenAmount.StatValue);
                }

                if (runtimeHealthStats.currentEnergyShieldsRegenCooldown <= 0)
                {
                    HealthManager.Instance.RestoreEnergyShields(this, runtimeHealthStats.energyShieldsRegenAmount.StatValue);
                }
            }

            public void UpdateStatusBars()
            {
                if (runtimeHealthBar != null)
                {
                    if (runtimeHealthBar.activeSelf)
                    {
                        float healthPercentLeft = -(1 - (runtimeHealthStats.currentHealth / runtimeHealthStats.maximumHealth.StatValue));
                        runtimeHealthBar.transform.GetChild(0).transform.localPosition = new Vector3(healthPercentLeft, 0f, 0f);
                    }
                }

                if (runtimeEnergyShieldBar != null)
                {
                    if (runtimeEnergyShieldBar.activeSelf)
                    {
                        float energyShieldPercentLeft = -(1 - (runtimeHealthStats.currentEnergyShields / runtimeHealthStats.maximumEnergyShields.StatValue));
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
                    if (runtimeHealthStats.maximumEnergyShields.StatValue > 0f)
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
        }
    }
}