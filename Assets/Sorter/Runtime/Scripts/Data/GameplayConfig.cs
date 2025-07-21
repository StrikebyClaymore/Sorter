using UnityEngine;

namespace Sorter.Data
{
    [CreateAssetMenu(fileName = "GameplayConfig", menuName = "Sorter/GameplayConfig")]
    public class GameplayConfig : ScriptableObject
    {
        [field: SerializeField] public LevelConfig[] Levels { get; set; }
        [field: SerializeField] public int VSync { get; set; } = 0;
        [field: SerializeField] public int TargetFps { get; set; } = 60;
    }
}