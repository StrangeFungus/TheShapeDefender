namespace ShapeDefender
{
    namespace MovementSystem
    {
        using UnityEngine;

        [CreateAssetMenu(menuName = "Movement Stats", fileName = "New Movement Stats")]
        public class MovementStatSO : ScriptableObject
        {
            [Header("Ground")]
            public StatEntry groundSpeed;
            public StatEntry groundTurningSpeed;
            public StatEntry groundAccelerationSpeed;
            public StatEntry groundBrakingSpeed;

            [Header("Surface Water")]
            public StatEntry surfaceWaterSpeed;
            public StatEntry surfaceWaterTurningSpeed;
            public StatEntry surfaceWaterAccelerationSpeed;
            public StatEntry surfaceWaterBrakingSpeed;

            [Header("Underwater")]
            public StatEntry underwaterSpeed;
            public StatEntry underwaterTurningSpeed;
            public StatEntry underwaterAccelerationSpeed;
            public StatEntry underwaterBrakingSpeed;

            [Header("Flying")]
            public StatEntry flyingSpeed;
            public StatEntry flyingTurningSpeed;
            public StatEntry flyingAccelerationSpeed;
            public StatEntry flyingBrakingSpeed;

            [Header("Space Travel")]
            public StatEntry spaceTravelSpeed;
            public StatEntry spaceTravelTurningSpeed;
            public StatEntry spaceTravelAccelerationSpeed;
            public StatEntry spaceTravelBrakingSpeed;
        }
    }
}