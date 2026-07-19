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
            protected MovementDataController entitiesMovementController;
            protected AttackContainer attackContainer;

            protected void Awake()
            {
                entitiesMovementController = GetComponent<MovementDataController>();
                attackContainer = GetComponent<AttackContainer>();
            }

            protected void Update()
            {
                attackContainer.AttemptToUseAttacks();
            }

            // on disable we can eventually return the entity back to the object pool
        }
    }
}