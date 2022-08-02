using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UK.Const.Ability;
using UK.Const.Effect;
using UK.Model.EffectAbility;

namespace UK.Unit.Effect
{
    public class EffectUnit
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        
        public AbilityType Ability
        {
            get { return _abilityType; }
        }
        
        public int Value
        {
            get { return _value; }
        }

        public int Turn
        {
            get { return _turn; }
        }
        
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        
        private AbilityType _abilityType = default;
        private int _value = default;
        private int _turn = default;
        
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        public EffectUnit() { }

        public EffectUnit(EffectAbilityModel model)
        {
            _abilityType = (AbilityType)model.AbilityType;
            _value = model.AbilityParameter1;
            _turn = model.AbilityParameter3;
        }

        // 持続ターン数の更新
        public void UpdateTurn()
        {
            if (_turn > 0)
            {
                --_turn;
            }
        }
        
        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

