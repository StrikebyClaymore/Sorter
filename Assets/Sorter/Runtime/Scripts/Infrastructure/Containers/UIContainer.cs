using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Sorter.UI;
using UnityEngine;

namespace Sorter.Infrastructure
{
    public class UIContainer : MonoBehaviour
    {
        [SerializeField] private List<BasePanel> _panels;
        public IReadOnlyCollection<BasePanel> Panel => _panels;
        
        public T GetPanel<T>() where T : BasePanel
        {
            Type type = typeof(T);
            foreach (var panel in _panels)
            {
                if (panel is T findView)
                    return findView;
            }
            Debug.LogError(new KeyNotFoundException($"Panel {type} not found."));
            return null;
        }
        
#if UNITY_EDITOR
        [Button]
        public void FindPanels()
        {
            _panels = GetComponentsInChildren<BasePanel>(true).ToList();
        }
#endif
    }
}