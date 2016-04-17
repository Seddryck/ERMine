﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.Core.Modeling
{
    public class Attribute
    {
        public string Label { get; set; }
        public string DataType { get; set; }
        public bool IsNullable { get; set; }
        public KeyType Key { get; set; }
        public bool IsPartOfPrimaryKey
        {
            get { return Key == KeyType.Primary; }
            set { Key = value ? KeyType.Primary : KeyType.None; }
        }
        public bool IsPartOfPartialKey
        {
            get { return Key == KeyType.Partial; }
            set { Key = value ? KeyType.Partial : KeyType.None; }
        }
        public bool IsMultiValued { get; set; }
        public bool IsDerived { get; set; }
    }
}