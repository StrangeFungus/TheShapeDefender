using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float currentHealth = 0;
    [SerializeField] private float maximumHealth = 0;
    [SerializeField] private float healthRegenAmount = 0;
    [SerializeField] private float healthRegenCooldown = 0;

    [SerializeField] private float currentEnergyShields = 0;
    [SerializeField] private float maximumEnergyShields = 0;
    [SerializeField] private float energyShieldRegenAmount = 0;
    [SerializeField] private float energyShieldRegenCooldown = 0;
}
