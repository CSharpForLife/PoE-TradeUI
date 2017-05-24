using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace PoE_TradeUI.Core.Defs {
    public static class Defs {

        public static List<ImageDef> ImageDefs;
        public static List<CursorDef> CursorDefs;
        public static List<ColourDef> ColourDefs;

        static Defs() {
            ImageDefs = Deserialize<List<ImageDef>>("image");
            CursorDefs = Deserialize<List<CursorDef>>("cursor");
            ColourDefs = Deserialize<List<ColourDef>>("colour");
        }

        private static T Deserialize<T>(string defFile) {
            return $"resources/defs/{defFile}defs.json".Deserialize<T>();
        }
        
        public static Def GetDefByName(string name) {
            var id = GetImageDefByName(name);
            if (id != null) return id;
            return GetCursorDefByName(name);
        }

        public static ImageDef GetImageDefByName(string name) {
            return ImageDefs.FirstOrDefault(imageDef => imageDef.Name.Equals(name));
        }

        public static CursorDef GetCursorDefByName(string name) {
            return CursorDefs.FirstOrDefault(cursorDef => cursorDef.Name.Equals(name));
        }

    }
}
