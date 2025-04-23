using System.Collections.Generic;
using DG.Tweening;
using Game;
using Global.AudioSystem;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Meta.Cutscene
{
    public class EndCutscene: MonoBehaviour
    {
        [SerializeField] private Image[] _images; // Array of Image elements
        [SerializeField] private Button _hideCutscene;
        
        public event UnityAction OnGameEnd;
        
        private Sequence _sequence;
        private List<float> _durations;
        private AudioManager _audioManager;

        public void Initialize(AudioManager audioManager)
        {
            _audioManager = audioManager;
            Debug.Log("EndCutscene initialized");
            _durations = new List<float>()
            {
                4f, 3f, 2f, 3f, 4f
            };
            foreach (var image in _images)
            {
                var color = image.color;
                color.a = 0f;
                image.color = color;
            }
            _hideCutscene.onClick.AddListener(HideEndCutscene);
        }

        public void ShowEndCutscene()
        {
            _audioManager.PlayOnceButShutAll(AudioNames.EndCutscene);
            gameObject.SetActive(true);
           _sequence = DOTween.Sequence();
           _sequence.AppendInterval(2f);
           for (var i = 0; i < _images.Length; i++)
           {
               _sequence.Append(_images[i].DOFade(1, 1f));
               _sequence.AppendInterval(_durations[i]);
               _sequence.Append(_images[i].DOFade(0, 1f));
               _sequence.AppendInterval(0.5f);
           }

           _sequence.AppendCallback(() => { this.gameObject.SetActive(false); });
           _sequence.AppendCallback(() => { OnGameEnd?.Invoke(); });
           _sequence.Play();
        }

        public void HideEndCutscene()
        {
            if (_sequence != null && _sequence.IsPlaying())
            {
                _sequence.Kill();
            }
            gameObject.SetActive(false);
        }
    }
}
