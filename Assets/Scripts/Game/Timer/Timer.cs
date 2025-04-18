using Global;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Timer
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        private float _maxTime;
        private float _currentTime;
        private bool _isPlaying;
        public event UnityAction OnTimerEnd;
    
        public void Initialize(float maxTime)
        {
            _maxTime = maxTime;
            _currentTime = maxTime;
            Play();
        }

        private void FixedUpdate()
        {
            if (!_isPlaying) return;
            var deltaTime = Time.fixedDeltaTime;
            if (deltaTime >= _currentTime)
            {
                OnTimerEnd?.Invoke();
                Stop();
                return;
            }
            _currentTime -= deltaTime;
            _timerText.text = _currentTime.ToString("00.00");
        }

        public void Play()
        {
            _isPlaying = true;
        }

        public void Pause()
        {
            _isPlaying = false;
        }

        public void Resume()
        {
        
        }

        public void SetActive(bool active)
        {
            _timerText.gameObject.SetActive(active);
        }
        
        public void Stop()
        {
            _isPlaying = false;
            OnTimerEnd = null;
        }
    }
}
