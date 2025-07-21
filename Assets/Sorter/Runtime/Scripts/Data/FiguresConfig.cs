using Sorter.Features;
using UISample.Utility;
using UnityEngine;

namespace Sorter.Data
{
    [CreateAssetMenu(fileName = "FiguresConfig", menuName = "Sorter/FiguresConfig")]
    public class FiguresConfig : ScriptableObject
    {
        [field: SerializeField] public Figure FigurePrefab { get; private set; }
        [field: SerializeField] public FigureEffects FigureEffectsPrefab { get; private set; }
        [field: SerializeField] public PairCollection<EFigure, Sprite[]> Sprites { get; private set; }
    }
}