using System;
using System.Collections.Generic;
using ShunLib.Const.Audio;
using ShunLib.Manager.Audio;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

using ShunLib.Singleton;
using ShunLib.Manager.Scene;
using UK.Manager.Master;

using UK.Dao;
using UK.Manager.TitleUI;
using UK.Model.Deck;
using UK.Model.CardMain;

namespace UK.Manager.Title
{
    public class TitleManager : SingletonMonoBehaviour<TitleManager>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        
        [SerializeField, Tooltip("Cameraオブジェクト")] protected Camera _mainCamera = default;
        
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------

        void Start()
        {
            Initialized();
        }

        // ---------- Public関数 ----------
        // ---------- Private関数 ----------

        // 初期化
        private void Initialized()
        {
            if (_mainCamera != default)
            {
                _mainCamera.gameObject.transform.DORotate(Vector3.up * 10.0f, 2.0f)
                    .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
            }
            
            TitleUIManager.Instance.Initialize();
            
            TitleUIManager.Instance.SetButtonEvent(TitleButtonType.CPU_BATTLE, () =>
            {
                OnClickCpuBattle();
            });
        }

        // デッキ選択ポップアップの表示
        private void OnClickCpuBattle()
        {
            DeckModel JAPAN_DECK = new DeckModel()
            {
                DeckId = 1,
                CardList = CreatePlayerDeck(),
                DeckName = "日本デッキ"
            };
            DeckModel AMERICA_DECK = new DeckModel()
            {
                DeckId = 2,
                CardList = CreateOpponentDeck(),
                DeckName = "アメリカデッキ"
            };
            
            Dictionary<string, Action> actions = new Dictionary<string, Action>();
            actions.Add(UK.Popup.DeckSelect.DeckSelectPopup.DECISION_BUTTON_EVENT, () =>
            {
                SceneLoadManager.Instance.TransitionScene("IngameScene");
            });
            var parameter = new
            {
                titleText = "デッキ選択",
                mainText = "Please select...",
                decisionText = "決定",
                cancelText = "戻る",
                verticalScroll = true,
                horizontalScroll = false,
                deckList = new List<DeckModel>(){JAPAN_DECK, AMERICA_DECK}
            };
            TitleUIManager.Instance.CreateOpenPopup(TitlePopupType.DECK_SELECT, actions, parameter);
        }
        
        // 開発テスト用関数：プレイヤーの山札生成
        private List<CardMainModel> CreatePlayerDeck()
        {
            List<CardMainModel> deck = new List<CardMainModel>();
            List<CardMainModel> list =  ((CardMainDao)UKMasterManager.Instance.GetDao("CardMainDao")).Get();
            deck.Add(list[0]);
            deck.Add(list[0]);
            deck.Add(list[0]);
            deck.Add(list[0]);
            deck.Add(list[1]);
            deck.Add(list[1]);
            deck.Add(list[1]);
            deck.Add(list[2]);
            deck.Add(list[3]);
            deck.Add(list[4]);
            deck.Add(list[4]);
            deck.Add(list[4]);
            deck.Add(list[5]);
            deck.Add(list[5]);
            deck.Add(list[5]);
            deck.Add(list[6]);
            deck.Add(list[6]);
            deck.Add(list[6]);
            deck.Add(list[7]);
            deck.Add(list[7]);
            deck.Add(list[8]);
            deck.Add(list[8]);
            deck.Add(list[9]);
            deck.Add(list[9]);
            deck.Add(list[9]);
            deck.Add(list[9]);
            deck.Add(list[10]);
            deck.Add(list[10]);
            deck.Add(list[11]);
            deck.Add(list[11]);
            deck.Add(list[12]);
            deck.Add(list[12]);
            deck.Add(list[13]);
            deck.Add(list[13]);
            deck.Add(list[14]);
            deck.Add(list[14]);
            deck.Add(list[15]);
            deck.Add(list[15]);
            deck.Add(list[16]);
            deck.Add(list[16]);
            return deck;
        }

        // 開発テスト用関数：相手の山札生成
        private List<CardMainModel> CreateOpponentDeck()
        {
            List<CardMainModel> deck = new List<CardMainModel>();
            List<CardMainModel> list =  ((CardMainDao)UKMasterManager.Instance.GetDao("CardMainDao")).Get();
            deck.Add(list[0]);
            deck.Add(list[0]);
            deck.Add(list[1]);
            deck.Add(list[1]);
            deck.Add(list[1]);
            deck.Add(list[17]);
            deck.Add(list[18]);
            deck.Add(list[19]);
            deck.Add(list[19]);
            deck.Add(list[19]);
            deck.Add(list[20]);
            deck.Add(list[20]);
            deck.Add(list[20]);
            deck.Add(list[21]);
            deck.Add(list[21]);
            deck.Add(list[21]);
            deck.Add(list[22]);
            deck.Add(list[22]);
            deck.Add(list[23]);
            deck.Add(list[23]);
            deck.Add(list[24]);
            deck.Add(list[24]);
            deck.Add(list[24]);
            deck.Add(list[24]);
            deck.Add(list[25]);
            deck.Add(list[25]);
            deck.Add(list[26]);
            deck.Add(list[26]);
            deck.Add(list[27]);
            deck.Add(list[27]);
            deck.Add(list[28]);
            deck.Add(list[28]);
            deck.Add(list[29]);
            deck.Add(list[29]);
            deck.Add(list[30]);
            deck.Add(list[30]);
            deck.Add(list[31]);
            deck.Add(list[31]);
            deck.Add(list[31]);
            deck.Add(list[31]);
            return deck;
        }
        
        // ---------- protected関数 ---------
    }
}