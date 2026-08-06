namespace ShapeDefender
{
    namespace EntitySystem
    {
        using System.Collections;
        using System.Collections.Generic;
        using ShapeDefender.LevelUpSystem;
        using ShapeDefender.UI;
        using TMPro;
        using UnityEngine;

        public class WaveSpawnManager : MonoBehaviour
        {
            public static WaveSpawnManager Instance;
            [HideInInspector] public int currentWave = 0;
            private float waveDifficultyRating = 0;
            [SerializeField] private EnemyAI defaultEnemyPrefab;
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
                float totalRating = Mathf.Max(1, (currentWave + PlayerExperienceController.Instance.playersDifficultyRating) / 100f);
                waveDifficultyRating += totalRating;
            }

            private void SpawnEnemies()
            {
                for (int i = 0; i < waveDifficultyRating; i++)
                {
                    EnemyAI newEnemy = null;
                    if (enemyPrefabs.Count > 0)
                    {
                        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Count - 1);
                        // Pick one up from the object pool if it exists.
                        newEnemy = Instantiate(enemyPrefabs[randomEnemyIndex]);
                    }
                    else
                    {
                        newEnemy = Instantiate(defaultEnemyPrefab);
                    }
                    RandomizeSpawnPoint(newEnemy);
                }
            }

            private void SpawnBoss()
            {
                EnemyAI newEnemy = null;
                if (enemyBossPrefabs.Count > 0)
                {
                    int randomEnemyIndex = Random.Range(0, enemyBossPrefabs.Count - 1);
                    // Pick one up from the object pool if it exists.
                    newEnemy = Instantiate(enemyBossPrefabs[randomEnemyIndex]);
                }
                else
                {
                    newEnemy = Instantiate(defaultEnemyPrefab);
                }
                RandomizeSpawnPoint(newEnemy);
            }

            private void RandomizeSpawnPoint(EnemyAI enemyToRandomize)
            {
                GameObject player = GameObject.Find("Player");
                float distanceFromPlayerToSpawn = 40f;

                Vector3 playersLocation = player.transform.position;
                float randomizedXLocation = playersLocation.x + Random.Range(-distanceFromPlayerToSpawn, distanceFromPlayerToSpawn);
                float randomizedYLocation = playersLocation.y + Random.Range(-distanceFromPlayerToSpawn, distanceFromPlayerToSpawn);
                Vector3 randomizedLocation = new Vector3(randomizedXLocation, randomizedYLocation, 0f);
                int attemptsMade = 1;
                while (true)
                {
                    if ((randomizedLocation - playersLocation).sqrMagnitude >= distanceFromPlayerToSpawn)
                    {
                        Debug.Log($"Attempts Taken To Find A Suitable Spawning Location: {attemptsMade}");
                        break;
                    }

                    attemptsMade++;
                    randomizedXLocation = playersLocation.x + Random.Range(-distanceFromPlayerToSpawn, distanceFromPlayerToSpawn);
                    randomizedYLocation = playersLocation.y + Random.Range(-distanceFromPlayerToSpawn, distanceFromPlayerToSpawn);
                    randomizedLocation = new Vector3(randomizedXLocation, randomizedYLocation, 0f);
                }

                enemyToRandomize.transform.position = randomizedLocation;
            }
        }
    }
}