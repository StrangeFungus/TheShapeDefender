namespace ShapeDefender
{
    using UnityEngine;

    [System.Serializable]
    public class StatEntry
    {
        [SerializeField] private float defaultStatValue;
        [SerializeField] private float additiveStatValuePerLevel;
        public float AdditiveStatValuePerLevel { get { return additiveStatValuePerLevel; } }
        [HideInInspector] public int statsLevel = 0;

        public float StatValue { get { if (!canLevelUp) { return 0.0f; } return Mathf.Max(0f, defaultStatValue + (additiveStatValuePerLevel * statsLevel)); } }

        public bool canLevelUp;
        public float expCostToUnlock;
        public float expCostToLevel;
        public string displaySuffix = "";

        public void IncreaseStatsLevel(int value)
        {
            statsLevel += value;
        }

        public void ResetStatsLevel()
        {
            statsLevel = 0;
        }
    }
}
