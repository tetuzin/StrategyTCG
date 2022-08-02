using System.Collections;
using System.Collections.Generic;
using UK.Const.Ability;
using UK.Manager.UI;
using UK.Unit.Effect;
using UnityEngine;

using UK.Unit.EffectList;

namespace UK.Unit.Player
{
    public class PlayerUnit : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                UIManager.Instance.GetStatusGroup(_isPlayer).SetNameText(_name);
            }
        }
        public int MaxHp
        {
            get
            {
                // TODO カード効果の考慮
                return _maxHp;
            }
            set
            {
                _maxHp = value;
                UIManager.Instance.GetStatusGroup(_isPlayer).SetMaxHpText(_maxHp);
            }
        }
        public int CurHp
        {
            get
            { return _curHp; }
            set
            {
                _curHp = value;
                UIManager.Instance.GetStatusGroup(_isPlayer).SetCurHpText(_curHp);
            }
        }
        public int Power
        {
            get
            {
                // TODO カード効果の考慮
                return _power;
            }
            set
            {
                _power = value;
                UIManager.Instance.GetStatusGroup(_isPlayer).SetPowerText(_power);
            }
        }
        public int PeopleNum
        {
            get
            {
                // TODO カード効果の考慮
                return _peopleNum;
            }
            set
            {
                _peopleNum = value;
                UIManager.Instance.GetStatusGroup(_isPlayer).SetPeopleNumText(_peopleNum);
            }
        }
        public int Fund
        {
            get
            {
                // TODO カード効果の考慮
                return _fund;
            }
            set
            {
                _fund = value;
                UIManager.Instance.GetStatusGroup(_isPlayer).SetFundText(_fund);
            }
        }
        public int TurnFund
        {
            get
            {
                int turnFund = _turnFund;
                foreach (EffectUnit effect in _effectList.GetEffectUnitList())
                {
                    switch (effect.Ability)
                    {
                        case AbilityType.TURN_FUND_UP:
                            turnFund += effect.Value;
                            break;
                    }
                }
                return turnFund;
            }
            set
            {
                _turnFund = value;
                UIManager.Instance.GetStatusGroup(_isPlayer).SetTurnNumText(_turnFund);
            }
        }

        public EffectUnitList EffectList
        {
            get { return _effectList; }
        }

        public bool IsPlayer
        {
            get { return _isPlayer; }
        }

        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private string _name = default;
        private int _maxHp = default;
        private int _curHp = default;
        private int _power = default;
        private int _peopleNum = default;
        private int _fund = default;
        private int _turnFund = default;
        private EffectUnitList _effectList = default;
        private bool _isPlayer = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        public void Initialize()
        {
            _effectList = new EffectUnitList();
            _effectList.Initialize();
        }

        public void Initialize(bool isPlayer)
        {
            Initialize();
            _isPlayer = isPlayer;
        }
        
        // ターン毎の更新処理
        public void UpdatePlayer()
        {
            Fund += TurnFund;
            _effectList.UpdateEffect();
        }

        // 相手に与えるダメージ値を算出し返す
        public int CalcAttackDamage()
        {
            int damage = _power;
            return damage;
        }
        
        // 受けるダメージを算出する
        public int CalcDefenseDamage(int value)
        {
            int damage = value;
            damage -= _power;
            
            // 発動中効果
            foreach (EffectUnit effect in _effectList.GetEffectUnitList())
            {
                switch (effect.Ability)
                {
                    // プレイヤーが受けるダメージをN減少させる
                    case AbilityType.PLAYER_DAMAGE_DOWN:
                        Debug.Log("効果発動：プレイヤーの被ダメ-" + effect.Value);
                        damage -= effect.Value;
                        break;
                    
                    default:
                        break;
                }
            }

            if (damage <= 0)
            {
                damage = 0;
            }
            
            return damage;
        }

        // ダメージを受ける
        public void ReceiveDamage(int damage)
        {
            if (damage <= 0)
            {
                // TODO ダメージがない処理
                Debug.Log("ダメージを与えられなかった！");
            }
            else
            {
                // TODO ダメージを受ける処理
                if (_curHp > damage)
                {
                    CurHp -= damage;
                }
                else
                {
                    CurHp = 0;
                }
                Debug.Log(damage + "ダメージを与えた！");
            }
        }
        
        // HPを回復
        public void HealHp(int value)
        {
            if (value <= 0) return;

            int curHpValue = _curHp;
            curHpValue += value;
            if (curHpValue > _maxHp)
            {
                curHpValue = _maxHp;
            }
            CurHp = curHpValue;
            Debug.Log(value + "HPを回復！");
        }
        
        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

