using Game.Configs;
using Global.SaveSystem;
using UnityEngine;

namespace SceneManagement
{
    public class MainEntryPoint : MonoBehaviour
    {
        public void Awake()
        {
            if (GameObject.FindGameObjectWithTag(TAGS.COMMON_OBJECT_TAG)) return;
            
            var commonObjectPrefab = Resources.Load<CommonObject>("CommonObject");
            var commonObject = Instantiate(commonObjectPrefab);
            DontDestroyOnLoad(commonObject);

            //commonObject.AudioManager.LoadOnce();
            //commonObject.SceneLoader.Initialize(commonObject.AudioManager);
            commonObject.SaveSystem = new();
            commonObject.SceneLoader.LoadMetaScene();
            
            // var saveSystem = new GameObject().AddComponent<SaveSystem>();
            // saveSystem.Initialize();
            // DontDestroyOnLoad(saveSystem);
            //
            // sceneLoader.LoadMetaScene();
        }
    }
}