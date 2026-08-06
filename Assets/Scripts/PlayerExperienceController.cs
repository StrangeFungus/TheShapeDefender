namespace ShapeDefender
{
    namespace UI
    {
        using TMPro;
        using UnityEngine;

        public class PlayerExperienceController : MonoBehaviour
        {
            public static PlayerExperienceController Instance;
            [SerializeField] private TextMeshProUGUI experienceText;

            public float playersExperiencePoints = 10000;
            public float experienceGainMultiplier = 1f;

            public int playersDifficultyRating = 2;

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

            private void Start()
            {
                UpdateExperiencePointTrackerText();
            }

            public void UpdateExperiencePointTrackerText()
            {
                experienceText.SetText($"{playersExperiencePoints:F2}");
            }
        }
    }
}