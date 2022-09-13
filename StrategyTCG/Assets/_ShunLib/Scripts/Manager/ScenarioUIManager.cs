using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ShunLib.Manager.UI;

namespace ShunLib.Manager.ScenarioUI
{
    public class ScenarioCanvasConst
    {
        public const int MAIN = 0;
    }
    
    public class ScenarioImageConst
    {
        public const int BACK_GROUND = 0;
    }

    public class ScenarioButtonConst
    {
        public const int WINDOW = 0;    // 画面タッチボタン
        public const int MENU = 1;      // メニューボタン
        public const int LOG = 2;       // ログ表示ボタン
        public const int HIDE = 3;      // テキスト非表示ボタン
        public const int SKIP = 4;      // スキップボタン
        public const int FAST = 5;      // 早送りボタン
        public const int AUTO = 6;      // オート再生ボタン
    }

    public class ScenarioUIManager : UIManager<ScenarioUIManager>
    {
        // 初期化
        public override void Initialize()
        {
            base.Initialize();

            SetBackgroundSize();
        }
    
        // 背景画像のサイズ設定
        private void SetBackgroundSize()
        {
            if (!IsBounds(ScenarioCanvasConst.MAIN, _canvasList.Count)) return;
            if (!IsBounds(ScenarioImageConst.BACK_GROUND, _imageList.Count)) return;

            RectTransform rectTransform = _canvasList[ScenarioCanvasConst.MAIN].GetComponent<RectTransform>();
            Vector2 rectTransSize = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);
            _imageList[ScenarioImageConst.BACK_GROUND].GetComponent<RectTransform>().sizeDelta = rectTransSize;
        }
    }
}

