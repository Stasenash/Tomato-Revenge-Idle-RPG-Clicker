using System.Collections.Generic;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.Cutscene
{
    public class IntroCutscene: MonoBehaviour
    {
        [SerializeField] private Image[] images; // Array of Image elements
        private Sequence _sequence;
        private List<float> _durations;

        public void Initialize()
        {
            Debug.Log("IntroCutscene initialized");
            _durations = new List<float>()
            {
                3f, 2f, 3f, 2f, 2f, 5f, 2f, 5f,
                2f, 2f, 2f, 2f, 2f, 5f, 2f, 5f,
                5f, 2f
            };
            foreach (var image in images)
            {
                var color = image.color;
                color.a = 0f;
                image.color = color;
            }
        }

        public void ShowIntroCutscene()
        {
           _sequence = DOTween.Sequence();
           _sequence.AppendInterval(2f);
           for (var i = 0; i < images.Length; i++)
           {
               _sequence.Append(images[i].DOFade(1, 1f));
               _sequence.AppendInterval(_durations[i]);
               _sequence.Append(images[i].DOFade(0, 1f));
               _sequence.AppendInterval(0.5f);
           }

           _sequence.AppendCallback(() => { this.gameObject.SetActive(false); });
           _sequence.Play();
        }
    }
}
