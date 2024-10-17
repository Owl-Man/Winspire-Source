using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    [SerializeField] private string key;

    private LocalizationManager localizationManager;
    private TMP_Text text;

    private void Start()
    {
        localizationManager = LocalizationManager.Instance;

        text = GetComponent<TMP_Text>();

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