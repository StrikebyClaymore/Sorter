using UnityEngine;

namespace Sorter.Features
{
    public class DropZone : MonoBehaviour
    {
        [field: SerializeField] public FigureSlot[] Slots { get; set; }
    }
}