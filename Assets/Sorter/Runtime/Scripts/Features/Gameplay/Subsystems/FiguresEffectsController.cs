using System.Collections.Generic;
using Pool;
using Sorter.Data;
using Sorter.UI;
using UnityEngine;

namespace Sorter.Features
{
    /// <summary>
    /// Отвечает за эффекты фигур. Спавнит партиклы и звуки.
    /// </summary>
    public class FiguresEffectsController
    {
        private readonly MonoPool<FigureEffects> _pool;
        private readonly List<FigureEffects> _effects = new();
        
        public FiguresEffectsController(FiguresConfig figuresConfig, SceneUI sceneUI)
        {
            var panel = sceneUI.GetPanel<GameplayPanel>();
            _pool = new MonoPool<FigureEffects>(figuresConfig.FigureEffectsPrefab, 1, panel.FiguresContainer);
        }

        public void Update()
        {
            for (int i = _effects.Count - 1; i >= 0; i--)
            {
                var effect = _effects[i];
                effect.CustomUpdate();
                if (!effect.IsPlaying)
                    ReleaseEffect(effect);
            }
        }
        
        public void SpawnDissolveEffect(Vector2 position)
        {
            var effect = _pool.Get();
            effect.transform.position = position;
            effect.Dissolve();
            _effects.Add(effect);
        }
        
        public void SpawnExplodeEffect(Vector2 position)
        {
            var effect = _pool.Get();
            effect.transform.position = position;
            effect.Explode();
            _effects.Add(effect);
        }

        private void ReleaseEffect(FigureEffects effect)
        {
            _effects.Remove(effect);
            _pool.Release(effect);
        }
    }
}