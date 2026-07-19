namespace ShapeDefender
{
    namespace LevelUpSystem
    {
        using TMPro;
        using UnityEngine;

        public class LevelUpMenuEntryController : MonoBehaviour
        {
            [SerializeField] private GameObject unlockButton;
            [SerializeField] private GameObject purchaseButton;

            [SerializeField] private TextMeshProUGUI unlockButtonText;
            [SerializeField] private TextMeshProUGUI purchaseButtonText;

            [SerializeField] private TextMeshProUGUI statsLevelValueText;

            public void UpdateUnlockButtonText(float valueToUnlock)
            {
                unlockButtonText.SetText($"Experience To Unlock:\n{valueToUnlock:F2}");
            }

            public void UpdatePurchaseButtonText(float valueToLevelUp)
            {
                purchaseButtonText.SetText($"Experience To Level Up:\n{valueToLevelUp:F2}");
            }

            public void UpdateStatsLevelValueText(float valueToDisplay, string endingStringExtension = "")
            {
                statsLevelValueText.SetText($"{valueToDisplay:F2} {endingStringExtension}");
            }

            public void UnlockPurchaseButton()
            {
                unlockButton.SetActive(false);
                purchaseButton.SetActive(true);
            }
        }
    }
}
