using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;

    [SerializeField] private AnimatorWorkOptimizer animatorWorkOptimizer;

    public float transitionTime = 1;

    public static SceneLoader Instance;

    private void Awake() => Instance = this;

    public void LoadScene(int levelIndex) => StartCoroutine(LoadingScene(levelIndex));

    public void LoadScene(string sceneName) => StartCoroutine(LoadingScene(sceneName));

    private IEnumerator LoadingScene(int levelIndex)
    {
        animatorWorkOptimizer.animator.enabled = true;
        
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    private IEnumerator LoadingScene(string sceneName)
    {
        animatorWorkOptimizer.animator.enabled = true;
        
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}