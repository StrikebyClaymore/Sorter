using Cysharp.Threading.Tasks;
using DiContainer;
using Sorter.Features;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sorter.UI
{
    public class WinPanel : BasePanel
    {
        private const string ScoreTextFormat = "Очки: {0}";
        [field: SerializeField] public TMP_Text ScoreText { get; private set; }
        [field: SerializeField] public Button RestartButton { get; private set; }
        private GameplayData _gameplayData;
        
        [MonoConstructor]
        public void Construct(GameplayData gameplayData)
        {
            _gameplayData = gameplayData;
            RestartButton.onClick.AddListener(RestartPressed);
        }

        public override async UniTask Show(bool instantly = false)
        {
            ScoreText.text = string.Format(ScoreTextFormat, _gameplayData.Score.Value.ToString());
            await base.Show(instantly);
        }

        private void RestartPressed()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}