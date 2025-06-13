using TMPro;
using UnityEngine;

public class GameClockController : MonoBehaviour
{
    /*
    Title: Create an in-game CLOCK -- (Day / Night -- Unity Tutorial 2024)
    Author: SpeedTutor
    Date: 20 May 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=6J8BbkrgdBY
    */

    [Header("text ui")]
    [SerializeField] private TextMeshProUGUI clockText;

    [Header("values")]
    private float elapsedTime;
    [SerializeField] private float timeScale = 2.0f;
    [SerializeField] private float timeInADay = 86400f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        elapsedTime = 20 * 3600f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime * timeScale;
        elapsedTime %= timeInADay;
        UpdateClockUi();
    }

    void UpdateClockUi()
    {
        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime - hours * 3600f) / 60f);
        int seconds = Mathf.FloorToInt((elapsedTime - hours * 3600f) - (minutes * 60f));

        string clockString = string.Format("{0:00}:{1:00}", hours, minutes);
        clockText.text = clockString;
    }
}
