using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisionAndMotionPro
{

    /// <summary>
    /// 工具类型
    /// </summary>
    public enum ToolType
    {
        None,
        HalconInterface,
        SDK_Basler,
        SDK_Congex,
        SDK_PointGray,
        SDK_IMAVision,
        SDK_MindVision,
        SDK_HIKVision,
        ShapeMatch,
        EyeHandCalibration,
        CircleCalibration,
        SubImage,
        BlobAnalyse,
        FindLine,
        FindCircle,
        CreateROI,
        CreatePosition,
        CoorTrans,
        OCR,
        Barcode,
        RegionFeature,
        RegionOperation,
        QRCode,
        KeyenceSR1000,
        DownCamAlign,
        ColorToRGB,
        DistancePL,
        DistanceSS,
        LLPoint,
        CodeEdit,
        Label,
        Logic,
        Output,
        CreateLine,
        Condition
    }

    /// <summary>
    /// 工具的输入输出类
    /// </summary>
       [Serializable]
    public class ToolIO
    {
        public ToolIO() { }
        public ToolIO(string IOName1, object value1, DataType ioType1)
        {
            this.IOName = IOName1;
            this.value = value1;
            this.ioType = ioType1;
        }

        public string IOName;
        public object value;
        public DataType ioType;
    }

    /// <summary>
    /// 字符类型   白纸黑字|黑纸白字
    /// </summary>
    internal enum CharType
    {
        BlackChar,
        WhiteChar,
    }

    [Serializable]
    internal struct Label
    {
        internal string ValueType;
        internal string ExpectValue;
        internal string OutputItem;
        internal string PreAddStr;
        internal string Row;
        internal string Col;
        internal string Incolor;
        internal string Size;
        internal string DownLimit;
        internal string UpLimit;
        internal string OutColor;
    }

    public enum DataType
    {
        String,
        Region,
        Image,
        Point,
        Line,
        Circle,
        Pose,
    }
    public enum PLCBrand
    {
        Omron,
        Panasonic,
        Mitsubishi,
        Siemens,
        AB,
    }

    /// <summary>
    /// 运动控制卡类型 固高GTS系列|雷赛IOC0640系列
    /// </summary>
    public enum CardType
    {
        无,
        固高_GTS,
        雷赛_IOC0640,
        雷塞_DMC2210,
        雷塞_DMC2410,
    }

    /// <summary>
    /// 填充模式 填充|轮廓
    /// </summary>
    internal enum FillMode
    {
        Fill,
        Margin,
    }

    /// <summary>
    /// 区域类型
    /// </summary>
    internal enum RegionType
    {
        None,
        Rectangle1,
        Rectangle2,
        Circle,
        Ellipse,
        Ring,
        Any,
        无,
        矩形,
        仿射矩形,
        圆,
        椭圆,
        圆环,
        任意,
        InputRegion,        //此区域类型指来自于输入的区域
    }

    /// <summary>
    /// 语言 中文|英文
    /// </summary>
    public enum Language
    {
        Chinese,
        English,
    }

    /// <summary>
    /// 标定类型 四点|九点
    /// </summary>
    internal enum CalibrationType
    {
        Four_Point,
        Nine_Point,
    }

    public enum CommunicationType
    {
        None,
        Internet_Client,
        Internet_Sever,
        SerialPort,
        IO,
    }

    [Serializable]
    internal class Line
    {
        internal Point StartPoint;
        internal Point EndPoint;
        internal string ToShowTip()
        {
            return StartPoint.Row.ToString() + " | " + StartPoint.Col.ToString() + " | " + EndPoint.Row.ToString() + " | " + EndPoint.Col.ToString();
        }
        private HTuple _angle;
        public double Angle
        {
            get
            {
                HOperatorSet.AngleLx(StartPoint.Row, StartPoint.Col, EndPoint.Row, EndPoint.Col, out _angle);
                return _angle;
            }
        }
    }

    /// <summary>
    /// 采集设备
    /// </summary>
    [Serializable]
    internal class AcquistionDevice
    {
        internal HTuple Handle;
        internal string DeviceStr;
        internal string InterfaceType;
        internal string DeviceDescriptionStr;
        internal double Exposure;
        internal int MinExposure;
        internal int MaxExposure;
    }

    /// <summary>
    /// 条码识别工具结果结构
    /// </summary>
    [Serializable]
    internal struct RunResult
    {
        internal string ResultString;
        internal string BarcodeType;
        internal HObject Region;
        internal double Row;
        internal double Col;
        internal double Angle;
    }

    /// <summary>
    /// 通讯配置项
    /// </summary>
    [Serializable]
    internal struct CommunicationItem
    {
        internal string ReceivedCommand;
        internal string JobName;
        internal string OutputItem;
        internal string PrefixStr;
    }

    /// <summary>
    /// 模板匹配结果
    /// </summary>
    [Serializable]
    internal struct MatchResult
    {
        internal double Row;
        internal double Col;
        internal double Angle;
        internal double Socre;
    }

    /// <summary>
    /// 斑点分析工具结果
    /// </summary>
    [Serializable]
    internal struct BlobResult
    {
        internal double Row;
        internal double Col;
        internal double Area;
        internal double CircumcircleRadius;
        internal HObject region;
    }

    /// <summary>
    /// 预处理项
    /// </summary>
    [Serializable]
    internal class PreProcessing
    {
        internal string PreProcessingType;
        internal string ElementType;
        internal int ElementSize;
        internal int MinArea;
        internal int MaxArea;
        internal bool Enable;
    }

    /// <summary>
    /// XYU结果
    /// </summary>
    [Serializable]
    internal class PosXYU
    {
        internal double X;
        internal double Y;
        internal double U;
        /// <summary>
        /// 将XYU类型转化成格式化字符串
        /// </summary>
        /// <returns></returns>
        internal string ToFormatStr()
        {
            return (X >= 0 ? "+" + X.ToString("0000.000") : X.ToString("0000.000")) + "," + (Y >= 0 ? "+" + Y.ToString("0000.000") : Y.ToString("0000.000")) + "," + (U >= 0 ? "+" + U.ToString("0000.000") : U.ToString("0000.000"));
        }
        internal string ToShowTip()
        {
            return X.ToString() + " | " + Y.ToString() + " | " + U.ToString();
        }
    }

    /// <summary>
    /// 阈值分割后的筛选项
    /// </summary>
    [Serializable]
    internal struct SelectItem
    {
        internal string SelectType;
        internal Int64 AreaUpLimit;
        internal Int64 AreaDownLimit;
    }

    [Serializable]
    internal class Point
    {
        internal Point() { }
        internal Point(double x, double y)
        {
            this.Row = x;
            this.Col = y;
        }
        internal double Row;
        internal double Col;
        /// <summary>
        /// 重写 -
        /// </summary>
        /// <param name="p1">点1</param>
        /// <param name="p2">点2</param>
        /// <returns></returns>
        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.Row - p2.Row, p1.Col - p2.Col);
        }
        /// <summary>
        /// 获得点矢量长度
        /// </summary>
        internal double GetDistance
        {
            get
            {
                return Math.Sqrt(Row * Row + Col * Col);
            }
        }
        internal string ToShowTip()
        {
            return Row.ToString() + " | " + Col.ToString();
        }
    }

}
