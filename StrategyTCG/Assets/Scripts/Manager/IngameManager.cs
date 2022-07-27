using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ch120.Singleton;
using Ch120.Manager.Master;

using UK.Const.Game;
using UK.Const.Effect;
using UK.Manager.UI;
using UK.Manager.Card;
using UK.Model.CardMain;
using UK.Dao;
using UK.Unit.Player;
using UK.Utils.Card;

namespace UK.Manager.Ingame
{
    public class IngameManager : SingletonMonoBehaviour<IngameManager>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        public PlayerUnit PlayerUnit
        {
            get { return _playerUnit; }
        }
        public PlayerUnit OpponentUnit
        {
            get { return _opponentUnit; }
        }
        public TimingType CurTiming
        {
            get { return _curTiming; }
        }
        public int CurTurn
        {
            get { return _curTurn; }
        }

        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private bool _isFirst = default;
        private PlayerUnit _playerUnit = default;
        private PlayerUnit _opponentUnit = default;
        private TimingType _curTiming = default;
        private int _curTurn = default;

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

            _curTurn = 1;

            // TODO お互いのユニットを読み込む
            _playerUnit = GetPlayerUnit();
            _opponentUnit = GetPlayerUnit();

            UIManager.Instance.Initialize();

            // ターン終了の処理をボタンに設定
            UIManager.Instance.SetTurnEndAction(AttackPlayer);

            StartGame();
        }

        // ゲーム開始
        private void StartGame()
        {
            // TODO
            Debug.Log("StartGame");

            // タイミング初期化
            _curTiming = TimingType.NONE;

            // TODO お互いのデッキを読み込む(現状、仮)
            List<CardMainModel> playerDeck = CreatePlayerDeck();
            List<CardMainModel> opponentDeck = CreateOpponentDeck();
            // List<CardMainModel> playerDeck = CreateOpponentDeck();
            // List<CardMainModel> opponentDeck = CreatePlayerDeck();

            // お互いのデッキをDeckUnitに設定
            CardManager.Instance.SetPlayerDeck(playerDeck);
            CardManager.Instance.SetOpponentDeck(opponentDeck);
            UIManager.Instance.InitializePlayerStatusGroup(_playerUnit);
            UIManager.Instance.InitializeOpponentStatusGroup(_opponentUnit);

            // 手札を取得
            CardManager.Instance.DeckDraw(GameConst.PLAYER, GameConst.START_HAND_CARD);
            CardManager.Instance.DeckDraw(GameConst.OPPONENT, GameConst.START_HAND_CARD);

            // 先手後手の初期化
            decisionIsFirst();
            UIManager.Instance.SwitchTurnText(_isFirst);
            if (_isFirst)
            {
                StartPlayerTurn();
            }
            else
            {
                StartOpponentTurn();
            }
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

            // タイミング：プレイヤーターンスタート
            _curTiming = TimingType.START_TURN_PLAYER;

            // TODO ターンスタートアニメーション

            // 山札一枚ドロー
            CardManager.Instance.DeckDraw(GameConst.PLAYER);

            // UI表示
            UIManager.Instance.SetActiveTurnEndButton(true);
            UIManager.Instance.SwitchTurnText(true);
            UIManager.Instance.SetActiveHandGroup(true);

            // 場の更新処理
            CardManager.Instance.GetCardBattleField(GameConst.PLAYER).UpdateFieldByStartTurn();

            // タイミング：プレイヤーターン
            _curTiming = TimingType.TURN_PLAYER;
        }

        // 相手の攻撃
        private void AttackPlayer()
        {
            Debug.Log("AttackPlayer");

            // タイミング：相手の攻撃
            _curTiming = TimingType.END_ATTACK_PLAYER;

            // TODO ターン最後の攻撃処理

            EndPlayerTurn();
        }

        // 自分のターン終了
        private void EndPlayerTurn()
        {
            Debug.Log("EndPlayerTurn");

            // タイミング：プレイヤーターンエンド
            _curTiming = TimingType.END_TURN_PLAYER;

            UpdateCurTurn();

            // UI非表示
            UIManager.Instance.SetActiveTurnEndButton(false);
            UIManager.Instance.SetActiveHandGroup(false);

            StartOpponentTurn();
        }

        // 相手のターン開始
        private void StartOpponentTurn()
        {
            Debug.Log("StartOpponentTurn");

            // タイミング：相手ターンスタート
            _curTiming = TimingType.START_TURN_OPPONENT;

            // TODO ターンスタートアニメーション

            // 山札一枚ドロー
            CardManager.Instance.DeckDraw(GameConst.OPPONENT);

            // UI表示
            UIManager.Instance.SwitchTurnText(false);

            // 場の更新処理
            CardManager.Instance.GetCardBattleField(GameConst.OPPONENT).UpdateFieldByStartTurn();

            // タイミング：相手ターン
            _curTiming = TimingType.TURN_OPPONENT;


            TestPlayOpponentTurn();
        }

        // 相手の攻撃
        private void AttackOpponent()
        {
            Debug.Log("AttackOpponent");

            // タイミング：相手の攻撃
            _curTiming = TimingType.END_ATTACK_OPPONENT;

            // TODO ターン最後の攻撃処理

            EndOpponentTurn();
        }

        // 相手のターン終了
        private void EndOpponentTurn()
        {
            Debug.Log("EndOpponentTurn");

            // タイミング：プレイヤーターンエンド
            _curTiming = TimingType.END_TURN_OPPONENT;

            // TODO 攻撃アニメーションと処理

            // TODO ターンエンドアニメーション

            UpdateCurTurn();

            StartPlayerTurn();
        }

        // プレイヤーの先手後手を決める
        private void decisionIsFirst()
        {
            bool isFirst = Random.Range(0, 2) == 0;
            _isFirst = isFirst;
        }

        // ターン数の加算
        private void UpdateCurTurn()
        {
            if (!_isFirst)
            {
                _curTurn++;
            }
        }

        // プレイヤーユニットの取得
        public PlayerUnit GetPlayerUnit(bool isPlayer)
        {
            if (isPlayer)
            {
                return _playerUnit;
            }
            else
            {
                return _opponentUnit;
            }
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
            AttackOpponent();
        }

        // 開発テスト用関数：プレイヤーの山札生成
        private List<CardMainModel> CreatePlayerDeck()
        {
            List<CardMainModel> deck = new List<CardMainModel>();
            List<CardMainModel> list =  ((CardMainDao)MasterManager.Instance.GetDao("CardMainDao")).Get();
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
            List<CardMainModel> list =  ((CardMainDao)MasterManager.Instance.GetDao("CardMainDao")).Get();
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

        // 開発テスト用関数：PlayerUnitの仮データを返す
        private PlayerUnit GetPlayerUnit()
        {
            PlayerUnit unit = new PlayerUnit();
            unit.Power = 100;
            unit.PeopleNum = 5;
            unit.MaxHp = 1000;
            unit.CurHp = 1000;
            unit.Fund = 100;
            unit.TurnFund = 50;
            unit.Name = "Player";
            return unit;
        }

        // ---------- protected関数 ---------
    }
}

