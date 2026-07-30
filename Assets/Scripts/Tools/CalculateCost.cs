namespace ShapeDefender
{
    namespace Tools
    {
        using UnityEngine;

        public static class CalculateCost
        {
            public static float StatExpCost(int statsLevel, float expCost, int purchaseMultiplier)
            {
                float totalCost = 0f;
                float currentExpCost = expCost * Mathf.Pow(1.1f, statsLevel);

                for (int i = 0; i < purchaseMultiplier; i++)
                {
                    totalCost += currentExpCost;
                    currentExpCost *= 1.1f;
                }

                return totalCost;
            }
        }
    }
}