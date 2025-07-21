using System;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

namespace Sorter.UI
{
    public class BasePanel : MonoBehaviour, IDisposable
    {
        [SerializeField] protected PanelAnimation _animation;
        public bool IsActive { get; protected set; }
        
        public virtual async UniTask Show(bool instantly = false)
        {
            gameObject.SetActive(true);
            if(_animation)
                await _animation.Show(instantly);
            IsActive = true;
        }

        public virtual async UniTask Hide(bool instantly = false)
        {
            IsActive = false;
            if(_animation)
                await _animation.Hide(instantly);
            gameObject.SetActive(false);
        }

        public virtual void Dispose()
        {
            _animation.Dispose();
        }
        
#if UNITY_EDITOR
        [Button("Show")]
        private void ShowTest() => Show().Forget();
        
        [Button("Hide")]
        private void HideTest() =>  Hide().Forget();
#endif
    }
}