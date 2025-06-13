using UnityEngine;

public class ImageSequenceScript : MonoBehaviour
{
    /*
    Title: How to change UI image Array with button in Unity | Unity Tutorial
    Author: Grafik Games
    Date: 14 May 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=UQ7FjIwbJsA&list=WL&index=3
    */

    [Header("gameObjects")]
    public GameObject[] background;

    [Header("previous buttons")]
    public GameObject previousButton;
    public GameObject previousButtonOFF;

    [Header("next buttons")]
    public GameObject nextButton;
    public GameObject nextButtonOFF;

    [Header("continue buttons")]
    public GameObject nextLevelButton;
    public GameObject nextLevelButtonOFF;

    private int indexAmount;
    private int index;

    private void Awake()
    {
        indexAmount = background.Length;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (index >= indexAmount)
        {
            index = indexAmount;//resets the index when it has been completed
        }

        if (index < 0)
        {
            index = 0;//sets the index to 0, thus ensuring it never enters the negative
        }

        if (index == 1)//if it is the image after the first, the "previous" button is enabled
        {
            previousButton.SetActive(true);
            previousButtonOFF.SetActive(false);
        }
        else if (index == 0)//if it is the first image, then the "previous" button is disabled
        {
            previousButton.SetActive(false);
            previousButtonOFF.SetActive(true);
        }

        if (index == indexAmount - 1)//if it is the last image in the sequence, then the next button is disabled, and the continue button is enabled
        {
            if (nextLevelButton != null && nextLevelButtonOFF != null)
            {
                nextLevelButton.SetActive(true);
                nextLevelButtonOFF.SetActive(false);
            }

            nextButton.SetActive(false);//disables the "next" button as there are no more images in index
            nextButtonOFF.SetActive(true);
        }
        else if (index < indexAmount)
        {
            if (nextLevelButton != null && nextLevelButtonOFF != null)//if there are still images in index, the "continue" button is disabled
            {
                nextLevelButtonOFF.SetActive(true);
                nextLevelButton.SetActive(false);
            }

            nextButton.SetActive(true);
            nextButtonOFF.SetActive(false);
        }

        if (index == 0)
        {
            background[0].gameObject.SetActive(true);//if the index is at 0, return to the first image
        }
    }

    public void Next()
    {
        index += 1;//adds 1 to the index

        for (int i = 0; i < background.Length; i++)//if [i] is less than the max number of images in the index, add 1 to it
        {
            background[i].gameObject.SetActive(false);//turns off current image, and enables the next
            background[index].gameObject.SetActive(true);//enables the next in the index, after 1 is added to the current
        }
    }

    public void ResetSequence()
    {
        index = 0;
    }

    public void Previous()
    {
        index -= 1;//does the same as the above ("next()") but in reverse, this time, subtracting 1, thus returning to the previous image

        for (int i = 0; i < background.Length; i++)
        {
            background[i].gameObject.SetActive(false);
            background[index].gameObject.SetActive(true);
        }
    }
}
