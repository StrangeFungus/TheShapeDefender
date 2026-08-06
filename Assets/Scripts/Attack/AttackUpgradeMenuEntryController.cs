namespace ShapeDefender
{
    namespace LevelUpSystem
    {
        using ShapeDefender.AttackSystem;
        using TMPro;
        using UnityEngine;

        public class AttackUpgradeMenuEntryController : MonoBehaviour
        {
            [SerializeField] private GameObject unlockButton;
            [SerializeField] private GameObject purchaseButton;

            [SerializeField] private TextMeshProUGUI unlockButtonText;
            [SerializeField] private TextMeshProUGUI purchaseButtonText;

            [SerializeField] private TextMeshProUGUI attacksLevelText;

            public void UpdateUnlockButtonText(AttackStatSO attackStatSO)
            {
                unlockButtonText.SetText($"Experience To Unlock:\n{attackStatSO.expCostToUnlock:F2}");
            }

            public void UnlockPurchaseButton(AttackStatSO attackStatSO, int purchaseMultiplier)
            {
                attackStatSO.canLevelUp = true;
                unlockButton.SetActive(false);
                purchaseButton.SetActive(true);
                UpdateMenuEntry(attackStatSO, purchaseMultiplier);
            }

            public void DisablePurchaseButton()
            {
                purchaseButton.SetActive(false);
            }

            public void UpdateMenuEntry(AttackStatSO attacksStatSO, int purchaseMultiplier)
            {
                purchaseButtonText.SetText($"Experience To Level Up:\n{attacksStatSO.CalculateExpCostToLevelUp(purchaseMultiplier):F2}");

                attacksLevelText.SetText($"{attacksStatSO.attacksLevel}");
            }
        }
    }
}
