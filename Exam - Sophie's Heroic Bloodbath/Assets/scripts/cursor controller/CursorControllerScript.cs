using UnityEngine;

public class CursorControllerScript : MonoBehaviour
{
    /*
    Title: Custom Cursor with Input System - Unity Tutorial
    Author: samyam
    Date: 17 May 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=PTL19bXCsNU&t=7s
    */

    public bool isInGame = false;

    public Texture2D cursor;
    public Texture2D cursorClicked;

    private Cursorcontroller controls;

    private CursorControllerScript cursorControllerScript;

    private void Awake()
    {
        controls = new Cursorcontroller();

        ChangeCursor(cursor);
        //Cursor.lockState = CursorLockMode.Confined;

        /*
        if (cursorControllerScript == null)
        {
            cursorControllerScript = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);//as a new scene is loaded, the gameobject this script is attached to will follow, and if the object already exists, then this (now duplicate) is deleted
        }
        */
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controls.mouse.click.started += _ => StartedClick();
        controls.mouse.click.performed += _ => EndedClick();
    }

    private void StartedClick()
    {
        ChangeCursor(cursorClicked);

        if (isInGame != true)
        {
            FindAnyObjectByType<AudioManager>().Play("typing");//only plays when clicking in the start menu
        }
    }

    private void EndedClick()
    {
        ChangeCursor(cursor);
    }

    private void ChangeCursor(Texture2D cursorType)
    {
        Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 4);//the cursor click point is at the centre top of the cursor sprite used
        Cursor.SetCursor(cursorType, hotspot, CursorMode.Auto );

        if (isInGame)
        {
            Vector2 inGameHotspot = new Vector2(cursorType.width / 2, cursorType.height / 3);//while in game, the cursor (crosshair) click point is centred
            Cursor.SetCursor(cursorType, hotspot, CursorMode.Auto);
        }
    }
}
