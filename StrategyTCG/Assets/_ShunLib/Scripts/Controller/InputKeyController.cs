using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ShunLib.InputKey
{
    public class InputKeyController : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        
        // キーボードが押され続けているときに実行する関数の連想配列
        private Dictionary<KeyCode, UnityAction> _keyStayActions = default;
        // キーボードが押されたときに実行する関数の連想配列
        private Dictionary<KeyCode, UnityAction> _keyDownActions = default;
        // キーボードが離されたときに実行する関数の連想配列
        private Dictionary<KeyCode, UnityAction> _keyUpActions = default;

        // ---------- Unity組込関数 ----------
        
        void FixedUpdate()
        {
            // キーボードが押され続けている時
            if (_keyStayActions.Count > 0)
            {
                foreach (KeyValuePair<KeyCode, UnityAction> actions in _keyStayActions)
                {
                    if (Input.GetKey(actions.Key))
                    {
                        actions.Value();
                    }
                }
            }

            // キーボードが押された時
            if (_keyDownActions.Count > 0)
            {
                foreach (KeyValuePair<KeyCode, UnityAction> actions in _keyDownActions)
                {
                    if (Input.GetKeyDown(actions.Key))
                    {
                        actions.Value();
                    }
                }
            }
            
            // キーボードが離された時
            if (_keyUpActions.Count > 0)
            {
                foreach (KeyValuePair<KeyCode, UnityAction> actions in _keyUpActions)
                {
                    if (Input.GetKeyUp(actions.Key))
                    {
                        actions.Value();
                    }
                }
            }
        }

        // ---------- Public関数 ----------

        // キーボードが押され続けているときの処理を設定する
        public void SetKeyStayAction(KeyCode key, UnityAction action)
        {
            if (_keyStayActions.ContainsKey(key))
            {
                _keyStayActions[key] = action;
            }
            else
            {
                _keyStayActions.Add(key, action);
            }
        }

        // キーボードが押されたときの処理を設定する
        public void SetKeyDownAction(KeyCode key, UnityAction action)
        {
            if (_keyDownActions.ContainsKey(key))
            {
                _keyDownActions[key] = action;
            }
            else
            {
                _keyDownActions.Add(key, action);
            }
        }

        // キーボードが離されたときの処理を設定する
        public void SetKeyUpAction(KeyCode key, UnityAction action)
        {
            if (_keyUpActions.ContainsKey(key))
            {
                _keyUpActions[key] = action;
            }
            else
            {
                _keyUpActions.Add(key, action);
            }
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

