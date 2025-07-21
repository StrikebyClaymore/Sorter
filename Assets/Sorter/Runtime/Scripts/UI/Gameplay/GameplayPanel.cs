using DiContainer;
using Sorter.Features;
using TMPro;
using UnityEngine;

namespace Sorter.UI
{
    public class GameplayPanel : BasePanel
    {
        [field: SerializeField] public TMP_Text ScoreText { get; private set; }
        [field: SerializeField] public TMP_Text HealthText { get; private set; }
        [field: SerializeField] public FigureLines FigureLines { get; private set; }
        [field: SerializeField] public DropZone DropZone { get; private set; }
        [field: SerializeField] public RectTransform DeathLine { get; private set; }
        [field: SerializeField] public  Transform FiguresContainer { get; private set; }

        [MonoConstructor]
        public void Construct(GameplayData gameplayData)
        {
            gameplayData.Score.OnValueChanged += ScoreChanged;
            gameplayData.Health.OnValueChanged += HealthChanged;
        }

        private void ScoreChanged(int value)
        {
            ScoreText.text = value.ToString();
        }
        
        private void HealthChanged(int value)
        {
            HealthText.text = value.ToString();
        }
    }
}