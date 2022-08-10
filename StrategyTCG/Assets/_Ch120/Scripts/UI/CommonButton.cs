using System;
using Ch120.Const.Audio;
using UK.Manager.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Ch120.UI.CommonBtn
{
    public class CommonButton : Button
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        
        [SerializeField, Tooltip("ボタンオブジェクト")] public GameObject _object = default;
        [SerializeField, Tooltip("イベントトリガー")] public EventTrigger _eventTrigger = default;
        [SerializeField, Tooltip("テキスト")] public TextMeshProUGUI _text = default;
        
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        
        [SerializeField, Tooltip("長押しする時間")] public float _downWaitTime = 1.0f;
        
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private bool _isActiveOnEvent = default;
        private bool _isActiveDownEvent = default;

        private bool _isDown = default;
        private bool _isOnDownEvent = default;
        private float _downTime = default;
        private Action _onEvent = default;
        private Action _onDownEvent = default;

        private EventTrigger.Entry _onEnter = default;
        private EventTrigger.Entry _onExit = default;
        private EventTrigger.Entry _onDown = default;
        private EventTrigger.Entry _onUp = default;
        
        // ---------- Unity組込関数 ----------

        void Awake() { Initialize(); }

        private void FixedUpdate()
        {
            // ボタン長押し検知処理
            if (_isDown && !_isOnDownEvent)
            {
                _downTime += Time.deltaTime;
                if (_downTime >= _downWaitTime)
                {
                    _isOnDownEvent = true;
                    if (_isActiveDownEvent) _onDownEvent?.Invoke();
                }
            }
        }

        // ---------- Public関数 ----------
        
        // ボタン長押し時のイベントを設定
        public void SetOnDownEvent(Action action)
        {
            _onDownEvent = action;
            // _isActiveDownEvent = true;
        }
        
        // ボタン長押し時のイベントを削除
        public void RemoveOnDownEvent()
        {
            _onDownEvent = () => { };
            // _isActiveDownEvent = false;
        }
        
        // ボタン長押し時イベントの活性化・非活性化
        public void SetOnDownActive(bool isActive)
        {
            _isActiveDownEvent = isActive;
        }
        
        // ボタン押下時のイベントを設定
        public void SetOnEvent(Action action)
        {
            _onEvent = action;
            // _isActiveOnEvent = true;
        }
        
        // ボタン押下時のイベントを削除
        public void RemoveOnEvent()
        {
            _onEvent = () => { };
            // _isActiveOnEvent = false;
        }
        
        // ボタン押下時イベントの活性化・非活性化
        public void SetOnActive(bool isActive)
        {
            _isActiveOnEvent = isActive;
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        
        // 初期化
        protected virtual void Initialize()
        {
            _isActiveOnEvent = true;
            _isActiveDownEvent = true;
            
            _isDown = false;
            _isOnDownEvent = false;
            _downTime = 0.0f;
            
            if (_eventTrigger == default) return;
            
            // ポインターがオブジェクトに入るときのイベント
            _onEnter = new EventTrigger.Entry();
            _onEnter.eventID = EventTriggerType.PointerEnter;
            _onEnter.callback.AddListener((data) => { OnPointerEnterButton((PointerEventData)data); });
            _eventTrigger.triggers.Add(_onEnter);
            
            // ポインターがオブジェクトから出たときのイベント
            _onExit = new EventTrigger.Entry();
            _onExit.eventID = EventTriggerType.PointerExit;
            _onExit.callback.AddListener((data) => { OnPointerExitButton((PointerEventData)data); });
            _eventTrigger.triggers.Add(_onExit);
            
            // ボタンを長押ししたときのイベント
            _onDown = new EventTrigger.Entry();
            _onDown.eventID = EventTriggerType.PointerDown;
            _onDown.callback.AddListener((data) => { OnPointerDownButton((PointerEventData)data); });
            _eventTrigger.triggers.Add(_onDown);
            
            // ボタンを離したときのイベント
            _onUp = new EventTrigger.Entry();
            _onUp.eventID = EventTriggerType.PointerUp;
            _onUp.callback.AddListener((data) => { OnPointerUpButton((PointerEventData)data); });
            _eventTrigger.triggers.Add(_onUp);
        }
        
        // ボタンにポインターが入ったとき
        protected virtual void OnPointerEnterButton(PointerEventData data)
        {
            UKAudioManager.Instance.PlaySE(AudioConst.SE_HOVER_BUTTON);
            _object.transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.3f);
        }
        
        // ボタンからポインターが出たとき
        protected virtual void OnPointerExitButton(PointerEventData data)
        {
            _object.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.3f);
        }
        
        // ボタンを長押ししたとき
        protected virtual void OnPointerDownButton(PointerEventData data)
        {
            _isDown = true;
            _downTime = 0.0f;
        }
        
        // ボタンを離したとき
        protected virtual void OnPointerUpButton(PointerEventData data)
        {
            _isDown = false;
            if (_isOnDownEvent)
            {
                _isOnDownEvent = false;
            }
            else
            {
                if (_isActiveOnEvent)
                {
                    UKAudioManager.Instance.PlaySE(AudioConst.SE_CLICK_BUTTON);
                    _onEvent?.Invoke();
                }
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(CommonButton))]
    public class CommonButtonEditor : UnityEditor.UI.ButtonEditor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            var component = (CommonButton) target;
            PropertyField(nameof(component._object), "ButtonObject");
            PropertyField(nameof(component._eventTrigger), "EventTrigger");
            PropertyField(nameof(component._text), "Text");
            PropertyField(nameof(component._downWaitTime), "DownWaitTime");
            serializedObject.ApplyModifiedProperties();
        }

        private void PropertyField(string property, string label) {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(property), new GUIContent(label));
        }
    }
#endif
}