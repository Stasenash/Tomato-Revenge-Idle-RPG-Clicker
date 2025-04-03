using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using Game.Configs;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        
        [SerializeField] private GameObject _loadingScreen;
        
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
            yield return new WaitForSeconds(0.5f);
            var circular = GameObject.FindGameObjectWithTag(TAGS.CIRCULAR_TAG).transform;
            if (circular)
            {
                circular
                    .DORotate(new Vector3(0, 0, -360), 1.0f, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Restart);
            }

            yield return LoadScene(Scenes.Loader);
            yield return LoadScene(sceneName);

            RemoveAllTweens();
            
            var sceneEntryPoint = FindFirstObjectByType<EntryPoint>();
            sceneEntryPoint.Run(enterParams);
            _loadingScreen.SetActive(false);
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }

        
        private void UnloadCurrentScene()
        {
            RemoveAllTweens();
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
        
        private void RemoveAllTweens()
        {
            // Отменяем все активные твины
            DOTween.KillAll();

            // Очищаем все твины
            DOTween.Clear();

            // Удаляем компонент DOTween, если он есть на GameObject
            var dotweenComponent = FindObjectOfType<DOTweenComponent>();
            if (dotweenComponent != null)
            {
                Destroy(dotweenComponent);
            }
        }
    }
}