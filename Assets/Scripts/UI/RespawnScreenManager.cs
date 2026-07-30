namespace ShapeDefender
{
    namespace UI
    {
        using ShapeDefender.DefenseSystem;
        using ShapeDefender.EntitySystem;
        using ShapeDefender.HealthSystem;
        using ShapeDefender.LevelUpSystem;
        using UnityEngine;

        public class RespawnScreenManager : MonoBehaviour
        {
            public static RespawnScreenManager Instance;
            [SerializeField] private GameObject respawnScreen;
            [SerializeField] private GameObject playerCharacter;

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

            public void ToggleRespawnScreen()
            {
                if (respawnScreen == null) { return; }

                if (!respawnScreen.activeSelf)
                {
                    respawnScreen.SetActive(true);
                }
                else
                {
                    respawnScreen.SetActive(false);
                }
            }

            public void Respawn()
            {
                if (playerCharacter != null)
                {
                    playerCharacter.transform.position = Vector3.zero;
                    playerCharacter.transform.localScale = Vector3.one;
                    playerCharacter.transform.rotation = Quaternion.identity;

                    playerCharacter.SetActive(true);
                    HealthStatContainer playersHealthStatContainer = playerCharacter.GetComponent<HealthStatContainer>();
                    playersHealthStatContainer.runtimeHealthStats.currentHealth = playersHealthStatContainer.runtimeHealthStats.maximumHealth.StatValue;
                    playersHealthStatContainer.runtimeHealthStats.currentEnergyShields = playersHealthStatContainer.runtimeHealthStats.maximumEnergyShields.StatValue;

                    playersHealthStatContainer.runtimeHealthStats.currentHealthRegenCooldown = playersHealthStatContainer.runtimeHealthStats.healthRegenCooldown.StatValue;
                    playersHealthStatContainer.runtimeHealthStats.currentEnergyShieldsRegenCooldown = playersHealthStatContainer.runtimeHealthStats.energyShieldsRegenCooldown.StatValue;
                    playersHealthStatContainer.UpdateStatusBars();

                    DefenseStatContainer playersDefenseStatContainer = playerCharacter.GetComponent<DefenseStatContainer>();
                    playersDefenseStatContainer.runtimeDefenseStats.currentParryCooldown = playersDefenseStatContainer.runtimeDefenseStats.parryCooldown.StatValue;
                    playersDefenseStatContainer.runtimeDefenseStats.currentBlockCooldown = playersDefenseStatContainer.runtimeDefenseStats.blockCooldown.StatValue;
                    playersDefenseStatContainer.runtimeDefenseStats.currentDodgeCooldown = playersDefenseStatContainer.runtimeDefenseStats.dodgeCooldown.StatValue;
                }

                WaveSpawnManager.Instance.StopWaveSpawner();

                GameObject[] floatingUIText = GameObject.FindGameObjectsWithTag("TempUI");
                if (floatingUIText != null)
                {
                    foreach (GameObject f in floatingUIText)
                    {
                        Destroy(f);
                    }
                }

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemies != null)
                {
                    foreach (GameObject e in enemies)
                    {
                        e.SetActive(false);
                    }
                }

                WaveSpawnManager.Instance.ResetWaveSpawner();

                // PLAYER RELATED:
                // -- Move Player back to the spawn point.
                // -- Reset Players scale and Rotation.
                // -- Reset Players active state.
                // -- Restore the Players Health and Energy Shields.
                // -- Update the overhead display for Health and Energy Shields.
                // -- Reset the Players Cooldowns for Health and Defense.

                // UI RELATED:
                // -- Clear floating text and close the upgrades menu.

                // RELATED TO PROJECTILES AND ENEMIES:
                // -- Destroy or Return to object pool -> all projectiles and enemies.
                // -- Restart the wave count.
                // -- Restart the wave spawner.

                LevelUpMenuManager.Instance.CloseMenu();
                LevelUpMenuManager.Instance.ReloadMenu();

                respawnScreen.SetActive(false);
            }
        }
    }
}