using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelScript : MonoBehaviour
{
    [Header("(ui elements as) gameObjects")]
    public GameObject interactText;
    public GameObject pauseMenu_gameManager;
    public GameObject levelCompleteUi;
    public GameObject hud;

    [Header("ui")]
    public Image flashScreen;
    public Image flashScreen2;
    public Image flashScreen3;

    //'serializedfield' means that that variable is still private, but is now accessable in the editor >> removed the serialized variable, but still good to know

    public void Awake()
    {
        pauseMenu_gameManager = GameObject.Find("game manager");

        interactText.GetComponent<TextMeshPro>().enabled = false;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
        levelCompleteUi.SetActive(false);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        interactText.GetComponent<TextMeshPro>().enabled = true;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = true;

        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.Space))
        {
            {
                StartCoroutine(Flash());

                //FindAnyObjectByType<AudioManager>().Stop("enemies are dead");

                collision.gameObject.GetComponent<PlayerControllerScript>().enabled = false;
                collision.gameObject.GetComponent<PlayerShootingScript>().enabled = false;
                collision.gameObject.SetActive(false);

                pauseMenu_gameManager.GetComponent<PauseMenuScript>().enabled = false;
            }
        }
    }

    IEnumerator Flash()
    {
        if (flashScreen != null)
        {
            FindAnyObjectByType<AudioManager>().Play("shoot");
            flashScreen.enabled = true;
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

            hud.SetActive(false);
            levelCompleteUi.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (interactText != null)
        {
            interactText.GetComponent<TextMeshPro>().enabled = false;
            interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
        }
    }
}
