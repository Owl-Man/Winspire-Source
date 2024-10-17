using System.Collections;
using UnityEngine;
using ScriptableObjects;
using TMPro;

public class GuideScript : MonoBehaviour
{
    [SerializeField] private GuidePage[] guidePage_RU, guidePage_EN, guidePage_TR, guidePage_DE;

    private GuidePage[] guidePage;

    [SerializeField] private TMP_Text PageName, PageContent, PageNumber;

    private int _currentPage;

    private void Start() => GuidePagesLocalize();

    public void GuidePagesLocalize()
    {
        if (LocalizationManager.Instance.CurrentLanguage == "ru_RU") guidePage = guidePage_RU;
        else if (LocalizationManager.Instance.CurrentLanguage == "tr_TR") guidePage = guidePage_TR;
        else if (LocalizationManager.Instance.CurrentLanguage == "de_DE") guidePage = guidePage_DE;
        else if (LocalizationManager.Instance.CurrentLanguage == "en_US") guidePage = guidePage_EN;

        SyncPageValues();
    }

    private void SyncPageValues()
    {
        PageName.text = guidePage[_currentPage].name;
        PageContent.text = guidePage[_currentPage].content;
        if (PageNumber != null) PageNumber.text = (_currentPage + 1).ToString();
    }

    public void OnNextPageButtonClick()
    {
        if (_currentPage + 1 < guidePage.Length)
        {
            _currentPage++;
            SyncPageValues();
        }
    }

    public void OnBackPageButtonClick()
    {
        if (_currentPage != 0)
        {
            _currentPage--;
            SyncPageValues();
        }
    }
    public void OnGuideButtonClick() => gameObject.SetActive(true);

    public void OnBackGuideButtonClick() => StartCoroutine(CloseGuidePanel());

    public IEnumerator CloseGuidePanel()
    {
        yield return new WaitForSeconds(0.35f);
        gameObject.SetActive(false);
    }
}