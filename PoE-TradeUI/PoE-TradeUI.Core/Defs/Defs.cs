using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace PoE_TradeUI.Core.Defs {
    public static class Defs {

        public static List<ImageDef> ImageDefs;
        public static List<CursorDef> CursorDefs;

        static Defs() {
            ImageDefs = JsonConvert.DeserializeObject<List<ImageDef>>(File.ReadAllText("resources/defs/imagedefs.json"));
            CursorDefs = JsonConvert.DeserializeObject<List<CursorDef>>(File.ReadAllText("resources/defs/cursordefs.json"));
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
