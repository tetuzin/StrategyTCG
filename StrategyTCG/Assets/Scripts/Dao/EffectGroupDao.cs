using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ch120.Dao;

using UK.Const.Resource;
using UK.Model.EffectGroup;

namespace UK.Dao
{
    public class EffectGroupDao : BaseDao<EffectGroupModel>
    {
        // IDと一致した全データを取得
        public List<EffectGroupModel> GetModelAllById(int effectId)
        {
            return Get().FindAll(x => x.EffectId == effectId);
        }

        // Jsonファイルのあるパス名を返す
        override protected string GetJsonPath()
        {
            return ResourceConst.JSON_PATH;
        }

        // JSONファイル名を返す
        override protected string GetJsonFile()
        {
            return ResourceConst.EFFECT_GROUP_JSON;
        }
    }
}

