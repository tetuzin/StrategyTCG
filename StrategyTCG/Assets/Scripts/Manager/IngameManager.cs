using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UK.Manager.UI;

// テスト用
using UK.Unit.Card;
using UK.Model.CardMain;
using UK.Dao;
using Ch120.Manager.Master;

namespace UK.Manager.Ingame
{
    public class IngameManager : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private bool _isFirst = default;
        // テスト用
        public CardUnit cardUnit = default;

        // ---------- Unity組込関数 ----------
        
        void Start()
        {
            InitializeIngame();
            CardMainDao dao = (CardMainDao)MasterManager.Instance.GetDao("CardMainDao");
            List<CardMainModel> list = dao.Get();
            CardMainModel model = list[0];
            cardUnit.Initialize(model);
        }

        // ---------- Public関数 ----------
        // ---------- Private関数 ----------

        // ゲームの初期化
        private void InitializeIngame()
        {
            // TODO
            Debug.Log("InitializeIngame");

            // ターン終了の処理をボタンに設定
            UIManager.Instance.SetTurnEndAction(EndPlayerTurn);

            StartGame();
        }

        // ゲーム開始
        private void StartGame()
        {
            // TODO
            Debug.Log("StartGame");

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

            // TODO 山札一枚ドロー

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

            // TODO 山札一枚ドロー

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

        // デバッグ用：相手ターン処理
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

        // ---------- protected関数 ---------
    }
}

