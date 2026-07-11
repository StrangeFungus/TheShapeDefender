using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Attack/Damage Data", fileName = "New Damage Data")]
public class DamageData : ScriptableObject
{
    [SerializeField] private float minimumDamage = 0f;
    public float MinimumDamage { get { return minimumDamage; } }
    [SerializeField] private float maximumDamage = 0f;
    public float MaximumDamage { get { return maximumDamage; } }
    [SerializeField] private float criticalHitChance = 0f;
    public float CriticalHitChance { get { return criticalHitChance; } }
    [SerializeField] private float criticalHitDamage = 0f;
    public float CriticalHitDamage { get { return criticalHitDamage; } }

    [SerializeField] private float splashDamageRadius = 0f;
    [SerializeField] private float splashDamageFalloffRate = 0f;

    [SerializeField] private float knockbackDistance = 0f;
    [SerializeField] private float knockbackDamage = 0f;
    [SerializeField] private float ignoreArmorAmount = 0f;

    private void Awake()
    {
        if (minimumDamage > maximumDamage)
        {
            minimumDamage = maximumDamage;
        }
    }
}
