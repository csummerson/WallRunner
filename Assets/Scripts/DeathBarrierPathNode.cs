using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBarrierPathNode : MonoBehaviour
{
    public static List<DeathBarrierPathNode> _path;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _path = new List<DeathBarrierPathNode>();
    }

    void Awake()
    {
        if (_path == null)
        {
            _path = new List<DeathBarrierPathNode>();
        }

        _path.Add(this);
    }
}