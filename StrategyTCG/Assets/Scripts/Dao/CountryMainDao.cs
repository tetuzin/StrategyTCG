using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ShunLib.Dao;

using UK.Const.Resource;
using UK.Model.CountryMain;

namespace UK.Dao
{
    public class CountryMainDao : BaseDao<CountryMainModel>
    {
        // IDと一致するデータを返す
        public CountryMainModel GetModelById(int countryId)
        {
            return Get().Find(x => x.CountryId == countryId);
        }
        
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

