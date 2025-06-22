/*
 * Author: CharSui
 * Created On: 2023.10.29
 * Description: 监听基础输入
 * TODO:加入对当前PlayerInput的获取以及事件监听的注入
 * 现在的使用方法是：通过PlayerInput组件进行UnityEvent绑定执行，但是这套方法非常不好用。很耦合。
 * 好在我们有指令系统可以分发控制指令。
 * 目前这套逻辑就充当对PlayerInput的包装。
 */

using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class BattleInputListener : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;

        // 对于武器而言，不需要知道什么逻辑，只需要知道扳机扣下还是没扣下就行了
        public bool Fire;
        
        // 主动装填。
        public bool Reload;
        
        public bool interactiveC;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        [ShowInInspector]
        public InputAction.CallbackContext debug;

        #region 输入检测
        
        public void OnMove(InputAction.CallbackContext value)
        {
            // 使用UnityEvent作为Input回调的时候无法使用InputValue.Get<Vector2>()
            // 无论后面反省是啥都不可以
            MoveCallBack(value.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext value)
        {
            if(cursorInputForLook)
            {
                LookCallBack(value.ReadValue<Vector2>());
            }
        }

        // public void OnJump(InputAction.CallbackContext value)
        // {
        //     JumpCallBack(value.started);
        // }

        // 如果按钮是Button、interaction选press，那么达成目标就会触发performed
        //  如果是清空，那么start和prefromed都会触发一次，松手一次
        public void OnFireInput(InputAction.CallbackContext value)
        {
            debug = value;
            //测试代码
            switch (value.phase)
            {
                case InputActionPhase.Performed:
                    Debug.Log($"[Input]value.Performed");
                    break;
                case InputActionPhase.Started:
                    Debug.Log($"[Input]value.Started");
                    break;
                case InputActionPhase.Canceled:
                    Debug.Log($"[Input]value.Canceled");
                    break;
            }
            
            
            
            FireCallBack(value.performed);
        }

        public void OnReloadInput(InputAction.CallbackContext value)
        {
            ReloadCallBack(value.performed);
        }
        
        public void OnInteractiveC(InputAction.CallbackContext value)
        {
            InteractiveCCallBack(value.started);
        }

        public void OnStart(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        #endregion 输入检测

        private void MoveCallBack(Vector2 newMoveDirection) 
        {
            move = newMoveDirection;
        } 

        private void LookCallBack(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        // private void JumpCallBack(bool newJumpState)
        // {
        //     Debug.Log("[StandardInputListener]JumpCallBack");
        //     jump = newJumpState;
        // }

        private void FireCallBack(bool newInteractive)
        {
            Debug.Log("[StandardInputListener]Button A CallBack");
            Fire = newInteractive;
        }		
		
        private void ReloadCallBack(bool newInteractive)
        {           
            Debug.Log("[StandardInputListener]Button B CallBack");
            Reload = newInteractive;
        }        
        
        private void InteractiveCCallBack(bool newInteractive)
        {           
            Debug.Log("[StandardInputListener]Button B CallBack");
            interactiveC = newInteractive;
        }

        // private void OnApplicationFocus(bool hasFocus)
        // {
        //     SetCursorState(cursorLocked);
        // }
        //
        // private void SetCursorState(bool newState)
        // {
        //     //Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        // }
    }
	
}