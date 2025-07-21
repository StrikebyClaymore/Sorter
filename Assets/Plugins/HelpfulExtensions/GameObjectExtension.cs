using UnityEngine;

namespace BladeRunnder.Utilities
{
    public static class GameObjectExtension
    {
        public static void Show(this GameObject gameObject)
        {
            gameObject.SetActive(true);
        }
        
        public static void Hide(this GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T component)
        {
            component = gameObject.GetComponentInChildren<T>();
            return component != null;
        }
    }
}