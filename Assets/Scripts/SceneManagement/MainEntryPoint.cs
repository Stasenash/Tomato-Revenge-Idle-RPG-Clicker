using Game.Configs;
using Global.SaveSystem;
using UnityEngine;

namespace SceneManagement
{
    public class MainEntryPoint : MonoBehaviour
    {
        public void Awake()
        {
            if (GameObject.FindGameObjectWithTag(TAGS.SCENE_LOADER_TAG)) return;
            
            var sceneLoaderPrefab = Resources.Load<SceneLoader>("SceneLoader");
            var sceneLoader = Instantiate(sceneLoaderPrefab);
            DontDestroyOnLoad(sceneLoader);
            
            var saveSystem = new GameObject().AddComponent<SaveSystem>();
            saveSystem.Initialize();
            DontDestroyOnLoad(saveSystem);

            sceneLoader.LoadMetaScene();
        }
    }
}