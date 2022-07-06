using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Ch120.UI.Gauge;

using UK.Manager.Card;
using UK.Unit.Player;

namespace UK.UI.StatusGroup
{
    public class PlayerStatusGroup : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("HPゲージ")] private CommonGauge _hpGauge = default;
        [SerializeField, Tooltip("プレイヤー名")] private TextMeshProUGUI _nameText = default;
        [SerializeField, Tooltip("軍事力")] private TextMeshProUGUI _powerText = default;
        [SerializeField, Tooltip("国民数")] private TextMeshProUGUI _peopleNumText = default;
        [SerializeField, Tooltip("資金")] private TextMeshProUGUI _fundText = default;
        [SerializeField, Tooltip("ターン毎の獲得資金額")] private TextMeshProUGUI _turnNumText = default;
        [SerializeField, Tooltip("HP現在値")] private TextMeshProUGUI _curHpText = default;
        [SerializeField, Tooltip("HP最大値")] private TextMeshProUGUI _maxHpText = default;
        [SerializeField, Tooltip("山札現在値")] private TextMeshProUGUI _curDeckNumText = default;
        [SerializeField, Tooltip("山札最大値")] private TextMeshProUGUI _maxDeckNumText = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        public PlayerUnit PlayerUnit
        {
            get { return _playerUnit; }
            set { _playerUnit = value; }
        }
        
        public bool IsPlayer
        {
            get { return _isPlayer; }
        }

        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private PlayerUnit _playerUnit = default;
        private bool _isPlayer = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize(PlayerUnit unit, bool isPlayer)
        {
            _playerUnit = unit;
            _isPlayer = isPlayer;
            SetNameText(_playerUnit.Name);
            SetPowerText(_playerUnit.Power);
            SetPeopleNumText(_playerUnit.PeopleNum);
            SetFundText(_playerUnit.Fund);
            SetTurnNumText(_playerUnit.TurnFund);
            SetCurHpText(_playerUnit.CurHp);
            SetMaxHpText(_playerUnit.MaxHp);
            SetActiveHpText(_isPlayer);
            UK.Unit.Deck.DeckUnit deck = CardManager.Instance.GetCardBattleField(_isPlayer).GetDeckUnit();
            SetCurDeckNumText(deck.GetCardNum());
            SetMaxDeckNumText(deck.GetMaxCardNum());
        }

        // HPゲージの表示・非表示
        public void SetActiveHpGauge(bool b)
        {
            _hpGauge.SetActive(b);
        }

        // HPゲージのテキストを表示・非表示
        public void SetActiveHpText(bool b)
        {
            _hpGauge.SetActiveGroupUI(b);
        }

        // 名前テキストの設定
        public void SetNameText(string name)
        {
            _nameText.text = name;
        }

        // 軍事力テキストの設定
        public void SetPowerText(int power)
        {
            _powerText.text = power.ToString();
        }

        // 国民数テキストの設定
        public void SetPeopleNumText(int peopleNum)
        {
            _peopleNumText.text = peopleNum.ToString();
        }

        // 資金テキストの設定
        public void SetFundText(int fund)
        {
            _fundText.text = fund.ToString();
        }

        // ターン毎資金テキストの設定
        public void SetTurnNumText(int turnNum)
        {
            _turnNumText.text = turnNum.ToString();
        }

        // 現在値HPテキストの設定
        public void SetCurHpText(int curHp)
        {
            _curHpText.text = curHp.ToString();
        }

        // 最大値HPテキストの設定
        public void SetMaxHpText(int maxHp)
        {
            _maxHpText.text = maxHp.ToString();
        }

        // 山札現在値テキストの設定
        public void SetCurDeckNumText(int curDeckNum)
        {
            _curDeckNumText.text = curDeckNum.ToString();
        }

        // 山札最大値テキストの設定
        public void SetMaxDeckNumText(int maxDeckNum)
        {
            _maxDeckNumText.text = maxDeckNum.ToString();
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}


