using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    [Header("player")]
    public GameObject player;

    [Header("(ui elements as) gameObjects")]
    public GameObject pauseUI;

    private bool gameIsPaused = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            gameObject.GetComponent<PauseMenuScript>().enabled = false;
            FindAnyObjectByType<AudioManager>().Play("enemies are dead");//this is meant to go in the gameover script, if you can find out why the audio bugs out when placed there
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused == false)
        {
            FindAnyObjectByType<AudioManager>().PlayForButton("click_forward");

            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused == true)
        {
            FindAnyObjectByType<AudioManager>().PlayForButton("click_backward");

            ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);

        gameIsPaused = true;


        player.GetComponent<PlayerControllerScript>().enabled = false;
        player.GetComponent<PlayerShootingScript>().enabled = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);

        gameIsPaused = false;

        player.GetComponent<PlayerControllerScript>().enabled = true;
        player.GetComponent<PlayerShootingScript>().enabled = true;
    }
}
