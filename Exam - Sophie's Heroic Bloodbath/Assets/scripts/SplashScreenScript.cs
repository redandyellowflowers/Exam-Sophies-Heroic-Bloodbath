using EZCameraShake;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SplashScreenScript : MonoBehaviour
{
    [Header("player")]
    public GameObject player;

    [Header("gameObjects")]
    public IntroScript intro;
    public GameObject pauseMenu_gameManager;

    [Header("ui")]
    public Image flashScreen;
    public Image flashScreen2;
    public Image flashScreen3;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");

        intro = gameObject.GetComponent<IntroScript>();
        pauseMenu_gameManager = GameObject.Find("game manager");

        intro.introUi.SetActive(false);
        intro.enabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(Intro());
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        if (flashScreen != null)
        {
            pauseMenu_gameManager.GetComponent<PauseMenuScript>().enabled = true;

            FindAnyObjectByType<AudioManager>().Play("shoot");
            flashScreen.enabled = true;
            //flashScreen.color = Color.red;
            CameraShaker.Instance.ShakeOnce(6f, 1f, .1f, .4f);

            yield return new WaitForSeconds(0.1f);

            flashScreen.enabled = false;
            StartCoroutine(Flash2());
        }
    }

    IEnumerator Flash2()
    {
        if (flashScreen != null)
        {
            FindAnyObjectByType<AudioManager>().Play("shoot");
            flashScreen2.enabled = true;
            CameraShaker.Instance.ShakeOnce(6f, 1f, .1f, .4f);

            yield return new WaitForSeconds(0.1f);

            flashScreen2.enabled = false;
            StartCoroutine(Flash3());

        }
    }

    IEnumerator Flash3()
    {
        if (flashScreen != null)
        {
            FindAnyObjectByType<AudioManager>().Play("shoot");
            flashScreen3.enabled = true;
            CameraShaker.Instance.ShakeOnce(6f, 1f, .1f, .4f);

            yield return new WaitForSeconds(0.15f);

            //FindAnyObjectByType<AudioManager>().Play("level complete");
            flashScreen3.enabled = false;

            pauseMenu_gameManager.GetComponent<PauseMenuScript>().enabled = true;

            intro.enabled = true;
            intro.introUi.SetActive(true);
        }
    }
}
