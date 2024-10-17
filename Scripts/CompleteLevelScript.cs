using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteLevelScript : MonoBehaviour
{
    [SerializeField] private GameObject rewardShow, unavailableRewardShow;
    
    [SerializeField] private Text RewardText;
    [SerializeField] private int level;

    public int Reward;

    private IEnumerator Start()
    {
        yield return null;
        
        if (PlayerPrefs.GetInt("isLevel" + level + "Completed") == 0)
        {
            PlayerPrefs.SetInt("Microchips", PlayerPrefs.GetInt("Microchips") + Reward);
            PlayerPrefs.SetInt("isLevel" + level + "Completed", 1);
        }
        else
        {
            rewardShow.SetActive(false);
            unavailableRewardShow.SetActive(true);
        }
        
        RewardText.text = Reward.ToString();
    }

    public void OnHomeButtonClick()
    {
        int adChance = Random.Range(0, 3);
        if (adChance == 0) InterAd.Instance.ShowAd();

        SceneLoader.Instance.LoadScene("Menu");
    }

    public void OnRestartButtonClick() => SceneLoader.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void OnNextButtonClick()
    {
        int adChance = Random.Range(0, 8);
        if (adChance == 0) InterAd.Instance.ShowAd();

        SceneLoader.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}