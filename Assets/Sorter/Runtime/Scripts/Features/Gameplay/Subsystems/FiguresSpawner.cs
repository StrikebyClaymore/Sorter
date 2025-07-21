using BladeRunnder.Utilities;
using Pool;
using Sorter.Data;
using UnityEngine;

namespace Sorter.Features
{
    /// <summary>
    /// Спавнер для фигур.
    /// </summary>
    public class FiguresSpawner
    {
        private readonly FiguresConfig _config;
        private readonly MonoPool<Figure> _pool;
        private readonly FiguresFactory _factory;
        private readonly Vector2[] _spawnPoints = new Vector2[3];
        private LevelConfig _levelConfig;

        public FiguresSpawner(FiguresConfig config, FigureLines figureLines, Canvas rootCanvas, Transform container)
        {
            _config = config;
            _pool = new MonoPool<Figure>(config.FigurePrefab, 1, container);
            _factory = new FiguresFactory(_pool, rootCanvas);
            _spawnPoints[0] = figureLines.TopLine.LeftRect.position;
            _spawnPoints[1] = figureLines.MiddleLine.LeftRect.position;
            _spawnPoints[2] = figureLines.BottomLine.LeftRect.position;
        }

        public void SetLevelConfig(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        public Figure SpawnRandomFigure()
        {
            var type = GetRandomType();
            return _factory.Create(type, GetRandomSprite(type), GetRandomSpawnPosition(), GetRandomSpeed());
        }

        public void DestroyFigure(Figure figure)
        {
            _pool.Release(figure);
        }

        private EFigure GetRandomType()
        {
            return RandomExtensions.Random<EFigure>();
        }

        private Sprite GetRandomSprite(EFigure type)
        {
            return RandomExtensions.Random(_config.Sprites[type]);
        }

        private Vector2 GetRandomSpawnPosition()
        {
            return RandomExtensions.Random(_spawnPoints);
        }
        
        private float GetRandomSpeed()
        {
            return Random.Range(_levelConfig.MoveSpeedRange.x, _levelConfig.MoveSpeedRange.y);
        }
    }
}