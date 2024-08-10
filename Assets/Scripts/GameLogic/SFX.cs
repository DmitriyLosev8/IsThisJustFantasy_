using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    internal abstract class SFX : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;

        private float _minPitch = 0.8f;
        private float _maxPitch = 1.5f;
        private System.Random _random = new System.Random();

        public void Stop()
        {
            _source.Stop();
        }

        protected void PlaySound(List<AudioClip> clipList, bool loop = false)
        {
            _source.Stop();
            _source.loop = loop;
            _source.clip = GetRandomClip(clipList);
            _source.pitch = UnityEngine.Random.Range(_minPitch, _maxPitch);
            _source.Play();
        }

        private AudioClip GetRandomClip(List<AudioClip> clipList)
        {
            if (clipList.Count == 0)
                throw new ArgumentException("There is no AudioClips!");

            return clipList[_random.Next(clipList.Count)];
        }
    }
}