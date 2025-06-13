using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinConditionScript : MonoBehaviour
{
    [Header("gameObjects")]
    public GameObject endTrigger;
    GameObject obstruction;

    [Header("text ui")]
    public GameObject levelCompleteTextBox;
    public TextMeshProUGUI enemyCount;
    public GameObject visibleEnemyCount;
    [TextArea(2, 3)]public string completionPrompt = "'leave!'";

    [Header("ui")]
    public Image flashScreen;
    public Image flashScreen2;

    [Header("list/s")]
    public GameObject[] enemies;

    private void Awake()
    {
        visibleEnemyCount = GameObject.Find("visible enemy count");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelCompleteTextBox.SetActive(false);

        visibleEnemyCount.gameObject.GetComponent<TextMeshProUGUI>().text = "kill the " + "<size=125%><b><u>" + enemies.Length.ToString() + "</size></b></u>" + " enemy combatants in the level. kill all of them. no exceptions";
        //endTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        obstruction = GameObject.FindGameObjectWithTag("Obstruction");

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int numberOfEnemies = enemies.Length;
        visibleEnemyCount.gameObject.GetComponent<TextMeshProUGUI>().text = "kill the " + "<size=125%><b><u>" + enemies.Length.ToString() + "</size></b></u>" + " enemy combatants in the level. kill all of them. no exceptions";

        /*
        if (numberOfEnemies == 0)
        {
            enemyCount.text = "<size=100%>everyone's <#ff0000>dead</color>";
        }
        */

        if (numberOfEnemies <= 0)
        {
            visibleEnemyCount.gameObject.GetComponent<TextMeshProUGUI>().text = "<b>all combatants have been <u>killed</u>.</b>";

            StartCoroutine(Flash());

            if (endTrigger != null)
            {
                endTrigger.SetActive(true);
            }

            if (obstruction != null)
            {
                Destroy(obstruction);
            }
        }
    }

    IEnumerator Flash()
    {
        if (flashScreen != null)
        {
            flashScreen.enabled = true;

            yield return new WaitForSeconds(0.1f);

            flashScreen.enabled = false;

            enemyCount.text = completionPrompt.ToString();
            levelCompleteTextBox.SetActive(true);

            FindAnyObjectByType<AudioManager>().Stop("background");
            FindAnyObjectByType<AudioManager>().Play("enemies are dead");

            gameObject.GetComponent<WinConditionScript>().enabled = false;
        }
    }
}
