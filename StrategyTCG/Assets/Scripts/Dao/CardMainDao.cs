using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ShunLib.Dao;

using UK.Const.Resource;
using UK.Model.CardMain;

namespace UK.Dao
{
    public class CardMainDao : BaseDao<CardMainModel>
    {
        // Jsonファイルのあるパス名を返す
        override protected string GetJsonPath()
        {
            return ResourceConst.JSON_PATH;
        }

        // JSONファイル名を返す
        override protected string GetJsonFile()
        {
            return ResourceConst.CARD_MAIN_JSON;
        }
    }
}

