using System;
using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.UI.Dialogs
{
    public class RestartDialog : MonoBehaviour
    {
        [SerializeField] private Button buttonCloseDialog;
        [SerializeField] private Button buttonRestartGame;

        [Inject] private SessionManager _sessionManager;

        private void Start()
        {
            GetComponent<RectTransform>().sizeDelta = Vector2.zero;

            buttonCloseDialog.onClick.AddListener(CloseDialog);
            buttonRestartGame.onClick.AddListener(RestartGame);
        }

        private void OnEnable() => Time.timeScale = 0;
        private void OnDisable() => Time.timeScale = 1;

        private void CloseDialog()
        {
            LeanPool.Despawn(this);
        }

        private void RestartGame()
        {
            _sessionManager.RestartGame();
            CloseDialog();
        }
    }
}