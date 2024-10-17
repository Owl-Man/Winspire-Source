using DataBases;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    [SerializeField] private AudioSource softButtonSound;

    [HideInInspector] public bool isSoundActivated;

    public static SoundScript instance;

    private void Awake()
    {
        instance = this;
        Initialize();
    }
    
    public void Initialize()
    {
        if (PlayerPrefs.GetInt(PPKeys.IS_SOUND_ACTIVATED_KEY, 1) == 1) isSoundActivated = true;
        else isSoundActivated = false;
    }
    
    public void OnButtonClick()
    {
        if (isSoundActivated) softButtonSound.Play();
    }
}