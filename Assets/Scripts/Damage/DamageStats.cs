
namespace ShapeDefender
{
    namespace DamageSystem
    {
        [System.Serializable]
        public class DamageStats
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
