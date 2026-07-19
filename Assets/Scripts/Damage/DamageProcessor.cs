namespace ShapeDefender
{
    namespace DamageSystem
    {
        using UnityEngine;
        using ShapeDefender.HealthSystem;

        public static class DamageProcessor
        {
            public static void ProcessDamageToTarget(GameObject targetHit, DamageData incomingRuntimeDamageData)
            {
                if (targetHit == null) { return; }
                if (incomingRuntimeDamageData == null) { return; }
                if (!targetHit.TryGetComponent<HealthDataController>(out HealthDataController entitiesHealthDataController)) { return; }

                // HealthData targetsHealthData = entitiesHealthDataController.RuntimeHealthData;
                // DefenseData targetsDefenseData = baseEntityComponent.RuntimeDefenseData;

                // Get damage amount, randomize damage amounr between them and apply it to health. check health amount in entity scripts
                float minDamage = incomingRuntimeDamageData.MinimumDamage;
                float maxDamage = incomingRuntimeDamageData.MaximumDamage;

                float damage = Random.Range(minDamage, maxDamage);

                entitiesHealthDataController.TakeDamage(damage);
            }
        }

    }
}