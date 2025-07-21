using UnityEngine;

namespace Sorter.Data
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Sorter/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2Int WinCountRange { get; set; } = new Vector2Int(5, 10);
        [field: SerializeField] public Vector2 SpawnIntervalRange { get; set; } = new Vector2(1, 3);
        [field: SerializeField] public Vector2 MoveSpeedRange { get; set; } = new Vector2(1, 3);
        [field: SerializeField] public int StartHealth { get; set; } = 10;
    }
}