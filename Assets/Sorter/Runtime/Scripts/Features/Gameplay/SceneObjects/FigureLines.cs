using UnityEngine;

namespace Sorter.Features
{
    [System.Serializable]
    public struct FigureLines
    {
        [field: SerializeField] public FigureLine TopLine { get; private set; }
        [field: SerializeField] public FigureLine MiddleLine { get; private set; }
        [field: SerializeField] public FigureLine BottomLine { get; private set; }
    }
}