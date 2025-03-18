using UnityEngine;

namespace SceneManagement
{
    public abstract class EntryPoint : MonoBehaviour
    {
        public abstract void Run(SceneEnterParams enterParams);
    }
}