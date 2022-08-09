using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ch120.Utils.Json;
using Ch120.Singleton;
using Ch120.Model.User;

namespace Ch120.Manager.User
{
    public class UserManager : SingletonMonoBehaviour<UserManager>
    {
        // ---------- 定数宣言 ----------
        
        private const string _fileName = "UserConfig";
        
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        
        private UserConfigModel _model = default;
        
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        
        // 初期化
        public void Initialize()
        {
            InitializeUserData();
        }
        
        // データの読み込み
        public void LoadUserData()
        {
            List<UserConfigModel> list = JsonUtils.LoadResourceFile<UserConfigModel>(_fileName);
            SetModel(list[0]);
        }

        // データの保存
        public void SaveUserData()
        {
            JsonUtils.SaveJsonModel<UserConfigModel>(_model, _fileName);
        }

        // データモデルの設定
        public void SetModel(UserConfigModel model)
        {
            _model = model;
        }

        // データモデルの取得
        public UserConfigModel GetModel()
        {
            return _model;
        }
        
        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        
        // データの初期化
        protected void InitializeUserData()
        {
            string path = "Assets/Resources/" + _fileName + ".json";
            if (File.Exists(path))
            {
                LoadUserData();
            }
            else
            {
                CreateUserData();
            }
        }

        // データの作成
        protected void CreateUserData()
        {
            _model = new UserConfigModel();
            _model.WindowWidth = 1920;
            _model.WindowHeight = 1080;
            _model.VolumeBGM = 50;
            _model.VolumeSE = 50;
            _model.Fps = 60;
            SaveUserData();
        }
    }
}