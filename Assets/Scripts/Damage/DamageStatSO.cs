namespace ShapeDefender
{
    namespace DamageSystem
    {
        using UnityEngine;

        [CreateAssetMenu(menuName = "Damage Stats", fileName = "New Damage Stats")]
        public class DamageStatSO : ScriptableObject
        {
            public StatEntry minimumDamage;
            public StatEntry maximumDamage;

            public StatEntry criticalHitChance;
            public StatEntry criticalHitDamage;

            public StatEntry splashDamageRadius;
            public StatEntry splashDamageFalloffRate;

            public StatEntry knockbackDistance;
            public StatEntry knockbackDamage;
            public StatEntry ignoreArmorAmount;
        }
    }
}
