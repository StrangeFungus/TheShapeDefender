namespace ShapeDefender
{
    namespace EntitySystem
    {
        using UnityEngine;
        using ShapeDefender.MovementSystem;
        using ShapeDefender.AttackSystem;

        [System.Serializable]
        public class BaseEntity : MonoBehaviour
        {
            protected MovementStatContainer entitiesMovementStatContainer;
            protected AttackContainer attackContainer;

            protected void Awake()
            {
                entitiesMovementStatContainer = GetComponent<MovementStatContainer>();
                attackContainer = GetComponent<AttackContainer>();
            }

            protected void Update()
            {
                attackContainer.AttemptToUseAttacks();
            }
        }
    }
}