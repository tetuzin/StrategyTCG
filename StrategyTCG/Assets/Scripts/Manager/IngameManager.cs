using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ch120.Singleton;
using Ch120.Manager.Master;

using UK.Const.Game;
using UK.Manager.UI;
using UK.Manager.Card;
using UK.Model.CardMain;
using UK.Dao;

namespace UK.Manager.Ingame
{
    public class IngameManager : SingletonMonoBehaviour<IngameManager>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private bool _isFirst = default;

        // ---------- Unity組込関数 ----------
        
        void Start()
        {
            InitializeIngame();
        }

        // ---------- Public関数 ----------
        // ---------- Private関数 ----------

        // ゲームの初期化
        private void InitializeIngame()
        {
            // TODO
            Debug.Log("InitializeIngame");

            UIManager.Instance.Initialize();

            // ターン終了の処理をボタンに設定
            UIManager.Instance.SetTurnEndAction(EndPlayerTurn);

            StartGame();
        }

        // ゲーム開始
        private void StartGame()
        {
            // TODO
            Debug.Log("StartGame");

            // TODO お互いのデッキを読み込む(現状、仮)
            List<CardMainModel> playerDeck = CreateDeck();
            List<CardMainModel> opponentDeck = CreateDeck();

            // お互いのデッキをDeckUnitに設定
            CardManager.Instance.SetPlayerDeck(playerDeck);
            CardManager.Instance.SetOpponentDeck(opponentDeck);

            // 手札を取得
            CardManager.Instance.GetCardBattleField(GameConst.PLAYER).DrawDeck(GameConst.START_HAND_CARD);
            CardManager.Instance.GetCardBattleField(GameConst.OPPONENT).DrawDeck(GameConst.START_HAND_CARD);

            // TODO プレイヤーの手札を表示

            // 先手後手の初期化
            decisionIsFirst();
            UIManager.Instance.SwitchTurnText(_isFirst);
        }

        // ゲーム終了
        private void EndGame()
        {
            // TODO
            Debug.Log("EndGame");
        }

        // 自分のターン開始
        private void StartPlayerTurn()
        {
            Debug.Log("StartPlayerTurn");

            // TODO ターンスタートアニメーション

            // 山札一枚ドロー
            CardManager.Instance.GetCardBattleField(GameConst.PLAYER).DrawDeck();

            // TODO UI表示
            UIManager.Instance.SetActiveActionUI(true);
            UIManager.Instance.SwitchTurnText(true);
        }

        // 自分のターン終了
        private void EndPlayerTurn()
        {
            Debug.Log("EndPlayerTurn");

            // TODO UI非表示
            UIManager.Instance.SetActiveActionUI(false);

            StartOpponentTurn();
        }

        // 相手のターン開始
        private void StartOpponentTurn()
        {
            Debug.Log("StartOpponentTurn");

            // TODO ターンスタートアニメーション

            // 山札一枚ドロー
            CardManager.Instance.GetCardBattleField(GameConst.OPPONENT).DrawDeck();

            // TODO UI表示
            UIManager.Instance.SwitchTurnText(false);


            TestPlayOpponentTurn();
            // EndOpponentTurn();
        }

        // 相手のターン終了
        private void EndOpponentTurn()
        {
            Debug.Log("EndOpponentTurn");

            // TODO 攻撃アニメーションと処理

            // TODO ターンエンドアニメーション

            StartPlayerTurn();
        }

        // プレイヤーの先手後手を決める
        private void decisionIsFirst()
        {
            bool isFirst = Random.Range(0, 2) == 0;
            _isFirst = isFirst;
        }







        // 開発テスト用関数：相手ターン処理
        private async void TestPlayOpponentTurn()
        {
            await System.Threading.Tasks.Task.Delay(1000);
            Debug.Log("1");
            await System.Threading.Tasks.Task.Delay(1000);
            Debug.Log("2");
            await System.Threading.Tasks.Task.Delay(1000);
            Debug.Log("3");
            await System.Threading.Tasks.Task.Delay(1000);
            EndOpponentTurn();
        }

        // 開発テスト用関数：山札生成
        private List<CardMainModel> CreateDeck()
        {
            List<CardMainModel> deck = new List<CardMainModel>();
            List<CardMainModel> list =  ((CardMainDao)MasterManager.Instance.GetDao("CardMainDao")).Get();
            for(int i = 0; i < 40; i++)
            {
                deck.Add(list[4]);
            }
            return deck;
        }

        // ---------- protected関数 ---------
    }
}

