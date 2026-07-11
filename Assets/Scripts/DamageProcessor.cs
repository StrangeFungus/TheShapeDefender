using UnityEngine;

public static class DamageProcessor
{
    public static void ProcessDamageToTarget(GameObject targetHit, DamageData incomingRuntimeDamageData)
    {
        if (targetHit == null) { return; }
        if (incomingRuntimeDamageData == null) { return; }
        if (!targetHit.TryGetComponent<BaseEntity>(out BaseEntity baseEntityComponent)) { return; }

        HealthData targetsHealthData = baseEntityComponent.RuntimeHealthData;
        DefenseData targetsDefenseData = baseEntityComponent.RuntimeDefenseData;

        // Get damage amount, randomize damage amounr between them and apply it to health. check health amount in entity scripts
        float minDamage = incomingRuntimeDamageData.MinimumDamage;
        float maxDamage = incomingRuntimeDamageData.MaximumDamage;

        float damage = Random.Range(minDamage, maxDamage);

        targetsHealthData.TakeDamage(damage, targetHit);
    }
}
