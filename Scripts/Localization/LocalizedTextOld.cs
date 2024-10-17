using UnityEngine;
using UnityEngine.UI;

public class LocalizedTextOld : MonoBehaviour
{
    [SerializeField] private string key;

    private LocalizationManager localizationManager;
    private Text text;

    private void Start()
    {
        localizationManager = LocalizationManager.Instance;

        text = GetComponent<Text>();

        localizationManager.OnLanguageChanged += UpdateText;

        UpdateText();
    }

    private void OnDestroy() => localizationManager.OnLanguageChanged -= UpdateText;

    virtual protected void UpdateText()
    {
        if (gameObject == null) return;

        text.text = localizationManager.GetLocalizedValue(key);
    }
}