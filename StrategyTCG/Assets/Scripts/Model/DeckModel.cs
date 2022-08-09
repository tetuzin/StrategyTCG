using System;
using System.Collections.Generic;
using UnityEngine;
using Ch120.Model;

using UK.Model.CardMain;

namespace UK.Model.Deck
{
    [Serializable]
    public class DeckModel : BaseModel
    {
        [SerializeField] private int _deckId = default;
        [SerializeField] private List<CardMainModel> _cardList = default;
        [SerializeField] private string _deckName = default;

        public int DeckId
        {
            get { return _deckId; }
            set { _deckId = value; }
        }
        
        public List<CardMainModel> CardList
        {
            get { return _cardList; }
            set { _cardList = value; }
        }
        
        public string DeckName
        {
            get { return _deckName; }
            set { _deckName = value; }
        }
    }
}