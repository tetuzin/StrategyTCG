using System;
using System.Collections.Generic;
using UnityEngine;
using Ch120.Model;

namespace UK.Model.CardMain
{
    [Serializable]
    public class CardMainModel : BaseModel
    {
        [SerializeField] private int _cardId = default;
        [SerializeField] private string _cardName = default;
        [SerializeField] private int _cardType = default;
        [SerializeField] private int _effectId = default;
        [SerializeField] private int _rarity = default;
        [SerializeField] private int _countryId = default;
        [SerializeField] private int _hp = default;
        [SerializeField] private int _attack = default;
        [SerializeField] private int _cost = default;
        [SerializeField] private string _text = default;
        [SerializeField] private string _year = default;
        [SerializeField] private string _image = default;

        public int CardId
        {
            get { return _cardId; }
            set { _cardId = value; }
        }
        public string CardName
        {
            get { return _cardName; }
            set { _cardName = value; }
        }
        public int CardType
        {
            get { return _cardType; }
            set { _cardType = value; }
        }
        public int EffectId
        {
            get { return _effectId; }
            set { _effectId = value; }
        }
        public int Rarity
        {
            get { return _rarity; }
            set { _rarity = value; }
        }
        public int CountryId
        {
            get { return _countryId; }
            set { _countryId = value; }
        }
        public int Hp
        {
            get { return _hp; }
            set { _hp = value; }
        }
        public int Attack
        {
            get { return _attack; }
            set { _attack = value; }
        }
        public int Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }
    }
}

