namespace ShapeDefender
{
    namespace AttackUpgradeSystem
    {
        using System.Collections;
        using System.Collections.Generic;
        using ShapeDefender.AttackSystem;
        using ShapeDefender.LevelUpSystem;
        using ShapeDefender.UI;
        using TMPro;
        using UnityEngine;
        using UnityEngine.UI;

        public class AttackUpgradeMenuManager : MonoBehaviour
        {
            [SerializeField] private AttackContainer playersAttackContainer;
            private PlayerExperienceController playersExpController;
            [SerializeField] private List<AttackUpgradeMenuEntryController> attackUpgradeMenuEntryControllers;

            [SerializeField] private Button openCloseButton;
            [SerializeField] private TextMeshProUGUI openCloseButtonsText;
            private readonly float menuDefaultDuration = 2f;
            private float currentMenuDuration = 0f;
            private float targetMenuDuration = 0f;
            private bool isMenuOpen = false;
            private Coroutine menuSlidingCoroutine;
            private RectTransform menusRectTransform;

            [SerializeField] private TextMeshProUGUI purchaseMultiButtonText;
            private readonly int[] purchaseMultiTextValuePresets = { 1, 5, 10, 100 };
            private int purchaseMultiTextIndex = 0;

            private void Start()
            {
                playersExpController = PlayerExperienceController.Instance;
                SetMenuValues();
                if (playersAttackContainer.runtimeAttackStatSO.TryGetValue(AttackName.NormalArrow, out AttackStatSO attackStatSO))
                {
                    attackUpgradeMenuEntryControllers[0].UnlockPurchaseButton(attackStatSO, purchaseMultiTextValuePresets[purchaseMultiTextIndex]); // Normal Arrow
                }
                menusRectTransform = (RectTransform)transform;
            }

            private void SetMenuValues()
            {
                // UNLOCK COSTS, EXPERIENCE COST AND ATTACKS LEVEL VALUE
                foreach (var entry in AttackUpgradeManager.Instance.RuntimeAttackStatSOTemplates)
                {
                    switch (entry.Key)
                    {
                        case AttackName.NormalArrow:
                            attackUpgradeMenuEntryControllers[0].UpdateUnlockButtonText(entry.Value); // Normal Arrow
                            attackUpgradeMenuEntryControllers[0].UpdateMenuEntry(entry.Value, purchaseMultiTextValuePresets[purchaseMultiTextIndex]); // Normal Arrow
                            break;
                        case AttackName.SimpleRam:
                            attackUpgradeMenuEntryControllers[1].UpdateUnlockButtonText(entry.Value); // Simple Ram
                            attackUpgradeMenuEntryControllers[1].UpdateMenuEntry(entry.Value, purchaseMultiTextValuePresets[purchaseMultiTextIndex]); // Simple Ram
                            break;
                        case AttackName.SmallBomb:
                            attackUpgradeMenuEntryControllers[2].UpdateUnlockButtonText(entry.Value); // Small Bomb
                            attackUpgradeMenuEntryControllers[2].UpdateMenuEntry(entry.Value, purchaseMultiTextValuePresets[purchaseMultiTextIndex]); // Small Bomb
                            break;
                        case AttackName.WindStrike:
                            attackUpgradeMenuEntryControllers[3].UpdateUnlockButtonText(entry.Value); // Wind Strike
                            attackUpgradeMenuEntryControllers[3].UpdateMenuEntry(entry.Value, purchaseMultiTextValuePresets[purchaseMultiTextIndex]); // Wind Strike
                            break;
                    }
                }
            }

            private void CheckForUnlockedAttacks()
            {
                foreach (var entry in playersAttackContainer.runtimeAttackStatSO)
                {
                    switch (entry.Key)
                    {
                        case AttackName.NormalArrow:
                            attackUpgradeMenuEntryControllers[0].UnlockPurchaseButton(entry.Value, purchaseMultiTextValuePresets[purchaseMultiTextIndex]);
                            break;
                        case AttackName.SimpleRam:
                            attackUpgradeMenuEntryControllers[1].UnlockPurchaseButton(entry.Value, purchaseMultiTextValuePresets[purchaseMultiTextIndex]);
                            break;
                        case AttackName.SmallBomb:
                            attackUpgradeMenuEntryControllers[2].UnlockPurchaseButton(entry.Value, purchaseMultiTextValuePresets[purchaseMultiTextIndex]);
                            break;
                        case AttackName.WindStrike:
                            attackUpgradeMenuEntryControllers[3].UnlockPurchaseButton(entry.Value, purchaseMultiTextValuePresets[purchaseMultiTextIndex]);
                            break;
                    }
                }
            }

            public void ReloadMenu()
            {
                SetMenuValues();
                CheckForUnlockedAttacks();
                playersExpController.UpdateExperiencePointTrackerText();
            }

            public void CloseMenu()
            {
                isMenuOpen = true;
                ToggleOpenCloseMenu();
            }

            public void ToggleOpenCloseMenu()
            {
                if (menuSlidingCoroutine != null) { StopCoroutine(menuSlidingCoroutine); }

                Vector3 targetPosition = Vector3.zero; // Closed Position

                if (isMenuOpen)
                {
                    isMenuOpen = false;
                    openCloseButtonsText.SetText("<");
                }
                else
                {
                    isMenuOpen = true;
                    float menuWidth = menusRectTransform.rect.width;
                    targetPosition = new Vector3(-menuWidth, 0, 0);
                    openCloseButtonsText.SetText(">");
                }

                if (currentMenuDuration > 0f) { targetMenuDuration = currentMenuDuration; }
                else { targetMenuDuration = menuDefaultDuration; }

                currentMenuDuration = 0f;
                menuSlidingCoroutine = StartCoroutine(MoveRoutine(targetPosition));
            }

            private IEnumerator MoveRoutine(Vector2 targetPosition)
            {
                Vector2 startPosition = menusRectTransform.anchoredPosition;

                while (currentMenuDuration < targetMenuDuration)
                {
                    currentMenuDuration += Time.deltaTime;
                    float percentageComplete = currentMenuDuration / targetMenuDuration;
                    menusRectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, percentageComplete);
                    yield return null;
                }

                menusRectTransform.anchoredPosition = targetPosition;
                currentMenuDuration = 0f;
            }

            public void TogglePurchaseMultiButton()
            {
                purchaseMultiTextIndex++;
                if (purchaseMultiTextIndex >= purchaseMultiTextValuePresets.Length)
                {
                    purchaseMultiTextIndex = 0;
                }

                purchaseMultiButtonText.SetText(purchaseMultiTextValuePresets[purchaseMultiTextIndex].ToString() + "x");
                ReloadMenu();
            }

            // HEALTH RELATED UNLOCK AND LEVEL UP FUNCTIONS
            public void UnlockNormalArrow()
            {
                bool wasAbleToUnlock = AttackUpgradeManager.Instance.AttemptToUnlockAttack(ref playersExpController.playersExperiencePoints, AttackName.NormalArrow, playersAttackContainer);
                if (!wasAbleToUnlock) { return; }

                attackUpgradeMenuEntryControllers[0].UnlockPurchaseButton(playersAttackContainer.runtimeAttackStatSO[AttackName.NormalArrow], purchaseMultiTextValuePresets[purchaseMultiTextIndex]);
                playersExpController.UpdateExperiencePointTrackerText();

                playersExpController.playersDifficultyRating += 2;
            }

            public void UnlockSimpleRam()
            {
                bool wasAbleToUnlock = AttackUpgradeManager.Instance.AttemptToUnlockAttack(ref playersExpController.playersExperiencePoints, AttackName.SimpleRam, playersAttackContainer);
                if (!wasAbleToUnlock) { Debug.Log($"Couldnt unlock simple ram attack."); return; }

                attackUpgradeMenuEntryControllers[1].UnlockPurchaseButton(playersAttackContainer.runtimeAttackStatSO[AttackName.SimpleRam], purchaseMultiTextValuePresets[purchaseMultiTextIndex]);
                playersExpController.UpdateExperiencePointTrackerText();

                playersExpController.playersDifficultyRating += 2;
            }

            public void UnlockSmallBomb()
            {
                bool wasAbleToUnlock = AttackUpgradeManager.Instance.AttemptToUnlockAttack(ref playersExpController.playersExperiencePoints, AttackName.SmallBomb, playersAttackContainer);
                if (!wasAbleToUnlock) { return; }

                attackUpgradeMenuEntryControllers[2].UnlockPurchaseButton(playersAttackContainer.runtimeAttackStatSO[AttackName.SmallBomb], purchaseMultiTextValuePresets[purchaseMultiTextIndex]);
                playersExpController.UpdateExperiencePointTrackerText();

                playersExpController.playersDifficultyRating += 2;
            }

            public void UnlockWindStrike()
            {
                bool wasAbleToUnlock = AttackUpgradeManager.Instance.AttemptToUnlockAttack(ref playersExpController.playersExperiencePoints, AttackName.WindStrike, playersAttackContainer);
                if (!wasAbleToUnlock) { return; }

                attackUpgradeMenuEntryControllers[3].UnlockPurchaseButton(playersAttackContainer.runtimeAttackStatSO[AttackName.WindStrike], purchaseMultiTextValuePresets[purchaseMultiTextIndex]);
                playersExpController.UpdateExperiencePointTrackerText();

                playersExpController.playersDifficultyRating += 2;
            }

            public void LevelUpNormalArrow()
            {
                bool wasAbleToLevelUp = AttackUpgradeManager.Instance.AttemptToLevelUpAttack(ref playersExpController.playersExperiencePoints, AttackName.NormalArrow, playersAttackContainer, purchaseMultiTextValuePresets[purchaseMultiTextIndex]);
                if (!wasAbleToLevelUp) { return; }

                attackUpgradeMenuEntryControllers[0].UpdateMenuEntry(playersAttackContainer.runtimeAttackStatSO[AttackName.NormalArrow], purchaseMultiTextValuePresets[purchaseMultiTextIndex]);
                playersExpController.UpdateExperiencePointTrackerText();

                playersExpController.playersDifficultyRating += 2;
            }

            public void LevelUpSimpleRam()
            {
                bool wasAbleToLevelUp = AttackUpgradeManager.Instance.AttemptToLevelUpAttack(ref playersExpController.playersExperiencePoints, AttackName.SimpleRam, playersAttackContainer, purchaseMultiTextValuePresets[purchaseMultiTextIndex]);
                if (!wasAbleToLevelUp) { return; }

                attackUpgradeMenuEntryControllers[1].UpdateMenuEntry(playersAttackContainer.runtimeAttackStatSO[AttackName.SimpleRam], purchaseMultiTextValuePresets[purchaseMultiTextIndex]);
                playersExpController.UpdateExperiencePointTrackerText();

                playersExpController.playersDifficultyRating += 2;
            }

            public void LevelUpSmallBomb()
            {
                bool wasAbleToLevelUp = AttackUpgradeManager.Instance.AttemptToLevelUpAttack(ref playersExpController.playersExperiencePoints, AttackName.SmallBomb, playersAttackContainer, purchaseMultiTextValuePresets[purchaseMultiTextIndex]);
                if (!wasAbleToLevelUp) { return; }

                attackUpgradeMenuEntryControllers[2].UpdateMenuEntry(playersAttackContainer.runtimeAttackStatSO[AttackName.SmallBomb], purchaseMultiTextValuePresets[purchaseMultiTextIndex]);
                playersExpController.UpdateExperiencePointTrackerText();

                playersExpController.playersDifficultyRating += 2;
            }

            public void LevelUpWindStrike()
            {
                bool wasAbleToLevelUp = AttackUpgradeManager.Instance.AttemptToLevelUpAttack(ref playersExpController.playersExperiencePoints, AttackName.WindStrike, playersAttackContainer, purchaseMultiTextValuePresets[purchaseMultiTextIndex]);
                if (!wasAbleToLevelUp) { return; }

                attackUpgradeMenuEntryControllers[3].UpdateMenuEntry(playersAttackContainer.runtimeAttackStatSO[AttackName.WindStrike], purchaseMultiTextValuePresets[purchaseMultiTextIndex]);
                playersExpController.UpdateExperiencePointTrackerText();

                playersExpController.playersDifficultyRating += 2;
            }
        }
    }
}