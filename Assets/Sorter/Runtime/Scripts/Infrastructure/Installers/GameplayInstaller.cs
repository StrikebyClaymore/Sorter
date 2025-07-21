using DiContainer;
using Sorter.Data;
using Sorter.Features;
using Sorter.UI;
using UnityEngine;

namespace Sorter.Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {
        [Header("Configurations")]
        [SerializeField] private GameplayConfig _gameplayConfig;
        [SerializeField] private FiguresConfig _figuresConfig;
        
        [Header("Scene Dependencies")]
        [SerializeField] private Canvas _rootCanvas;
        [SerializeField] private UIContainer _uiContainer;
        
        private DIContainer _diContainer;
        private GameplayData _gameplayData;
        
        public override void Install()
        {
            EventBus.Dispose();
            InstallDIContainer();
            InstallConfigs();
            InstallGameplayData();
            InstallUI();
            InstallGameplayController();
        }

        private void InstallDIContainer()
        {
            _diContainer = new DIContainer();
        }

        private void InstallConfigs()
        {
            _diContainer.RegisterInstance(_gameplayConfig, true);
            _diContainer.RegisterInstance(_figuresConfig, true);
        }

        private void InstallGameplayData()
        {
            _diContainer.RegisterNew<GameplayData>(true);
        }
        
        private void InstallGameplayController()
        {
            _diContainer.RegisterNew<GameplayController>(true);
        }

        private void InstallUI()
        {
            _diContainer.RegisterInstance<Canvas>(_rootCanvas, true);
            _diContainer.RegisterInstance<UIContainer>(_uiContainer, true);
            var sceneUI = _diContainer.RegisterNew<SceneUI>();
            _diContainer.Inject<GameplayPanel>(sceneUI.GetPanel<GameplayPanel>());
            _diContainer.Inject<WinPanel>(sceneUI.GetPanel<WinPanel>());
            _diContainer.Inject<LosePanel>(sceneUI.GetPanel<LosePanel>());
        }
    }
}