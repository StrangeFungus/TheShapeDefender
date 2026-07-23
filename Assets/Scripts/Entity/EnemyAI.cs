namespace ShapeDefender
{
    namespace EntitySystem
    {
        using ShapeDefender.LevelUpSystem;
        using UnityEngine;

        [System.Serializable]
        public class EnemyAI : BaseEntity
        {
            private Vector2 movementDirection;
            private GameObject playerObject;
            [SerializeField] private float experienceReward = 10f;

            private void Start()
            {
                playerObject = GameObject.Find("Player");
                entitiesMovementStatContainer.runtimeMovementStats.groundSpeed.canLevelUp = true;
                entitiesMovementStatContainer.runtimeMovementStats.groundTurningSpeed.canLevelUp = true;
                entitiesMovementStatContainer.runtimeMovementStats.groundAccelerationSpeed.canLevelUp = true;
                entitiesMovementStatContainer.runtimeMovementStats.groundBrakingSpeed.canLevelUp = true;
                entitiesMovementStatContainer.UpdateMovementStats();
            }

            private new void Update()
            {
                movementDirection = (playerObject.transform.position - transform.position).normalized;
                base.Update();
            }

            private void OnDisable()
            {
                LevelUpMenuManager.Instance.playersExperiencePoints += experienceReward;
                LevelUpMenuManager.Instance.UpdateExperiencePointTrackerText();
            }

            private void FixedUpdate()
            {
                if (playerObject != null)
                {
                    entitiesMovementStatContainer.Move(movementDirection);
                }
            }

            // on disable we can eventually return the entity back to the object pool
        }
    }
}