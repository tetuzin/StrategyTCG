using System.Collections.Generic;
using ShunLib.Utils.Json;

namespace ShunLib.Dao
{
    public interface BaseDao
    {
        void Initialize();
        void LoadJsonMasterList();
        
        void LoadJsonList(string jsontext);
        void SaveJsonMasterList();
    }

    public class BaseDao<T> : BaseDao where T : new()
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        protected List<T> _list = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // リストの初期化
        public void Initialize()
        {
            _list = new List<T>();
        }

        // Modelのリストを取得
        public List<T> Get()
        {
            return _list;
        }

        // Modelのリストを設定
        public void Set(List<T> list)
        {
            _list = list;
        }

        // JSONからデータを読み込みリストを返す
        virtual public void LoadJsonMasterList()
        {
            string filePath = GetJsonPath() + GetJsonFile();
            Set(JsonUtils.LoadResourceFile<T>(filePath));
        }
        
        // JSONからデータを読み込みリストを返す
        virtual public void LoadJsonList(string jsontext)
        {
            Set(JsonUtils.LoadJsonFile<T>(jsontext));
        }

        // リストを読み込みJSONに保存する
        virtual public void SaveJsonMasterList()
        {
            JsonUtils.SaveJsonList<T>(Get(), GetJsonFile());
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------

        // Jsonファイルのあるパス名を返す（継承先でOverrideする）
        virtual protected string GetJsonPath()
        {
            return "";
        }

        // JSONファイル名を返す（継承先でOverrideする）
        virtual protected string GetJsonFile()
        {
            return "";
        }
    }
}
