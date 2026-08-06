namespace ShapeDefender
{
    namespace AttackSystem
    {
        using ShapeDefender.DamageSystem;
        using ShapeDefender.Tools;
        using UnityEngine;

        [System.Serializable]
        public class AttackController : MonoBehaviour
        {
            [HideInInspector] public AttackStatSO runtimeAttackStatSO;

            private GameObject targetObject;
            private Vector3 targetLocation;
            private string targetTag;
            private Vector3 targetDirection;

            private Vector3 spawnPosition;
            private float distanceTraveled = 0f;

            private Rigidbody2D hostsRigidbody2D;

            private void Awake()
            {
                hostsRigidbody2D = GetComponent<Rigidbody2D>();
                SetSpawnPosition();
            }

            private void Update()
            {
                distanceTraveled = (spawnPosition - transform.position).sqrMagnitude;
                if (distanceTraveled >= runtimeAttackStatSO.attackRangeMaximum.StatValue)
                {
                    Destroy(gameObject);
                }
            }

            private void FixedUpdate()
            {
                Vector2 forward = transform.up;
                hostsRigidbody2D.linearVelocity = forward * runtimeAttackStatSO.projectileSpeed.StatValue;
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.CompareTag(targetTag))
                {
                    bool wasAttackReflected = DamageProcessor.ProcessDamageToTarget(collision.gameObject, gameObject, runtimeAttackStatSO);
                    if (!wasAttackReflected)
                    {
                        Destroy(gameObject);
                    }
                }
            }

            public void SetSpawnPosition()
            {
                spawnPosition = transform.position;
            }

            public void SetTarget(GameObject targetsObject)
            {
                if (targetsObject == null) return;

                targetObject = targetsObject;
                targetLocation = targetsObject.transform.position;
                targetTag = targetsObject.tag;
                targetDirection = (targetLocation - transform.position).normalized;

                float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
                hostsRigidbody2D.SetRotation(targetAngle);

                hostsRigidbody2D.linearVelocity = transform.up * runtimeAttackStatSO.projectileSpeed.StatValue;
            }
            /*
            private void OnDrawGizmos()
            {
                if (targetLocation != null)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(transform.position, targetLocation);
                }
            }
            */
        }
    }
}