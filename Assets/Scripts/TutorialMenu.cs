using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    public static bool TutorialIsUP = false;

    public GameObject tutorialMenuUI;

    public GameObject tutorialButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (TutorialIsUP)
            {
                ResumeTutorial();
            }
            else
            {
                PauseTutorial();
            }
        }
    }

    public void ResumeTutorial()
    {
        tutorialMenuUI.SetActive(false);
        Time.timeScale = 1f;
        TutorialIsUP = false;
    }

    public void PauseTutorial()
    {
        tutorialMenuUI.SetActive(true);
        Time.timeScale = 0f;
        TutorialIsUP = true;
    }
}

