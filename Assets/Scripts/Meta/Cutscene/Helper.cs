using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.Cutscene
{
    public class Helper : MonoBehaviour
    {
        [SerializeField] private List<Image> _frames;
        [SerializeField] private Button _buttonNext;
        [SerializeField] private Button _buttonPrevious;
        
        private int _frameIndex;
        
        public void Initialize()
        {
            gameObject.SetActive(true);
            foreach (var frame in _frames)
            {
                frame.gameObject.SetActive(false);
            }
            _frames[0].gameObject.SetActive(true);
            _frameIndex = 0;
            _buttonNext.onClick.AddListener(NextFrame);
            _buttonPrevious.onClick.AddListener(PreviousFrame);
            _buttonPrevious.gameObject.SetActive(false);
        }

        private void NextFrame()
        {
            _frames[_frameIndex].gameObject.SetActive(false);
            _frameIndex++;
            if (_frameIndex >= _frames.Count)
            {
                gameObject.SetActive(false);
                return;
            }
            _buttonPrevious.gameObject.SetActive(true);
            _frames[_frameIndex].gameObject.SetActive(true);
        }
        
        private void PreviousFrame()
        {
            _frames[_frameIndex].gameObject.SetActive(false);
            _frameIndex--;
            if (_frameIndex == 0)
            {
                _buttonPrevious.gameObject.SetActive(false);
            }

            if (_frameIndex < 0)
            {
                _frames[0].gameObject.SetActive(true);
            }
            _frames[_frameIndex].gameObject.SetActive(true);
        }
    }
}