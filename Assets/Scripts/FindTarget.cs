using UnityEngine;

public static class FindTarget
{
    public static GameObject FindTargetInRange(GameObject callingObject, float minRange, float maxRange)
    {
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (var target in allTargets)
        {
            float distSquared = (target.transform.position - callingObject.transform.position).sqrMagnitude;

            // Check min range + max range
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
