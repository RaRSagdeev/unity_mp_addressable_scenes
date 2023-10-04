#nullable enable

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{
    private static AsyncSceneLoader _instance;
    private AsyncOperationHandle<SceneInstance>? _currentSceneHandle;

    static string EmptyScene = "Assets/Scenes/VeryEmptyScene.unity";

    public static AsyncSceneLoader Instance
    {

        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AsyncSceneLoader>();
            }

//#if UNITY_EDITOR
//            // в редакторе можем запускать любую сцену, на которой может не быть инстанса AsyncSceneLoader
//            if (_instance == null)
//            {
//                _instance = GameObject.Instantiate(Resources.Load<AsyncSceneLoader>("Prefabs/Utils/DevelopmentAsyncSceneLoader"));
//            }
//#endif
            return _instance;
        }
    }

    public event Action SceneLoadingStarted;
    public event Action SceneLoadingCompleted;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _instance = this;
    }

    public void LoadScene(string scenePath)
    {
        StartCoroutine(LoadSceneAsync(scenePath));
    }

    // работает так:
    // загружаем пустую сцену и делаем её активной
    // текущую неактивную сцену выгружаем
    // загружаем новую сцену и делаем её активной
    // выгружаем пустую сцену
    private IEnumerator LoadSceneAsync(string scenePath)
    {
        Debug.Log($"loading {scenePath}");
        SceneLoadingStarted?.Invoke();
        var currentScene = SceneManager.GetActiveScene();


        Debug.Log($"current scene is {currentScene.path}");
        yield return new WaitForSecondsRealtime(0.3f); // для красоты

        var asyncOp = SceneManager.LoadSceneAsync(EmptyScene, LoadSceneMode.Additive);
        yield return new WaitUntil(() => asyncOp.isDone);

        SceneManager.SetActiveScene(SceneManager.GetSceneByPath(EmptyScene));
        Debug.Log($"loaded empty scene ");

        if (_currentSceneHandle == null)
        {

            Debug.Log($"unloading {currentScene.path} from scene manager");
            var unloading = SceneManager.UnloadSceneAsync(currentScene);
            yield return new WaitUntil(() => unloading.isDone);
        }
        else
        {

            Debug.Log($"unloading {_currentSceneHandle.Value.Result.Scene.path} from scene manager");
            var unloading = Addressables.UnloadSceneAsync(_currentSceneHandle.Value);
            yield return new WaitUntil(() => unloading.IsDone);
        }

        Debug.Log($"loading {scenePath} as addressable");

#if UNITY_EDITOR
        Debug.Log($"scene deps: {String.Join("\n", UnityEditor.AssetDatabase.GetDependencies(scenePath))}");
#endif

        _currentSceneHandle = Addressables.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
        yield return new WaitUntil(() =>
        {
            var c = _currentSceneHandle.Value;
            return _currentSceneHandle.Value.IsDone;
        });
        Debug.Log($"addressable scene loaded with status {_currentSceneHandle.Value.Status}");

        SceneManager.SetActiveScene(_currentSceneHandle.Value.Result.Scene);
        Debug.Log($"set active scene to {_currentSceneHandle.Value.Result.Scene.path}");

        asyncOp = SceneManager.UnloadSceneAsync(EmptyScene);
        yield return new WaitUntil(() => asyncOp.isDone);

        Debug.Log($"unloaded empty stub scene, DONE");

        SceneLoadingCompleted?.Invoke();
    }
}
