using EZCameraShake;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OutOfBoundsScript : MonoBehaviour
{
    [Header("player")]
    public GameObject player;

    [Header("gameObjects")]
    public OutOfBoundsScript outOfBounds;
    public GameObject startingPoint;

    [Header("values")]
    public int detectionRadius = 30;

    [Header("ui")]
    public Image flashScreen;

    [TextArea(2, 4)]
    public string text = "gameover" + "<align=right><#ffffff><br><size=25%><br>you have <#ff0000>died</color>. you cannot through here, not ever.";

    [HideInInspector]
    public float distance;

    Vector2 playerPos;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startingPoint = GameObject.Find("starting point");

        outOfBounds = gameObject.GetComponent<OutOfBoundsScript>();
    }

    public void FixedUpdate()
    {
        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance > detectionRadius && player != null)
            {
                /*
                player.GetComponent<PlayerHealthScript>().TakeDamage(100);
                outOfBounds.enabled = false;

                gameObject.GetComponent<GameoverScript>().text = text;
                */

                StartCoroutine(Flash());

                player.transform.position = startingPoint.transform.position;
            }
        }
    }

    IEnumerator Flash()
    {
        if (flashScreen != null)
        {
            FindAnyObjectByType<AudioManager>().Play("click_error");
            flashScreen.enabled = true;

            player.GetComponent<PlayerControllerScript>().enabled = false;
            player.GetComponent<PlayerShootingScript>().enabled = false;

            CameraShaker.Instance.ShakeOnce(5f, 1f, .1f, .4f);

            yield return new WaitForSeconds(.1f);

            flashScreen.enabled = false;

            player.GetComponent<PlayerControllerScript>().enabled = true;
            player.GetComponent<PlayerShootingScript>().enabled = true;
        }
    }
}
