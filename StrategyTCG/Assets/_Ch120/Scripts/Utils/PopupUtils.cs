using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Ch120.Popup;

namespace Ch120.Utils.Popup
{
    public class PopupUtils : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        
        // モーダルウィンドウサイズ
        private static Vector3 WINDOW_SCALE = new Vector3(1920, 1080, 0);
        // モーダル名
        private static string MODAL_NAME = "ModalButton";
        // モーダルカラー
        private static Color MODAL_COLOR = new Color32 (0, 0, 0, 100);
        // 表示済みpopup名のリスト
        private static List<string> _openPopupNameList = new List<string>();

        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // ポップアップを開く
        public static void OpenPopup(
            GameObject parentObj,
            GameObject popupPrefab,
            Dictionary<string, Action> actions = null,
            dynamic param = null
            )
        {
            // 既に開いているポップアップは開かない
            if (CheckPopupName(popupPrefab)) { return; }

            // Popupインスタンス生成
            GameObject obj = Instantiate(popupPrefab, parentObj.transform.position, Quaternion.identity);
            BasePopup popup = obj.GetComponent<BasePopup>();
            obj.name = obj.name.Replace( "(Clone)", "" );

            // ポップアップが既に開いているなら開かない
            if (popup.CheckOpen()) { return; }

            // Popupをキャンバスの下に配置
            obj.transform.SetParent(parentObj.transform);

            // Popup表示
            popup.InitPopup(actions, param);
            popup.Open();
        }

        // Popup名をリストに追加
        public static void AddPopupName(GameObject obj)
        {
            _openPopupNameList.Add(obj.name);
        }

        // Popup名をリストから削除
        public static void RemovePopupName(GameObject obj)
        {
            _openPopupNameList.Remove(obj.name);
        }

        // 表示済みPopupに存在するか
        public static bool CheckPopupName(GameObject obj)
        {
            return _openPopupNameList.Contains(obj.name);
        }

        // モーダル生成
        public static GameObject CreateModal(GameObject parentObj)
        {
            // インスタンス生成
            GameObject modal = new GameObject(MODAL_NAME);
            modal.transform.localPosition = Vector3.zero;
            modal.transform.localScale = WINDOW_SCALE;
            modal.transform.SetParent(parentObj.transform);

            // コンポーネント追加
            modal.AddComponent<Button>();
            modal.AddComponent<Image>();

            // モーダル色設定
            Image image = modal.GetComponent<Image>();
            image.color = MODAL_COLOR;
            return modal;
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}