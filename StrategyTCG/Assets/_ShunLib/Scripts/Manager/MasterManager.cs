using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using ShunLib.Singleton;
using ShunLib.Dao;

namespace ShunLib.Manager.Master
{
    public class MasterManager : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
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

        public async Task Initialize()
        {
            await InitializeMaster();
        }
        
        // ---------- Private関数 ----------
        
        // マスタ配列の初期化
        private Task InitializeMaster()
        {
            _daoDict = new Dictionary<string, BaseDao>();
            foreach (string daoName in GetDaoClassNameList())
            {
                Type daoType = Type.GetType(GetDaoClassNamespace() + daoName, true);
                BaseDao dao = (BaseDao)Activator.CreateInstance(daoType);
                dao.LoadJsonMasterList();
                _daoDict.Add(daoName, dao);
            }

            return Task.CompletedTask;
        }
        
        // ---------- protected関数 ---------
        
        // DAOクラス名の配列を返す
        protected virtual string[] GetDaoClassNameList()
        {
            return new string[]{};
        }
        
        // DAOクラスの名前空間を返す
        protected virtual string GetDaoClassNamespace()
        {
            return "";
        }
    }
}