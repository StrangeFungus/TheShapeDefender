namespace ShapeDefender
{
    namespace LevelUpSystem
    {
        using ShapeDefender.Tools;
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

            public bool AttemptToLevelUpStat(ref float callersExperiencePoints, StatEntry callersStatEntry, int purchaseMultiplier)
            {
                float totalCostToLevel = CalculateCost.StatExpCost(callersStatEntry.statsLevel, callersStatEntry.expCostToLevel, purchaseMultiplier);

                if (callersExperiencePoints >= totalCostToLevel)
                {
                    callersExperiencePoints -= totalCostToLevel;
                    callersStatEntry.IncreaseStatsLevel(purchaseMultiplier);
                    Debug.Log($"callersExperiencePoints: {callersExperiencePoints} / Statslevel: {callersStatEntry.statsLevel} / StatsExpCost: {callersStatEntry.expCostToLevel}, Multi: {purchaseMultiplier}");
                    return true;
                }

                return false;
            }
        }
    }
}