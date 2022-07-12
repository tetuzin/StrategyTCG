using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ch120.ScrollView
{
    public class CommonScrollView : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("ScrollRect")] private ScrollRect _scrollRect = default;
        [SerializeField, Tooltip("縦スクロールバー")] private Scrollbar _verticalScrollbar = default;
        [SerializeField, Tooltip("横スクロールバー")] private Scrollbar _horizontalScrollbar = default;
        [SerializeField, Tooltip("スクロールコンテンツ")] private GameObject _content = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize()
        {

        }

        // オブジェクトの追加
        public void AddContent(GameObject obj)
        {
            obj.transform.SetParent(_content.transform);
            obj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        }

        // 縦スクロールの設定
        public void SetVerticalScroll(bool b)
        {
            _scrollRect.vertical = b;
        }

        // 横スクロールの設定
        public void SetHorizontalScroll(bool b)
        {
            _scrollRect.horizontal = b;
        }

        // 縦スクロールの位置設定
        public void SetVerticalPosition(float position = 1.0f)
        {
            _scrollRect.verticalNormalizedPosition = position;
        }

        // 横スクロールの位置設定
        public void SetHorizontalPosition(float position = 1.0f)
        {
            _scrollRect.horizontalNormalizedPosition = position;
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

