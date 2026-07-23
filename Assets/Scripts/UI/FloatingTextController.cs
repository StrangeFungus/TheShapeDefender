namespace ShapeDefender
{
    namespace UI
    {
        using TMPro;
        using UnityEngine;

        [System.Serializable]
        public class FloatingTextController : MonoBehaviour
        {
            [SerializeField] private float floatingSpeed = 2f;
            [SerializeField] private TextMeshPro floatingText;
            private Color textsDisplayColor;
            private float alphaValue = 1.0f;
            [SerializeField] private float fadeRate = 0.0001f;

            void Update()
            {
                transform.position += new Vector3(0.0f, floatingSpeed * Time.deltaTime, 0.0f);
                alphaValue -= fadeRate;
                Color newColor = new Color(textsDisplayColor.r, textsDisplayColor.g, textsDisplayColor.b, alphaValue);
                floatingText.color = newColor;

                if (alphaValue <= 0.0f)
                {
                    Destroy(gameObject);
                }
            }

            public void SetFloatingText(string text, Color textsColor)
            {
                floatingText.SetText(text);
                floatingText.color = textsColor;
                textsDisplayColor = textsColor;
            }
        }
    }
}