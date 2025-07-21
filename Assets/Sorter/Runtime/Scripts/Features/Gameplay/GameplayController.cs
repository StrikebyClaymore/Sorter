using System.Collections.Generic;
using DiContainer;
using Sorter.Data;
using Sorter.Infrastructure;
using Sorter.UI;
using UnityEngine;

namespace Sorter.Features
{
    /// <summary>
    /// Контроллер геймплея. Отвечает за обновление игровой логики, победу и поражение.
    /// </summary>
    public class GameplayController : MonoBehaviour
    {
        private enum GameState
        {
            None,
            Play,
        }

        private GameplayConfig _config;
        private readonly List<Figure> _figures = new();
        private FiguresSpawnController _figuresSpawnController;
        private FiguresHandler _figuresHandler;
        private FiguresEffectsController _figuresEffects;
        private SceneUI _sceneUI;
        private GameplayData _gameplayData;
        private LevelConfig _levelConfig;
        private GameState _state;

        [MonoConstructor]
        public void Construct(GameplayConfig config, FiguresConfig figuresConfig, SceneUI sceneUI, GameplayData gameplayData)
        {
            _config = config;
            _sceneUI = sceneUI;
            _gameplayData = gameplayData;
            var panel = sceneUI.GetPanel<GameplayPanel>();
            _figuresEffects = new FiguresEffectsController(figuresConfig, sceneUI);
            _figuresSpawnController = new FiguresSpawnController(figuresConfig, sceneUI, _figures);
            _figuresHandler = new FiguresHandler(_figuresSpawnController, _gameplayData, panel.DeathLine, _figures, _figuresEffects);
            QualitySettings.vSyncCount = config.VSync;
            Application.targetFrameRate = config.TargetFps;
            EventBus.OnLastFigureDestroyed.AddListener(WinOrLose);
            _gameplayData.Health.OnValueChanged += HealthChanged;
            InitializeLevel();
        }

        private void Update()
        {
            if (_state is GameState.Play)
            {
                _figuresSpawnController.Update();
                _figuresHandler.Update();
                _figuresEffects.Update();
            }
        }

        private void InitializeLevel()
        {
            _levelConfig = _config.Levels[0];
            _figuresSpawnController.SetLevelConfig(_levelConfig);
            _gameplayData.Score.Value = 0;
            _gameplayData.Health.Value = _levelConfig.StartHealth;
            SetState(GameState.Play);
        }

        private void SetState(GameState newState)
        {
            _state = newState;
        }

        private void HealthChanged(int value)
        {
            if(value == 0)
                Lose();
        }

        private void WinOrLose()
        {
            if(_gameplayData.Health.Value > 0)
                Win();
        }

        private void Win()
        {
            SetState(GameState.None);
            _sceneUI.ShowPanel<WinPanel>();
        }
        
        private void Lose()
        {
            SetState(GameState.None);
            _sceneUI.ShowPanel<LosePanel>();
        }
    }
}