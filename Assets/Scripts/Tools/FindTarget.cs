namespace ShapeDefender
{
    namespace Tools
    {
        using UnityEngine;

        public static class FindTarget
        {
            public static GameObject FindTargetInRange(GameObject callingObject, float minRange, float maxRange)
            {
                if (callingObject.CompareTag("Enemy"))
                {
                    GameObject playerTarget = GameObject.Find("Player");
                    if (playerTarget != null)
                    {
                        float distSquared = (playerTarget.transform.position - callingObject.transform.position).sqrMagnitude;
                        if (distSquared >= minRange && distSquared <= maxRange)
                        {
                            return playerTarget;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Enemy");

                    GameObject closestTarget = null;
                    float closestDistance = float.MaxValue;

                    foreach (var target in allTargets)
                    {
                        float distSquared = (target.transform.position - callingObject.transform.position).sqrMagnitude;
                        if (distSquared >= minRange && distSquared <= maxRange)
                        {
                            if (distSquared < closestDistance)
                            {
                                closestDistance = distSquared;
                                closestTarget = target;
                            }
                        }
                    }

                    return closestTarget;
                }
            }
        }
    }
}