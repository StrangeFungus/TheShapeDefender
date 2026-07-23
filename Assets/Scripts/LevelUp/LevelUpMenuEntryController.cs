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

            public void UpdateUnlockButtonText(StatEntry statsEntry)
            {
                float valueToUnlock = statsEntry.expCostToUnlock;
                unlockButtonText.SetText($"Experience To Unlock:\n{valueToUnlock:F2}");
            }

            public void UnlockPurchaseButton(StatEntry statsEntry)
            {
                statsEntry.canLevelUp = true;
                unlockButton.SetActive(false);
                purchaseButton.SetActive(true);
                UpdateMenuEntry(statsEntry);
            }

            public void DisablePurchaseButton()
            {
                purchaseButton.SetActive(false);
            }

            public void UpdateMenuEntry(StatEntry statsEntry)
            {
                float valueToLevelUp = statsEntry.expCostToLevel;
                purchaseButtonText.SetText($"Experience To Level Up:\n{valueToLevelUp:F2}");

                float valueToDisplay = statsEntry.StatValue;
                statsLevelValueText.SetText($"{valueToDisplay:F2} {statsEntry.displaySuffix}");
            }
        }
    }
}
