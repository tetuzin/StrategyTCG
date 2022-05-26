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

        // TODO
        // Daoクラスの名前リスト（新しく作成したDaoクラスはここに記述する）
        private static readonly string[] DAO_CLASS_NAME = {
            "CardMainDao",
        };

        // TODO
        // Daoクラスの名前空間
        private readonly string DAO_CLASS_NAMESPACE = "UK.Dao.";

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

        // マスタ配列の初期化
        public void InitializeMaster()
        {
            _daoDict = new Dictionary<string, BaseDao>();
            foreach (string daoName in DAO_CLASS_NAME)
            {
                Type daoType = Type.GetType(DAO_CLASS_NAMESPACE + daoName, true);
                BaseDao dao = (BaseDao)Activator.CreateInstance(daoType);
                dao.LoadJsonMasterList();
                _daoDict.Add(daoName, dao);
            }
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}