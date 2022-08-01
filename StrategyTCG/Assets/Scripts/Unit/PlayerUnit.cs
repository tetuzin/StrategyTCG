using System.Collections;
using System.Collections.Generic;
using UK.Manager.UI;
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
            get { return _maxHp; }
            set
            {
                _maxHp = value;
                UIManager.Instance.GetStatusGroup(_isPlayer).SetMaxHpText(_maxHp);
            }
        }
        public int CurHp
        {
            get { return _curHp; }
            set
            {
                _curHp = value;
                UIManager.Instance.GetStatusGroup(_isPlayer).SetCurHpText(_curHp);
            }
        }
        public int Power
        {
            get { return _power; }
            set
            {
                _power = value;
                UIManager.Instance.GetStatusGroup(_isPlayer).SetPowerText(_power);
            }
        }
        public int PeopleNum
        {
            get { return _peopleNum; }
            set
            {
                _peopleNum = value;
                UIManager.Instance.GetStatusGroup(_isPlayer).SetPeopleNumText(_peopleNum);
            }
        }
        public int Fund
        {
            get { return _fund; }
            set
            {
                _fund = value;
                UIManager.Instance.GetStatusGroup(_isPlayer).SetFundText(_fund);
            }
        }
        public int TurnFund
        {
            get { return _turnFund; }
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

