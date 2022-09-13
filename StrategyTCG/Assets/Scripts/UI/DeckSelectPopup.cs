using System;
using System.Collections.Generic;
using ShunLib.Manager.Game;
using UnityEngine;

using ShunLib.Popup.ScrollView;
using ShunLib.UI.ListItem;
using UK.Manager.User;
using UK.Manager.TitleUI;
using UK.Model.Deck;

namespace UK.Popup.DeckSelect
{
    public class DeckSelectPopup : ScrollViewPopup
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        
        [SerializeField, Tooltip("リストアイテムプレハブ")] private GameObject _listItemObj = default;
        
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private List<DeckModel> _deckList = default;
        private List<CommonListItem> _itemList = default;
        private CommonListItem _selectItem = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        // ---------- Private関数 ----------
        
        // デッキリストからリストアイテムを生成する
        private void SetListItem()
        {
            foreach (DeckModel model in _deckList)
            {
                GameObject obj = Instantiate(_listItemObj, Vector3.zero, Quaternion.identity);
                CommonListItem item = obj.GetComponent<CommonListItem>();
                item.Initialize(true);
                item.SetMainText(model.DeckName);
                _itemList.Add(item);
                item.SetItemSelectEvent(() =>
                {
                    if (item == _selectItem)
                    {
                        _selectItem = default;
                        foreach (CommonListItem listItem in _itemList)
                        {
                            listItem.SetGrayOut(false);
                        }
                    }
                    else
                    {
                        _selectItem = item;
                        GameManager.Instance.SetDeckModel(model);
                        foreach (CommonListItem listItem in _itemList)
                        {
                            listItem.SetGrayOut(true);
                        }
                        _selectItem.SetGrayOut(false);
                    }
                });
                SetContent(obj);
            }
        }
        
        // ---------- protected関数 ---------
        
        // 初期化
        protected override void Initialize()
        {
            base.Initialize();

            _itemList = new List<CommonListItem>();
        }
        
        // ボタンイベントの設定
        protected override void SetButtonEvents()
        {
            if (_decisionButton != default)
            {
                _decisionButton.SetOnEvent(() => {
                    if (_selectItem != default)
                    {
                        Action action = GetAction(DECISION_BUTTON_EVENT);
                        action();
                        Close();
                    }
                    else
                    {
                        TitleUIManager.Instance.ShowSimplePopup("デッキを選択してください！");
                    }
                });
            }
            
            if (_cancelButton != default)
            {
                _cancelButton.SetOnEvent(() => {
                    Action action = GetAction(CANCEL_BUTTON_EVENT);
                    action();
                    Close();
                });
            }
        }

        // データの設定
        protected override void SetData(dynamic param)
        {
            var parameter = new
            {
                titleText = param.titleText,
                mainText = param.mainText,
                decisionText = param.decisionText,
                cancelText = param.cancelText,
                verticalScroll = param.verticalScroll,
                horizontalScroll = param.horizontalScroll,
                deckList = param.deckList
            };

            _deckList = parameter.deckList;
            SetListItem();

            base.SetData(parameter);
        }
    }
}

