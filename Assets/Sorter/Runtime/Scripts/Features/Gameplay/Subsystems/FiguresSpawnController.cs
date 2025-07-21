using System;
using System.Collections.Generic;
using Sorter.Data;
using Sorter.Infrastructure;
using Sorter.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sorter.Features
{
    /// <summary>
    /// Создаёт фигуры в соответствии с временным интервалом.
    /// Вызывает событие OnAllFiguresSpawned после создания последней фигуры.
    /// </summary>
    public class FiguresSpawnController
    {
        private readonly List<Figure> _figures;
        private readonly FiguresSpawner _figuresSpawner;
        private LevelConfig _levelConfig;
        private int _levelFiguresMaxCount;
        private int _levelFiguresCount;
        private float _spawnTime;

        public FiguresSpawnController(FiguresConfig figuresConfig, SceneUI sceneUI, List<Figure> figures)
        {
            _figures = figures;
            var panel = sceneUI.GetPanel<GameplayPanel>();
            _figuresSpawner = new FiguresSpawner(figuresConfig, panel.FigureLines, sceneUI.RootCanvas, panel.FiguresContainer);
        }

        public void Update()
        {
            _spawnTime -= Time.unscaledDeltaTime;
            if (_spawnTime <= 0)
            {
                _spawnTime = GetRandomSpawnInterval();
                SpawnFigure();
            }
        }

        public void SetLevelConfig(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
            _figuresSpawner.SetLevelConfig(_levelConfig);
            _spawnTime = GetRandomSpawnInterval();
            _levelFiguresCount = 0;
            _levelFiguresMaxCount = Random.Range(_levelConfig.WinCountRange.x, _levelConfig.WinCountRange.y + 1);
        }

        private void SpawnFigure()
        {
            if(_levelFiguresCount == _levelFiguresMaxCount)
                return;
            var figure = _figuresSpawner.SpawnRandomFigure();
            _figures.Add(figure);
            _levelFiguresCount++;
            if(_levelFiguresCount == _levelFiguresMaxCount)
                EventBus.OnAllFiguresSpawned?.Invoke();
        }

        public void DespawnFigure(Figure figure)
        {
            _figuresSpawner.DestroyFigure(figure);
            _figures.Remove(figure);
        }

        private float GetRandomSpawnInterval()
        {
            return Random.Range(_levelConfig.SpawnIntervalRange.x, _levelConfig.SpawnIntervalRange.y + 1);
        }
    }
}