using Sorter.Infrastructure;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sorter.Features
{
    public class FigureSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private EFigure _inputType;
        
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent<Figure>(out var figure))
            {
                if (figure.Type == _inputType)
                    EventBus.OnValidFigureDropped?.Invoke(figure);
                else
                    EventBus.OnInvalidFigureDropped?.Invoke(figure);
            }
        }
    }
}