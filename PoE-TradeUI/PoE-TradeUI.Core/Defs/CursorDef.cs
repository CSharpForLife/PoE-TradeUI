using System;

namespace PoE_TradeUI.Core.Defs {
    public class CursorDef : Def {

        public string Path() => $"{AppDomain.CurrentDomain.BaseDirectory}/Resources/Cursors/{File}.cur";

    }
}