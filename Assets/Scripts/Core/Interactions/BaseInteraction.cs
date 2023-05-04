using System;
using UnityEngine;
using Zenject;

namespace Core.Interactions
{
    public class BaseInteraction : MonoBehaviour
    {
        [Inject] protected SessionManager SessionManager;

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}