using System.Collections;
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

        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // ポップアップを開く
        public static void OpenPopup(
            GameObject canvas,
            GameObject popupPrefab,
            Dictionary<string, UnityAction> actions,
            dynamic param
            )
        {
            GameObject obj = Instantiate(popupPrefab, canvas.transform.position, Quaternion.identity);
            BasePopup popup = obj.GetComponent<BasePopup>();
            if (popup.CheckOpen()) { return; }
            GameObject modal = CreateModal(canvas);
            obj.transform.SetParent(canvas.transform);
            if (actions == null)
            {
                actions = new Dictionary<string, UnityAction>();
            }
            popup.InitPopup(actions, modal, param);
            popup.Open();
        }

        // ---------- Private関数 ----------

        // モーダル生成
        private static GameObject CreateModal(GameObject parentObj)
        {
            GameObject modal = new GameObject(MODAL_NAME);
            modal.transform.localPosition = Vector3.zero;
            modal.transform.localScale = WINDOW_SCALE;
            modal.transform.SetParent(parentObj.transform);
            modal.AddComponent<Button>();
            modal.AddComponent<Image>();
            Image image = modal.GetComponent<Image>();
            image.color = MODAL_COLOR;
            return modal;
        }

        // ---------- protected関数 ---------
    }
}