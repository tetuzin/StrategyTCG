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
        private UserConfigModel _model = default;

        private const string _fileName = "UserConfig";

        // 初期化
        public void Initialize()
        {
            InitializeUserData();
        }

        // データの初期化
        private void InitializeUserData()
        {
            // string path = Application.persistentDataPath + "/" + _fileName + ".json";
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
        public void CreateUserData()
        {
            _model = new UserConfigModel();
            _model.WindowWidth = 1920;
            _model.WindowHeight = 1080;
            _model.VolumeBGM = 50;
            _model.VolumeSE = 50;
            _model.Fps = 60;
            SaveUserData();
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
    }
}