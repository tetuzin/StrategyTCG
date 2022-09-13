using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using ShunLib.Popup.Common;
using ShunLib.UI.CommonBtn;
using ShunLib.ScrollView;

namespace ShunLib.Popup.ScrollView
{
    public class ScrollViewPopup : CommonPopup
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        
        [SerializeField, Tooltip("スクロールビュー")] protected CommonScrollView _scrollView;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ---------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        
        // スクロールビューに子要素の追加
        public virtual void SetContent(GameObject obj)
        {
            _scrollView.AddContent(obj);
        }
        
        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        
        // 初期化
        protected override void Initialize()
        {
            _scrollView.Initialize();
        }

        // データの設定
        protected override void SetData(dynamic param)
        {
            var parameter = new
            {
                titleText = param.titleText,
                mainText = param.mainText,
                decisionText = param.decisionText,
                cancelText = param.cancelText,
                verticalScroll = param.verticalScroll,
                horizontalScroll = param.horizontalScroll
            };

            base.SetData(parameter);
            
            _scrollView.SetVerticalScroll(parameter.verticalScroll);
            _scrollView.SetHorizontalScroll(parameter.horizontalScroll);
        }
    }
}

