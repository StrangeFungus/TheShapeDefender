namespace ShapeDefender
{
    namespace LevelUpSystem
    {
        using System.Collections;
        using System.Collections.Generic;
        using ShapeDefender.DefenseSystem;
        using ShapeDefender.HealthSystem;
        using ShapeDefender.MovementSystem;
        using TMPro;
        using UnityEngine;
        using UnityEngine.UI;

        public class LevelUpMenuManager : MonoBehaviour
        {
            public static LevelUpMenuManager Instance;

            public float playersExperiencePoints = 10000;
            [SerializeField] private TextMeshProUGUI experienceText;
            [SerializeField] private HealthStatContainer playersHealthStatContainer;
            [SerializeField] private DefenseStatContainer playersDefenseStatContainer;
            [SerializeField] private MovementStatContainer playersMovementStatContainer;

            [SerializeField] private List<LevelUpMenuEntryController> levelUpMenuEntryControllers;

            [SerializeField] private Button openCloseButton;
            [SerializeField] private TextMeshProUGUI openCloseButtonsText;
            private readonly float menuDefaultDuration = 2f;
            private float currentMenuDuration = 0f;
            private float targetMenuDuration = 0f;
            private bool isMenuOpen = true;
            private Coroutine menuSlidingCoroutine;
            private RectTransform menusRectTransform;

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

                targetMenuDuration = menuDefaultDuration;
            }

            private void Start()
            {
                SetDefaultMenuValues();
                SetDefaultMenuState();
                menusRectTransform = (RectTransform)transform;
                UpdateExperiencePointTrackerText();
                ToggleOpenCloseMenu();
            }

            private void SetDefaultMenuValues()
            {
                // UNLOCK COSTS
                levelUpMenuEntryControllers[1].UpdateUnlockButtonText(playersHealthStatContainer.runtimeHealthStats.healthRegenAmount); // Health Regen Amount
                levelUpMenuEntryControllers[3].UpdateUnlockButtonText(playersHealthStatContainer.runtimeHealthStats.maximumEnergyShields); // Max Energy Shields
                levelUpMenuEntryControllers[4].UpdateUnlockButtonText(playersHealthStatContainer.runtimeHealthStats.energyShieldsRegenAmount); // Energy Shield Regen Amount
                levelUpMenuEntryControllers[6].UpdateUnlockButtonText(playersDefenseStatContainer.runtimeDefenseStats.parryChance); // Parry Chance
                levelUpMenuEntryControllers[8].UpdateUnlockButtonText(playersDefenseStatContainer.runtimeDefenseStats.counterAttackChance); // Counter Attack Chance
                levelUpMenuEntryControllers[9].UpdateUnlockButtonText(playersDefenseStatContainer.runtimeDefenseStats.blockChance); // Block Chance
                levelUpMenuEntryControllers[11].UpdateUnlockButtonText(playersDefenseStatContainer.runtimeDefenseStats.blockAmount); // Block Amount
                levelUpMenuEntryControllers[12].UpdateUnlockButtonText(playersDefenseStatContainer.runtimeDefenseStats.reflectAttackChance); // Reflect Attack Chance
                levelUpMenuEntryControllers[13].UpdateUnlockButtonText(playersDefenseStatContainer.runtimeDefenseStats.reflectAttackAngle); // Reflect Attack Angle
                levelUpMenuEntryControllers[14].UpdateUnlockButtonText(playersDefenseStatContainer.runtimeDefenseStats.dodgeChance); // Dodge Chance
                levelUpMenuEntryControllers[16].UpdateUnlockButtonText(playersDefenseStatContainer.runtimeDefenseStats.armorValue); // Armor Value
                levelUpMenuEntryControllers[17].UpdateUnlockButtonText(playersDefenseStatContainer.runtimeDefenseStats.thornsAmount); // Thorns Amount
                levelUpMenuEntryControllers[18].UpdateUnlockButtonText(playersDefenseStatContainer.runtimeDefenseStats.criticalHitChanceResist); // Critical Hit Chance Resist
                levelUpMenuEntryControllers[19].UpdateUnlockButtonText(playersDefenseStatContainer.runtimeDefenseStats.criticalHitDamageResist); // Critical Hit Damage Resist
                levelUpMenuEntryControllers[20].UpdateUnlockButtonText(playersDefenseStatContainer.runtimeDefenseStats.statusEffectResist); // Status Effect Resist
                levelUpMenuEntryControllers[21].UpdateUnlockButtonText(playersDefenseStatContainer.runtimeDefenseStats.physicalDamageResist); // Physical Damage Resist
                levelUpMenuEntryControllers[22].UpdateUnlockButtonText(playersDefenseStatContainer.runtimeDefenseStats.magicalDamageResist); // Magical Damage Resist
                levelUpMenuEntryControllers[23].UpdateUnlockButtonText(playersDefenseStatContainer.runtimeDefenseStats.energyDamageResist); // Energy Damage Resist
                levelUpMenuEntryControllers[24].UpdateUnlockButtonText(playersMovementStatContainer.runtimeMovementStats.groundSpeed); // Ground Speed
                levelUpMenuEntryControllers[28].UpdateUnlockButtonText(playersMovementStatContainer.runtimeMovementStats.surfaceWaterSpeed); // Surface Water Speed
                levelUpMenuEntryControllers[32].UpdateUnlockButtonText(playersMovementStatContainer.runtimeMovementStats.underwaterSpeed); // Underwater Speed
                levelUpMenuEntryControllers[36].UpdateUnlockButtonText(playersMovementStatContainer.runtimeMovementStats.flyingSpeed); // Flying Speed
                levelUpMenuEntryControllers[40].UpdateUnlockButtonText(playersMovementStatContainer.runtimeMovementStats.spaceTravelSpeed); // Space Travel Speed

                // EXPERIENCE COST AND STATS LEVEL VALUE
                levelUpMenuEntryControllers[0].UpdateMenuEntry(playersHealthStatContainer.runtimeHealthStats.maximumHealth); // Maximum Health
                levelUpMenuEntryControllers[1].UpdateMenuEntry(playersHealthStatContainer.runtimeHealthStats.healthRegenAmount); // Health Regen Amount
                levelUpMenuEntryControllers[2].UpdateMenuEntry(playersHealthStatContainer.runtimeHealthStats.healthRegenCooldown); // Health Regen Cooldown
                levelUpMenuEntryControllers[3].UpdateMenuEntry(playersHealthStatContainer.runtimeHealthStats.maximumEnergyShields); // Max Energy Shields
                levelUpMenuEntryControllers[4].UpdateMenuEntry(playersHealthStatContainer.runtimeHealthStats.energyShieldsRegenAmount); // Energy Shield Regen Amount
                levelUpMenuEntryControllers[5].UpdateMenuEntry(playersHealthStatContainer.runtimeHealthStats.energyShieldsRegenCooldown); // Energy Shield Regen Cooldown
                levelUpMenuEntryControllers[6].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.parryChance); // Parry Chance
                levelUpMenuEntryControllers[7].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.parryCooldown); // Parry Cooldown
                levelUpMenuEntryControllers[8].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.counterAttackChance); // Counter Attack Chance
                levelUpMenuEntryControllers[9].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.blockChance); // Block Chance
                levelUpMenuEntryControllers[10].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.blockCooldown); // Block Cooldown
                levelUpMenuEntryControllers[11].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.blockAmount); // Block Amount
                levelUpMenuEntryControllers[12].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.reflectAttackChance); // Reflect Attack Chance
                levelUpMenuEntryControllers[13].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.reflectAttackChance); // Reflect Attack Angle
                levelUpMenuEntryControllers[14].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.dodgeChance); // Dodge Chance
                levelUpMenuEntryControllers[15].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.dodgeCooldown); // Dodge Cooldown
                levelUpMenuEntryControllers[16].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.armorValue); // Armor Value
                levelUpMenuEntryControllers[17].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.thornsAmount); // Thorns Amount
                levelUpMenuEntryControllers[18].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.criticalHitChanceResist); // Critical Hit Chance Resist
                levelUpMenuEntryControllers[19].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.criticalHitDamageResist); // Critical Hit Damage Resist
                levelUpMenuEntryControllers[20].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.statusEffectResist); // Status Effect Resist
                levelUpMenuEntryControllers[21].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.physicalDamageResist); // Physical Damage Resist
                levelUpMenuEntryControllers[22].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.magicalDamageResist); // Magical Damage Resist
                levelUpMenuEntryControllers[23].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.energyDamageResist); // Energy Damage Resist
                levelUpMenuEntryControllers[24].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.groundSpeed); // Ground Speed
                levelUpMenuEntryControllers[25].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.groundTurningSpeed); // Ground Turning Speed
                levelUpMenuEntryControllers[26].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.groundAccelerationSpeed); // Ground Acceleration Speed
                levelUpMenuEntryControllers[27].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.groundBrakingSpeed); // Ground Braking Speed
                levelUpMenuEntryControllers[28].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.surfaceWaterSpeed); // Surface Water Speed
                levelUpMenuEntryControllers[29].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.surfaceWaterTurningSpeed); // Surface Water Turning Speed
                levelUpMenuEntryControllers[30].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.surfaceWaterAccelerationSpeed); // Surface Water Acceleration Speed
                levelUpMenuEntryControllers[31].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.surfaceWaterBrakingSpeed); // Surface Water Braking Speed
                levelUpMenuEntryControllers[32].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.underwaterSpeed); // Underwater Speed
                levelUpMenuEntryControllers[33].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.underwaterTurningSpeed); // Underwater Turning Speed
                levelUpMenuEntryControllers[34].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.underwaterAccelerationSpeed); // Underwater Acceleration Speed
                levelUpMenuEntryControllers[35].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.underwaterBrakingSpeed); // Underwater Braking Speed
                levelUpMenuEntryControllers[36].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.flyingSpeed); // Flying Speed
                levelUpMenuEntryControllers[37].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.flyingTurningSpeed); // Flying Turning Speed
                levelUpMenuEntryControllers[38].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.flyingAccelerationSpeed); // Flying Acceleration Speed
                levelUpMenuEntryControllers[39].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.flyingBrakingSpeed); // Flying Braking Speed
                levelUpMenuEntryControllers[40].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.spaceTravelSpeed); // Space Travel Speed
                levelUpMenuEntryControllers[41].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.spaceTravelTurningSpeed); // Space Travel Turning Speed
                levelUpMenuEntryControllers[42].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.spaceTravelAccelerationSpeed); // Space Travel Acceleration Speed
                levelUpMenuEntryControllers[43].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.spaceTravelBrakingSpeed); // Space Travel Braking Speed
            }

            private void SetDefaultMenuState()
            {
                levelUpMenuEntryControllers[0].UnlockPurchaseButton(playersHealthStatContainer.runtimeHealthStats.maximumHealth); // Max Health
                levelUpMenuEntryControllers[2].gameObject.SetActive(false); // Health Regen Cooldown
                levelUpMenuEntryControllers[4].gameObject.SetActive(false); // Energy Shield Regen Amount
                levelUpMenuEntryControllers[5].gameObject.SetActive(false); // Energy Shield Regen Cooldown

                levelUpMenuEntryControllers[7].gameObject.SetActive(false); // Parry Cooldown
                levelUpMenuEntryControllers[8].gameObject.SetActive(false); // Counter Attack Chance
                levelUpMenuEntryControllers[10].gameObject.SetActive(false); // Block Cooldown
                levelUpMenuEntryControllers[11].gameObject.SetActive(false); // Block Amount
                levelUpMenuEntryControllers[12].gameObject.SetActive(false); // Reflect Attack Chance
                levelUpMenuEntryControllers[13].gameObject.SetActive(false); // Reflect Attack Angle
                levelUpMenuEntryControllers[15].gameObject.SetActive(false); // Dodge Cooldown

                levelUpMenuEntryControllers[25].gameObject.SetActive(false); // Ground Turning Speed
                levelUpMenuEntryControllers[26].gameObject.SetActive(false); // Ground Acceleration Speed
                levelUpMenuEntryControllers[27].gameObject.SetActive(false); // Ground Braking Speed
                levelUpMenuEntryControllers[29].gameObject.SetActive(false); // Surface Water Turning Speed
                levelUpMenuEntryControllers[30].gameObject.SetActive(false); // Surface Water Acceleration Speed
                levelUpMenuEntryControllers[31].gameObject.SetActive(false); // Surface Water Braking Speed
                levelUpMenuEntryControllers[33].gameObject.SetActive(false); // Underwater Turning Speed
                levelUpMenuEntryControllers[34].gameObject.SetActive(false); // Underwater Acceleration Speed
                levelUpMenuEntryControllers[35].gameObject.SetActive(false); // Underwater Braking Speed
                levelUpMenuEntryControllers[37].gameObject.SetActive(false); // Flying Turning Speed
                levelUpMenuEntryControllers[38].gameObject.SetActive(false); // Flying Acceleration Speed
                levelUpMenuEntryControllers[39].gameObject.SetActive(false); // Flying Braking Speed
                levelUpMenuEntryControllers[41].gameObject.SetActive(false); // Space Travel Turning Speed
                levelUpMenuEntryControllers[42].gameObject.SetActive(false); // Space Travel Acceleration Speed
                levelUpMenuEntryControllers[43].gameObject.SetActive(false); // Space Travel Braking Speed
            }

            public void ToggleOpenCloseMenu()
            {
                if (menuSlidingCoroutine != null) { StopCoroutine(menuSlidingCoroutine); }

                Vector3 targetPosition = Vector3.zero; // Open Position

                if (isMenuOpen)
                {
                    isMenuOpen = false;
                    float menuWidth = menusRectTransform.rect.width;
                    targetPosition = new Vector3(-menuWidth, 0, 0);
                    openCloseButtonsText.SetText(">");
                }
                else
                {
                    isMenuOpen = true;
                    openCloseButtonsText.SetText("<");
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

            public void UpdateExperiencePointTrackerText()
            {
                experienceText.SetText($"{playersExperiencePoints:F2}");
            }

            // HEALTH RELATED UNLOCK AND LEVEL UP FUNCTIONS
            public void UnlockHealthRegenAmount()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersHealthStatContainer.runtimeHealthStats.healthRegenAmount);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[1].UnlockPurchaseButton(playersHealthStatContainer.runtimeHealthStats.healthRegenAmount); // Health Regen Amount
                levelUpMenuEntryControllers[2].UnlockPurchaseButton(playersHealthStatContainer.runtimeHealthStats.healthRegenCooldown); // Health Regen Cooldown
                levelUpMenuEntryControllers[2].gameObject.SetActive(true); // Health Regen Cooldown
                UpdateExperiencePointTrackerText();
                playersHealthStatContainer.runtimeHealthStats.currentHealthRegenCooldown = playersHealthStatContainer.runtimeHealthStats.healthRegenCooldown.StatValue;
            }

            public void UnlockMaximumEnergyShield()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersHealthStatContainer.runtimeHealthStats.maximumEnergyShields);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[3].UnlockPurchaseButton(playersHealthStatContainer.runtimeHealthStats.maximumEnergyShields); // Max Energy Shields
                levelUpMenuEntryControllers[4].gameObject.SetActive(true); // Energy Shield Regen Amount
                UpdateExperiencePointTrackerText();
                playersHealthStatContainer.ToggleStatusBars(true);
                playersHealthStatContainer.UpdateStatusBars();
            }

            public void UnlockEnergyShieldRegenAmount()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersHealthStatContainer.runtimeHealthStats.energyShieldsRegenAmount);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[4].UnlockPurchaseButton(playersHealthStatContainer.runtimeHealthStats.energyShieldsRegenAmount); // Energy Shield Regen Amount
                levelUpMenuEntryControllers[5].UnlockPurchaseButton(playersHealthStatContainer.runtimeHealthStats.energyShieldsRegenCooldown); // Energy Shield Regen Cooldown
                levelUpMenuEntryControllers[5].gameObject.SetActive(true); // Energy Shield Regen Cooldown
                UpdateExperiencePointTrackerText();
                playersHealthStatContainer.runtimeHealthStats.currentHealthRegenCooldown = playersHealthStatContainer.runtimeHealthStats.energyShieldsRegenCooldown.StatValue;
            }

            public void LevelUpMaximumHealth()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersHealthStatContainer.runtimeHealthStats.maximumHealth);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[0].UpdateMenuEntry(playersHealthStatContainer.runtimeHealthStats.maximumHealth); // Maximum Health
                UpdateExperiencePointTrackerText();
                playersHealthStatContainer.UpdateStatusBars();
            }

            public void LevelUpHealthRegenAmount()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersHealthStatContainer.runtimeHealthStats.healthRegenAmount);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[1].UpdateMenuEntry(playersHealthStatContainer.runtimeHealthStats.healthRegenAmount); // Health Regen Amount
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpHealthRegenCooldown()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersHealthStatContainer.runtimeHealthStats.healthRegenCooldown);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[2].UpdateMenuEntry(playersHealthStatContainer.runtimeHealthStats.healthRegenCooldown); // Health Regen Cooldown
                playersHealthStatContainer.runtimeHealthStats.currentHealthRegenCooldown += playersHealthStatContainer.runtimeHealthStats.healthRegenCooldown.AdditiveStatValuePerLevel;
                UpdateExperiencePointTrackerText();

                if (playersHealthStatContainer.runtimeHealthStats.healthRegenCooldown.StatValue <= 0f)
                {
                    levelUpMenuEntryControllers[2].DisablePurchaseButton();
                }
            }

            public void LevelUpMaximumEnergyShield()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersHealthStatContainer.runtimeHealthStats.maximumEnergyShields);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[3].UpdateMenuEntry(playersHealthStatContainer.runtimeHealthStats.maximumEnergyShields); // Max Energy Shields
                UpdateExperiencePointTrackerText();
                playersHealthStatContainer.ToggleStatusBars(true);
                playersHealthStatContainer.UpdateStatusBars();
            }

            public void LevelUpEnergyShieldRegenAmount()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersHealthStatContainer.runtimeHealthStats.energyShieldsRegenAmount);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[4].UpdateMenuEntry(playersHealthStatContainer.runtimeHealthStats.energyShieldsRegenAmount); // Energy Shield Regen Amount
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpEnergyShieldRegenCooldown()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersHealthStatContainer.runtimeHealthStats.energyShieldsRegenCooldown);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[5].UpdateMenuEntry(playersHealthStatContainer.runtimeHealthStats.energyShieldsRegenCooldown); // Energy Shield Regen Cooldown
                playersHealthStatContainer.runtimeHealthStats.currentEnergyShieldsRegenCooldown += playersHealthStatContainer.runtimeHealthStats.energyShieldsRegenCooldown.AdditiveStatValuePerLevel;
                UpdateExperiencePointTrackerText();

                if (playersHealthStatContainer.runtimeHealthStats.energyShieldsRegenCooldown.StatValue <= 0f)
                {
                    levelUpMenuEntryControllers[5].DisablePurchaseButton();
                }
            }

            // DEFENSE RELATED UNLOCK AND LEVEL UP FUNCTIONS
            public void UnlockParryChance()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.parryChance);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[6].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.parryChance); // Parry Chance
                levelUpMenuEntryControllers[7].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.parryCooldown); // Parry Cooldown
                levelUpMenuEntryControllers[7].gameObject.SetActive(true); // Parry Cooldown
                levelUpMenuEntryControllers[8].gameObject.SetActive(true); // Counter Attack Chance
                UpdateExperiencePointTrackerText();
            }

            public void UnlockCounterAttackChance()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.counterAttackChance);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[8].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.counterAttackChance); // Counter Attack Chance
                UpdateExperiencePointTrackerText();
            }

            public void UnlockBlockChance()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.blockChance);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[9].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.blockChance); // Block Chance
                levelUpMenuEntryControllers[10].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.blockCooldown); // Block Cooldown
                levelUpMenuEntryControllers[10].gameObject.SetActive(true); // Block Cooldown
                levelUpMenuEntryControllers[11].gameObject.SetActive(true); // Block Amount
                levelUpMenuEntryControllers[12].gameObject.SetActive(true); // Reflect Attack Chance
                UpdateExperiencePointTrackerText();
            }

            public void UnlockBlockAmount()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.blockAmount);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[11].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.blockAmount); // Block Amount
                UpdateExperiencePointTrackerText();
            }

            public void UnlockReflectAttackChance()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.reflectAttackChance);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[12].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.reflectAttackChance); // Reflect Attack Chance
                levelUpMenuEntryControllers[13].gameObject.SetActive(true); // Reflect Attack Angle
                UpdateExperiencePointTrackerText();
            }

            public void UnlockReflectAttackAngle()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.reflectAttackAngle);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[13].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.reflectAttackAngle); // Reflect Attack Angle
                UpdateExperiencePointTrackerText();
            }

            public void UnlockDodgeChance()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.dodgeChance);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[14].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.dodgeChance); // Dodge Chance
                levelUpMenuEntryControllers[15].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.dodgeCooldown); // Dodge Cooldown
                levelUpMenuEntryControllers[15].gameObject.SetActive(true); // Dodge Cooldown
                UpdateExperiencePointTrackerText();
            }

            public void UnlockArmorValue()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.armorValue);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[16].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.armorValue); // Armor Value
                UpdateExperiencePointTrackerText();
            }

            public void UnlockThornsAmount()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.thornsAmount);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[17].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.thornsAmount); // Thorns Amount
                UpdateExperiencePointTrackerText();
            }

            public void UnlockCriticalHitChanceResist()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.criticalHitChanceResist);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[18].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.criticalHitChanceResist); // Critical Hit Chance Resist
                UpdateExperiencePointTrackerText();
            }

            public void UnlockCriticalHitDamageResist()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.criticalHitDamageResist);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[19].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.criticalHitDamageResist); // Critical Hit Damage Resist
                UpdateExperiencePointTrackerText();
            }

            public void UnlockStatusEffectResist()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.statusEffectResist);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[20].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.statusEffectResist); // Status Effect Resist
                UpdateExperiencePointTrackerText();
            }

            public void UnlockPhysicalDamageResist()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.physicalDamageResist);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[21].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.physicalDamageResist); // Physical Damage Resist
                UpdateExperiencePointTrackerText();
            }

            public void UnlockMagicalDamageResist()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.magicalDamageResist);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[22].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.magicalDamageResist); // Magical Damage Resist
                UpdateExperiencePointTrackerText();
            }

            public void UnlockEnergyDamageResist()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.energyDamageResist);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[23].UnlockPurchaseButton(playersDefenseStatContainer.runtimeDefenseStats.energyDamageResist); // Energy Damage Resist
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpParryChance()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.parryChance);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[6].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.parryChance); // Parry Chance
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpParryCooldown()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.parryCooldown);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[7].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.parryCooldown); // Parry Cooldown
                playersDefenseStatContainer.runtimeDefenseStats.currentParryCooldown += playersDefenseStatContainer.runtimeDefenseStats.parryCooldown.AdditiveStatValuePerLevel;
                UpdateExperiencePointTrackerText();

                if (playersDefenseStatContainer.runtimeDefenseStats.parryCooldown.StatValue <= 0f)
                {
                    levelUpMenuEntryControllers[7].DisablePurchaseButton();
                }
            }

            public void LevelUpCounterAttackChance()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.counterAttackChance);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[8].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.counterAttackChance); // Counter Attack Chance
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpBlockChance()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.blockChance);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[9].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.blockChance); // Block Chance
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpBlockCooldown()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.blockCooldown);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[10].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.blockCooldown); // Block Cooldown
                playersDefenseStatContainer.runtimeDefenseStats.currentBlockCooldown += playersDefenseStatContainer.runtimeDefenseStats.blockCooldown.AdditiveStatValuePerLevel;
                UpdateExperiencePointTrackerText();

                if (playersDefenseStatContainer.runtimeDefenseStats.blockCooldown.StatValue <= 0f)
                {
                    levelUpMenuEntryControllers[10].DisablePurchaseButton();
                }
            }

            public void LevelUpBlockAmount()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.blockAmount);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[11].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.blockAmount); // Block Amount
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpReflectAttackChance()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.reflectAttackChance);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[12].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.reflectAttackChance); // Reflect Attack Chance
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpReflectAttackAngle()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.reflectAttackAngle);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[13].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.reflectAttackChance); // Reflect Attack Angle
                UpdateExperiencePointTrackerText();

                if (playersDefenseStatContainer.runtimeDefenseStats.reflectAttackAngle.StatValue <= 0f)
                {
                    levelUpMenuEntryControllers[13].DisablePurchaseButton();
                }
            }

            public void LevelUpDodgeChance()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.dodgeChance);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[14].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.dodgeChance); // Dodge Chance
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpDodgeCooldown()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.dodgeCooldown);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[15].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.dodgeCooldown); // Dodge Cooldown
                playersDefenseStatContainer.runtimeDefenseStats.currentDodgeCooldown += playersDefenseStatContainer.runtimeDefenseStats.dodgeCooldown.AdditiveStatValuePerLevel;
                UpdateExperiencePointTrackerText();

                if (playersDefenseStatContainer.runtimeDefenseStats.dodgeCooldown.StatValue <= 0f)
                {
                    levelUpMenuEntryControllers[15].DisablePurchaseButton();
                }
            }

            public void LevelUpArmorValue()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.armorValue);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[16].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.armorValue); // Armor Value
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpThornsAmount()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.thornsAmount);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[17].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.thornsAmount); // Thorns Amount
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpCriticalHitChanceResist()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.criticalHitChanceResist);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[18].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.criticalHitChanceResist); // Critical Hit Chance Resist
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpCriticalHitDamageResist()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.criticalHitDamageResist);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[19].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.criticalHitDamageResist); // Critical Hit Damage Resist
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpStatusEffectResist()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.statusEffectResist);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[20].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.statusEffectResist); // Status Effect Resist
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpPhysicalDamageResist()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.physicalDamageResist);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[21].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.physicalDamageResist); // Physical Damage Resist
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpMagicalDamageResist()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.magicalDamageResist);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[22].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.magicalDamageResist); // Magical Damage Resist
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpEnergyDamageResist()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersDefenseStatContainer.runtimeDefenseStats.energyDamageResist);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[23].UpdateMenuEntry(playersDefenseStatContainer.runtimeDefenseStats.energyDamageResist); // Energy Damage Resist
                UpdateExperiencePointTrackerText();
            }

            // MOVEMENT RELATED UNLOCK AND LEVEL UP FUNCTIONS
            public void UnlockGroundSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.groundSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[24].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.groundSpeed); // Ground Speed
                levelUpMenuEntryControllers[25].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.groundTurningSpeed); // Ground Turning Speed
                levelUpMenuEntryControllers[25].gameObject.SetActive(true); // Ground Turning Speed
                levelUpMenuEntryControllers[26].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.groundAccelerationSpeed); // Ground Acceleration Speed
                levelUpMenuEntryControllers[26].gameObject.SetActive(true); // Ground Acceleration Speed
                levelUpMenuEntryControllers[27].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.groundBrakingSpeed); // Ground Braking Speed
                levelUpMenuEntryControllers[27].gameObject.SetActive(true); // Ground Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void UnlockSurfaceWaterSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.surfaceWaterSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[28].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.surfaceWaterSpeed); // Surface Water Speed
                levelUpMenuEntryControllers[29].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.surfaceWaterTurningSpeed); // Surface Water Turning Speed
                levelUpMenuEntryControllers[29].gameObject.SetActive(true); // Surface Water Turning Speed
                levelUpMenuEntryControllers[30].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.surfaceWaterAccelerationSpeed); // Surface Water Acceleration Speed
                levelUpMenuEntryControllers[30].gameObject.SetActive(true); // Surface Water Acceleration Speed
                levelUpMenuEntryControllers[31].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.surfaceWaterBrakingSpeed); // Surface Water Braking Speed
                levelUpMenuEntryControllers[31].gameObject.SetActive(true); // Surface Water Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void UnlockUnderwaterSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.underwaterSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[32].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.underwaterSpeed); // Underwater Speed
                levelUpMenuEntryControllers[33].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.underwaterTurningSpeed); // Underwater Turning Speed
                levelUpMenuEntryControllers[33].gameObject.SetActive(true); // Underwater Turning Speed
                levelUpMenuEntryControllers[34].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.underwaterAccelerationSpeed); // Underwater Acceleration Speed
                levelUpMenuEntryControllers[34].gameObject.SetActive(true); // Underwater Acceleration Speed
                levelUpMenuEntryControllers[35].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.underwaterBrakingSpeed); // Underwater Braking Speed
                levelUpMenuEntryControllers[35].gameObject.SetActive(true); // Underwater Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void UnlockFlyingSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.flyingSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[36].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.flyingSpeed); // Flying Speed
                levelUpMenuEntryControllers[37].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.flyingTurningSpeed); // Flying Turning Speed
                levelUpMenuEntryControllers[37].gameObject.SetActive(true); // Flying Turning Speed
                levelUpMenuEntryControllers[38].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.flyingAccelerationSpeed); // Flying Acceleration Speed
                levelUpMenuEntryControllers[38].gameObject.SetActive(true); // Flying Acceleration Speed
                levelUpMenuEntryControllers[39].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.flyingBrakingSpeed); // Flying Braking Speed
                levelUpMenuEntryControllers[39].gameObject.SetActive(true); // Flying Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void UnlockSpaceTravelSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToUnlockStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.spaceTravelSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[40].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.spaceTravelSpeed); // Space Travel Speed
                levelUpMenuEntryControllers[41].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.spaceTravelTurningSpeed); // Space Travel Turning Speed
                levelUpMenuEntryControllers[41].gameObject.SetActive(true); // Space Travel Turning Speed
                levelUpMenuEntryControllers[42].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.spaceTravelAccelerationSpeed); // Space Travel Acceleration Speed
                levelUpMenuEntryControllers[42].gameObject.SetActive(true); // Space Travel Acceleration Speed
                levelUpMenuEntryControllers[43].UnlockPurchaseButton(playersMovementStatContainer.runtimeMovementStats.spaceTravelBrakingSpeed); // Space Travel Braking Speed
                levelUpMenuEntryControllers[43].gameObject.SetActive(true); // Space Travel Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpGroundSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.groundSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[24].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.groundSpeed); // Ground Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpGroundTurningSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.groundTurningSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[25].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.groundTurningSpeed); // Ground Turning Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpGroundAccelerationSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.groundAccelerationSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[26].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.groundAccelerationSpeed); // Ground Acceleration Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpGroundBrakingSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.groundBrakingSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[27].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.groundBrakingSpeed); // Ground Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpSurfaceWaterSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.surfaceWaterSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[28].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.surfaceWaterSpeed); // Surface Water Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpSurfaceWaterTurningSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.surfaceWaterTurningSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[29].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.surfaceWaterTurningSpeed); // Surface Water Turning Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpSurfaceWaterAccelerationSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.surfaceWaterAccelerationSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[30].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.surfaceWaterAccelerationSpeed); // Surface Water Acceleration Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpSurfaceWaterBrakingSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.surfaceWaterBrakingSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[31].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.surfaceWaterBrakingSpeed); // Surface Water Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpUnderwaterSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.underwaterSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[32].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.underwaterSpeed); // Underwater Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpUnderwaterTurningSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.underwaterTurningSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[33].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.underwaterTurningSpeed); // Underwater Turning Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpUnderwaterAccelerationSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.underwaterAccelerationSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[34].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.underwaterAccelerationSpeed); // Underwater Acceleration Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpUnderwaterBrakingSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.underwaterBrakingSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[35].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.underwaterBrakingSpeed); // Underwater Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpFlyingSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.flyingSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[36].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.flyingSpeed); // Flying Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpFlyingTurningSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.flyingTurningSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[37].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.flyingTurningSpeed); // Flying Turning Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpFlyingAccelerationSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.flyingAccelerationSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[38].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.flyingAccelerationSpeed); // Flying Acceleration Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpFlyingBrakingSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.flyingBrakingSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[39].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.flyingBrakingSpeed); // Flying Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpSpaceTravelSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.spaceTravelSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[40].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.spaceTravelSpeed); // Space Travel Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpSpaceTravelTurningSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.spaceTravelTurningSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[41].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.spaceTravelTurningSpeed); // Space Travel Turning Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpSpaceTravelAccelerationSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.spaceTravelAccelerationSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[42].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.spaceTravelAccelerationSpeed); // Space Travel Acceleration Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }

            public void LevelUpSpaceTravelBrakingSpeed()
            {
                bool wasAbleToUnlock = LevelUpManager.Instance.AttemptToLevelUpStat(ref playersExperiencePoints, playersMovementStatContainer.runtimeMovementStats.spaceTravelBrakingSpeed);
                if (!wasAbleToUnlock) { return; }

                levelUpMenuEntryControllers[43].UpdateMenuEntry(playersMovementStatContainer.runtimeMovementStats.spaceTravelBrakingSpeed); // Space Travel Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementStatContainer.UpdateMovementStats();
            }
        }
    }
}
