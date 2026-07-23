namespace ShapeDefender
{
    namespace UI
    {
        using UnityEngine;
        using UnityEngine.SceneManagement;

        public class RespawnScreenManager : MonoBehaviour
        {
            public static RespawnScreenManager Instance;
            [SerializeField] private GameObject respawnScreen;

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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}