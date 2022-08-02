using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UK.Model.EffectAbility;
using UK.Unit.Effect;

namespace UK.Unit.EffectList
{
    public class EffectUnitList
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private List<EffectUnit> _effectUnitList = default;
        
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize()
        {
            _effectUnitList = new List<EffectUnit>();
        }

        // カード効果の追加
        public void AddEffectUnit(EffectAbilityModel model)
        {
            EffectUnit effectUnit = new EffectUnit(model);
            _effectUnitList.Add(effectUnit);
        }
        
        // カード効果リストの取得
        public List<EffectUnit> GetEffectUnitList()
        {
            return _effectUnitList;
        }
        
        // 効果のターン毎の更新処理
        public void UpdateEffect()
        {
            List<EffectUnit> effectList = _effectUnitList;
            foreach (EffectUnit effect in effectList)
            {
                effect.UpdateTurn();
                if (effect.Turn == 0)
                {
                    _effectUnitList.Remove(effect);
                }
            }
        }
        
        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

