using System.Collections;
using UnityEngine;

namespace Menu
{
    public class LevelsPanelScript : MonoBehaviour
    {
        [SerializeField] private GameObject[] levelTexts;
        [SerializeField] private GameObject[] levelCompleteIcons;

        private const byte CountOfLevels = 14;
        
        private IEnumerator Start()
        {
            yield return null;

            for (byte i = 1; i <= CountOfLevels; i++)
            {
                if (PlayerPrefs.GetInt("isLevel" + i + "Completed") == 1)
                {
                    levelTexts[i - 1].SetActive(false);
                    levelCompleteIcons[i - 1].SetActive(true);
                }
            }
        }

        public void OnLevelChooseButtonClick(string scene)
        {
            int adChance = Random.Range(0, 5);
            if (scene != "Level1" && scene != "Level2" && adChance == 0) InterAd.Instance.ShowAd();
            SceneLoader.Instance.LoadScene(scene);
        }
    }
}