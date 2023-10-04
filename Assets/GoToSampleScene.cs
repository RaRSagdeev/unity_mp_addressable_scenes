using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSampleScene : MonoBehaviour
{
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (NetworkManager.Singleton.IsClient)
        {
            if (GUILayout.Button("Go back to sample scene"))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        GUILayout.EndArea();
    }

}
