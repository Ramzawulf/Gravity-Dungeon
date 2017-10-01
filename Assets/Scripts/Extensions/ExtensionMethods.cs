using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class ExtensionMethods{

        public static Vector3 DirectionTo(this Vector3 value, Vector3 target){
            return (target - value).normalized;
        }
    }
}
