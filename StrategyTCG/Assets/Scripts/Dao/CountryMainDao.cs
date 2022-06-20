using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ch120.Dao;

using UK.Const.Resource;
using UK.Model.CountryMain;

namespace UK.Dao
{
    public class CountryMainDao : BaseDao<CountryMainModel>
    {
        // Jsonファイルのあるパス名を返す
        override protected string GetJsonPath()
        {
            return ResourceConst.JSON_PATH;
        }

        // JSONファイル名を返す
        override protected string GetJsonFile()
        {
            return ResourceConst.COUNTRY_MAIN_JSON;
        }
    }
}

