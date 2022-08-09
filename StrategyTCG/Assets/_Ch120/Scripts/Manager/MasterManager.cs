using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ch120.Singleton;
using Ch120.Dao;

namespace Ch120.Manager.Master
{
    public class MasterManager : SingletonMonoBehaviour<MasterManager>
    {
        // ---------- 定数宣言 ----------
        
        // TODO Daoクラスの名前リスト（新しく作成したDaoクラスはここに記述する）
        protected static readonly string[] DAO_CLASS_NAME = {
            "CardMainDao",
            "CountryMainDao",
            "EffectMainDao",
            "EffectAbilityDao",
            "EffectGroupDao",
        };
        
        // TODO Daoクラスの名前空間
        protected readonly string DAO_CLASS_NAMESPACE = "UK.Dao.";
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        public BaseDao GetDao(string daoName)
        {
            return _daoDict[daoName];
        }
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private Dictionary<string, BaseDao> _daoDict = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        public void Initialize()
        {
            InitializeMaster();
        }
        
        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        
        // マスタ配列の初期化
        protected void InitializeMaster()
        {
            _daoDict = new Dictionary<string, BaseDao>();
            foreach (string daoName in GetDaoClassNameList())
            {
                Type daoType = Type.GetType(GetDaoClassNamespace() + daoName, true);
                BaseDao dao = (BaseDao)Activator.CreateInstance(daoType);
                dao.LoadJsonMasterList();
                _daoDict.Add(daoName, dao);
            }
        }
        
        // DAOクラス名の配列を返す
        protected string[] GetDaoClassNameList()
        {
            return DAO_CLASS_NAME;
        }
        
        // DAOクラスの名前空間を返す
        protected string GetDaoClassNamespace()
        {
            return DAO_CLASS_NAMESPACE;
        }
    }
}