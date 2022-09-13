using System;
using System.Collections.Generic;
using UnityEngine;
using Ch120.Model;

namespace Ch120.Demo
{
    [Serializable]
    public class DemoMainModel : BaseModel
    {
        [SerializeField] private int _id = default;
        [SerializeField] private string _text = default;
        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
    }
}