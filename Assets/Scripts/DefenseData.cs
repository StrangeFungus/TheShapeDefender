using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Entity/Defense Data", fileName = "New Defense Data")]
public class DefenseData : ScriptableObject
{
    [SerializeField] private float parryChance = 0;
    [SerializeField] private float parryCooldown = 0;
    [SerializeField] private float counterAttackChance = 0;

    [SerializeField] private float blockChance = 0;
    [SerializeField] private float blockCooldown = 0;
    [SerializeField] private float blockAmount = 0;
    [SerializeField] private float reflectAttackChance = 0;
    [SerializeField] private float reflectAttackAngle = 0;

    [SerializeField] private float dodgeChance = 0;
    [SerializeField] private float dodgeCooldown = 0;

    [SerializeField] private float armorValue = 0;
    [SerializeField] private float thornsAmount = 0;

    [SerializeField] private float criticalHitResist = 0;
    [SerializeField] private float criticalHitDamageResist = 0;

    [SerializeField] private float statusEffectResist = 0;
    [SerializeField] private float physicalDamageResist = 0;
    [SerializeField] private float magicalDamageResist = 0;
    [SerializeField] private float energyDamageResist = 0;
}
