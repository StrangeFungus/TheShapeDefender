using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Attack/Attack Data", fileName = "New Attack Data")]
public class AttackData : ScriptableObject
{
    [SerializeField] private string attackName = "Default Attack";
    [SerializeField] private GameObject projectilePrefab;
    public GameObject ProjectilePrefab { get { return projectilePrefab; } }

    [SerializeField] private bool homesOntoTarget = false;
    public bool HomesOntoTarget { get { return homesOntoTarget; } }

    [SerializeField] private float attackRangeMinimum = 0f;
    public float AttackRangeMinimum { get { return attackRangeMinimum; } }
    [SerializeField] private float attackRangeMaximum = 0f;
    public float AttackRangeMaximum { get { return attackRangeMaximum; } }

    [SerializeField] private float attackCooldown = 0f;
    private float attackCooldownRemaining = 0f;
    public float AttackCooldownRemaining { get { return attackCooldownRemaining; } }
    [SerializeField] private float attackAccuracyAngle = 0f;

    [SerializeField] private float projectileCount = 0f;
    [SerializeField] private float multistrikeChance = 0f;
    [SerializeField] private float multistrikeMaxHitsCombo = 0f;

    [SerializeField] private float targetPiercingQuantity = 0f;

    [SerializeField] private float areaOfEffectRadius = 0f;

    [SerializeField] private float currentSummonsAmount = 0f;
    [SerializeField] private float maximumSummonsLimit = 0f;
    [SerializeField] private float summonSpawnCooldown = 0f;

    public void ReduceCurrentCooldownTime(float timeDelta)
    {
        attackCooldownRemaining -= timeDelta;
    }

    public void ResetCooldownTimer()
    {
        attackCooldownRemaining = attackCooldown;
    }

    public string GetAttackValuesString()
    {
        string textToDisplay = $"Attack Name: {attackName}\n";
        textToDisplay += $"Projectile Prefab: {projectilePrefab}\n";
        textToDisplay += $"Homes Onto Target: {homesOntoTarget}\n";
        textToDisplay += $"Attack Range Minimum: {attackRangeMinimum}\n";
        textToDisplay += $"Attack Range Maximum: {attackRangeMaximum}\n";
        textToDisplay += $"Attack Cooldown: {attackCooldown}\n";
        textToDisplay += $"Attack Cooldown Remaining: {attackCooldownRemaining}\n";
        textToDisplay += $"Attack Accuracy Angle: {attackAccuracyAngle}\n";
        textToDisplay += $"Projectile Count: {projectileCount}\n";
        textToDisplay += $"Multistrike Chance: {multistrikeChance}\n";
        textToDisplay += $"Multistrike Max Hits Combo: {multistrikeMaxHitsCombo}\n";
        textToDisplay += $"Target Piercing Quantity: {targetPiercingQuantity}\n";
        textToDisplay += $"Area Of Effect Radius: {areaOfEffectRadius}\n";
        textToDisplay += $"Current Summons Amount: {currentSummonsAmount}\n";
        textToDisplay += $"Maximum Summons Limit: {maximumSummonsLimit}\n";
        textToDisplay += $"Summon Spawn Cooldown: {summonSpawnCooldown}\n";

        return textToDisplay;
    }
}
