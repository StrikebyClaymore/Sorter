using System;
using System.Collections.Generic;
using Sorter.Infrastructure;
using UnityEngine;

namespace Sorter.Features
{
    /// <summary>
    /// Двигает фигуры и обрабатывает сброс фигур в дроп зону.
    /// Вызывает событие OnLastFigureDestroyed когда уничтожается последняя фигура.
    /// </summary>
    public class FiguresHandler
    {
        private readonly List<Figure> _figures;
        private readonly FiguresSpawnController _spawnController;
        private readonly GameplayData _gameplayData;
        private readonly RectTransform _deathLine;
        private readonly FiguresEffectsController _figuresEffects;
        private bool _allFiguresSpawned;

        public FiguresHandler(FiguresSpawnController spawnController, GameplayData gameplayData,
            RectTransform deathLine, List<Figure> figures, FiguresEffectsController figuresEffects)
        {
            _spawnController = spawnController;
            _figures = figures;
            _gameplayData = gameplayData;
            _deathLine = deathLine;
            _figuresEffects = figuresEffects;
            EventBus.OnValidFigureDropped.AddListener(ValidFigureDropped);
            EventBus.OnInvalidFigureDropped.AddListener(InvalidFigureDropped);
            EventBus.OnAllFiguresSpawned.AddListener(AllFiguresSpawned);
        }

        public void Update()
        {
            for (int i = _figures.Count - 1; i >= 0; i--)
            {
                var figure = _figures[i];
                figure.Move(Time.deltaTime);
                if (EnterToDeathZone(figure.RectTransform))
                    InvalidFigureDropped(figure);
            }
        }

        private void ValidFigureDropped(Figure figure)
        {
            _spawnController.DespawnFigure(figure);
            _gameplayData.Score.Value += 1;
            _figuresEffects.SpawnDissolveEffect(figure.transform.position);
            if (IsLastFigure())
                EventBus.OnLastFigureDestroyed?.Invoke();
        }

        private void InvalidFigureDropped(Figure figure)
        {
            _spawnController.DespawnFigure(figure);
            _gameplayData.Health.Value -= 1;
            _figuresEffects.SpawnExplodeEffect(figure.transform.position);
            if (IsLastFigure())
                EventBus.OnLastFigureDestroyed?.Invoke();
        }

        private bool EnterToDeathZone(RectTransform rectTransform)
        {
            return rectTransform.position.x >= _deathLine.position.x;
        }

        private void AllFiguresSpawned()
        {
            _allFiguresSpawned = true;
        }

        private bool IsLastFigure()
        {
            return _allFiguresSpawned && _figures.Count == 0;
        }
    }
}