using System.Collections.Generic;
using DG.Tweening;
using Global.AudioSystem;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Meta.Cutscene
{
    public class IntroCutscene: MonoBehaviour
    {
        [FormerlySerializedAs("images")] [SerializeField] private Image[] _images; // Array of Image elements
        [SerializeField] private Button _hideCutscene;
        private Sequence _sequence;
        private List<float> _durations;
        private AudioManager _audioManager;

        public void Initialize(AudioManager audioManager)
        {
            _audioManager = audioManager;
            Debug.Log("IntroCutscene initialized");
            _durations = new List<float>()
            {
                3f, 2f, 3f, 2f, 2f, 5f, 2f, 5f,
                2f, 2f, 2f, 2f, 2f, 5f, 2f, 5f,
                5f, 2f
            };
            foreach (var image in _images)
            {
                var color = image.color;
                color.a = 0f;
                image.color = color;
            }
            _hideCutscene.onClick.AddListener(HideIntroCutscene);
        }

        public void ShowIntroCutscene()
        { 
            _audioManager.PlayClip(AudioNames.IntroCutscene, true);
            _sequence = DOTween.Sequence();
            _sequence.AppendInterval(2f);
           for (var i = 0; i < _images.Length; i++)
           {
               _sequence.Append(_images[i].DOFade(1, 1f));
               _sequence.AppendInterval(_durations[i]);
               _sequence.Append(_images[i].DOFade(0, 1f));
               _sequence.AppendInterval(0.5f);
           }

           _sequence.AppendCallback(() => { this.gameObject.SetActive(false);});
           _sequence.AppendCallback(() => { _audioManager.PlayClip(AudioNames.BackgroundMeta);});
           _sequence.Play();
        }

        public void HideIntroCutscene()
        {
            _audioManager.PlayClip(AudioNames.BackgroundMeta);
            if (_sequence != null && _sequence.IsPlaying())
            {
                _sequence.Kill();
            }
            gameObject.SetActive(false);
        }
    }
}
