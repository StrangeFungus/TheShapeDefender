namespace ShapeDefender
{
    namespace LevelUpSystem
    {
        using ShapeDefender.DefenseSystem;
        using ShapeDefender.HealthSystem;
        using ShapeDefender.MovementSystem;
        using UnityEngine;

        [System.Serializable]
        public class LevelUpManager : MonoBehaviour
        {
            public static LevelUpManager Instance;

            private void Awake()
            {
                if (Instance == null)
                {
                    Instance = this;
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            public bool AttemptToUnlockStat(ref float callersExperiencePoints, StatEntry callersStatEntry)
            {
                if (callersExperiencePoints >= callersStatEntry.expCostToUnlock)
                {
                    callersExperiencePoints -= callersStatEntry.expCostToUnlock;
                    callersStatEntry.canLevelUp = true;
                    return true;
                }

                return false;
            }

            public bool AttemptToLevelUpStat(ref float callersExperiencePoints, StatEntry callersStatEntry)
            {
                if (callersExperiencePoints >= callersStatEntry.expCostToLevel)
                {
                    callersExperiencePoints -= callersStatEntry.expCostToLevel;
                    callersStatEntry.IncreaseStatsLevel();
                    return true;
                }

                return false;
            }
        }
    }
}