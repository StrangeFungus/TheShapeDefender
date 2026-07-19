namespace ShapeDefender
{
    namespace UI
    {
        using TMPro;
        using UnityEngine;

        [System.Serializable]
        public class FloatingTextSpawner : MonoBehaviour
        {
            [SerializeField] private GameObject floatingTextPrefab;

            public void SpawnText(string textToDisplay, Color textsColor, Vector3 locationToSpawn)
            {
                GameObject newFloatingText = Instantiate(floatingTextPrefab, locationToSpawn, floatingTextPrefab.transform.rotation);
                TextMeshPro newFloatingTextTMP = newFloatingText.GetComponent<TextMeshPro>();
                newFloatingTextTMP.text = textToDisplay;
                newFloatingTextTMP.color = textsColor;
            }
        }
    }
}