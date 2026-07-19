namespace ShapeDefender
{
    namespace MovementSystem
    {
        using UnityEngine;

        [CreateAssetMenu(menuName = "Shape Defender Systems/Movement Data", fileName = "New Movement Data")]
        public class MovementData : ScriptableObject
        {
            [Header("Ground -- Default Values")]
            [SerializeField] private float defaultGroundSpeed = 0f;
            [SerializeField] private float defaultGroundTurningSpeed = 0f;
            [SerializeField] private float defaultGroundAccelerationSpeed = 0f;
            [SerializeField] private float defaultGroundBrakingSpeed = 0f;

            [Header("Ground -- Level Up Values")]
            [SerializeField] private float groundSpeedPerLevelUp = 0f;
            [SerializeField] private float groundTurningSpeedPerLevelUp = 0f;
            [SerializeField] private float groundAccelerationSpeedPerLevelUp = 0f;
            [SerializeField] private float groundBrakingSpeedPerLevelUp = 0f;

            [Header("Ground -- Stats Levels")]
            public int groundSpeedLevel = 1;
            public int groundTurningSpeedLevel = 1;
            public int groundAccelerationSpeedLevel = 1;
            public int groundBrakingSpeedLevel = 1;

            [Header("Ground -- Stat Level Up Costs")]
            public float groundSpeedUnlockCost = 0f;
            
            public float groundSpeedExpCost = 0f;
            public float groundTurningSpeedExpCost = 0f;
            public float groundAccelerationSpeedExpCost = 0f;
            public float groundBrakingSpeedExpCost = 0f;

            [Header("Ground -- Level Up Bools")]
            public bool canLevelUpGroundSpeedStats = true;

            public float GroundSpeed { get { if (!canLevelUpGroundSpeedStats) { return 0f; } else { return defaultGroundSpeed + (groundSpeedPerLevelUp * groundSpeedLevel); } } }
            public float GroundTurningSpeed { get { if (!canLevelUpGroundSpeedStats) { return 0f; } else { return defaultGroundTurningSpeed + (groundTurningSpeedPerLevelUp * groundTurningSpeedLevel); } } }
            public float GroundAccelerationSpeed { get { if (!canLevelUpGroundSpeedStats) { return 0f; } else { return defaultGroundAccelerationSpeed + (groundAccelerationSpeedPerLevelUp * groundAccelerationSpeedLevel); } } }
            public float GroundBrakingSpeed { get { if (!canLevelUpGroundSpeedStats) { return 0f; } else { return defaultGroundBrakingSpeed + (groundBrakingSpeedPerLevelUp * groundBrakingSpeedLevel); } } }

            [Header("Surface Water -- Default Values")]
            [SerializeField] private float defaultSurfaceWaterSpeed = 0f;
            [SerializeField] private float defaultSurfaceWaterTurningSpeed = 0f;
            [SerializeField] private float defaultSurfaceWaterAccelerationSpeed = 0f;
            [SerializeField] private float defaultSurfaceWaterBrakingSpeed = 0f;

            [Header("Surface Water -- Level Up Values")]
            [SerializeField] private float surfaceWaterSpeedPerLevelUp = 0f;
            [SerializeField] private float surfaceWaterTurningSpeedPerLevelUp = 0f;
            [SerializeField] private float surfaceWaterAccelerationSpeedPerLevelUp = 0f;
            [SerializeField] private float surfaceWaterBrakingSpeedPerLevelUp = 0f;

            [Header("Surface Water -- Stats Levels")]
            public int surfaceWaterSpeedLevel = 1;
            public int surfaceWaterTurningSpeedLevel = 1;
            public int surfaceWaterAccelerationSpeedLevel = 1;
            public int surfaceWaterBrakingSpeedLevel = 1;

            [Header("Surface Water -- Stat Level Up Costs")]
            public float surfaceWaterSpeedUnlockCost = 0f;
            
            public float surfaceWaterSpeedExpCost = 0f;
            public float surfaceWaterTurningSpeedExpCost = 0f;
            public float surfaceWaterAccelerationSpeedExpCost = 0f;
            public float surfaceWaterBrakingSpeedExpCost = 0f;

            [Header("Surface Water -- Level Up Bools")]
            public bool canLevelUpSurfaceWaterSpeedStats = true;

            public float SurfaceWaterSpeed { get { if (!canLevelUpSurfaceWaterSpeedStats) { return 0f; } else { return defaultSurfaceWaterSpeed + (surfaceWaterSpeedPerLevelUp * surfaceWaterSpeedLevel); } } }
            public float SurfaceWaterTurningSpeed { get { if (!canLevelUpSurfaceWaterSpeedStats) { return 0f; } else { return defaultSurfaceWaterTurningSpeed + (surfaceWaterTurningSpeedPerLevelUp * surfaceWaterTurningSpeedLevel); } } }
            public float SurfaceWaterAccelerationSpeed { get { if (!canLevelUpSurfaceWaterSpeedStats) { return 0f; } else { return defaultSurfaceWaterAccelerationSpeed + (surfaceWaterAccelerationSpeedPerLevelUp * surfaceWaterAccelerationSpeedLevel); } } }
            public float SurfaceWaterBrakingSpeed { get { if (!canLevelUpSurfaceWaterSpeedStats) { return 0f; } else { return defaultSurfaceWaterBrakingSpeed + (surfaceWaterBrakingSpeedPerLevelUp * surfaceWaterBrakingSpeedLevel); } } }

            [Header("Underwater -- Default Values")]
            [SerializeField] private float defaultUnderwaterSpeed = 0f;
            [SerializeField] private float defaultUnderwaterTurningSpeed = 0f;
            [SerializeField] private float defaultUnderwaterAccelerationSpeed = 0f;
            [SerializeField] private float defaultUnderwaterBrakingSpeed = 0f;

            [Header("Underwater -- Level Up Values")]
            [SerializeField] private float underwaterSpeedPerLevelUp = 0f;
            [SerializeField] private float underwaterTurningSpeedPerLevelUp = 0f;
            [SerializeField] private float underwaterAccelerationSpeedPerLevelUp = 0f;
            [SerializeField] private float underwaterBrakingSpeedPerLevelUp = 0f;

            [Header("Underwater -- Stats Levels")]
            public int underwaterSpeedLevel = 1;
            public int underwaterTurningSpeedLevel = 1;
            public int underwaterAccelerationSpeedLevel = 1;
            public int underwaterBrakingSpeedLevel = 1;

            [Header("Underwater -- Stat Level Up Costs")]
            public float underwaterSpeedUnlockCost = 0f;
            
            public float underwaterSpeedExpCost = 0f;
            public float underwaterTurningSpeedExpCost = 0f;
            public float underwaterAccelerationSpeedExpCost = 0f;
            public float underwaterBrakingSpeedExpCost = 0f;

            [Header("Underwater -- Level Up Bools")]
            public bool canLevelUpUnderwaterSpeedStats = true;

            public float UnderwaterSpeed { get { if (!canLevelUpUnderwaterSpeedStats) { return 0f; } else { return defaultUnderwaterSpeed + (underwaterSpeedPerLevelUp * underwaterSpeedLevel); } } }
            public float UnderwaterTurningSpeed { get { if (!canLevelUpUnderwaterSpeedStats) { return 0f; } else { return defaultUnderwaterTurningSpeed + (underwaterTurningSpeedPerLevelUp * underwaterTurningSpeedLevel); } } }
            public float UnderwaterAccelerationSpeed { get { if (!canLevelUpUnderwaterSpeedStats) { return 0f; } else { return defaultUnderwaterAccelerationSpeed + (underwaterAccelerationSpeedPerLevelUp * underwaterAccelerationSpeedLevel); } } }
            public float UnderwaterBrakingSpeed { get { if (!canLevelUpUnderwaterSpeedStats) { return 0f; } else { return defaultUnderwaterBrakingSpeed + (underwaterBrakingSpeedPerLevelUp * underwaterBrakingSpeedLevel); } } }

            [Header("Flying -- Default Values")]
            [SerializeField] private float defaultFlyingSpeed = 0f;
            [SerializeField] private float defaultFlyingTurningSpeed = 0f;
            [SerializeField] private float defaultFlyingAccelerationSpeed = 0f;
            [SerializeField] private float defaultFlyingBrakingSpeed = 0f;

            [Header("Flying -- Level Up Values")]
            [SerializeField] private float flyingSpeedPerLevelUp = 0f;
            [SerializeField] private float flyingTurningSpeedPerLevelUp = 0f;
            [SerializeField] private float flyingAccelerationSpeedPerLevelUp = 0f;
            [SerializeField] private float flyingBrakingSpeedPerLevelUp = 0f;

            [Header("Flying -- Stats Levels")]
            public int flyingSpeedLevel = 1;
            public int flyingTurningSpeedLevel = 1;
            public int flyingAccelerationSpeedLevel = 1;
            public int flyingBrakingSpeedLevel = 1;

            [Header("Flying -- Stat Level Up Costs")]
            public float flyingSpeedUnlockCost = 0f;
            
            public float flyingSpeedExpCost = 0f;
            public float flyingTurningSpeedExpCost = 0f;
            public float flyingAccelerationSpeedExpCost = 0f;
            public float flyingBrakingSpeedExpCost = 0f;

            [Header("Flying -- Level Up Bools")]
            public bool canLevelUpFlyingSpeedStats = true;

            public float FlyingSpeed { get { if (!canLevelUpFlyingSpeedStats) { return 0f; } else { return defaultFlyingSpeed + (flyingSpeedPerLevelUp * flyingSpeedLevel); } } }
            public float FlyingTurningSpeed { get { if (!canLevelUpFlyingSpeedStats) { return 0f; } else { return defaultFlyingTurningSpeed + (flyingTurningSpeedPerLevelUp * flyingTurningSpeedLevel); } } }
            public float FlyingAccelerationSpeed { get { if (!canLevelUpFlyingSpeedStats) { return 0f; } else { return defaultFlyingAccelerationSpeed + (flyingAccelerationSpeedPerLevelUp * flyingAccelerationSpeedLevel); } } }
            public float FlyingBrakingSpeed { get { if (!canLevelUpFlyingSpeedStats) { return 0f; } else { return defaultFlyingBrakingSpeed + (flyingBrakingSpeedPerLevelUp * flyingBrakingSpeedLevel); } } }

            [Header("Space Travel -- Default Values")]
            [SerializeField] private float defaultSpaceTravelSpeed = 0f;
            [SerializeField] private float defaultSpaceTravelTurningSpeed = 0f;
            [SerializeField] private float defaultSpaceTravelAccelerationSpeed = 0f;
            [SerializeField] private float defaultSpaceTravelBrakingSpeed = 0f;

            [Header("Space Travel -- Level Up Values")]
            [SerializeField] private float spaceTravelSpeedPerLevelUp = 0f;
            [SerializeField] private float spaceTravelTurningSpeedPerLevelUp = 0f;
            [SerializeField] private float spaceTravelAccelerationSpeedPerLevelUp = 0f;
            [SerializeField] private float spaceTravelBrakingSpeedPerLevelUp = 0f;

            [Header("Space Travel -- Stats Levels")]
            public int spaceTravelSpeedLevel = 1;
            public int spaceTravelTurningSpeedLevel = 1;
            public int spaceTravelAccelerationSpeedLevel = 1;
            public int spaceTravelBrakingSpeedLevel = 1;

            [Header("Space Travel -- Stat Level Up Costs")]
            public float spaceTravelSpeedUnlockCost = 0f;
            
            public float spaceTravelSpeedExpCost = 0f;
            public float spaceTravelTurningSpeedExpCost = 0f;
            public float spaceTravelAccelerationSpeedExpCost = 0f;
            public float spaceTravelBrakingSpeedExpCost = 0f;

            [Header("Space Travel -- Level Up Bools")]
            public bool canLevelUpSpaceTravelSpeedStats = true;

            public float SpaceTravelSpeed { get { if (!canLevelUpSpaceTravelSpeedStats) { return 0f; } else { return defaultSpaceTravelSpeed + (spaceTravelSpeedPerLevelUp * spaceTravelSpeedLevel); } } }
            public float SpaceTravelTurningSpeed { get { if (!canLevelUpSpaceTravelSpeedStats) { return 0f; } else { return defaultSpaceTravelTurningSpeed + (spaceTravelTurningSpeedPerLevelUp * spaceTravelTurningSpeedLevel); } } }
            public float SpaceTravelAccelerationSpeed { get { if (!canLevelUpSpaceTravelSpeedStats) { return 0f; } else { return defaultSpaceTravelAccelerationSpeed + (spaceTravelAccelerationSpeedPerLevelUp * spaceTravelAccelerationSpeedLevel); } } }
            public float SpaceTravelBrakingSpeed { get { if (!canLevelUpSpaceTravelSpeedStats) { return 0f; } else { return defaultSpaceTravelBrakingSpeed + (spaceTravelBrakingSpeedPerLevelUp * spaceTravelBrakingSpeedLevel); } } }
        }
    }
}