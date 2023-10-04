using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkCommandLine : MonoBehaviour
{
    private NetworkManager netManager;

    public string AppType
    {
        get
        {
            var args = GetCommandlineArgs();

            if (args.TryGetValue("-mode", out string mode))
            {
                return mode;
            }
            return "";
        }
    }

    void Start()
    {
        netManager = GetComponentInParent<NetworkManager>();

        if (Application.isEditor) return;

        if (AppType == "client")
        {
            NetworkManager.Singleton.StartClient();
        }

        else if (AppType == "server")
        {
            NetworkManager.Singleton.StartServer();
        }
    }

    private Dictionary<string, string> GetCommandlineArgs()
    {
        Dictionary<string, string> argDictionary = new Dictionary<string, string>();

        var args = System.Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; ++i)
        {
            var arg = args[i].ToLower();
            Debug.Log(arg);
            if (arg.StartsWith("-"))
            {
                var value = i < args.Length - 1 ? args[i + 1].ToLower() : null;
                value = (value?.StartsWith("-") ?? false) ? null : value;

                argDictionary.Add(arg, value);
            }
        }
        return argDictionary;
    }
}