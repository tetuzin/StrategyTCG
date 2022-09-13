using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ShunLib.Dao;

using UK.Const.Resource;
using UK.Model.EffectAbility;

namespace UK.Dao
{
    public class EffectAbilityDao : BaseDao<EffectAbilityModel>
    {
        // IDと一致するデータを取得
        public EffectAbilityModel GetModelById(int effectAbilityId)
        {
            return Get().Find(x => x.EffectAbilityId == effectAbilityId);
        }
        
        // Jsonファイルのあるパス名を返す
        override protected string GetJsonPath()
        {
            return ResourceConst.JSON_PATH;
        }

        // JSONファイル名を返す
        override protected string GetJsonFile()
        {
            return ResourceConst.EFFECT_ABILITY_JSON;
        }
    }
}

