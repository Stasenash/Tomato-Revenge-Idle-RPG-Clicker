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
            StartCoroutine(LoadAndStart(enterParams, "MetaScene"));
        }

        public void LoadGameplayScene(SceneEnterParams enterParams = null)
        {
            StartCoroutine(LoadAndStart(enterParams, "LevelScene"));
        }
        
        private IEnumerator LoadAndStart(SceneEnterParams enterParams, string sceneType)
        {
            _loadingScreen.SetActive(true);

            yield return LoadScene(Scenes.Loader);
            if(sceneType == "LevelScene")
                yield return LoadScene(Scenes.LevelScene);
            else if (sceneType == "MetaScene")
                yield return LoadScene(Scenes.MetaScene);

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