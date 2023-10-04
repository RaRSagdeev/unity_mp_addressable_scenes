using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EverySceneLoader : MonoBehaviour
{
    bool _loaded;

    // for some reason doesn't work in "start"
    void Update()
    {
        if (NetworkManager.Singleton.IsServer && !_loaded)
        {
            _loaded = true;
            for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                var newScene = SceneManager.GetSceneByBuildIndex(i);
                if (!newScene.isLoaded)
                {
                    var name = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
                    NetworkManager.Singleton.SceneManager.LoadScene(name, LoadSceneMode.Additive);
                }
            }

        }
    }
}
