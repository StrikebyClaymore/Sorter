using System;
using UnityEngine;

namespace Sorter.Features
{
    public class FigureEffects : MonoBehaviour, IDisposable
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _dissolveClip;
        [SerializeField] private AudioClip _explodeClip;
        private float _playTime;
        public bool IsPlaying { get; private set; }
        
        public void CustomUpdate()
        {
            if (!IsPlaying)
                return;
            _playTime -= Time.deltaTime;
            if (_playTime <= 0)
                IsPlaying = false;
        }

        public void Dispose()
        {
            IsPlaying = false;
            _audioSource.Stop();
            _particleSystem.Stop();
        }

        public void Dissolve()
        {
            Dispose();
            _audioSource.clip = _dissolveClip;
            _audioSource.Play();
            _playTime = _audioSource.clip.length;
            IsPlaying = true;
        }

        public void Explode()
        {
            Dispose();
            _audioSource.clip = _explodeClip;
            _audioSource.Play();
            _particleSystem.Play();
            _playTime = _audioSource.clip.length > _particleSystem.main.duration ? _audioSource.clip.length : _particleSystem.main.duration;
            IsPlaying = true;
        }
    }
}