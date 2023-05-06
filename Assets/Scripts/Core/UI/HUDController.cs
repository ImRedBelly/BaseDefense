using System;
using Lean.Pool;
using Setups;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private Button buttonCallRestartDialog;

        [Inject] private SessionManager _sessionManager;
        [Inject] private PrefabContainer _prefabContainer;

        private void Start()
        {
            buttonCallRestartDialog.onClick.AddListener(CallRestartDialog);
            _sessionManager.Player.OnDestroyCharacter += CallRestartDialog;
        }

        private void OnDisable()
        {
            _sessionManager.Player.OnDestroyCharacter -= CallRestartDialog;
        }

        private void CallRestartDialog()
        {
            LeanPool.Spawn(_prefabContainer.restartDialog, transform);
        }
    }
}