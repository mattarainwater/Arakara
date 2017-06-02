﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Card
{
    public class Card
    {
        public BattleAction Action { get; set; }
        public Grade Grade { get; set; }
    }

    public class Card<TEnum> : Card where TEnum : struct, IComparable, IFormattable
    {
        public TEnum Animation { get; set; }
    }
}
