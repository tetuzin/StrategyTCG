using System;
using System.Collections.Generic;
using Ch120.Const.Audio;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Ch120.Popup.Simple;
using UK.Manager.Audio;
using UK.Manager.Card;
using UK.Model.CardMain;
using UK.Unit.Card;

namespace UK.Popup.CardEffectActivate
{
    public class CardEffectActivatePopup : SimpleTextPopup
    {
        // ---------- 定数宣言 ----------
        
        public const string CALLBACK_EVENT = "CallBackEvent";
        
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        
        [SerializeField, Tooltip("タイトルテキスト")] private TextMeshProUGUI _titleText = default;
        [SerializeField, Tooltip("カードオブジェクト生成箇所")] private GameObject _cardObject = default;
        [SerializeField, Tooltip("背景")] private Image _backgroundImage = default;
        
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        public override void Show()
        {
            base.Show();
            UKAudioManager.Instance.PlaySE(AudioConst.SE_EFFECT);
        }
        
        public override void Hide()
        {
            _isShow = false;
            _curTime = 0.0f;
            Close();
            Action action = GetAction(CALLBACK_EVENT);
            action();
        }
        
        // ---------- Private関数 ----------
        
        // タイトルテキストの設定
        private void SetTitleText(string text)
        {
            if (_titleText == default) return;
            
            _titleText.text = text;
        }
        
        // カードオブジェクトの設定
        private void SetCardObject(CardMainModel model, bool isPlayer)
        {
            if (_cardObject == default) return;
            
            CardUnit unit = CardManager.Instance.Instantiate2DCardUnit(model, isPlayer);
            unit.gameObject.transform.SetParent(_cardObject.transform);
            unit.gameObject.transform.localPosition = Vector3.zero;
        }
        
        // 背景色の設定
        private void SetBackgroundColor(bool isPlayer)
        {
            if (_backgroundImage == default) return;

            if (isPlayer)
            {
                _backgroundImage.color = new Color32(20, 20, 120, 130);
            }
            else
            {
                _backgroundImage.color = new Color32(120, 20, 20, 130);
            }
        }
        
        // ---------- protected関数 ---------
        
        // データの設定
        protected override void SetData(dynamic param)
        {
            // データの初期化
            var paramter = new {
                mainText = param.mainText,
                titleText = param.titleText,
                cardMainModel = param.cardMainModel,
                isPlayer = param.isPlayer
            };
            
            SetTitleText(paramter.titleText);
            SetCardObject(paramter.cardMainModel, paramter.isPlayer);
            SetBackgroundColor(paramter.isPlayer);

            base.SetData(paramter);
        }
    }
}

