using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

using Ch120.Manager.Popup;

namespace UK.Manager.TitleUI
{
    public class TitleButtonType
    {
        public const int BATTLE_MENU = 0;       // 対戦メニューボタン
        public const int CARD_MENU = 1;         // カードメニューボタン
        public const int OPTION_MENU = 2;       // オプションボタン
        public const int CPU_BATTLE = 3;        // CPU対戦ボタン
        public const int ONLINE_BATTLE = 4;     // オンライン対戦ボタン
        public const int CARD_PACK = 5;         // カードパック購入ボタン
        public const int CARD_LIST = 6;         // カードリストボタン
        public const int DECK_LIST = 7;         // デッキリストボタン
        public const int GAME_EXIT = 8;         // ゲーム終了ボタン
        public const int BACK = 9;              // もどるボタン
    }
    
    public class TitleImageType
    {
        public const int TITLE = 0;             // タイトル画像
    }

    public class TitleCanvasGroupType
    {
        public const int TITLE = 0;             // タイトル画面UI
        public const int BATTLE_MENU = 1;       // 対戦メニューUI
        public const int CARD_MENU = 2;         // カードメニューUI
    }

    public class TitlePopupType
    {
        public const int SIMPLE = 0;            // シンプルテキストポップアップ
    }
    
    public class TitleUIManager : BaseUIManager
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        
        // 初期化
        public override void Initialize()
        {
            base.Initialize();
            
            // 戻るボタンの非表示
            SetButtonActive(TitleButtonType.BACK, false);
            
            // キャンバスグループの初期設定
            SetCanvasGroupActive(TitleCanvasGroupType.TITLE, true);
            SetCanvasGroupActive(TitleCanvasGroupType.BATTLE_MENU, false);
            SetCanvasGroupActive(TitleCanvasGroupType.CARD_MENU, false);
            
            _imageList[TitleImageType.TITLE].gameObject.transform.DOScale(
                new Vector3(1.1f, 1.1f, 1.1f), 2.0f).SetLoops(-1,LoopType.Yoyo
            );
            
            // 対戦メニューボタンのイベント設定
            SetButtonEvent(TitleButtonType.BATTLE_MENU, () =>
            {
                SetCanvasGroupActive(TitleCanvasGroupType.TITLE, false);
                SetCanvasGroupActive(TitleCanvasGroupType.BATTLE_MENU, true);
                SetButtonEvent(TitleButtonType.BACK, () =>
                {
                    SetCanvasGroupActive(TitleCanvasGroupType.TITLE, true);
                    SetCanvasGroupActive(TitleCanvasGroupType.BATTLE_MENU, false);
                    SetButtonActive(TitleButtonType.BACK, false);
                });
                SetButtonActive(TitleButtonType.BACK, true); 
            });
            
            SetButtonEvent(TitleButtonType.CARD_MENU, () =>
            {
                CreateOpenPopup(TitlePopupType.SIMPLE, null, new {mainText = "鋭意作成中です！ごめんね"});
            });
            SetButtonEvent(TitleButtonType.OPTION_MENU, () =>
            {
                CreateOpenPopup(TitlePopupType.SIMPLE, null, new {mainText = "鋭意作成中です！ごめんね"});
            });
            SetButtonEvent(TitleButtonType.ONLINE_BATTLE, () =>
            {
                CreateOpenPopup(TitlePopupType.SIMPLE, null, new {mainText = "鋭意作成中です！ごめんね"});
            });
            SetButtonEvent(TitleButtonType.CARD_PACK, () =>
            {
                CreateOpenPopup(TitlePopupType.SIMPLE, null, new {mainText = "鋭意作成中です！ごめんね"});
            });
            SetButtonEvent(TitleButtonType.CARD_LIST, () =>
            {
                CreateOpenPopup(TitlePopupType.SIMPLE, null, new {mainText = "鋭意作成中です！ごめんね"});
            });
            SetButtonEvent(TitleButtonType.DECK_LIST, () =>
            {
                CreateOpenPopup(TitlePopupType.SIMPLE, null, new {mainText = "鋭意作成中です！ごめんね"});
            });
            SetButtonEvent(TitleButtonType.GAME_EXIT, () =>
            {
                CreateOpenPopup(TitlePopupType.SIMPLE, null, new {mainText = "鋭意作成中です！ごめんね"});
            });
        }
        
        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}