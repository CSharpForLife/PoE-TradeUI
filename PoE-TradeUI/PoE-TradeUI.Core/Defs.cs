using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace PoE_TradeUI.Core {
    public static class Defs {

        public static List<ImageDef> ImageDefs;

        public static void Init() {
            ImageDefs = JsonConvert.DeserializeObject<List<ImageDef>>(File.ReadAllText("defs/imagedefs.json"));
        }

        public static ImageDef GetImageDefByName(string name) {
            return ImageDefs.FirstOrDefault(imageDef => imageDef.Name.Equals(name));
        }

    }
}
