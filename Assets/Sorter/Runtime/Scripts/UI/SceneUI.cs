using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sorter.Infrastructure;
using UnityEngine;

namespace Sorter.UI
{
    public class SceneUI : IDisposable
    {
        protected readonly Dictionary<Type, BasePanel> _panels = new();
        protected readonly Dictionary<Type, BasePanel> _showedPanels = new();
        protected readonly Stack<List<Type>> _previousPanelsStack = new();
        public Canvas RootCanvas { get; private set; }

        public SceneUI(UIContainer uiContainer, Canvas rootCanvas)
        {
            RootCanvas = rootCanvas;
            RegisterPanel(typeof(GameplayPanel), uiContainer.GetPanel<GameplayPanel>());
            RegisterPanel(typeof(WinPanel), uiContainer.GetPanel<WinPanel>());
            RegisterPanel(typeof(LosePanel), uiContainer.GetPanel<LosePanel>());
            HideAllPanels(true);
            ShowPanel<GameplayPanel>();
        }
        
        public void RegisterPanel(Type type, BasePanel panel)
        {
            _panels.Add(type, panel);
            panel.Hide(true).Forget();
        }

        public void Dispose()
        {
            foreach (var panel in _panels.Values)
                panel.Dispose();
            ClearPanels();
        }
        
        public T GetPanel<T>() where T : BasePanel
        {
            foreach (var panel in _panels.Values)
            {
                if (panel is T tPanel)
                {
                    return tPanel;
                }
            }
            Debug.LogError($"Panel {typeof(T)} not found.");
            return null;
        }

        public bool ShowPanel<T>() where T : BasePanel
        {
            var type = typeof(T);
            return ShowPanel(type);
        }

        public bool HidePanel<T>(bool savePrevious = true) where T : BasePanel
        {
            var type = typeof(T);
            if (savePrevious)
                SaveCurrentPanels();
            if (_showedPanels.TryGetValue(type, out var panel))
            {
                panel.Hide().Forget();
                _showedPanels.Remove(type);
                return true;
            }
            Debug.LogWarning($"Can't hide {type} panel.");
            return false;
        }

        public void ShowPreviousPanels()
        {
            if (_previousPanelsStack.Count == 0)
                return;
            HideAllPanels(false);
            var previousPanels = _previousPanelsStack.Pop();
            foreach (var type in previousPanels)
            {
                ShowPanel(type);
            }
        }

        public void HideAllPanels(bool instant = false, bool savePrevious = true, BasePanel excluding = null)
        {
            if (savePrevious)
                SaveCurrentPanels();
            foreach (var pair in _showedPanels)
            {
                var panel = pair.Value;
                if (panel == excluding)
                    continue;
                panel.Hide(instant).Forget();
            }
            _showedPanels.Clear();
        }

        public void ClearPanelsStack()
        {
            _previousPanelsStack.Clear();
        }
        
        private bool ShowPanel(Type type)
        {
            if (_showedPanels.ContainsKey(type))
            {
                Debug.LogWarning($"The {type} panel is already open.");
                return false;
            }
            if (_panels.TryGetValue(type, out var panel))
            {
                panel.Show().Forget();
                _showedPanels.Add(type, panel);
                return true;
            }
            Debug.LogWarning($"Can't show {type} panel.");
            return false;
        }

        private void SaveCurrentPanels()
        {
            if (_showedPanels.Count > 0)
            {
                _previousPanelsStack.Push(new List<Type>(_showedPanels.Keys));
            }
        }

        private void ClearPanels()
        {
            _panels.Clear();
            _showedPanels.Clear();
            _previousPanelsStack.Clear();
        }
    }
}