using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ch120.Dao;

using UK.Const.Resource;
using UK.Model.EffectMain;

namespace UK.Dao
{
    public class EffectMainDao : BaseDao<EffectMainModel>
    {
        // IDと一致するデータを返す
        public EffectMainModel GetModelById(int effectId)
        {
            return Get().Find(x => x.EffectId == effectId);
        }
        
        // Jsonファイルのあるパス名を返す
        override protected string GetJsonPath()
        {
            return ResourceConst.JSON_PATH;
        }

        // JSONファイル名を返す
        override protected string GetJsonFile()
        {
            return ResourceConst.EFFECT_MAIN_JSON;
        }
    }
}

