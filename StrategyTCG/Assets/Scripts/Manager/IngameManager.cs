using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

using Ch120.Singleton;
using Ch120.Manager.Master;
using Ch120.Manager.Scene;
using Ch120.Manager.User;
using UK.Const.Game;
using UK.Const.Effect;
using UK.Manager.UI;
using UK.Manager.Card;
using UK.Model.CardMain;
using UK.Dao;
using UK.Unit.Player;
using UK.CpuCtrl;
using UK.Manager.Popup;

namespace UK.Manager.Ingame
{
    public class IngameManager : SingletonMonoBehaviour<IngameManager>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        
        [SerializeField, Tooltip("カメラ")] protected Camera _camera;
        
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
        private CpuController _cpu = default;

        // ---------- Unity組込関数 ----------
        
        void Start()
        {
            StartGame();
        }

        // ---------- Public関数 ----------
        
        // 攻撃処理の取得
        public Action GetAttackAction(bool isPlayer)
        {
            return isPlayer ? AttackPlayer : AttackOpponent;
        }
        
        // 操作を行っているプレイヤーかどうか判断する
        public bool CheckPlayer(bool isPlayer)
        {
            return _playerUnit.IsPlayer == isPlayer;
        }
        
        // ---------- Private関数 ----------

        // ゲームの初期化
        private async Task InitializeIngame()
        {
            Debug.Log("InitializeIngame");

            _curTurn = 1;

            // TODO お互いのユニットを読み込む
            _playerUnit = CreatePlayerUnit(GameConst.PLAYER);
            _opponentUnit = CreatePlayerUnit(GameConst.OPPONENT);
            
            // CPUの準備
            _cpu = new CpuController();
            _cpu.Initialize(GameConst.OPPONENT);

            UIManager.Instance.Initialize();

            // ターン終了の処理をボタンに設定
            UIManager.Instance.SetTurnEndAction(AttackPlayer);
            
            // お互いのデッキを読み込む
            List<CardMainModel> playerDeck = UserManager.Instance.GetModel().PlayerDeck.CardList;
            List<CardMainModel> opponentDeck = UserManager.Instance.GetModel().OpponentDeck.CardList;

            // お互いのデッキをDeckUnitに設定
            CardManager.Instance.SetPlayerDeck(playerDeck);
            CardManager.Instance.SetOpponentDeck(opponentDeck);
            UIManager.Instance.InitializePlayerStatusGroup(_playerUnit);
            UIManager.Instance.InitializeOpponentStatusGroup(_opponentUnit);

            // 手札を取得
            CardManager.Instance.DeckDraw(GameConst.PLAYER, GameConst.START_HAND_CARD);
            CardManager.Instance.DeckDraw(GameConst.OPPONENT, GameConst.START_HAND_CARD);
            
            // カメラの移動
            _camera.gameObject.transform.position = new Vector3(0.0f, 350.0f, -700.0f);
            _camera.gameObject.transform.rotation = Quaternion.identity;
            _camera.gameObject.transform.DOMove(new Vector3(0.0f, 155.0f, 0.0f), 2.0f).SetEase(Ease.InOutQuart);
            await _camera.gameObject.transform.DORotate(new Vector3(90.0f, 0.0f, 0.0f), 2.0f).AsyncWaitForCompletion();
        }

        // ゲーム開始
        private async void StartGame()
        {
            await InitializeIngame();
            
            // TODO
            Debug.Log("StartGame");

            // タイミング初期化
            _curTiming = TimingType.NONE;

            // UIの表示
            UIManager.Instance.SetCanvasActive(true);

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
            Debug.Log("EndGame");

            if (_playerUnit.IsDeath)
            {
                bool isRematch = false;
                Dictionary<string, Action> actions = new Dictionary<string, Action>();
                actions.Add(UK.Popup.Result.ResultPopup.RESULT_END_BUTTON, TransitionTitle);
                PopupManager.Instance.SetResultLosePopup(isRematch, actions);
                PopupManager.Instance.ShowResultLosePopup();
            }
            else
            {
                bool isRematch = false;
                Dictionary<string, Action> actions = new Dictionary<string, Action>();
                actions.Add(UK.Popup.Result.ResultPopup.RESULT_END_BUTTON, TransitionTitle);
                PopupManager.Instance.SetResultWinPopup(isRematch, actions);
                PopupManager.Instance.ShowResultWinPopup();
            }
        }

        // 自分のターン開始
        private void StartPlayerTurn()
        {
            Debug.Log("StartPlayerTurn");

            // タイミング：プレイヤーターンスタート
            _curTiming = TimingType.START_TURN_PLAYER;
            
            GetPlayerUnit(GameConst.PLAYER).UpdatePlayer();

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

        // 自分の攻撃
        private void AttackPlayer()
        {
            Debug.Log("AttackPlayer");

            // タイミング：相手の攻撃
            _curTiming = TimingType.END_ATTACK_PLAYER;

            // ターン最後の攻撃処理
            DoAttack(_playerUnit, _opponentUnit);

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

            if (_playerUnit.IsDeath || _opponentUnit.IsDeath)
            {
                EndGame();
            }
            else
            {
                StartOpponentTurn();
            }
        }

        // 相手のターン開始
        private void StartOpponentTurn()
        {
            Debug.Log("StartOpponentTurn");

            // タイミング：相手ターンスタート
            _curTiming = TimingType.START_TURN_OPPONENT;

            GetPlayerUnit(GameConst.OPPONENT).UpdatePlayer();

            // TODO ターンスタートアニメーション

            // 山札一枚ドロー
            CardManager.Instance.DeckDraw(GameConst.OPPONENT);

            // UI表示
            UIManager.Instance.SwitchTurnText(false);

            // 場の更新処理
            CardManager.Instance.GetCardBattleField(GameConst.OPPONENT).UpdateFieldByStartTurn();

            // タイミング：相手ターン
            _curTiming = TimingType.TURN_OPPONENT;


            _cpu.PlayTurn();
        }

        // 相手の攻撃
        private void AttackOpponent()
        {
            Debug.Log("AttackOpponent");

            // タイミング：相手の攻撃
            _curTiming = TimingType.END_ATTACK_OPPONENT;

            // ターン最後の攻撃処理
            DoAttack(_opponentUnit, _playerUnit);

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
            
            if (_playerUnit.IsDeath || _opponentUnit.IsDeath)
            {
                EndGame();
            }
            else
            {
                StartPlayerTurn();
            }
        }

        // プレイヤーの先手後手を決める
        private void decisionIsFirst()
        {
            // TODO 確定した先手後手を画面上に大きく描画する
            bool isFirst = UnityEngine.Random.Range(0, 2) == 0;
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
        
        // UserTypeからプレイヤーユニットを返す
        public PlayerUnit GetPlayerUnit(UserType userType)
        {
            switch (userType)
            {
                case UserType.USE_PLAYER:
                    return GetPlayerUnit(GameConst.PLAYER);
                case UserType.USE_OPPONENT:
                    return GetPlayerUnit(GameConst.OPPONENT);
                default:
                    return null;
            }
        }
        
        // 攻撃処理
        private void DoAttack(PlayerUnit attackUnit, PlayerUnit defenseUnit)
        {
            // ダメージの算出
            int atkValue = attackUnit.CalcAttackDamage();
            int damage = defenseUnit.CalcDefenseDamage(atkValue);

            defenseUnit.ReceiveDamage(damage);
        }
        
        // タイトル画面へ遷移
        private void TransitionTitle()
        {
            SceneLoadManager.Instance.TransitionScene("TitleScene");
        }







        // // 開発テスト用関数：相手ターン処理
        // private async void TestPlayOpponentTurn()
        // {
        //     await System.Threading.Tasks.Task.Delay(1000);
        //     Debug.Log("1");
        //     await System.Threading.Tasks.Task.Delay(1000);
        //     Debug.Log("2");
        //     await System.Threading.Tasks.Task.Delay(1000);
        //     Debug.Log("3");
        //     await System.Threading.Tasks.Task.Delay(1000);
        //     AttackOpponent();
        // }

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
        private PlayerUnit CreatePlayerUnit(bool isPlayer)
        {
            PlayerUnit unit = new PlayerUnit();
            unit.Initialize(isPlayer);
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

