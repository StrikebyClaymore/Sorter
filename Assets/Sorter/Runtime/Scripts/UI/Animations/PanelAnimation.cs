using System;
using System.Collections.Generic;
using System.Threading;
using BladeRunnder.Utilities;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Sorter.UI
{
    public class PanelAnimation : MonoBehaviour, IDisposable
    {
        [Header("Components")]
        [SerializeField] private Image _shadowImage;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        [Header("Settings")]
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private EPanelAnimation _type;
        [SerializeField] private Ease _ease = Ease.InQuad;
        [SerializeField] private float _shadowAlpha = 0.8f;
        
        private readonly List<UniTask> _sequence = new();
        private CancellationTokenSource _cts;

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _sequence.Clear();
        }

        public async UniTask Show(bool instantly = false)
        {
            Dispose();
            _cts = new CancellationTokenSource();
            await PlayAnimation(EAnimationAction.Show, instantly);
        }
        
        public async UniTask Hide(bool instantly = false)
        {
            Dispose();
            _cts = new CancellationTokenSource();
            await PlayAnimation(EAnimationAction.Hide, instantly);
        }

        private async UniTask PlayAnimation(EAnimationAction action, bool instantly = false)
        {
            switch (action)
            {
                case EAnimationAction.Show:
                    switch (_type)
                    {
                        case EPanelAnimation.Scale:
                            _sequence.Add(ScaleAnimation(Vector3.zero, Vector3.one, instantly));
                            _sequence.Add(ShadowAnimation(0, _shadowAlpha, instantly));
                            break;
                    }
                    break;

                case EAnimationAction.Hide:
                    switch (_type)
                    {
                        case EPanelAnimation.Scale:
                            _sequence.Add(ScaleAnimation(Vector3.one, Vector3.zero, instantly));
                            _sequence.Add(ShadowAnimation(_shadowAlpha, 0, instantly));
                            break;
                    }
                    break;
            }

            await UniTask.WhenAll(_sequence);
        }

        private UniTask ScaleAnimation(Vector3 from, Vector3 to, bool instantly = false)
        {
            if (instantly)
            {
                _rectTransform.localScale = to;
                return UniTask.CompletedTask;
            }
            _rectTransform.localScale = from;
            return _rectTransform
                .DOScale(to, _duration)
                .SetEase(_ease)
                .ToUniTask(cancellationToken: _cts.Token)
                .SuppressCancellationThrow();
        }

        private UniTask ShadowAnimation(float from, float to, bool instantly = false)
        {
            if (instantly)
            {
                _shadowImage.SetAlpha(to);
                return UniTask.CompletedTask;
            }
            _shadowImage.SetAlpha(from);
            return _shadowImage.DOFade(to, _duration)
                .SetEase(_ease)
                .ToUniTask(cancellationToken: _cts.Token)
                .SuppressCancellationThrow();
        }
    }
}