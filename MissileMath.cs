using UnityEngine;

namespace SagsMissiles
{
    public class MissileMath
    {
        public static float CircleArea(float r)
        {
            return r * r * Mathf.PI;
        }

        public static float DragEquation(float v, float rho, float Cd, float A)
        {
            return 0.5f * rho * v * v * Cd * A;
        }
    }
}