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

            private float playersExperiencePoints = 10000;
            [SerializeField] private TextMeshProUGUI experienceText;
            [SerializeField] private HealthDataController playersHealthDataController;
            [SerializeField] private DefenseDataController playersDefenseDataController;
            [SerializeField] private MovementDataController playersMovementDataController;

            [SerializeField] private List<LevelUpMenuEntryController> levelUpMenuEntryControllers;

            [SerializeField] private Button openCloseButton;
            [SerializeField] private TextMeshProUGUI openCloseButtonsText;
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
            }

            private void Start()
            {
                SetDefaultMenuValues();
                SetDefaultMenuState();
                menusRectTransform = (RectTransform)transform;
                UpdateExperiencePointTrackerText();
            }

            private void SetDefaultMenuValues()
            {
                // UNLOCK COSTS
                levelUpMenuEntryControllers[1].UpdateUnlockButtonText(playersHealthDataController.RuntimeHealthData.healthRegenAmountUnlockCost); // Health Regen Amount
                levelUpMenuEntryControllers[3].UpdateUnlockButtonText(playersHealthDataController.RuntimeHealthData.maximumEnergyShieldUnlockCost); // Max Energy Shields
                levelUpMenuEntryControllers[4].UpdateUnlockButtonText(playersHealthDataController.RuntimeHealthData.energyShieldsRegenAmountUnlockCost); // Energy Shield Regen Amount
                levelUpMenuEntryControllers[6].UpdateUnlockButtonText(playersDefenseDataController.RuntimeDefenseData.parryChanceUnlockCost); // Parry Chance
                levelUpMenuEntryControllers[8].UpdateUnlockButtonText(playersDefenseDataController.RuntimeDefenseData.counterAttackChanceUnlockCost); // Counter Attack Chance
                levelUpMenuEntryControllers[9].UpdateUnlockButtonText(playersDefenseDataController.RuntimeDefenseData.blockChanceUnlockCost); // Block Chance
                levelUpMenuEntryControllers[11].UpdateUnlockButtonText(playersDefenseDataController.RuntimeDefenseData.blockAmountUnlockCost); // Block Amount
                levelUpMenuEntryControllers[12].UpdateUnlockButtonText(playersDefenseDataController.RuntimeDefenseData.reflectAttackChanceUnlockCost); // Reflect Attack Chance
                levelUpMenuEntryControllers[13].UpdateUnlockButtonText(playersDefenseDataController.RuntimeDefenseData.reflectAttackAngleUnlockCost); // Reflect Attack Angle
                levelUpMenuEntryControllers[14].UpdateUnlockButtonText(playersDefenseDataController.RuntimeDefenseData.dodgeChanceUnlockCost); // Dodge Chance
                levelUpMenuEntryControllers[16].UpdateUnlockButtonText(playersDefenseDataController.RuntimeDefenseData.armorValueUnlockCost); // Armor Value
                levelUpMenuEntryControllers[17].UpdateUnlockButtonText(playersDefenseDataController.RuntimeDefenseData.thornsAmountUnlockCost); // Thorns Amount
                levelUpMenuEntryControllers[18].UpdateUnlockButtonText(playersDefenseDataController.RuntimeDefenseData.criticalHitChanceResistUnlockCost); // Critical Hit Chance Resist
                levelUpMenuEntryControllers[19].UpdateUnlockButtonText(playersDefenseDataController.RuntimeDefenseData.criticalHitDamageResistUnlockCost); // Critical Hit Damage Resist
                levelUpMenuEntryControllers[20].UpdateUnlockButtonText(playersDefenseDataController.RuntimeDefenseData.statusEffectResistUnlockCost); // Status Effect Resist
                levelUpMenuEntryControllers[21].UpdateUnlockButtonText(playersDefenseDataController.RuntimeDefenseData.physicalDamageResistUnlockCost); // Physical Damage Resist
                levelUpMenuEntryControllers[22].UpdateUnlockButtonText(playersDefenseDataController.RuntimeDefenseData.magicalDamageResistUnlockCost); // Magical Damage Resist
                levelUpMenuEntryControllers[23].UpdateUnlockButtonText(playersDefenseDataController.RuntimeDefenseData.energyDamageResistUnlockCost); // Energy Damage Resist
                levelUpMenuEntryControllers[24].UpdateUnlockButtonText(playersMovementDataController.RuntimeMovementData.groundSpeedUnlockCost); // Ground Speed
                levelUpMenuEntryControllers[28].UpdateUnlockButtonText(playersMovementDataController.RuntimeMovementData.surfaceWaterSpeedUnlockCost); // Surface Water Speed
                levelUpMenuEntryControllers[32].UpdateUnlockButtonText(playersMovementDataController.RuntimeMovementData.underwaterSpeedUnlockCost); // Underwater Speed
                levelUpMenuEntryControllers[36].UpdateUnlockButtonText(playersMovementDataController.RuntimeMovementData.flyingSpeedUnlockCost); // Flying Speed
                levelUpMenuEntryControllers[40].UpdateUnlockButtonText(playersMovementDataController.RuntimeMovementData.spaceTravelSpeedUnlockCost); // Space Travel Speed

                // EXPERIENCE COST
                levelUpMenuEntryControllers[0].UpdatePurchaseButtonText(playersHealthDataController.RuntimeHealthData.maximumHealthExpCost); // Maximum Health
                levelUpMenuEntryControllers[1].UpdatePurchaseButtonText(playersHealthDataController.RuntimeHealthData.healthRegenAmountExpCost); // Health Regen Amount
                levelUpMenuEntryControllers[2].UpdatePurchaseButtonText(playersHealthDataController.RuntimeHealthData.healthRegenCooldownExpCost); // Health Regen Cooldown
                levelUpMenuEntryControllers[3].UpdatePurchaseButtonText(playersHealthDataController.RuntimeHealthData.maximumEnergyShieldExpCost); // Max Energy Shields
                levelUpMenuEntryControllers[4].UpdatePurchaseButtonText(playersHealthDataController.RuntimeHealthData.energyShieldsRegenAmountExpCost); // Energy Shield Regen Amount
                levelUpMenuEntryControllers[5].UpdatePurchaseButtonText(playersHealthDataController.RuntimeHealthData.energyShieldsRegenCooldownExpCost); // Energy Shield Regen Cooldown
                levelUpMenuEntryControllers[6].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.parryChanceExpCost); // Parry Chance
                levelUpMenuEntryControllers[7].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.parryCooldownExpCost); // Parry Cooldown
                levelUpMenuEntryControllers[8].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.counterAttackChanceExpCost); // Counter Attack Chance
                levelUpMenuEntryControllers[9].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.blockChanceExpCost); // Block Chance
                levelUpMenuEntryControllers[10].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.blockCooldownExpCost); // Block Cooldown
                levelUpMenuEntryControllers[11].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.blockAmountExpCost); // Block Amount
                levelUpMenuEntryControllers[12].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.reflectAttackChanceExpCost); // Reflect Attack Chance
                levelUpMenuEntryControllers[13].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.reflectAttackChanceExpCost); // Reflect Attack Angle
                levelUpMenuEntryControllers[14].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.dodgeChanceExpCost); // Dodge Chance
                levelUpMenuEntryControllers[15].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.dodgeCooldownExpCost); // Dodge Cooldown
                levelUpMenuEntryControllers[16].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.armorValueExpCost); // Armor Value
                levelUpMenuEntryControllers[17].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.thornsAmountExpCost); // Thorns Amount
                levelUpMenuEntryControllers[18].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.criticalHitChanceResistExpCost); // Critical Hit Chance Resist
                levelUpMenuEntryControllers[19].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.criticalHitDamageResistExpCost); // Critical Hit Damage Resist
                levelUpMenuEntryControllers[20].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.statusEffectResistExpCost); // Status Effect Resist
                levelUpMenuEntryControllers[21].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.physicalDamageResistExpCost); // Physical Damage Resist
                levelUpMenuEntryControllers[22].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.magicalDamageResistExpCost); // Magical Damage Resist
                levelUpMenuEntryControllers[23].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.energyDamageResistExpCost); // Energy Damage Resist
                levelUpMenuEntryControllers[24].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.groundSpeedExpCost); // Ground Speed
                levelUpMenuEntryControllers[25].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.groundTurningSpeedExpCost); // Ground Turning Speed
                levelUpMenuEntryControllers[26].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.groundAccelerationSpeedExpCost); // Ground Acceleration Speed
                levelUpMenuEntryControllers[27].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.groundBrakingSpeedExpCost); // Ground Braking Speed
                levelUpMenuEntryControllers[28].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.surfaceWaterSpeedExpCost); // Surface Water Speed
                levelUpMenuEntryControllers[29].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.surfaceWaterTurningSpeedExpCost); // Surface Water Turning Speed
                levelUpMenuEntryControllers[30].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.surfaceWaterAccelerationSpeedExpCost); // Surface Water Acceleration Speed
                levelUpMenuEntryControllers[31].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.surfaceWaterBrakingSpeedExpCost); // Surface Water Braking Speed
                levelUpMenuEntryControllers[32].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.underwaterSpeedExpCost); // Underwater Speed
                levelUpMenuEntryControllers[33].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.underwaterTurningSpeedExpCost); // Underwater Turning Speed
                levelUpMenuEntryControllers[34].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.underwaterAccelerationSpeedExpCost); // Underwater Acceleration Speed
                levelUpMenuEntryControllers[35].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.underwaterBrakingSpeedExpCost); // Underwater Braking Speed
                levelUpMenuEntryControllers[36].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.flyingSpeedExpCost); // Flying Speed
                levelUpMenuEntryControllers[37].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.flyingTurningSpeedExpCost); // Flying Turning Speed
                levelUpMenuEntryControllers[38].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.flyingAccelerationSpeedExpCost); // Flying Acceleration Speed
                levelUpMenuEntryControllers[39].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.flyingBrakingSpeedExpCost); // Flying Braking Speed
                levelUpMenuEntryControllers[40].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.spaceTravelSpeedExpCost); // Space Travel Speed
                levelUpMenuEntryControllers[41].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.spaceTravelTurningSpeedExpCost); // Space Travel Turning Speed
                levelUpMenuEntryControllers[42].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.spaceTravelAccelerationSpeedExpCost); // Space Travel Acceleration Speed
                levelUpMenuEntryControllers[43].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.spaceTravelBrakingSpeedExpCost); // Space Travel Braking Speed

                // STATS LEVEL VALUE
                levelUpMenuEntryControllers[0].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.MaximumHealth); // Maximum Health
                levelUpMenuEntryControllers[1].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.HealthRegenAmount); // Health Regen Amount
                levelUpMenuEntryControllers[2].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.HealthRegenCooldown); // Health Regen Cooldown
                levelUpMenuEntryControllers[3].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.MaximumEnergyShields); // Max Energy Shields
                levelUpMenuEntryControllers[4].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.EnergyShieldRegenAmount); // Energy Shield Regen Amount
                levelUpMenuEntryControllers[5].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.EnergyShieldRegenCooldown); // Energy Shield Regen Cooldown
                levelUpMenuEntryControllers[6].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ParryChance); // Parry Chance
                levelUpMenuEntryControllers[7].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ParryCooldown); // Parry Cooldown
                levelUpMenuEntryControllers[8].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.CounterAttackChance); // Counter Attack Chance
                levelUpMenuEntryControllers[9].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.BlockChance); // Block Chance
                levelUpMenuEntryControllers[10].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.BlockCooldown); // Block Cooldown
                levelUpMenuEntryControllers[11].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.BlockAmount); // Block Amount
                levelUpMenuEntryControllers[12].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ReflectAttackChance); // Reflect Attack Chance
                levelUpMenuEntryControllers[13].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ReflectAttackChance); // Reflect Attack Angle
                levelUpMenuEntryControllers[14].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.DodgeChance); // Dodge Chance
                levelUpMenuEntryControllers[15].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.DodgeCooldown); // Dodge Cooldown
                levelUpMenuEntryControllers[16].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ArmorValue); // Armor Value
                levelUpMenuEntryControllers[17].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ThornsAmount); // Thorns Amount
                levelUpMenuEntryControllers[18].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.CriticalHitChanceResist); // Critical Hit Chance Resist
                levelUpMenuEntryControllers[19].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.CriticalHitDamageResist); // Critical Hit Damage Resist
                levelUpMenuEntryControllers[20].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.StatusEffectResist); // Status Effect Resist
                levelUpMenuEntryControllers[21].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.PhysicalDamageResist); // Physical Damage Resist
                levelUpMenuEntryControllers[22].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.MagicalDamageResist); // Magical Damage Resist
                levelUpMenuEntryControllers[23].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.EnergyDamageResist); // Energy Damage Resist
                levelUpMenuEntryControllers[24].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.GroundSpeed); // Ground Speed
                levelUpMenuEntryControllers[25].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.GroundTurningSpeed); // Ground Turning Speed
                levelUpMenuEntryControllers[26].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.GroundAccelerationSpeed); // Ground Acceleration Speed
                levelUpMenuEntryControllers[27].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.GroundBrakingSpeed); // Ground Braking Speed
                levelUpMenuEntryControllers[28].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SurfaceWaterSpeed); // Surface Water Speed
                levelUpMenuEntryControllers[29].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SurfaceWaterTurningSpeed); // Surface Water Turning Speed
                levelUpMenuEntryControllers[30].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SurfaceWaterAccelerationSpeed); // Surface Water Acceleration Speed
                levelUpMenuEntryControllers[31].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SurfaceWaterBrakingSpeed); // Surface Water Braking Speed
                levelUpMenuEntryControllers[32].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.UnderwaterSpeed); // Underwater Speed
                levelUpMenuEntryControllers[33].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.UnderwaterTurningSpeed); // Underwater Turning Speed
                levelUpMenuEntryControllers[34].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.UnderwaterAccelerationSpeed); // Underwater Acceleration Speed
                levelUpMenuEntryControllers[35].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.UnderwaterBrakingSpeed); // Underwater Braking Speed
                levelUpMenuEntryControllers[36].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.FlyingSpeed); // Flying Speed
                levelUpMenuEntryControllers[37].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.FlyingTurningSpeed); // Flying Turning Speed
                levelUpMenuEntryControllers[38].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.FlyingAccelerationSpeed); // Flying Acceleration Speed
                levelUpMenuEntryControllers[39].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.FlyingBrakingSpeed); // Flying Braking Speed
                levelUpMenuEntryControllers[40].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SpaceTravelSpeed); // Space Travel Speed
                levelUpMenuEntryControllers[41].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SpaceTravelTurningSpeed); // Space Travel Turning Speed
                levelUpMenuEntryControllers[42].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SpaceTravelAccelerationSpeed); // Space Travel Acceleration Speed
                levelUpMenuEntryControllers[43].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SpaceTravelBrakingSpeed); // Space Travel Braking Speed
            }

            private void SetDefaultMenuState()
            {
                levelUpMenuEntryControllers[0].UnlockPurchaseButton(); // Max Health
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

                float duration = 2f;
                menuSlidingCoroutine = StartCoroutine(MoveRoutine(targetPosition, duration));
            }

            private void UpdateExperiencePointTrackerText()
            {
                experienceText.SetText($"{playersExperiencePoints:F2}");
            }

            private IEnumerator MoveRoutine(Vector2 targetPosition, float duration)
            {
                Vector2 startPosition = menusRectTransform.anchoredPosition;
                float elapsedTime = 0f;

                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    float percentageComplete = elapsedTime / duration;
                    menusRectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, percentageComplete);
                    yield return null;
                }

                menusRectTransform.anchoredPosition = targetPosition;
            }

            // HEALTH RELATED UNLOCK AND LEVEL UP FUNCTIONS
            public void UnlockHealthRegenAmount()
            {
                playersHealthDataController.UnlockHealthRegenAmount(ref playersExperiencePoints);
                levelUpMenuEntryControllers[1].UnlockPurchaseButton(); // Health Regen Amount
                levelUpMenuEntryControllers[2].UnlockPurchaseButton(); // Health Regen Cooldown
                levelUpMenuEntryControllers[2].gameObject.SetActive(true); // Health Regen Cooldown

                levelUpMenuEntryControllers[1].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.HealthRegenAmount); // Health Regen Amount
                levelUpMenuEntryControllers[2].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.HealthRegenCooldown); // Health Regen Cooldown
                UpdateExperiencePointTrackerText();
            }

            public void UnlockMaximumEnergyShield()
            {
                playersHealthDataController.UnlockMaximumEnergyShield(ref playersExperiencePoints);
                levelUpMenuEntryControllers[3].UnlockPurchaseButton(); // Max Energy Shields
                levelUpMenuEntryControllers[4].gameObject.SetActive(true); // Energy Shield Regen Amount
                levelUpMenuEntryControllers[3].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.MaximumEnergyShields); // Max Energy Shields
                UpdateExperiencePointTrackerText();
            }

            public void UnlockEnergyShieldRegenAmount()
            {
                playersHealthDataController.UnlockEnergyShieldRegenAmount(ref playersExperiencePoints);
                levelUpMenuEntryControllers[4].UnlockPurchaseButton(); // Energy Shield Regen Amount
                levelUpMenuEntryControllers[5].UnlockPurchaseButton(); // Energy Shield Regen Cooldown
                levelUpMenuEntryControllers[5].gameObject.SetActive(true); // Energy Shield Regen Cooldown
                levelUpMenuEntryControllers[4].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.EnergyShieldRegenAmount); // Energy Shield Regen Amount
                levelUpMenuEntryControllers[5].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.EnergyShieldRegenCooldown); // Energy Shield Regen Cooldown
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpMaximumHealth()
            {
                playersHealthDataController.LevelUpMaximumHealth(ref playersExperiencePoints);
                levelUpMenuEntryControllers[0].UpdatePurchaseButtonText(playersHealthDataController.RuntimeHealthData.maximumHealthExpCost); // Maximum Health
                levelUpMenuEntryControllers[0].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.MaximumHealth); // Maximum Health
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpHealthRegenAmount()
            {
                playersHealthDataController.LevelUpHealthRegenAmount(ref playersExperiencePoints);
                levelUpMenuEntryControllers[1].UpdatePurchaseButtonText(playersHealthDataController.RuntimeHealthData.healthRegenAmountExpCost); // Health Regen Amount
                levelUpMenuEntryControllers[1].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.HealthRegenAmount); // Health Regen Amount
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpHealthRegenCooldown()
            {
                playersHealthDataController.LevelUpHealthRegenCooldown(ref playersExperiencePoints);
                levelUpMenuEntryControllers[2].UpdatePurchaseButtonText(playersHealthDataController.RuntimeHealthData.healthRegenCooldownExpCost); // Health Regen Cooldown
                levelUpMenuEntryControllers[2].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.HealthRegenCooldown); // Health Regen Cooldown
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpMaximumEnergyShield()
            {
                playersHealthDataController.LevelUpMaximumEnergyShield(ref playersExperiencePoints);
                levelUpMenuEntryControllers[3].UpdatePurchaseButtonText(playersHealthDataController.RuntimeHealthData.maximumEnergyShieldExpCost); // Max Energy Shields
                levelUpMenuEntryControllers[3].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.MaximumEnergyShields); // Max Energy Shields
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpEnergyShieldRegenAmount()
            {
                playersHealthDataController.LevelUpEnergyShieldRegenAmount(ref playersExperiencePoints);
                levelUpMenuEntryControllers[4].UpdatePurchaseButtonText(playersHealthDataController.RuntimeHealthData.energyShieldsRegenAmountExpCost); // Energy Shield Regen Amount
                levelUpMenuEntryControllers[4].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.EnergyShieldRegenAmount); // Energy Shield Regen Amount
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpEnergyShieldRegenCooldown()
            {
                playersHealthDataController.LevelUpEnergyShieldRegenCooldown(ref playersExperiencePoints);
                levelUpMenuEntryControllers[5].UpdatePurchaseButtonText(playersHealthDataController.RuntimeHealthData.energyShieldsRegenCooldownExpCost); // Energy Shield Regen Cooldown
                levelUpMenuEntryControllers[5].UpdateStatsLevelValueText(playersHealthDataController.RuntimeHealthData.EnergyShieldRegenCooldown); // Energy Shield Regen Cooldown
                UpdateExperiencePointTrackerText();
            }

            // DEFENSE RELATED UNLOCK AND LEVEL UP FUNCTIONS
            public void UnlockParryChance()
            {
                playersDefenseDataController.UnlockParryChance(ref playersExperiencePoints);
                levelUpMenuEntryControllers[6].UnlockPurchaseButton(); // Parry Chance
                levelUpMenuEntryControllers[7].UnlockPurchaseButton(); // Parry Cooldown
                levelUpMenuEntryControllers[7].gameObject.SetActive(true); // Parry Cooldown
                levelUpMenuEntryControllers[8].gameObject.SetActive(true); // Counter Attack Chance
                levelUpMenuEntryControllers[6].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ParryChance); // Parry Chance
                levelUpMenuEntryControllers[7].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ParryCooldown); // Parry Cooldown
                UpdateExperiencePointTrackerText();
            }

            public void UnlockCounterAttackChance()
            {
                playersDefenseDataController.UnlockCounterAttackChance(ref playersExperiencePoints);
                levelUpMenuEntryControllers[8].UnlockPurchaseButton(); // Counter Attack Chance
                levelUpMenuEntryControllers[8].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.CounterAttackChance); // Counter Attack Chance
                UpdateExperiencePointTrackerText();
            }

            public void UnlockBlockChance()
            {
                playersDefenseDataController.UnlockBlockChance(ref playersExperiencePoints);
                levelUpMenuEntryControllers[9].UnlockPurchaseButton(); // Block Chance
                levelUpMenuEntryControllers[10].UnlockPurchaseButton(); // Block Cooldown
                levelUpMenuEntryControllers[10].gameObject.SetActive(true); // Block Cooldown
                levelUpMenuEntryControllers[11].gameObject.SetActive(true); // Block Amount
                levelUpMenuEntryControllers[12].gameObject.SetActive(true); // Reflect Attack Chance
                levelUpMenuEntryControllers[9].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.BlockChance); // Block Chance
                levelUpMenuEntryControllers[10].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.BlockCooldown); // Block Cooldown
                UpdateExperiencePointTrackerText();
            }

            public void UnlockBlockAmount()
            {
                playersDefenseDataController.UnlockBlockAmount(ref playersExperiencePoints);
                levelUpMenuEntryControllers[11].UnlockPurchaseButton(); // Block Amount
                levelUpMenuEntryControllers[11].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.BlockAmount); // Block Amount
                UpdateExperiencePointTrackerText();
            }

            public void UnlockReflectAttackChance()
            {
                playersDefenseDataController.UnlockReflectAttackChance(ref playersExperiencePoints);
                levelUpMenuEntryControllers[12].UnlockPurchaseButton(); // Reflect Attack Chance
                levelUpMenuEntryControllers[13].gameObject.SetActive(true); // Reflect Attack Angle
                levelUpMenuEntryControllers[12].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ReflectAttackChance); // Reflect Attack Chance
                UpdateExperiencePointTrackerText();
            }

            public void UnlockReflectAttackAngle()
            {
                playersDefenseDataController.UnlockReflectAttackAngle(ref playersExperiencePoints);
                levelUpMenuEntryControllers[13].UnlockPurchaseButton(); // Reflect Attack Angle
                levelUpMenuEntryControllers[13].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ReflectAttackChance); // Reflect Attack Angle
                UpdateExperiencePointTrackerText();
            }

            public void UnlockDodgeChance()
            {
                playersDefenseDataController.UnlockDodgeChance(ref playersExperiencePoints);
                levelUpMenuEntryControllers[14].UnlockPurchaseButton(); // Dodge Chance
                levelUpMenuEntryControllers[15].UnlockPurchaseButton(); // Dodge Cooldown
                levelUpMenuEntryControllers[15].gameObject.SetActive(true); // Dodge Cooldown
                levelUpMenuEntryControllers[14].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.DodgeChance); // Dodge Chance
                levelUpMenuEntryControllers[15].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.DodgeCooldown); // Dodge Cooldown
                UpdateExperiencePointTrackerText();
            }

            public void UnlockArmorValue()
            {
                playersDefenseDataController.UnlockArmorValue(ref playersExperiencePoints);
                levelUpMenuEntryControllers[16].UnlockPurchaseButton(); // Armor Value
                levelUpMenuEntryControllers[16].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ArmorValue); // Armor Value
                UpdateExperiencePointTrackerText();
            }

            public void UnlockThornsAmount()
            {
                playersDefenseDataController.UnlockThornsAmount(ref playersExperiencePoints);
                levelUpMenuEntryControllers[17].UnlockPurchaseButton(); // Thorns Amount
                levelUpMenuEntryControllers[17].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ThornsAmount); // Thorns Amount
                UpdateExperiencePointTrackerText();
            }

            public void UnlockCriticalHitChanceResist()
            {
                playersDefenseDataController.UnlockCriticalHitChanceResist(ref playersExperiencePoints);
                levelUpMenuEntryControllers[18].UnlockPurchaseButton(); // Critical Hit Chance Resist
                levelUpMenuEntryControllers[18].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.CriticalHitChanceResist); // Critical Hit Chance Resist
                UpdateExperiencePointTrackerText();
            }

            public void UnlockCriticalHitDamageResist()
            {
                playersDefenseDataController.UnlockCriticalHitDamageResist(ref playersExperiencePoints);
                levelUpMenuEntryControllers[19].UnlockPurchaseButton(); // Critical Hit Damage Resist
                levelUpMenuEntryControllers[19].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.CriticalHitDamageResist); // Critical Hit Damage Resist
                UpdateExperiencePointTrackerText();
            }

            public void UnlockStatusEffectResist()
            {
                playersDefenseDataController.UnlockStatusEffectResist(ref playersExperiencePoints);
                levelUpMenuEntryControllers[20].UnlockPurchaseButton(); // Status Effect Resist
                levelUpMenuEntryControllers[20].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.StatusEffectResist); // Status Effect Resist
                UpdateExperiencePointTrackerText();
            }

            public void UnlockPhysicalDamageResist()
            {
                playersDefenseDataController.UnlockPhysicalDamageResist(ref playersExperiencePoints);
                levelUpMenuEntryControllers[21].UnlockPurchaseButton(); // Physical Damage Resist
                levelUpMenuEntryControllers[21].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.PhysicalDamageResist); // Physical Damage Resist
                UpdateExperiencePointTrackerText();
            }

            public void UnlockMagicalDamageResist()
            {
                playersDefenseDataController.UnlockMagicalDamageResist(ref playersExperiencePoints);
                levelUpMenuEntryControllers[22].UnlockPurchaseButton(); // Magical Damage Resist
                levelUpMenuEntryControllers[22].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.MagicalDamageResist); // Magical Damage Resist
                UpdateExperiencePointTrackerText();
            }

            public void UnlockEnergyDamageResist()
            {
                playersDefenseDataController.UnlockEnergyDamageResist(ref playersExperiencePoints);
                levelUpMenuEntryControllers[23].UnlockPurchaseButton(); // Energy Damage Resist
                levelUpMenuEntryControllers[23].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.EnergyDamageResist); // Energy Damage Resist
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpParryChance()
            {
                playersDefenseDataController.LevelUpParryChance(ref playersExperiencePoints);
                levelUpMenuEntryControllers[6].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.parryChanceExpCost); // Parry Chance
                levelUpMenuEntryControllers[6].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ParryChance); // Parry Chance
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpParryCooldown()
            {
                playersDefenseDataController.LevelUpParryCooldown(ref playersExperiencePoints);
                levelUpMenuEntryControllers[7].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.parryCooldownExpCost); // Parry Cooldown
                levelUpMenuEntryControllers[7].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ParryCooldown); // Parry Cooldown
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpCounterAttackChance()
            {
                playersDefenseDataController.LevelUpCounterAttackChance(ref playersExperiencePoints);
                levelUpMenuEntryControllers[8].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.counterAttackChanceExpCost); // Counter Attack Chance
                levelUpMenuEntryControllers[8].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.CounterAttackChance); // Counter Attack Chance
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpBlockChance()
            {
                playersDefenseDataController.LevelUpBlockChance(ref playersExperiencePoints);
                levelUpMenuEntryControllers[9].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.blockChanceExpCost); // Block Chance
                levelUpMenuEntryControllers[9].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.BlockChance); // Block Chance
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpBlockCooldown()
            {
                playersDefenseDataController.LevelUpBlockCooldown(ref playersExperiencePoints);
                levelUpMenuEntryControllers[10].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.blockCooldownExpCost); // Block Cooldown
                levelUpMenuEntryControllers[10].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.BlockCooldown); // Block Cooldown
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpBlockAmount()
            {
                playersDefenseDataController.LevelUpBlockAmount(ref playersExperiencePoints);
                levelUpMenuEntryControllers[11].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.blockAmountExpCost); // Block Amount
                levelUpMenuEntryControllers[11].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.BlockAmount); // Block Amount
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpReflectAttackChance()
            {
                playersDefenseDataController.LevelUpReflectAttackChance(ref playersExperiencePoints);
                levelUpMenuEntryControllers[12].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.reflectAttackChanceExpCost); // Reflect Attack Chance
                levelUpMenuEntryControllers[12].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ReflectAttackChance); // Reflect Attack Chance
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpReflectAttackAngle()
            {
                playersDefenseDataController.LevelUpReflectAttackAngle(ref playersExperiencePoints);
                levelUpMenuEntryControllers[13].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.reflectAttackChanceExpCost); // Reflect Attack Angle
                levelUpMenuEntryControllers[13].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ReflectAttackChance); // Reflect Attack Angle
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpDodgeChance()
            {
                playersDefenseDataController.LevelUpDodgeChance(ref playersExperiencePoints);
                levelUpMenuEntryControllers[14].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.dodgeChanceExpCost); // Dodge Chance
                levelUpMenuEntryControllers[14].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.DodgeChance); // Dodge Chance
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpDodgeCooldown()
            {
                playersDefenseDataController.LevelUpDodgeCooldown(ref playersExperiencePoints);
                levelUpMenuEntryControllers[15].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.dodgeCooldownExpCost); // Dodge Cooldown
                levelUpMenuEntryControllers[15].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.DodgeCooldown); // Dodge Cooldown
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpArmorValue()
            {
                playersDefenseDataController.LevelUpArmorValue(ref playersExperiencePoints);
                levelUpMenuEntryControllers[16].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.armorValueExpCost); // Armor Value
                levelUpMenuEntryControllers[16].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ArmorValue); // Armor Value
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpThornsAmount()
            {
                playersDefenseDataController.LevelUpThornsAmount(ref playersExperiencePoints);
                levelUpMenuEntryControllers[17].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.thornsAmountExpCost); // Thorns Amount
                levelUpMenuEntryControllers[17].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.ThornsAmount); // Thorns Amount
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpCriticalHitChanceResist()
            {
                playersDefenseDataController.LevelUpCriticalHitChanceResist(ref playersExperiencePoints);
                levelUpMenuEntryControllers[18].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.criticalHitChanceResistExpCost); // Critical Hit Chance Resist
                levelUpMenuEntryControllers[18].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.CriticalHitChanceResist); // Critical Hit Chance Resist
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpCriticalHitDamageResist()
            {
                playersDefenseDataController.LevelUpCriticalHitDamageResist(ref playersExperiencePoints);
                levelUpMenuEntryControllers[19].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.criticalHitDamageResistExpCost); // Critical Hit Damage Resist
                levelUpMenuEntryControllers[19].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.CriticalHitDamageResist); // Critical Hit Damage Resist
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpStatusEffectResist()
            {
                playersDefenseDataController.LevelUpStatusEffectResist(ref playersExperiencePoints);
                levelUpMenuEntryControllers[20].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.statusEffectResistExpCost); // Status Effect Resist
                levelUpMenuEntryControllers[20].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.StatusEffectResist); // Status Effect Resist
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpPhysicalDamageResist()
            {
                playersDefenseDataController.LevelUpPhysicalDamageResist(ref playersExperiencePoints);
                levelUpMenuEntryControllers[21].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.physicalDamageResistExpCost); // Physical Damage Resist
                levelUpMenuEntryControllers[21].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.PhysicalDamageResist); // Physical Damage Resist
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpMagicalDamageResist()
            {
                playersDefenseDataController.LevelUpMagicalDamageResist(ref playersExperiencePoints);
                levelUpMenuEntryControllers[22].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.magicalDamageResistExpCost); // Magical Damage Resist
                levelUpMenuEntryControllers[22].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.MagicalDamageResist); // Magical Damage Resist
                UpdateExperiencePointTrackerText();
            }

            public void LevelUpEnergyDamageResist()
            {
                playersDefenseDataController.LevelUpEnergyDamageResist(ref playersExperiencePoints);
                levelUpMenuEntryControllers[23].UpdatePurchaseButtonText(playersDefenseDataController.RuntimeDefenseData.energyDamageResistExpCost); // Energy Damage Resist
                levelUpMenuEntryControllers[23].UpdateStatsLevelValueText(playersDefenseDataController.RuntimeDefenseData.EnergyDamageResist); // Energy Damage Resist
                UpdateExperiencePointTrackerText();
            }

            // MOVEMENT RELATED UNLOCK AND LEVEL UP FUNCTIONS
            public void UnlockGroundSpeed()
            {
                playersMovementDataController.UnlockGroundSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[24].UnlockPurchaseButton(); // Ground Speed
                levelUpMenuEntryControllers[25].UnlockPurchaseButton(); // Ground Turning Speed
                levelUpMenuEntryControllers[25].gameObject.SetActive(true); // Ground Turning Speed
                levelUpMenuEntryControllers[26].UnlockPurchaseButton(); // Ground Acceleration Speed
                levelUpMenuEntryControllers[26].gameObject.SetActive(true); // Ground Acceleration Speed
                levelUpMenuEntryControllers[27].UnlockPurchaseButton(); // Ground Braking Speed
                levelUpMenuEntryControllers[27].gameObject.SetActive(true); // Ground Braking Speed
                levelUpMenuEntryControllers[24].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.GroundSpeed); // Ground Speed
                levelUpMenuEntryControllers[25].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.GroundTurningSpeed); // Ground Turning Speed
                levelUpMenuEntryControllers[26].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.GroundAccelerationSpeed); // Ground Acceleration Speed
                levelUpMenuEntryControllers[27].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.GroundBrakingSpeed); // Ground Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void UnlockSurfaceWaterSpeed()
            {
                playersMovementDataController.UnlockSurfaceWaterSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[28].UnlockPurchaseButton(); // Surface Water Speed
                levelUpMenuEntryControllers[29].UnlockPurchaseButton(); // Surface Water Turning Speed
                levelUpMenuEntryControllers[29].gameObject.SetActive(true); // Surface Water Turning Speed
                levelUpMenuEntryControllers[30].UnlockPurchaseButton(); // Surface Water Acceleration Speed
                levelUpMenuEntryControllers[30].gameObject.SetActive(true); // Surface Water Acceleration Speed
                levelUpMenuEntryControllers[31].UnlockPurchaseButton(); // Surface Water Braking Speed
                levelUpMenuEntryControllers[31].gameObject.SetActive(true); // Surface Water Braking Speed
                levelUpMenuEntryControllers[28].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SurfaceWaterSpeed); // Surface Water Speed
                levelUpMenuEntryControllers[29].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SurfaceWaterTurningSpeed); // Surface Water Turning Speed
                levelUpMenuEntryControllers[30].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SurfaceWaterAccelerationSpeed); // Surface Water Acceleration Speed
                levelUpMenuEntryControllers[31].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SurfaceWaterBrakingSpeed); // Surface Water Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void UnlockUnderwaterSpeed()
            {
                playersMovementDataController.UnlockUnderwaterSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[32].UnlockPurchaseButton(); // Underwater Speed
                levelUpMenuEntryControllers[33].UnlockPurchaseButton(); // Underwater Turning Speed
                levelUpMenuEntryControllers[33].gameObject.SetActive(true); // Underwater Turning Speed
                levelUpMenuEntryControllers[34].UnlockPurchaseButton(); // Underwater Acceleration Speed
                levelUpMenuEntryControllers[34].gameObject.SetActive(true); // Underwater Acceleration Speed
                levelUpMenuEntryControllers[35].UnlockPurchaseButton(); // Underwater Braking Speed
                levelUpMenuEntryControllers[35].gameObject.SetActive(true); // Underwater Braking Speed
                levelUpMenuEntryControllers[32].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.UnderwaterSpeed); // Underwater Speed
                levelUpMenuEntryControllers[33].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.UnderwaterTurningSpeed); // Underwater Turning Speed
                levelUpMenuEntryControllers[34].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.UnderwaterAccelerationSpeed); // Underwater Acceleration Speed
                levelUpMenuEntryControllers[35].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.UnderwaterBrakingSpeed); // Underwater Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void UnlockFlyingSpeed()
            {
                playersMovementDataController.UnlockFlyingSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[36].UnlockPurchaseButton(); // Flying Speed
                levelUpMenuEntryControllers[37].UnlockPurchaseButton(); // Flying Turning Speed
                levelUpMenuEntryControllers[37].gameObject.SetActive(true); // Flying Turning Speed
                levelUpMenuEntryControllers[38].UnlockPurchaseButton(); // Flying Acceleration Speed
                levelUpMenuEntryControllers[38].gameObject.SetActive(true); // Flying Acceleration Speed
                levelUpMenuEntryControllers[39].UnlockPurchaseButton(); // Flying Braking Speed
                levelUpMenuEntryControllers[39].gameObject.SetActive(true); // Flying Braking Speed
                levelUpMenuEntryControllers[36].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.FlyingSpeed); // Flying Speed
                levelUpMenuEntryControllers[37].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.FlyingTurningSpeed); // Flying Turning Speed
                levelUpMenuEntryControllers[38].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.FlyingAccelerationSpeed); // Flying Acceleration Speed
                levelUpMenuEntryControllers[39].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.FlyingBrakingSpeed); // Flying Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void UnlockSpaceTravelSpeed()
            {
                playersMovementDataController.UnlockSpaceTravelSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[40].UnlockPurchaseButton(); // Space Travel Speed
                levelUpMenuEntryControllers[41].UnlockPurchaseButton(); // Space Travel Turning Speed
                levelUpMenuEntryControllers[41].gameObject.SetActive(true); // Space Travel Turning Speed
                levelUpMenuEntryControllers[42].UnlockPurchaseButton(); // Space Travel Acceleration Speed
                levelUpMenuEntryControllers[42].gameObject.SetActive(true); // Space Travel Acceleration Speed
                levelUpMenuEntryControllers[43].UnlockPurchaseButton(); // Space Travel Braking Speed
                levelUpMenuEntryControllers[43].gameObject.SetActive(true); // Space Travel Braking Speed
                levelUpMenuEntryControllers[40].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SpaceTravelSpeed); // Space Travel Speed
                levelUpMenuEntryControllers[41].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SpaceTravelTurningSpeed); // Space Travel Turning Speed
                levelUpMenuEntryControllers[42].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SpaceTravelAccelerationSpeed); // Space Travel Acceleration Speed
                levelUpMenuEntryControllers[43].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SpaceTravelBrakingSpeed); // Space Travel Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpGroundSpeed()
            {
                playersMovementDataController.LevelUpGroundSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[24].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.groundSpeedExpCost); // Ground Speed
                levelUpMenuEntryControllers[24].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.GroundSpeed); // Ground Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpGroundTurningSpeed()
            {
                playersMovementDataController.LevelUpGroundTurningSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[25].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.groundTurningSpeedExpCost); // Ground Turning Speed
                levelUpMenuEntryControllers[25].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.GroundTurningSpeed); // Ground Turning Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpGroundAccelerationSpeed()
            {
                playersMovementDataController.LevelUpGroundAccelerationSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[26].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.groundAccelerationSpeedExpCost); // Ground Acceleration Speed
                levelUpMenuEntryControllers[26].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.GroundAccelerationSpeed); // Ground Acceleration Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpGroundBrakingSpeed()
            {
                playersMovementDataController.LevelUpGroundBrakingSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[27].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.groundBrakingSpeedExpCost); // Ground Braking Speed
                levelUpMenuEntryControllers[27].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.GroundBrakingSpeed); // Ground Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpSurfaceWaterSpeed()
            {
                playersMovementDataController.LevelUpSurfaceWaterSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[28].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.surfaceWaterSpeedExpCost); // Surface Water Speed
                levelUpMenuEntryControllers[28].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SurfaceWaterSpeed); // Surface Water Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpSurfaceWaterTurningSpeed()
            {
                playersMovementDataController.LevelUpSurfaceWaterTurningSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[29].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.surfaceWaterTurningSpeedExpCost); // Surface Water Turning Speed
                levelUpMenuEntryControllers[29].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SurfaceWaterTurningSpeed); // Surface Water Turning Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpSurfaceWaterAccelerationSpeed()
            {
                playersMovementDataController.LevelUpSurfaceWaterAccelerationSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[30].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.surfaceWaterAccelerationSpeedExpCost); // Surface Water Acceleration Speed
                levelUpMenuEntryControllers[30].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SurfaceWaterAccelerationSpeed); // Surface Water Acceleration Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpSurfaceWaterBrakingSpeed()
            {
                playersMovementDataController.LevelUpSurfaceWaterBrakingSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[31].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.surfaceWaterBrakingSpeedExpCost); // Surface Water Braking Speed
                levelUpMenuEntryControllers[31].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SurfaceWaterBrakingSpeed); // Surface Water Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpUnderwaterSpeed()
            {
                playersMovementDataController.LevelUpUnderwaterSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[32].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.underwaterSpeedExpCost); // Underwater Speed
                levelUpMenuEntryControllers[32].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.UnderwaterSpeed); // Underwater Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpUnderwaterTurningSpeed()
            {
                playersMovementDataController.LevelUpUnderwaterTurningSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[33].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.underwaterTurningSpeedExpCost); // Underwater Turning Speed
                levelUpMenuEntryControllers[33].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.UnderwaterTurningSpeed); // Underwater Turning Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpUnderwaterAccelerationSpeed()
            {
                playersMovementDataController.LevelUpUnderwaterAccelerationSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[34].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.underwaterAccelerationSpeedExpCost); // Underwater Acceleration Speed
                levelUpMenuEntryControllers[34].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.UnderwaterAccelerationSpeed); // Underwater Acceleration Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpUnderwaterBrakingSpeed()
            {
                playersMovementDataController.LevelUpUnderwaterBrakingSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[35].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.underwaterBrakingSpeedExpCost); // Underwater Braking Speed
                levelUpMenuEntryControllers[35].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.UnderwaterBrakingSpeed); // Underwater Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpFlyingSpeed()
            {
                playersMovementDataController.LevelUpFlyingSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[36].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.flyingSpeedExpCost); // Flying Speed
                levelUpMenuEntryControllers[36].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.FlyingSpeed); // Flying Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpFlyingTurningSpeed()
            {
                playersMovementDataController.LevelUpFlyingTurningSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[37].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.flyingTurningSpeedExpCost); // Flying Turning Speed
                levelUpMenuEntryControllers[37].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.FlyingTurningSpeed); // Flying Turning Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpFlyingAccelerationSpeed()
            {
                playersMovementDataController.LevelUpFlyingAccelerationSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[38].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.flyingAccelerationSpeedExpCost); // Flying Acceleration Speed
                levelUpMenuEntryControllers[38].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.FlyingAccelerationSpeed); // Flying Acceleration Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpFlyingBrakingSpeed()
            {
                playersMovementDataController.LevelUpFlyingBrakingSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[39].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.flyingBrakingSpeedExpCost); // Flying Braking Speed
                levelUpMenuEntryControllers[39].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.FlyingBrakingSpeed); // Flying Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpSpaceTravelSpeed()
            {
                playersMovementDataController.LevelUpSpaceTravelSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[40].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.spaceTravelSpeedExpCost); // Space Travel Speed
                levelUpMenuEntryControllers[40].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SpaceTravelSpeed); // Space Travel Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpSpaceTravelTurningSpeed()
            {
                playersMovementDataController.LevelUpSpaceTravelTurningSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[41].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.spaceTravelTurningSpeedExpCost); // Space Travel Turning Speed
                levelUpMenuEntryControllers[41].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SpaceTravelTurningSpeed); // Space Travel Turning Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpSpaceTravelAccelerationSpeed()
            {
                playersMovementDataController.LevelUpSpaceTravelAccelerationSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[42].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.spaceTravelAccelerationSpeedExpCost); // Space Travel Acceleration Speed
                levelUpMenuEntryControllers[42].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SpaceTravelAccelerationSpeed); // Space Travel Acceleration Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }

            public void LevelUpSpaceTravelBrakingSpeed()
            {
                playersMovementDataController.LevelUpSpaceTravelBrakingSpeed(ref playersExperiencePoints);
                levelUpMenuEntryControllers[43].UpdatePurchaseButtonText(playersMovementDataController.RuntimeMovementData.spaceTravelBrakingSpeedExpCost); // Space Travel Braking Speed
                levelUpMenuEntryControllers[43].UpdateStatsLevelValueText(playersMovementDataController.RuntimeMovementData.SpaceTravelBrakingSpeed); // Space Travel Braking Speed
                UpdateExperiencePointTrackerText();
                playersMovementDataController.UpdateMovementStats();
            }
        }
    }
}
