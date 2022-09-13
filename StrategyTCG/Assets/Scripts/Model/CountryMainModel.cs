using System;
using System.Collections.Generic;
using UnityEngine;
using ShunLib.Model;

namespace UK.Model.CountryMain
{
    [Serializable]
    public class CountryMainModel : BaseModel
    {
        [SerializeField] private int _countryId = default;
        [SerializeField] private string _countryName = default;
        [SerializeField] private string _countryText = default;

        public int CountryId
        {
            get { return _countryId; }
            set { _countryId = value; }
        }
        public string CountryName
        {
            get { return _countryName; }
            set { _countryName = value; }
        }
        public string CountryText
        {
            get { return _countryText; }
            set { _countryText = value; }
        }
    }
}


