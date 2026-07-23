namespace ShapeDefender
{
    namespace UI
    {
        using TMPro;
        using UnityEngine;

        [System.Serializable]
        public class FloatingTextSpawner : MonoBehaviour
        {
            public static FloatingTextSpawner Instance;
            [SerializeField] private GameObject floatingTextPrefab;

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

            public void SpawnText(string textToDisplay, Color textsColor, Vector3 locationToSpawn)
            {
                GameObject newFloatingText = Instantiate(floatingTextPrefab, locationToSpawn, floatingTextPrefab.transform.rotation);
                FloatingTextController newFloatingTextController = newFloatingText.GetComponent<FloatingTextController>();
                newFloatingTextController.SetFloatingText(textToDisplay, textsColor);
            }
        }
    }
}