using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

using ShunLib.Model;

namespace ShunLib.Utils.Json
{
    public class JsonUtils
    {
        // JSONファイルを読み込んでリストにして返す
        public static List<T> Load<T>(string jsonPath)
        {
            List<T> list = new List<T>();
            list.AddRange(JsonToArray<T>(LoadJson(jsonPath)));
            return list;
        }
        
        // オブジェクト(list)をJSONファイル形式で保存する
        public static void SaveJsonList<T>(List<T> list, string fileName)
        {
            string json = "[" + JsonUtility.ToJson (list) + "]";
            SaveJsonFile(json, fileName);
        }

        // オブジェクト(model)をJSONファイル形式で保存する
        public static void SaveJsonModel<T>(T model, string fileName)
        {
            string json = "[" + JsonUtility.ToJson (model) + "]";
            SaveJsonFile(json, fileName);
        }

        public static void SaveJsonFile(string json, string fileName)
        {
            // string path = Application.persistentDataPath + "/" + fileName + ".json";
            string path = "Assets/Resources/" + fileName + ".json";
            StreamWriter sw = new StreamWriter(path,false);
            sw.WriteLine(json);
            sw.Flush();
            sw.Close();
        }

        // PATHからJSONを読み込んで文字列にして返す
        public static string LoadJson(string path)
        {
            StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();
            reader.Close();
            return json;
        }

        // JSON形式を指定した型の配列に変換して返す
        public static T[] JsonToArray<T>(string json)
        {
            string jsonObject = "{\"item\":" + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(jsonObject);
            return wrapper.item;
        }

        // JSONファイル名からModelのListを生成する
        public static List<T> LoadResourceFile<T>(string file)
        {
            List<T> list = new List<T>();
            if (System.IO.File.Exists(@"Assets/Resources/" + file + ".json"))
            {
                list.AddRange(JsonToArray<T>(Resources.Load<TextAsset>(file).ToString()));
            }
            else
            {
                Debug.LogWarning(file + "は存在しません。");
            }
            return list;
        }
    }

    [Serializable]
    public class Wrapper<T>
    {
        public T[] item;
    }
}

