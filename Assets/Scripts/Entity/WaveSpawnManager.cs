namespace ShapeDefender
{
    namespace EntitySystem
    {
        using System.Collections;
        using System.Collections.Generic;
        using ShapeDefender.LevelUpSystem;
        using TMPro;
        using UnityEngine;

        public class WaveSpawnManager : MonoBehaviour
        {
            public static WaveSpawnManager Instance;
            [HideInInspector] public int currentWave = 0;
            private float waveDifficultyRating = 0;
            [SerializeField] private GameObject defaultEnemyPrefab;
            [SerializeField] private List<EnemyAI> enemyPrefabs;
            [SerializeField] private List<EnemyAI> enemyBossPrefabs;

            private Coroutine waveSpawnerCoroutine;
            private bool canSpawnNextWave = true;

            [SerializeField] private TextMeshProUGUI waveTMP;
            [SerializeField] private TextMeshProUGUI waveCooldownTMP;

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

                StartWaveSpawner();
            }

            public void StartWaveSpawner()
            {
                if (waveSpawnerCoroutine != null)
                {
                    StopWaveSpawner();
                }
                waveSpawnerCoroutine = StartCoroutine(WaveSpawnerCoroutine());
            }

            public void StopWaveSpawner()
            {
                if (waveSpawnerCoroutine != null)
                {
                    StopCoroutine(waveSpawnerCoroutine);
                    waveSpawnerCoroutine = null;
                }
            }

            public void ResetWaveSpawner()
            {
                currentWave = 0;
                waveDifficultyRating = 0;
                canSpawnNextWave = true;
                waveCooldownTMP.gameObject.SetActive(true);
                StartWaveSpawner();
            }

            private IEnumerator WaveSpawnerCoroutine()
            {
                float waveCooldownTimer = 10f;
                while (true)
                {
                    int numberOfEnemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
                    if (numberOfEnemiesLeft == 0 && !canSpawnNextWave)
                    {
                        waveCooldownTimer = 10f;
                        canSpawnNextWave = true;
                        waveCooldownTMP.gameObject.SetActive(true);
                    }

                    if (canSpawnNextWave)
                    {
                        if (waveCooldownTimer > 0)
                        {
                            waveCooldownTimer -= Time.deltaTime;
                            waveCooldownTMP.SetText($"Until Next Wave: {waveCooldownTimer:F2} sec");
                        }
                        else if (waveCooldownTimer <= 0)
                        {
                            waveCooldownTMP.gameObject.SetActive(false);
                            currentWave++;
                            waveTMP.SetText($"Wave: {currentWave}");
                            CalculateWaveDifficulty();
                            if (currentWave % 10 == 0)
                            {
                                SpawnBoss();
                            }

                            SpawnEnemies();
                            canSpawnNextWave = false;
                        }
                    }

                    yield return null;
                }
            }

            private void CalculateWaveDifficulty()
            {
                float totalRating = Mathf.Max(1, (currentWave + LevelUpMenuManager.Instance.playersDifficultyRating) / 100f);
                waveDifficultyRating += totalRating;
            }

            private void SpawnEnemies()
            {
                for (int i = 0; i < waveDifficultyRating; i++)
                {
                    if (enemyPrefabs.Count > 0)
                    {
                        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Count - 1);
                        // Pick one up from the object pool if it exists.
                        Instantiate(enemyPrefabs[randomEnemyIndex]);
                    }
                    else
                    {
                        Instantiate(defaultEnemyPrefab);
                    }
                }
            }

            private void SpawnBoss()
            {
                if (enemyBossPrefabs.Count > 0)
                {
                    int randomEnemyIndex = Random.Range(0, enemyBossPrefabs.Count - 1);
                    // Pick one up from the object pool if it exists.
                    Instantiate(enemyBossPrefabs[randomEnemyIndex]);
                }
                else
                {
                    Instantiate(defaultEnemyPrefab);
                }
            }
        }
    }
}