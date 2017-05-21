using System.Windows;

namespace PoE_TradeUI {

    public static class Constants {

        public static class Ui {

            public static double CaptionHeight = SystemParameters.CaptionHeight +
                                                 SystemParameters.WindowResizeBorderThickness.Left +
                                                 SystemParameters.WindowNonClientFrameThickness.Left;

            public static double BorderWidth = SystemParameters.WindowResizeBorderThickness.Left +
                                               SystemParameters.WindowNonClientFrameThickness.Left;

            public static double ScaleRatio = 1.62;
        }
    }
}
