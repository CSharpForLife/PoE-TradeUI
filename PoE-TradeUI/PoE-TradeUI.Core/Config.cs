using System.IO;
using System.Windows.Input;
using Newtonsoft.Json;

namespace PoE_TradeUI.Core {
    public static class Config {

        public static UserCfg UserConfig;

        static Config() {
            UserConfig = JsonConvert.DeserializeObject<UserCfg>(File.ReadAllText("Data/config.json"));
        }

        public class UserCfg {
            public Key Hotkey { get; set; }
        }
    }
}
