using System.Windows;
using System.Windows.Forms;

namespace PoE_TradeUI.Core {
    public static class Constants {

        public static class Wpf {

            public static class Ui {

                public static double CaptionHeight = SystemParameters.CaptionHeight +
                                                     SystemParameters.WindowResizeBorderThickness.Left +
                                                     SystemParameters.WindowNonClientFrameThickness.Left;

                public static double BorderWidth = SystemParameters.WindowResizeBorderThickness.Left +
                                                   SystemParameters.WindowNonClientFrameThickness.Left;

                public static double ScaleRatio = 1.62;
            }
        }

        public static class WinForms {

            public static class Ui {
                public static int WindowBorderTop = SystemInformation.CaptionHeight + SystemInformation.VerticalResizeBorderThickness +
                                              SystemInformation.FrameBorderSize.Height;

                public static int WindowBorderLeft = SystemInformation.HorizontalResizeBorderThickness +
                                               SystemInformation.FrameBorderSize.Width;

                public static int WindowBorderHeight = SystemInformation.CaptionHeight +
                                                 SystemInformation.VerticalResizeBorderThickness * 2 +
                                                 SystemInformation.FrameBorderSize.Height * 2;

                public static int WindowBorderWidth = SystemInformation.HorizontalResizeBorderThickness +
                                                SystemInformation.FrameBorderSize.Width;

                public static double ScaleRatio => Wpf.Ui.ScaleRatio;
            }

        }
    }
}
