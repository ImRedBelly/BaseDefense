﻿using System;
using Core.AI.Characters;
using Zenject;
using UnityEngine;

namespace Core.Interactions
{
    public class BaseInteraction : MonoBehaviour
    {
        [Inject] protected SessionManager SessionManager;

        public virtual void Interaction(Character character)
        {
        } 
        public virtual void OnEnter(Character character)
        {
        }

        public virtual void OnExit()
        {
        }
    }
}