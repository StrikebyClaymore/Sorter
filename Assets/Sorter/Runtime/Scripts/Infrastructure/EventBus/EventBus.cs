using System;
using Sorter.Features;
using UnityEngine.Events;

namespace Sorter.Infrastructure
{
    /// <summary>
    /// Добален по запросу из ТЗ. Так бы делал через локальные события внутри классов.
    /// </summary>
    public class EventBus
    {
        public static readonly UnityEvent OnLastFigureDestroyed = new();
        public static readonly UnityEvent OnAllFiguresSpawned = new();
        public static readonly UnityEvent<Figure> OnValidFigureDropped = new();
        public static readonly UnityEvent<Figure> OnInvalidFigureDropped = new();
        
        public static void Dispose()
        {
            OnLastFigureDestroyed.RemoveAllListeners();
            OnAllFiguresSpawned.RemoveAllListeners();
            OnValidFigureDropped.RemoveAllListeners();
            OnInvalidFigureDropped.RemoveAllListeners();
        }
    }
}