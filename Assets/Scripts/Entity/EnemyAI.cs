namespace ShapeDefender
{
    namespace EntitySystem
    {
        using ShapeDefender.UI;
        using UnityEngine;

        [System.Serializable]
        public class EnemyAI : BaseEntity
        {
            private Vector2 movementDirection;
            private GameObject playerObject;
            [SerializeField] private float experienceReward = 10f;
            public int difficultyValue = 1;

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
                if (playerObject != null)
                {
                    movementDirection = (playerObject.transform.position - transform.position).normalized;
                }
                base.Update();
            }

            private void OnDisable()
            {
                PlayerExperienceController.Instance.playersExperiencePoints += experienceReward * PlayerExperienceController.Instance.experienceGainMultiplier;
                PlayerExperienceController.Instance.UpdateExperiencePointTrackerText();
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