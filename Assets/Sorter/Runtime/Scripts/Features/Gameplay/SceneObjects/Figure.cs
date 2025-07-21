using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sorter.Features
{
    public class Figure : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [field: SerializeField] public RectTransform RectTransform { get; private set; }
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _viewImage;
        [SerializeField] private float _returnDuration = 0.3f;
        private float _moveSpeed;
        private float _canvasScaleFactor;
        private Vector3 _originalPosition;
        private bool _isDragging;
        public EFigure Type { get; private set; }

        public void Initialize(EFigure type, Sprite sprite, Vector2 position, float moveSpeed, float scaleFactor)
        {
            Type = type;
            _viewImage.sprite = sprite;
            RectTransform.position = position;
            _moveSpeed = moveSpeed;
            _canvasScaleFactor = scaleFactor;
            _canvasGroup.blocksRaycasts = true;
            _isDragging = false;
        }

        public void Move(float delta)
        {
            if (_isDragging)
                return;
            RectTransform.anchoredPosition += Vector2.right * (_moveSpeed * delta / _canvasScaleFactor);
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
            _canvasGroup.blocksRaycasts = false;
            _originalPosition = RectTransform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransform.anchoredPosition += eventData.delta / _canvasScaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            RectTransform.DOAnchorPos(_originalPosition, _returnDuration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(ReturnCompleted);


            void ReturnCompleted()
            {
                _canvasGroup.blocksRaycasts = true;
                _isDragging = false;
            }
        }
    }
}