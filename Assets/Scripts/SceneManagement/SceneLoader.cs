using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            yield return LoadScene(Scenes.Loader);
            yield return LoadScene(sceneName);

            var sceneEntryPoint = FindFirstObjectByType<EntryPoint>();
            sceneEntryPoint.Run(enterParams);
            
            _loadingScreen.SetActive(false);
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}