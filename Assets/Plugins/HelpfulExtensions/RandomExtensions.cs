using System;
using System.Collections.Generic;
using System.Linq;

namespace BladeRunnder.Utilities
{
    public static class RandomExtensions
    {
        public static T Random<T>(T[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public static T Random<T>(List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static T RandomWithExcluding<T>(T[] array, IEnumerable<T> excluding)
        {
            var filtered = array.Where(x => !excluding.Contains(x)).ToArray();
            return filtered[UnityEngine.Random.Range(0, filtered.Length)];
        }

        public static T RandomWithExcluding<T>(List<T> list, IEnumerable<T> excluding)
        {
            var filtered = list.Where(x => !excluding.Contains(x)).ToList();
            return filtered[UnityEngine.Random.Range(0, filtered.Count)];
        }
        
        public static T Random<T>() where T : Enum
        {
            var values = (T[])Enum.GetValues(typeof(T));
            return values[UnityEngine.Random.Range(0, values.Length)];
        }
        
        public static T RandomWithExcluding<T>(List<T> excluding) where T : Enum
        {
            var values = ((T[])Enum.GetValues(typeof(T)))
                .Where(v => !excluding.Contains(v))
                .ToArray();
            return values[UnityEngine.Random.Range(0, values.Length)];
        }
    }
}