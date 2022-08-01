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
            set { _name = value; }
        }
        public int MaxHp
        {
            get { return _maxHp; }
            set { _maxHp = value; }
        }
        public int CurHp
        {
            get { return _curHp; }
            set { _curHp = value; }
        }
        public int Power
        {
            get { return _power; }
            set { _power = value; }
        }
        public int PeopleNum
        {
            get { return _peopleNum; }
            set { _peopleNum = value; }
        }
        public int Fund
        {
            get { return _fund; }
            set { _fund = value; }
        }
        public int TurnFund
        {
            get { return _turnFund; }
            set { _turnFund = value; }
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
                _curHp -= damage;
                UIManager.Instance.GetStatusGroup(_isPlayer).SetCurHpText(_curHp);
                Debug.Log(damage + "ダメージを与えた！");
            }
        }
        
        // HPを回復
        public void HealHp(int value)
        {
            if (value <= 0) return;

            _curHp += value;
            if (_curHp > _maxHp)
            {
                _curHp = _maxHp;
            }
            UIManager.Instance.GetStatusGroup(_isPlayer).SetCurHpText(_curHp);
            Debug.Log(value + "HPを回復！");
        }
        
        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

