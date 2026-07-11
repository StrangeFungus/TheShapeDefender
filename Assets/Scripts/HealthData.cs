using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Entity/Health Data", fileName = "New Health Data")]
public class HealthData : ScriptableObject
{
    private float currentHealth = 0f;
    [SerializeField] private float maximumHealth = 0f;
    [SerializeField] private float healthRegenAmount = 0f;
    [SerializeField] private float healthRegenCooldown = 0f;

    [SerializeField] private GameObject healthBarPrefab;
    private GameObject runtimeHealthBar;

    private float currentEnergyShields = 0f;
    [SerializeField] private float maximumEnergyShields = 0f;
    [SerializeField] private float energyShieldRegenAmount = 0f;
    [SerializeField] private float energyShieldRegenCooldown = 0f;

    public bool IsDead => currentHealth <= 0f;

    private FloatingTextSpawner floatingTextSpawner;

    private void Awake()
    {
        currentHealth = maximumHealth;
        currentEnergyShields = maximumEnergyShields;
        runtimeHealthBar = Instantiate(healthBarPrefab);
        floatingTextSpawner = GameObject.Find("FloatingTextSpawner").GetComponent<FloatingTextSpawner>();
    }

    public void TakeDamage(float damageAmount, GameObject targetHit)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();
        floatingTextSpawner.SpawnText(damageAmount.ToString(), Color.red, targetHit.transform.position);
    }

    public string GetHealthValuesString()
    {
        string textToDisplay = $"Current Health: {currentHealth}\n";
        textToDisplay += $"Maximum Health: {maximumHealth}\n";
        textToDisplay += $"Health Regen Amount: {healthRegenAmount}\n";
        textToDisplay += $"Health Regen Cooldown: {healthRegenCooldown}\n";

        textToDisplay += $"Current Energy Shields: {currentEnergyShields}\n";
        textToDisplay += $"Maximum Energy Shields: {maximumEnergyShields}\n";
        textToDisplay += $"Energy Shield Regen Amount: {energyShieldRegenAmount}\n";
        textToDisplay += $"Energy Shield Regen Cooldown: {energyShieldRegenCooldown}";

        return textToDisplay;
    }

    private void UpdateHealthBar()
    {
        float healthPercentLeft = -(1 - (currentHealth / maximumHealth));
        runtimeHealthBar.transform.GetChild(0).transform.localPosition = new Vector3(healthPercentLeft, 0f, 0f);
    }

    public void UpdateHealthBarLocation(Vector2 targetLocation)
    {
        if (runtimeHealthBar != null)
        {
            runtimeHealthBar.transform.position = targetLocation;
        }
    }
}
