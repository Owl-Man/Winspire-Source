using System.Collections;
using UnityEngine;

public class AnimatorWorkOptimizer : MonoBehaviour
{
    [SerializeField] private float enableTime;
    
    public Animator animator;

    private bool _isAnimatorDisabling;

    private void Start() => StartCoroutine(DisableAnimator());

    private void OnEnable() => StartCoroutine(DisableAnimator());

    private void OnDisable() => animator.enabled = true;
    
    public void StartAnimation(string animationName)
    {
        animator.enabled = false;
        StopCoroutine(DisableAnimator());
        animator.enabled = true;
        animator.Play(animationName);
    }

    public void StartDisablingAnimator() => StartCoroutine(DisableAnimator());

    private IEnumerator DisableAnimator()
    {
        if (!_isAnimatorDisabling)
        {
            _isAnimatorDisabling = true;
            yield return new WaitForSeconds(enableTime);
            animator.enabled = false;
            _isAnimatorDisabling = false;
        }
    }
}