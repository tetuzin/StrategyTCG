using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private string _name = default;
        private int _maxHp = default;
        private int _curHp = default;
        private int _power = default;
        private int _peopleNum = default;
        private int _fund = default;
        private int _turnFund = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

