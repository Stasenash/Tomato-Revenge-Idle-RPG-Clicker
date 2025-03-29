using System.Collections;
using DG.Tweening;
using Game.Configs;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        
        [SerializeField] private GameObject _loadingScreen;
        private Tween _tween;
        
        public void LoadMetaScene(SceneEnterParams enterParams = null)
        {
            StartCoroutine(LoadAndStart(enterParams, Scenes.MetaScene));
        }

        public void LoadGameplayScene(SceneEnterParams enterParams = null)
        {
            StartCoroutine(LoadAndStart(enterParams, Scenes.LevelScene));
        }
        
        private IEnumerator LoadAndStart(SceneEnterParams enterParams, string sceneName)
        {
            _loadingScreen.SetActive(true);

            _tween = GameObject.FindGameObjectWithTag(TAGS.CIRCULAR_TAG).transform
                .DORotate(new Vector3(0, 0, -360), 1.0f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
            
            yield return LoadScene(Scenes.Loader);
            yield return LoadScene(sceneName);
            
            var sceneEntryPoint = FindFirstObjectByType<EntryPoint>();
            sceneEntryPoint.Run(enterParams);
            if (_tween != null && _tween.IsActive())
            {
                _tween.Kill(); // Останавливаем твин
            }
            _loadingScreen.SetActive(false);
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}