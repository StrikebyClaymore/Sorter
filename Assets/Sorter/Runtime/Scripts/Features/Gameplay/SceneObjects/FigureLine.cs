using UnityEngine;

namespace Sorter.Features
{
    public class FigureLine : MonoBehaviour
    {
        [field: SerializeField] public RectTransform LeftRect { get; set; }
        [field: SerializeField] public RectTransform RightRect { get; set; }
        
#if UNITY_EDITOR
        private void Reset()
        {
            LeftRect = transform.Find("Left").GetComponent<RectTransform>();
            RightRect = transform.Find("Right").GetComponent<RectTransform>();
        }
#endif
    }
}