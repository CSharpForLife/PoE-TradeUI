using System.Text.RegularExpressions;

namespace PoE_TradeUI.Core {
    public static class CefUtils {

        public static string StyleScript(this string css, string id, bool minify = true) {
            if (minify) css = css.MinifyCss();
            return $@"
                (() => {{
                    if(document.getElementById('{id}')) return; 
                    let style = document.createElement('style'); 
                    style.id = '{id}'; 
                    style.innerText = '{css}'; 
                    document.head.append(style); 
                    console.log(style); 
                }})();";
        }

        public static string MinifyCss(this string css) {
            css = Regex.Replace(css, @"[a-zA-Z]+#", "#");
            css = Regex.Replace(css, @"[\n\r]+\s*", string.Empty);
            css = Regex.Replace(css, @"\s+", " ");
            css = Regex.Replace(css, @"\s?([:,;{}])\s?", "$1");
            css = css.Replace(";}", "}");
            css = Regex.Replace(css, @"([\s:]0)(px|pt|%|em)", "$1");
            css = Regex.Replace(css, @"/\*[\d\D]*?\*/", string.Empty);
            return css;
        }

    }
}
