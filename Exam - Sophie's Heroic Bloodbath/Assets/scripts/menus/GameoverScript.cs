using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using EZCameraShake;
using UnityEngine.UI;

public class GameoverScript : MonoBehaviour
{
    [Header("player")]
    public GameObject player;

    [Header("(ui elements as) gameObjects")]
    public GameObject gameOverUI;
    public GameObject hud;
    public Image flashScreen;

    [Header("text")]
    public TextMeshProUGUI gameoverText;
    //[TextArea(2, 4)]
    //public string text;

    [Header("volume")]
    //[SerializeField] private Volume volume;
    public GameObject volume;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        gameoverText = gameOverUI.GetComponentInChildren<TextMeshProUGUI>();
        hud = GameObject.Find("heads up display");

        volume = GameObject.Find("Global Volume");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            StartCoroutine(DeathThenUi());
        }
    }

    IEnumerator DeathThenUi()
    {
        FindAnyObjectByType<AudioManager>().Stop("background");
        //FindAnyObjectByType<AudioManager>().Play("enemies are dead");
        Time.timeScale = .2f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        yield return new WaitForSeconds(.3f);

        if (flashScreen != null)
        {
            StartCoroutine(Flash());
        }

        if (volume.GetComponent<Volume>().profile.TryGet(out ColorAdjustments colorAdjust))
        {
            colorAdjust.saturation.value = -100;
        }

        gameOverUI.SetActive(true);
        gameoverText.text = "<size=50%>" + "gameover" + "<br><align=right><size=25%><#FFFFFF>" + " sophie has died. there is no one left to avenge her husband. is there?".ToString();
        //Time.timeScale = 1;
    }

    IEnumerator Flash()
    {
        if (flashScreen != null)
        {
            //FindAnyObjectByType<AudioManager>().Play("click_error");
            flashScreen.enabled = true;
            
            CameraShaker.Instance.ShakeOnce(6f, 1f, .1f, .4f);

            yield return new WaitForSeconds(0.02f);

            flashScreen.enabled = false;

            hud.SetActive(false);

            StopAllCoroutines();
            //Destroy(flashScreen.gameObject);
        }
    }
}
