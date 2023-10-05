using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
    [SerializeField] Button _button;

    private void Awake()
    {
        _button.onClick.AddListener(() => SpawnSomething());
    }

    private void SpawnSomething()
    {
        FindObjectOfType<SpawnTest>().SpawnCube();
    }
}
