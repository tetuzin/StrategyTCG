using System;
using System.Collections.Generic;
using UnityEngine;
using Ch120.Model;

using UK.Model.Deck;

namespace Ch120.Model.User
{
    [Serializable]
    public class UserConfigModel : BaseModel
    {
        [SerializeField] private int windowWidth;
        [SerializeField] private int windowHeight;
        [SerializeField] private float volumeSE;
        [SerializeField] private float volumeBGM;
        [SerializeField] private int fps;
        [SerializeField] private DeckModel playerDeck;
        [SerializeField] private DeckModel opponentDeck;
        public int WindowWidth
        {
            get { return windowWidth; }
            set { windowWidth = value; }
        }
        public int WindowHeight
        {
            get { return windowHeight; }
            set { windowHeight = value; }
        }
        public float VolumeSE
        {
            get { return volumeSE; }
            set { volumeSE = value; }
        }
        public float VolumeBGM
        {
            get { return volumeBGM; }
            set { volumeBGM = value; }
        }
        public int Fps
        {
            get { return fps; }
            set { fps = value; }
        }
        public DeckModel PlayerDeck
        {
            get { return playerDeck; }
            set { playerDeck = value; }
        }
        public DeckModel OpponentDeck
        {
            get { return opponentDeck; }
            set { opponentDeck = value; }
        }
    }
}