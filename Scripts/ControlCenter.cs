using System.Collections;
using UnityEngine;
using WIS;

public class ControlCenter : MonoBehaviour
{
        [SerializeField] private GameObject DebuggerPanel, AdditionalPanel;

        [SerializeField] private float timeToShowCompilation = 0.3f;
        [SerializeField] private float timeCloseWindow = 0.25f;

        [SerializeField] private Animator additionalpanelAnimator;
        [SerializeField] private AnimatorWorkOptimizer terminalAnimOptimizator;

        private bool _isAddionalPanelActive;
        private bool _isFullModeActive = true;

        private WinspireInteractionSystem _wis;

        private void Start() => _wis = WinspireInteractionSystem.Instance;

        public void OnCompilationButtonClick() => StartCompilation(timeToShowCompilation);

        public void OnFastCompilationButtonClick() => StartCompilation(0);

        public void OnStopCompilationButtonClick() => _wis.StopCompilation();

        public void OnDebuggerButtonClick() => DebuggerPanel.SetActive(true);

        public void OnBackForDebuggerButtonClick() => StartCoroutine(CloseDebuggerWindow());

        public void OnCleanTerminalButtonClick() => _wis.CleanTerminal();

        public void OnFullModeTerminalButtonClick()
        {
            if (!_isFullModeActive)
            {
                terminalAnimOptimizator.StartAnimation("OpenTerminal");
                terminalAnimOptimizator.StartDisablingAnimator();
                _isFullModeActive = true;
            }
            else
            {
                if (_isAddionalPanelActive) OnAdditionalButtonClick();
                
                terminalAnimOptimizator.StartAnimation("CloseTerminal");
                terminalAnimOptimizator.StartDisablingAnimator();
                _isFullModeActive = false;
            }
        }

        public void OnAdditionalButtonClick() 
        {
            if (!_isFullModeActive) return;
            
            if (!_isAddionalPanelActive)
            {
                AdditionalPanel.SetActive(true);
                _isAddionalPanelActive = true;
            }
            else
            {
                StartCoroutine(CloseAdditionalWindow());
            }
        }

        private IEnumerator CloseAdditionalWindow()
        {
            additionalpanelAnimator.Play("AdditionalPanelDisappear");
            yield return new WaitForSeconds(timeCloseWindow);
            AdditionalPanel.SetActive(false);
            _isAddionalPanelActive = false;
        }

        private IEnumerator CloseDebuggerWindow()
        {
            yield return new WaitForSeconds(timeCloseWindow);
            DebuggerPanel.SetActive(false);
        }

        private void StartCompilation(float timeToShowCompilation)
        {
            _wis.Compile(CodeDrawer.Instance.originalCode, timeToShowCompilation);
        }
}
