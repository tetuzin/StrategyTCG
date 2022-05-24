using Ch120.Model;

namespace UK.Model.Card
{
    public class CardModel : BaseModel
    {
        private int _cardId = default;
        private string _cardName = default;
        private int _cardType = default;
        private string _summaryText = default;
        private string _effectText = default;
        private int _effectId = default;
        private int _imageId = default;
        private int _rarity = default;
        private int _hp = default;
        private int _attack = default;
        private int _cost = default;

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
        public string SummaryText
        {
            get { return _summaryText; }
            set { _summaryText = value; }
        }
        public string EffectText
        {
            get { return _effectText; }
            set { _effectText = value; }
        }
        public int EffectId
        {
            get { return _effectId; }
            set { _effectId = value; }
        }
        public int ImageId
        {
            get { return _imageId; }
            set { _imageId = value; }
        }
        public int Rarity
        {
            get { return _rarity; }
            set { _rarity = value; }
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
    }
}

