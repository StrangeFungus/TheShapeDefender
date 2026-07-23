namespace ShapeDefender
{
    namespace DefenseSystem
    {
        using UnityEngine;

        public class DefenseStatContainer : MonoBehaviour
        {
            [SerializeField] private DefenseStatSO defenseStatSOTemplate;
            [HideInInspector] public DefenseStatSO runtimeDefenseStats;

            private void Awake()
            {
                if (defenseStatSOTemplate != null)
                {
                    runtimeDefenseStats = Instantiate(defenseStatSOTemplate);
                    runtimeDefenseStats.currentParryCooldown = runtimeDefenseStats.parryCooldown.StatValue;
                    runtimeDefenseStats.currentBlockCooldown = runtimeDefenseStats.blockCooldown.StatValue;
                    runtimeDefenseStats.currentDodgeCooldown = runtimeDefenseStats.dodgeCooldown.StatValue;
                }
            }

            private void Update()
            {
                if (runtimeDefenseStats == null) { return; }

                runtimeDefenseStats.currentParryCooldown -= Time.deltaTime;
                runtimeDefenseStats.currentBlockCooldown -= Time.deltaTime;
                runtimeDefenseStats.currentDodgeCooldown -= Time.deltaTime;
            }

            public void ResetParryCooldown()
            {
                runtimeDefenseStats.currentParryCooldown = runtimeDefenseStats.parryCooldown.StatValue;
            }

            public void ResetBlockCooldown()
            {
                runtimeDefenseStats.currentBlockCooldown = runtimeDefenseStats.blockCooldown.StatValue;
            }

            public void ResetDodgeCooldown()
            {
                runtimeDefenseStats.currentDodgeCooldown = runtimeDefenseStats.dodgeCooldown.StatValue;
            }
        }
    }
}