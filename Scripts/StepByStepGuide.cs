using System.Collections;
using DataBases;
using UnityEngine;

public class StepByStepGuide : MonoBehaviour
{
    [SerializeField] private GameObject[] GuidePanels;
    [SerializeField] private bool[] isGuideWithBG;
    
    [SerializeField] private GameObject Background;
    [SerializeField] private GameObject HelpButton;
    
    [SerializeField] private byte level;
    
    private int _currentGuide;

    private IEnumerator Start()
    {
        if (GuidePanels.Length != 0)
        {
            yield return null;

            if (PlayerPrefs.GetInt(PPKeys.IS_HELP_BUTTON_ACTIVATED_KEY, 1) != 1)
            {
                HelpButton.SetActive(false);
            }

            if (PlayerPrefs.GetInt(PPKeys.IS_GUIDE_SHOWING_ONLY_FIRST_TIME_KEY, 1) == 0
                || PlayerPrefs.GetInt("is" + level + "LevelGuideShowed", 0) == 0)
            {
                OnOpenHelpButtonClick();
                PlayerPrefs.SetInt("is" + level + "LevelGuideShowed", 1);
            }
        }
    }

    public void OnNextGuideButtonClick()
    {
        GuidePanels[_currentGuide].SetActive(false);

        if (_currentGuide + 1 >= GuidePanels.Length)
        {
            Background.SetActive(false);
            return;
        }

        _currentGuide++;
        GuidePanels[_currentGuide].SetActive(true);
        
        if (isGuideWithBG[_currentGuide]) Background.SetActive(true);
        else Background.SetActive(false);
    }

    public void OnOpenHelpButtonClick()
    {
        _currentGuide = 0;
            
        GuidePanels[_currentGuide].SetActive(true);
        if (isGuideWithBG[_currentGuide]) Background.SetActive(true);
    }
}