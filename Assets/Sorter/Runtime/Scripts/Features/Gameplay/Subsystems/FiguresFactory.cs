using Pool;
using UnityEngine;

namespace Sorter.Features
{
    /// <summary>
    /// Фабрика для фигру.
    /// Требование из ТЗ. Считаю в данном случае достаточно спавнера.
    /// </summary>
    public class FiguresFactory
    {
        private readonly Canvas _rootCanvas; 
        private readonly MonoPool<Figure> _pool;

        public FiguresFactory(MonoPool<Figure> pool, Canvas rootCanvas)
        {
            _rootCanvas = rootCanvas;
            _pool = pool;
        }
        
        public Figure Create(EFigure type, Sprite sprite, Vector2 position, float speed)
        {
            var instance = _pool.Get();
            instance.Initialize(type, sprite, position, speed, _rootCanvas.scaleFactor);
            return instance;
        }
    }
}