using UnityEngine;
using TMPro;

public class LogicScript : MonoBehaviour
{
    /*
    Title: The Unity Tutorial For Complete Beginners
    Author: Game Maker's Toolkit
    Date: 21 May 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=XtQMytORBmM
    */

    [Header("values")]
    public int playerScore;

    [Header("ui")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScore;

    private void Start()
    {
        highScore.text = PlayerPrefs.GetInt("highscore", 0).ToString();
    }

    //the below is used for testing within unity - it creates a "button" that allows you to run the function while the game is running - similar to the debug dot log but nor really
    [ContextMenu("increase Score")]
    //"public" in public void means it can now be accessed in other scripts
    public void addScore(int scoreToAdd)//scoreToAdd makes it so that in any other script using this score function, they can add different ammounts, i.e. if you want the player to get 5 points rather than 1 after the 50th pipe?
    {
        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();

        //highscore using player prefs
        if (playerScore > PlayerPrefs.GetInt("highscore", 0))
        {
            //FindAnyObjectByType<AudioManager>().Play("highScore");
            PlayerPrefs.SetInt("highscore", playerScore);
            highScore.text = playerScore.ToString();
        }
    }

    [ContextMenu("ResetScore")]
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("highscore");
        highScore.text = "0";//this is to have it update automatically rather than when the program is reopened
    }
}
