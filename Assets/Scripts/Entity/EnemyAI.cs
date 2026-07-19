namespace ShapeDefender
{
    namespace EntitySystem
    {
        using UnityEngine;

        [System.Serializable]
        public class EnemyAI : BaseEntity
        {
            private Vector2 movementDirection;
            private GameObject playerObject;

            private void Start()
            {
                playerObject = GameObject.Find("Player");
            }

            private new void Update()
            {
                movementDirection = (playerObject.transform.position - transform.position).normalized;
                base.Update();
            }

            private void FixedUpdate()
            {
                if (playerObject != null)
                {
                    entitiesMovementController.Move(movementDirection);
                }
            }
        }
    }
}