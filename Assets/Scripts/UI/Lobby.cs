using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : NetworkBehaviour
{
    [SerializeField] private Button _start;

    private void OnEnable()
    {
        Debug.Log("start");
        if (isServer)
        {
            Debug.Log("server");
            _start.gameObject.SetActive(true);
        }

        Time.timeScale = 0;

        _start.onClick.AddListener(UnpauseGame);
    }

    private void OnDisable()
    {
        _start.onClick.RemoveListener(UnpauseGame);
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1;
    }
}
