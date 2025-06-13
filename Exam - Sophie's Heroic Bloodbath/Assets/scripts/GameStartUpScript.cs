using System.Net.NetworkInformation;
using UnityEngine;

public class GameStartUpScript : MonoBehaviour
{
    public SceneManagerScript sceneManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;       
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            FindAnyObjectByType<AudioManager>().Play("typing");
            sceneManager.NextLevel();
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
