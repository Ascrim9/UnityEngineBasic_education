﻿using RPG.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Controllers;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.UI
{
    public class UIManager : SingletonBase<UIManager> 
    {
        public Dictionary<Type, IUI> uis = new Dictionary<Type, IUI>();
        public LinkedList<IUI> uisShown = new LinkedList<IUI>();

        public void Register(IUI ui)
        {
            if (uis.TryAdd(ui.GetType(), ui) == false)
                throw new Exception($"[UIManager] : Failed to register {ui.GetType()}");
        }

        public bool TryGet<T>(out T ui) where T : IUI
        {
            if (uis.TryGetValue(typeof(T), out IUI value))
            {
                ui = (T)value;
                return true;
            }

            ui = default(T);
            return false;
        }

        public void Push(IUI ui)
        {
            if (uisShown.Count > 0 &&
                uisShown.Last.Value == ui)
                return;

            int sortingOrder = 0;
            if (uisShown.Last != null)
            {
                uisShown.Last.Value.inputActionEnalbed = false;
                sortingOrder = uisShown.Last.Value.sortingOrder + 1;
            }
            uisShown.Remove(ui);
            uisShown.AddLast(ui);
            ui.inputActionEnalbed = true;
            ui.sortingOrder = sortingOrder;

            if (uisShown.Count is not 1) return;
            if (!ControllerManager.instance.TryGet(out PlayerController playerController) ||
                !ControllerManager.instance.TryGet(out VCam_FollowingPlayer vCamFollowingPlayer)) return;
            playerController.controlEnabled = false;
            vCamFollowingPlayer.controlEnabled = false;
                
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;


        }

        public void Pop(IUI ui)
        {
            uisShown.Remove(ui);

            if (uisShown.Count > 0)
            {
                uisShown.Last.Value.inputActionEnalbed = true;
            }
            
            
            if (uisShown.Count is not  0) return;
            if (!ControllerManager.instance.TryGet(out PlayerController playerController) ||
                !ControllerManager.instance.TryGet(out VCam_FollowingPlayer vCamFollowingPlayer)) return;
            playerController.controlEnabled = false;
            vCamFollowingPlayer.controlEnabled = false;
                
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;


        }

        public void HideLast()
        {
            if (uisShown.Count <= 0)
                return;

            uisShown.Last.Value.Hide();
        }
    }
}