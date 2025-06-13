using System.Collections;
using TMPro;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    [Header("player")]
    public GameObject player;
    public GameObject cam;

    [Header("animation/s")]
    public Animator anim;
    bool intro;

    [Header("values")]
    public float exitAnimTime;

    [Header("text")]
    public TextMeshProUGUI dialogueText;
    public GameObject introUi;
    public TextMeshProUGUI pressToContinue;
    public GameObject skipPrompt;

    [Header("level title")]
    public TextMeshProUGUI titleTextUi;
    [TextArea(2,2)]public string titleText;

    [Header("dialogue")]
    public float dialogueSpeed;
    public string npcName;
    [TextArea(2, 4)] public string[] sentences;
    private int index = 0;//tracking the sentences

    private bool isDoneTalking = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        //FindAnyObjectByType<AudioManager>().Play("background");

        pressToContinue.text = "press <#E0B300>[space]</color> to continue...".ToString();
        skipPrompt.GetComponent<TextMeshProUGUI>().text = "press the <#E0B300>[f]</color> key to skip intro.".ToString();

        if (titleTextUi != null)
        {
            titleTextUi.text = titleText.ToString();
        }

        FindAnyObjectByType<AudioManager>().Play("level complete");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NextSentence();

        introUi.SetActive(true);

        intro = true;
    }

    // Update is called once per frame
    void Update()
    {
        player.GetComponent<PlayerControllerScript>().enabled = false;
        player.GetComponent<PlayerShootingScript>().enabled = false;
        cam.GetComponent<CameraFollowScript>().enabled = false;

        if (Input.GetKeyDown(KeyCode.Space) && isDoneTalking)
        {
            dialogueText.text = "<#FFFFFF><size=150%><u>" + npcName + "</size></u><size=100%>" + "<br>" + "<br>" + "";
            NextSentence();
        }

        if (Input.GetKeyDown(KeyCode.F) && isDoneTalking)
        {
            StartCoroutine(DisableUi());
        }

        anim.SetBool("intro", intro);
    }

    void NextSentence()
    {
        if (index <= sentences.Length - 1)
        {
            dialogueText.text = "<#FFFFFF><size=150%><u>" + npcName + "</size></u><size=100%>" + "<br>" + "<br>" + "";
            StartCoroutine(WriteSentence());
        }
        else
        {
            StartCoroutine(DisableUi());
            //Destroy(gameObject, .5f);
        }
    }

    IEnumerator WriteSentence()
    {
        foreach (char character in sentences[index].ToCharArray())
        {
            dialogueText.text += character;
            FindAnyObjectByType<AudioManager>().PlayForButton("typing");
            yield return new WaitForSeconds(dialogueSpeed);

            isDoneTalking = false;
            skipPrompt.SetActive(false);
            pressToContinue.enabled = false;
        }
        isDoneTalking = true;
        skipPrompt.SetActive(true);
        pressToContinue.enabled = true;
        index++;
    }

    IEnumerator DisableUi()
    {
        FindAnyObjectByType<AudioManager>().PlayForButton("click_backward");

        dialogueText.text = "<#FFFFFF><size=150%><u>" + npcName + "</size></u><size=100%>" + "<br>" + "<br>" + "";
        index = 0;

        isDoneTalking = true;

        intro = false;

        FindAnyObjectByType<AudioManager>().Play("background");
        FindAnyObjectByType<AudioManager>().Stop("level complete");

        yield return new WaitForSeconds(exitAnimTime);

        introUi.SetActive(false);
        gameObject.GetComponent<IntroScript>().enabled = false;
        player.GetComponent<PlayerControllerScript>().enabled = true;
        player.GetComponent<PlayerShootingScript>().enabled = true;
        cam.GetComponent<CameraFollowScript>().enabled = true;
    }
}
