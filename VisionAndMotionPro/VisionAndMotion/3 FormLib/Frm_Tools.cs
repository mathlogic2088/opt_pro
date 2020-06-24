using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using VisionAndMotionPro.Properties;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;

namespace VisionAndMotionPro
{
    internal partial class Frm_Tools : DockContent
    {
        internal Frm_Tools()
        {
            InitializeComponent();
            Init_Language();
            this.tvw_jobs.ImageList = Job.imageList;
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_Tools _instance;
        internal static Frm_Tools Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_Tools();
                return _instance;
            }
        }


        /// <summary>
        /// 初始化语言
        /// </summary>
        private void Init_Language()
        {
            try
            {
                if (Configuration.language == Language.English)
                {
                    this.Text = "Toolbox";
                    lbl_info.Text = "Notes：Such tools are used for image acquisition";
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        private void UpdataTool(string jobName, ToolInfo toolInfo,int insert_pos = -1)
        {
            if(-1 == insert_pos)
            {
                //toolInfo._id.Add(0);
                Job.GetJobByName(jobName).L_toolList.Add(toolInfo);
            }
            else
            {
                Job.GetJobByName(jobName).L_toolList.Insert(insert_pos, toolInfo);
            }
        }

        /// <summary>
        /// 向流程中添加工具
        /// </summary>
        /// <param name="tool">工具类型</param>
        /// <param name="isInsert">插入位置，当为-1时，表示在末尾插入，当不为-1时，表示被插入的工具索引</param>
        internal void Add_Tool(string tool, int insertPos = -1)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Developer))
                    return;

                string jobName = Frm_Job.Instance.tbc_jobs.SelectedTab.Text;
                ToolInfo toolInfo = new ToolInfo();
                TreeNode toolNode = new TreeNode();
                switch (tool)
                {
                    case "Halcon采集接口":
                    case "ImageAcquistion":
                        HalconInterfaceTool halconInterfaceTool = new HalconInterfaceTool();
                        toolInfo.toolType = ToolType.HalconInterface;
                        toolInfo.tool = halconInterfaceTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "ImageAcquistion" : "Halcon采集接口");
                        if (toolInfo.toolName == "Error")       //此工具添加个数已达到上限，不让继续添加
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 1, 1);                                                    
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 1, 1);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        //添加必用项
                        TreeNode itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "-->OutputImage" : "-->输出图像", 26, 26);
                        itemNode.ForeColor = Color.Blue;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Image;
                        toolNode.ToolTipText = Configuration.language == Language.English ? "Graphic variables do not support display" : "图形变量不支持显示";
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).output.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输出图像", "", DataType.Image));
                        Project.GetJobTree(jobName).ShowNodeToolTips = true;
                        break;

                    case "SDK_巴斯勒":
                    case "SDK_Basler":
                        SDK_BaslerTool SDK_baslerTool = new SDK_BaslerTool();
                        toolInfo.toolType = ToolType.SDK_Basler;
                        toolInfo.tool = SDK_baslerTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "SDK_Basler" : "SDK_巴斯勒");
                        if (toolInfo.toolName == "Error")       //此工具添加个数已达到上限，不让继续添加
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 1, 1);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 1, 1);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "-->OutputImage" : "-->输出图像", 26, 26);
                        itemNode.ForeColor = Color.Blue;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Image;
                        toolNode.ToolTipText = Configuration.language == Language.English ? "Graphic variables do not support display" : "图形变量不支持显示";
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).output.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输出图像", "", DataType.Image));
                        Project.GetJobTree(jobName).ShowNodeToolTips = true;
                        break;

                    case "SDK_康耐视":
                    case "SDK_Cognex":
                        SDK_CongexTool SDK_congexTool = new SDK_CongexTool();
                        toolInfo.toolType = ToolType.SDK_Congex;
                        toolInfo.tool = SDK_congexTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "SDK_congexTool" : "SDK_康耐视");
                        if (toolInfo.toolName == "Error")       //此工具添加个数已达到上限，不让继续添加
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 1, 1);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 1, 1);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "-->OutputImage" : "-->输出图像", 26, 26);
                        itemNode.ForeColor = Color.Blue;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Image;
                        toolNode.ToolTipText = Configuration.language == Language.English ? "Graphic variables do not support display" : "图形变量不支持显示";
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).output.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输出图像", "", DataType.Image));
                        Project.GetJobTree(jobName).ShowNodeToolTips = true;
                        break;

                    case "SDK_灰点":
                    case "SDK_PointGray":
                        SDK_PointGrayTool SDK_pointGrayTool = new SDK_PointGrayTool();
                        toolInfo.toolType = ToolType.SDK_PointGray;
                        toolInfo.tool = SDK_pointGrayTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "SDK_PointGray" : "SDK_灰点");
                        if (toolInfo.toolName == "Error")       //此工具添加个数已达到上限，不让继续添加
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 1, 1);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 1, 1);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "-->OutputImage" : "-->输出图像", 26, 26);
                        itemNode.ForeColor = Color.Blue;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Image;
                        toolNode.ToolTipText = Configuration.language == Language.English ? "Graphic variables do not support display" : "图形变量不支持显示";
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).output.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输出图像", "", DataType.Image));
                        Project.GetJobTree(jobName).ShowNodeToolTips = true;
                        break;

                    case "SDK_大恒":
                    case "SDK_IMAVision":
                        SDK_IMAVisionTool SDK_imaVisionTool = new SDK_IMAVisionTool();
                        toolInfo.toolType = ToolType.SDK_IMAVision;
                        toolInfo.tool = SDK_imaVisionTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "SDK_imaVisionTool" : "SDK_大恒");
                        if (toolInfo.toolName == "Error")       //此工具添加个数已达到上限，不让继续添加
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 1, 1);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 1, 1);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "-->OutputImage" : "-->输出图像", 26, 26);
                        itemNode.ForeColor = Color.Blue;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Image;
                        toolNode.ToolTipText = Configuration.language == Language.English ? "Graphic variables do not support display" : "图形变量不支持显示";
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).output.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输出图像", "", DataType.Image));
                        Project.GetJobTree(jobName).ShowNodeToolTips = true;
                        break;

                    case "SDK_迈德威视":
                    case "SDK_MindVision":
                        SDK_MindVisionTool SDK_mindVisionTool = new SDK_MindVisionTool();
                        toolInfo.toolType = ToolType.SDK_MindVision;
                        toolInfo.tool = SDK_mindVisionTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "SDK_MindVisionTool" : "SDK_迈德威视");
                        if (toolInfo.toolName == "Error")       //此工具添加个数已达到上限，不让继续添加
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 1, 1);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 1, 1);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "-->OutputImage" : "-->输出图像", 26, 26);
                        itemNode.ForeColor = Color.Blue;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Image;
                        toolNode.ToolTipText = Configuration.language == Language.English ? "Graphic variables do not support display" : "图形变量不支持显示";
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).output.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输出图像", "", DataType.Image));
                        Project.GetJobTree(jobName).ShowNodeToolTips = true;
                        break;

                    case "SDK_海康威视":
                    case "SDK_HIKVison":
                        SDK_HIKVisionTool SDK_hiKVisionTool = new SDK_HIKVisionTool();
                        toolInfo.toolType = ToolType.SDK_HIKVision;
                        toolInfo.tool = SDK_hiKVisionTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "SDK_HIKVison" : "SDK_海康威视");
                        if (toolInfo.toolName == "Error")       //此工具添加个数已达到上限，不让继续添加
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 1, 1);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 1, 1);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);


                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "-->OutputImage" : "-->输出图像", 26, 26);
                        itemNode.ForeColor = Color.Blue;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Image;
                        toolNode.ToolTipText = Configuration.language == Language.English ? "Graphic variables do not support display" : "图形变量不支持显示";
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).output.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输出图像", "", DataType.Image));
                        Project.GetJobTree(jobName).ShowNodeToolTips = true;
                        break;

                    case "形状匹配":
                    case "ShapeMatch":
                        ShapeMatchTool shapeMatchTool = new ShapeMatchTool();
                        toolInfo.toolType = ToolType.ShapeMatch;
                        toolInfo.tool = shapeMatchTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "ShapeMatch" : "形状匹配");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 2, 2);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 2, 2);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "<--OutputImage" : "<--输入图像", 26, 26);
                        itemNode.ForeColor = Color.DarkMagenta;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Image;
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).input.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输入图像", "", DataType.Image));
                        break;

                    case "手眼标定":
                    case "EyeHandCalibration":
                        EyeHandCalibrationTool calibrationTool = new EyeHandCalibrationTool();
                        toolInfo.toolType = ToolType.EyeHandCalibration;
                        toolInfo.tool = calibrationTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "EyeHandCalibration" : "手眼标定");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 3, 3);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 3, 3);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);
                        break;

                    case "圆标定":
                    case "CircleCalibration":
                        CircleCalibrationTool circleCalibrationTool = new CircleCalibrationTool();
                        toolInfo.toolType = ToolType.CircleCalibration;
                        toolInfo.tool = circleCalibrationTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "CircleCalibration" : "圆标定");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 17, 17);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 17, 17);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        break;

                    case "减图像":
                    case "SubImage":
                        SubImageTool subImageTool = new SubImageTool();
                        toolInfo.toolType = ToolType.SubImage;
                        toolInfo.tool = subImageTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "SubImage" : "减图像");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 5, 5);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 5, 5);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        break;

                    case "斑点分析":
                    case "BlobAnalyse":
                        BlobAnalyseTool bolbAnalyseTool = new BlobAnalyseTool();
                        toolInfo.toolType = ToolType.BlobAnalyse;
                        toolInfo.tool = bolbAnalyseTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "BlobAnalyse" : "斑点分析");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 6, 6);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 6, 6);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);


                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "<--OutputImage" : "<--输入图像", 26, 26);
                        itemNode.ForeColor = Color.DarkMagenta;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Image;
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).input.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输入图像", "", DataType.Image));
                        break;

                    case "机械手下相机定位":
                    case "RobotDownCamAlign":
                        RobotDownCamAlignTool robotDownCamAlignTool = new RobotDownCamAlignTool();
                        toolInfo.toolType = ToolType.DownCamAlign;
                        toolInfo.tool = robotDownCamAlignTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "RobotDownCamAlign" : "机械手下相机定位");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 7, 7);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 7, 7);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        break;

                    case "彩图转RGB图":
                    case "FindLinex":
                        ColorToRGBTool colorToRGBTool = new ColorToRGBTool();
                        toolInfo.toolType = ToolType.ColorToRGB;
                        toolInfo.tool = colorToRGBTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "FindLine" : "彩图转RGB图");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 27, 27);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 27, 27);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);


                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "<--OutputImage" : "<--输入图像", 26, 26);
                        itemNode.ForeColor = Color.DarkMagenta;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Image;
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).output.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输入图像", "", DataType.Image));
                        break;

                    case "找边":
                    case "FindLine":
                        FindLineTool findLineTool = new FindLineTool();
                        toolInfo.toolType = ToolType.FindLine;
                        toolInfo.tool = findLineTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "FindLine" : "找边");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 9, 9);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 9, 9);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);


                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "<--OutputImage" : "<--输入图像", 26, 26);
                        itemNode.ForeColor = Color.DarkMagenta;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Image;
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).input.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输入图像", "", DataType.Image));
                        break;

                    case "找圆":
                    case "FindCircle":
                        FindCircleTool findCircleTool = new FindCircleTool();
                        toolInfo.toolType = ToolType.FindCircle;
                        toolInfo.tool = findCircleTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "FindCircle" : "找圆");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 10, 10);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 10, 10);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "<--OutputImage" : "<--输入图像", 26, 26);
                        itemNode.ForeColor = Color.DarkMagenta;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Image;
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).output.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输入图像", "", DataType.Image));
                        break;

                    case "创建ROI":
                    case "FindCirVVVcle":
                        CreateROITool CreateROITool = new CreateROITool();
                        toolInfo.toolType = ToolType.CreateROI;
                        toolInfo.tool = CreateROITool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "FindCircle" : "创建ROI");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 17, 17);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 17, 17);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        break;

                    case "创建位置":
                    case "FindCirVxVVcle":
                        CreatePositionTool createPositionTool = new CreatePositionTool();
                        toolInfo.toolType = ToolType.CreatePosition;
                        toolInfo.tool = createPositionTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "FindCircle" : "创建位置");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 17, 17);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 17, 17);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        break;

                    case "创建线":
                    case "FindCirVxXVVcle":
                        CreateLineTool createLineTool = new CreateLineTool();
                        toolInfo.toolType = ToolType.CreateLine;
                        toolInfo.tool = createLineTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "FindCircle" : "创建线");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 17, 17);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 17, 17);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        break;

                    //////case "拟合线":
                    //////case "FitLine":
                    //////    FitLineTool fitLineTool = new FitLineTool();
                    //////    toolInfo.toolType = ToolType.FindLine;
                    //////    toolInfo.tool = fitLineTool;
                    //////    toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "FitLineTool" : "拟合线");
                    //////    if (toolInfo.toolName == "Error")
                    //////    {
                    //////        return;
                    //////    }
                    //////    node = Job.GetJobByName(jobName).tvw_job.Nodes.Add("", toolInfo.toolName, 22, 22);
                    //////    break;

                    //////case "点线距离":
                    //////case "DistancePLTool":
                    //////    DistancePLTool distancePointLineTool = new DistancePLTool();
                    //////    toolInfo.toolType = ToolType.DistancePL;
                    //////    toolInfo.tool = distancePointLineTool;
                    //////    toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "DistancePLTool" : "点线距离");
                    //////    if (toolInfo.toolName == "Error")
                    //////    {
                    //////        return;
                    //////    }
                    //////    node = Job.GetJobByName(jobName).tvw_job.Nodes.Add("", toolInfo.toolName, 20, 20);
                    //////    break;

                    case "点线距离":
                    case "DistancePS":
                        DistancePLTool distancePLTool = new DistancePLTool();
                        toolInfo.toolType = ToolType.DistancePL;
                        toolInfo.tool = distancePLTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "DistanceSegmentSegment" : "点线距离");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 19, 19);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 19, 19);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        break;

                    case "线线距离":
                    case "DistanceSS":
                        DistanceLLTool distanceLLTool = new DistanceLLTool();
                        toolInfo.toolType = ToolType.DistanceSS;
                        toolInfo.tool = distanceLLTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "DistanceSegmentSegment" : "线线距离");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 8, 8);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 8, 8);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        break;

                    case "线线交点":
                    case "DistanceSSxxx":
                        LLPointTool llPointTool = new LLPointTool();
                        toolInfo.toolType = ToolType.LLPoint;
                        toolInfo.tool = llPointTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "DistanceSegmentSegment" : "线线交点");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 17, 17);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 17, 17);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "<--OutputImage" : "<--输入线段1", 26, 26);
                        itemNode.ForeColor = Color.DarkMagenta;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Line;
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).input.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输入线段1", "", DataType.Line));

                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "<--OutputImage" : "<--输入线段2", 26, 26);
                        itemNode.ForeColor = Color.DarkMagenta;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Line;
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).input.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输入线段2", "", DataType.Line));

                             itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "<--OutputImage" : "-->输出点", 26, 26);
                        itemNode.ForeColor = Color.Blue ;
                        toolNode.ExpandAll();                                                  
                        itemNode.Tag = DataType.Point ;
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).input.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输出点", "", DataType.Point ));

                        break;

                    case "OCR":
                        OCRTool ocrTool = new OCRTool();
                        toolInfo.toolType = ToolType.OCR;
                        toolInfo.tool = ocrTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "OCR" : "OCR");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 23, 23);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 23, 23);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "<--OutputImage" : "<--输入图像", 26, 26);
                        itemNode.ForeColor = Color.DarkMagenta;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Image;
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).input.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输入图像", "", DataType.Image));
                        break;

                    case "条码":
                    case "Barcode":
                        BarcodeTool barcodeTool = new BarcodeTool();
                        toolInfo.toolType = ToolType.Barcode;
                        toolInfo.tool = barcodeTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "Barcode" : "条码");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 24, 24);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 24, 24);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);


                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "<--OutputImage" : "<--输入图像", 26, 26);
                        itemNode.ForeColor = Color.DarkMagenta;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Image;
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).input .Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输入图像", "", DataType.Image));
                        break;

                    case "区域特征":
                    case "Barxxxcode":
                        RegionFeatureTool regionFeatureTool = new RegionFeatureTool();
                        toolInfo.toolType = ToolType.RegionFeature;
                        toolInfo.tool = regionFeatureTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "Barcode" : "区域特征");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 17, 17);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 17, 17);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "<--OutputImage" : "<--输入区域", 26, 26);
                        itemNode.ForeColor = Color.DarkMagenta;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Region;
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).input.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输入区域", "", DataType.Region));
                        break;

                    case "二维码":
                    case "Barcodexx":
                        QRCodeTool qrCodeTool = new QRCodeTool();
                        toolInfo.toolType = ToolType.QRCode;
                        toolInfo.tool = qrCodeTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "Barcode" : "二维码");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 25, 25);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 25, 25);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);


                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "<--OutputImage" : "<--输入图像", 26, 26);
                        itemNode.ForeColor = Color.DarkMagenta;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Image;
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).output.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输入图像", "", DataType.Image));
                        break;

                    case "基恩士SR1000":
                    case "Barcodexxxx":
                        KeyenceSR1000Tool keyenceSR1000Tool = new KeyenceSR1000Tool();
                        toolInfo.toolType = ToolType.KeyenceSR1000;
                        toolInfo.tool = keyenceSR1000Tool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "Barcode" : "基恩士SR1000");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 17, 17);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 17, 17);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);


                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "-->OutputImage" : "-->扫码结果", 26, 26);
                        itemNode.ForeColor = Color.Blue;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.Image;
                        toolNode.ToolTipText = Configuration.language == Language.English ? "Graphic variables do not support display" : "图形变量不支持显示";
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).output.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "扫码结果", "", DataType.String));
                        Project.GetJobTree(jobName).ShowNodeToolTips = true;
                        break;

                    case "脚本编辑":
                    case "CodeEdit":
                        CodeEditTool codeEditTool = new CodeEditTool();
                        toolInfo.toolType = ToolType.CodeEdit;
                        toolInfo.tool = codeEditTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "CodeEdit" : "脚本编辑");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 11, 11);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 11, 11);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);


                        break;

                    case "标签":
                    case "Label":
                        LabelTool labelTool = new LabelTool();
                        toolInfo.toolType = ToolType.Label;
                        toolInfo.tool = labelTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "Label" : "标签");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 12, 12);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 12, 12);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);


                        //添加必用项
                        itemNode = toolNode.Nodes.Add("", Configuration.language == Language.English ? "<--OutputImage" : "<--输入项1", 26, 26);
                        itemNode.ForeColor = Color.DarkMagenta;
                        toolNode.ExpandAll();
                        itemNode.Tag = DataType.String;
                        Job.GetToolInfoByToolName(jobName, Configuration.language == Language.English ? "HalconAcqInterface" : toolInfo.toolName).input.Add(new ToolIO(Configuration.language == Language.English ? "OutputImage" : "输入项1", "", DataType.String));
                        break;

                    case "输出":
                    case "Output":
                        //输出工具只运行添加一个，所以此处要判断是否已经存在了
                        if (Job.GetJobByName(jobName).Exist_Output())
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n输出工具已存在，此工具最多只能添加一个");
                            return;
                        }
                        ConditionTool outputBoxTool = new ConditionTool();
                        toolInfo.toolType = ToolType.Output;
                        toolInfo.tool = outputBoxTool;
                        toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "Output" : "输出");
                        if (toolInfo.toolName == "Error")
                        {
                            return;
                        }
                        if (insertPos == -1)
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 13, 13);
                        }
                        else
                        {
                            toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 13, 13);
                        }

                        this.UpdataTool(jobName, toolInfo, insertPos);

                        break;
                    case "条件选择":
                    case "Condition":
                        {                           
                            ConditionTool conditionTool = new ConditionTool();
                            toolInfo.toolType = ToolType.Condition;
                            toolInfo.tool = conditionTool;
                            toolInfo.toolName = Job.GetJobByName(jobName).GetNewToolName(Configuration.language == Language.English ? "Condition" : "条件选择");
                            if (toolInfo.toolName == "Error")
                            {
                                return;
                            }
                            if (insertPos == -1)
                            {
                                toolNode = Project.GetJobTree(jobName).Nodes.Add("", toolInfo.toolName, 13, 13);
                            }
                            else
                            {
                                toolNode = Project.GetJobTree(jobName).Nodes.Insert(insertPos, "", toolInfo.toolName, 13, 13);
                            }

                            this.UpdataTool(jobName, toolInfo, insertPos);

                        }
                        break;
                    default:
                        this.TopMost = false;
                        Frm_MessageBox.Instance.MessageBoxShow("\r\n此工具尚未开发！");
                        this.TopMost = true;
                        return;
                }
                Project.GetJobTree(jobName).Nodes[Project.GetJobTree(jobName).Nodes.Count - 1].EnsureVisible();
                toolNode.ToolTipText = Configuration.language == Language.English ? "Not_Start_Run" : "未运行";
                Frm_ImageWindow.Instance.cbx_toolRunResultImageList.Items.Add(toolInfo.toolName);
                Project.GetJobTree(jobName).ShowNodeToolTips = true;
                Application.DoEvents();
                Job.GetJobByName(Frm_Job.Instance.tbc_jobs.SelectedTab.Text).DrawLine();
                Frm_Main.Save(Frm_Job.Instance.tbc_jobs.SelectedTab.Text);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }


        private void Frm_Tools_Load(object sender, EventArgs e)
        {
            try
            {
                //图像采集主节点
                TreeNode imageAcaquisitionNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "AcqDevice" : "图像采集", 0, 0);
                {
                    imageAcaquisitionNode.Nodes.Add("", Configuration.language == Language.English ? "HalconAcqInterface" : "Halcon采集接口", 1, 1);
                    imageAcaquisitionNode.Nodes.Add("", Configuration.language == Language.English ? "SDK_Basler" : "SDK_巴斯勒", 1, 1);
                    imageAcaquisitionNode.Nodes.Add("", Configuration.language == Language.English ? "SDK_Cognex" : "SDK_康耐视", 1, 1);
                    imageAcaquisitionNode.Nodes.Add("", Configuration.language == Language.English ? "SDK_PointGray" : "SDK_灰点", 1, 1);
                    imageAcaquisitionNode.Nodes.Add("", Configuration.language == Language.English ? "SDK_HIKVision" : "SDK_大恒", 1, 1);
                    imageAcaquisitionNode.Nodes.Add("", Configuration.language == Language.English ? "SDK_MindVision" : "SDK_迈德威视", 1, 1);
                    imageAcaquisitionNode.Nodes.Add("", Configuration.language == Language.English ? "SDK_HIKVision" : "SDK_海康威视", 1, 1);
                }

                //图像处理
                TreeNode imageHandle = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "SaveImage" : "图像处理", 0, 0);
                {
                    imageHandle.Nodes.Add("", Configuration.language == Language.English ? "ShapeMatch" : "图像预处理", 17, 17);
                    imageHandle.Nodes.Add("", Configuration.language == Language.English ? "GrayMatch" : "图像存储", 17, 17);
                }

                //模板匹配主节点
                TreeNode matchNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "Match" : "匹配", 0, 0);
                {
                    matchNode.Nodes.Add("", Configuration.language == Language.English ? "ShapeMatch" : "形状匹配", 2, 2);
                    matchNode.Nodes.Add("", Configuration.language == Language.English ? "GrayMatch" : "灰度匹配", 16, 16);
                }

                //坐标变换主节点
                TreeNode CoordTransNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "CoorTrans" : "坐标变换", 0, 0);
                {
                    CoordTransNode.Nodes.Add("", Configuration.language == Language.English ? "EyeHandCalibration" : "手眼标定", 3, 3);
                }

                //标定
                TreeNode Calibration = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "Calibration" : "标定", 0, 0);
                {
                    Calibration.Nodes.Add("", Configuration.language == Language.English ? "CircleCalibration" : "圆标定", 17, 17);
                    Calibration.Nodes.Add("", Configuration.language == Language.English ? "RectCalibration" : "正方形标定", 17, 17);
                }

                //检测主节点
                TreeNode DetectionNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "Detection" : "检测", 0, 0);
                {
                    DetectionNode.Nodes.Add("", Configuration.language == Language.English ? "SubImage" : "减图像", 5, 5);
                }

                //特征主节点
                TreeNode CharacterNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "Detection" : "区域", 0, 0);
                {
                    CharacterNode.Nodes.Add("", Configuration.language == Language.English ? "SubImage" : "区域特征", 17, 17);
                    CharacterNode.Nodes.Add("", Configuration.language == Language.English ? "SubImage" : "区域运算", 17, 17);
                }

                //斑点分析主节点
                TreeNode BolbAnalyseNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "ThresholdSegmentation" : "阈值分割", 0, 0);
                {
                    BolbAnalyseNode.Nodes.Add("", Configuration.language == Language.English ? "BlobAnalyse" : "斑点分析", 6, 6);
                }

                //机械手定位主节点
                TreeNode AlignNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "RobotAlign" : "机械手定位", 0, 0);
                {
                    AlignNode.Nodes.Add("", Configuration.language == Language.English ? "RobotDownCamAlign" : "机械手下相机定位", 7, 7);
                    AlignNode.Nodes.Add("", Configuration.language == Language.English ? "RobotUpCameraAlign(EyeHandIntegral)" : "上相机定位工具(手眼一体)", 17, 17);
                    AlignNode.Nodes.Add("", Configuration.language == Language.English ? "RobotUpCameraAlign(EyeHandSeparation)" : "上相机定位工具(手眼分离)", 17, 17);
                }

                //图像转换主节点
                TreeNode ImageConvertNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "ImageConvert" : "图像转化", 0, 0);
                {
                    ImageConvertNode.Nodes.Add("", Configuration.language == Language.English ? "ColorToRGB" : "彩图转RGB图", 27, 27);
                }

                //查找与拟合主节点
                TreeNode FindAndFitNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "FindAndFit" : "查找与拟合", 0, 0);
                {
                    FindAndFitNode.Nodes.Add("", Configuration.language == Language.English ? "FindLine" : "找边", 9, 9);
                    FindAndFitNode.Nodes.Add("", Configuration.language == Language.English ? "FindCircle" : "找圆", 10, 10);
                    FindAndFitNode.Nodes.Add("", Configuration.language == Language.English ? "FitLine" : "拟合线", 21, 21);
                    FindAndFitNode.Nodes.Add("", Configuration.language == Language.English ? "FitCircle" : "拟合圆", 22, 22);
                }

                //创建主节点
                TreeNode CreateNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "FindAndFit" : "创建", 0, 0);
                {
                    CreateNode.Nodes.Add("", Configuration.language == Language.English ? "FindLine" : "创建ROI", 17, 17);
                    CreateNode.Nodes.Add("", Configuration.language == Language.English ? "FindLine" : "创建位置", 17, 17);
                    CreateNode.Nodes.Add("", Configuration.language == Language.English ? "FindLine" : "创建线", 17, 17);
                }

                //几何主节点
                TreeNode SegmentNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "Measurement" : "几何", 0, 0);
                {
                    SegmentNode.Nodes.Add("", Configuration.language == Language.English ? "DistancePP" : "点点距离", 18, 18);
                    SegmentNode.Nodes.Add("", Configuration.language == Language.English ? "DistancePL" : "点线距离", 19, 19);
                    SegmentNode.Nodes.Add("", Configuration.language == Language.English ? "AngleLL" : "线线角度", 20, 20);
                    SegmentNode.Nodes.Add("", Configuration.language == Language.English ? "DistanceSS" : "线线距离", 8, 8);
                    SegmentNode.Nodes.Add("", Configuration.language == Language.English ? "DistanceSS" : "线线交点", 17, 17);
                }

                //识别主节点
                TreeNode IdentityNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "Identity" : "识别", 0, 0);
                {
                    IdentityNode.Nodes.Add("", Configuration.language == Language.English ? "OCRTool" : "OCR", 23, 23);
                    IdentityNode.Nodes.Add("", Configuration.language == Language.English ? "OCVTool" : "OCV", 17, 17);
                    IdentityNode.Nodes.Add("", Configuration.language == Language.English ? "Barcode" : "条码", 24, 24);
                    IdentityNode.Nodes.Add("", Configuration.language == Language.English ? "2DUncode" : "二维码", 25, 25);
                }

                //扫码枪主节点
                TreeNode ScanerNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "Identity" : "扫码枪", 0, 0);
                {
                    ScanerNode.Nodes.Add("", Configuration.language == Language.English ? "OCRTool" : "基恩士SR1000", 17, 17);
                }

                //运算节点
                TreeNode CalculateNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "Operation" : "运算", 0, 0);
                {
                    TreeNode ArithmeticNode = CalculateNode.Nodes.Add("", Configuration.language == Language.English ? "Arithmetic" : "算术", 17, 17);
                }

                //光源主节点
                TreeNode LightNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "Light" : "光源", 0, 0);
                {
                    LightNode.Nodes.Add("", Configuration.language == Language.English ? "Light_OPT" : "欧普特光源控制", 17, 17);
                    LightNode.Nodes.Add("", Configuration.language == Language.English ? "Light_CST" : "康视达光源控制", 17, 17);
                    LightNode.Nodes.Add("", Configuration.language == Language.English ? "Light_LOTS" : "乐视光源控制", 17, 17);
                }

                //3D主节点
                TreeNode D3Node = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "3D" : "3D", 0, 0);

                //逻辑节点
                TreeNode ConditionNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "Condition" : "逻辑控制", 0, 0);
                {
                    ConditionNode.Nodes.Add("", Configuration.language == Language.English ? "Light_OPT" : "条件选择", 17, 17);
                }

                //C# 脚本编辑节点
                TreeNode CSharpCodeEdit = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "CodeEdit" : "脚本编辑", 11, 11);

                //标签显示节点
                TreeNode LabelNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "Label" : "标签", 12, 12);

                //输出节点
                TreeNode OutputNode = tvw_jobs.Nodes.Add("", Configuration.language == Language.English ? "Output" : "输出", 13, 13);

                //默认展开第一个图像采集节点
                this.tvw_jobs.GetNodeAt(0, 0).Expand();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void tvw_job_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (tvw_jobs.SelectedNode.Text)
            {
                case "图像采集":
                case "AcqDevice":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：Such tools are used for image acquisition" : "注释：此类工具用于图像的获取");
                    break;

                case "Halcon采集接口":
                case "HalconAcqInterface":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool supports two modes of reading images locally and collecting images from devices, which can be switched freely" : "注释：此工具支持从本地读取图像和从设备采集图像两种模式，可随意切换");
                    break;

                case "SDK_康耐视":
                case "SDK_Cognex":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is the Cognex Camera SDK Drawing Tool" : "注释：此工具为康耐视相机SDK采图工具");
                    break;

                case "SDK_灰点":
                case "SDK_PointGray":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is the gray point camera SDK drawing tool" : "注释：此工具为灰点相机SDK采图工具");
                    break;

                case "SDK_巴斯勒":
                case "SDK_Basler":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is the Mario Basler Camera SDK Drawing Tool" : "注释：此工具为巴斯勒相机SDK采图工具");
                    break;

                case "SDK_大恒":
                case "SDK_Baslxer":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is the Mario Basler Camera SDK Drawing Tool" : "注释：此工具为大恒相机SDK采图工具");
                    break;

                case "SDK_迈德威视":
                case "SDK_MindVision":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is a mapping tool of Medway Vision Camera SDK" : "注释：此工具为迈德威视相机SDK采图工具");
                    break;

                case "SDK_海康威视":
                case "SDK_HIKVision":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is the Hikvision Camera SDK Drawing Tool" : "注释：此工具为海康威视相机SDK采图工具");
                    break;

                case "图像处理":
                case "SDK_HIKVisixxon":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is the Hikvision Camera SDK Drawing Tool" : "注释：此类工具为图像处理类工具，包括预处理、图像本地存储等工具");
                    break;

                case "匹配":
                case "Match":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This type of tool is used for template matching to locate" : "注释：此类工具用于模板匹配来进行目标定位");
                    break;

                case "形状匹配":
                case "ShapeMatch":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used for shape matching positioning" : "注释：此工具用于形状匹配定位");
                    break;

                case "灰度匹配":
                case "GrayMatch":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used for gray matching positioning" : "注释：此工具用于灰度匹配定位");
                    break;

                case "坐标变换":
                case "CoorTrans":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This type of tool is used for coordinate system transformation" : "注释：此类工具用于坐标系变换");
                    break;

                case "手眼标定":
                case "EyeHandCalibration":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used to calibrate the manipulator and camera" : "注释：此工具用于标定图像坐标系和机械手坐标系");
                    break;

                case "坐标转化":
                case "CoorTransTool":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used for coordinate system transformation" : "注释：此工具用于坐标系变换");
                    break;

                case "标定":
                case "Calibration":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：Dimension calibration of such tools" : "注释：此类工具用于尺寸标定");
                    break;

                case "圆标定":
                case "CircleCalibration":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：Dimension Calibration Using Circular Calibration Target" : "注释：使用圆型标定板进行尺寸标定");
                    break;

                case "正方形标定":
                case "RectCalibration":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：Dimension Calibration Using Square Calibration Target" : "注释：使用正方形标定板进行尺寸标定");
                    break;

                case "检测":
                case "Detection":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：Such tools are used for feature detection" : "注释：此类工具用于特征检测");
                    break;

                case "减图像":
                case "SubImage":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool performs feature detection or recognition by subtracting two images" : "注释：此工具使用两图像相减的方式进行特征检测或识别");
                    break;

                case "阈值分割":
                case "ThresholdSegmentation":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This type of tool processes based on gray values" : "注释：此类工具基于灰度值做处理");
                    break;

                case "斑点分析":
                case "BlobAnalyse":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used for spot analysis" : "注释：此工具用于斑点分析");
                    break;

                case "机械手定位":
                case "RobotAlign":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：Such tools are related tools in manipulator positioning applications" : "注释：此类工具为机械手定位类应用中的相关工具");
                    break;

                case "机械手下相机定位":
                case "RobotDownCamAlign":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is an application tool for camera positioning under the machine" : "注释：此工具为机械手下相机定位应用工具，机械手在上，相机在下，相机从下方对机械手夹具上的产品进行定位");
                    break;

                case "上相机定位工具(手眼一体)":
                case "RobotUpCameraAlign(EyeHandIntegral)":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used for positioning, grabbing and placing the camera fixed on the robot hand" : "注释：此工具应用于机械手上固定相机进行定位抓取和放置的情况，一般相机固定在机械手第三轴或第四轴上，对待抓取产品进行定位，然后引导机械手抓取");
                    break;

                case "上相机定位工具(手眼分离)":
                case "RobotUpCameraAlign(EyeHandSeparation)":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used when the camera is on the same side as the manipulator but not fixed on the manipulator" : "注释：此工具应用于相机与机械手处于同一侧，但未固定在机械手上的情况");
                    break;

                case "图像转化":
                case "ImageConvert":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：Such tools are used for image conversion, such as color image conversion to gray image, etc" : "注释：此类工具用于图像转化，如彩图转化为灰度图等情况");
                    break;

                case "彩图转RGB图":
                case "ChannelConvert":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool can divide the color map into three color single channel maps" : "注释：此工具可将彩色图分成三种颜色的单通道图");
                    break;

                case "几何":
                case "Segment":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：Such tools are used for measurement" : "注释：此类工具为几何类工具");
                    break;

                case "点点距离":
                case "DistancePP":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used for point-to-point distance measurement" : "注释：此工具用于点与点距离测量");
                    break;

                case "点线距离":
                case "DistancePL":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used for measuring distance between points and lines" : "注释：此工具用于点与线距离测量");
                    break;

                case "线线角度":
                case "AngleLL":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used for line and line angle measurement" : "注释：此工具用于线与线角度测量");
                    break;

                case "线线距离":
                case "DistanceLL":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used for line-to-line distance measurement" : "注释：此工具用于线段与线段间距离测量");
                    break;

                case "查找与拟合":
                case "FindAndFit":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This type of tool is used for finding and fitting" : "注释：此类工具用于查找和拟合");
                    break;

                case "找边":
                case "FindLineTool":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used to find straight lines" : "注释：此工具用于查找边");
                    break;

                case "找圆":
                case "FindCircle":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used to find circles" : "注释：此工具用于查找圆");
                    break;

                case "拟合线":
                case "FitLine":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used to fit straight lines" : "注释：此工具用于拟合直线");
                    break;

                case "拟合圆":
                case "FitCircle":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used to fit circles" : "注释：此工具用于拟合圆");
                    break;

                case "识别":
                case "Identity":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：Such tools are used to identify" : "注释：此类工具用于识别");
                    break;

                case "OCR":
                case "OCRTool":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used for OCR recognition" : "注释：此工具用于OCR识别");
                    break;

                case "OCV":
                case "OCVTool":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used for OCV identification" : "注释：此工具用于OCV验证");
                    break;

                case "条码":
                case "Barcode":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：Such tools are used for bar code identification" : "注释：此工具用于条码识别");
                    break;

                case "二维码":
                case "2DUncode":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：Such tools are used for two-dimensional code identification" : "注释：此工具用于二维码识别");
                    break;

                case "运算":
                case "Operation":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This type of tool is used for arithmetic processing" : "注释：此类工具用于运算处理");
                    break;

                case "算术":
                case "Arithemtic":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used for mathematical arithmetic operations" : "注释：此工具用于数学算术运算");
                    break;

                case "光源":
                case "Light":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：These tools are used to control the Lighting controller of various brands" : "注释：此类工具用于各品牌光源控制器的控制");
                    break;

                case "欧普特光源控制":
                case "Light_OPT":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is the control tool of OPT Lighting controller" : "注释：此工具为欧普特光源控制器的控制工具");
                    break;

                case "乐视光源控制":
                case "Light_CST":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is Letv Lighting controller's control tool" : "注释：此工具为乐视光源控制器的控制工具");
                    break;

                case "康视达光源控制":
                case "Light_LOTS":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is the control tool of Conspicuous Lighting controller" : "注释：此工具为康视达光源控制器的控制工具");
                    break;

                case "3D":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：Such tools are used for 3D processing" : "注释：此类工具用于3D处理");
                    break;

                case "脚本编辑":
                case "CodeEdit":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used to edit CSharp scripts" : "注释：此工具用于编辑CSharp脚本");
                    break;

                case "标签":
                case "Label":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used to display string content on an image" : "注释：此工具用于在图像上显示字符串信息");
                    break;

                case "输出":
                case "Output":
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：This tool is used to add related items to the output collection for external invocation and retrieval" : "注释：此工具用于将相关项添加到输出集合，用于外部调用或获取");
                    break;

                default:
                    lbl_info.Text = (Configuration.language == Language.English ? "Notes：Unknown" : "注释：未知");
                    break;
            }
        }
        private void tvw_job_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (tvw_jobs.SelectedNode.SelectedImageIndex == 0)         //如果双击的是文件夹节点，返回
                    return;
                if (Frm_Job.Instance.tbc_jobs.TabPages.Count > 0)        //如果已存在流程
                {
                    Add_Tool(tvw_jobs.SelectedNode.Text);
                }
                else
                {
                    //如果当前不存在可用流程，先创建流程，在添加工具
                    Frm_Main.Instance.Create_New_Job();
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void 展开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvw_jobs.ExpandAll();
            tvw_jobs.SelectedNode = tvw_jobs.Nodes[0].Nodes[0];
            tvw_jobs.AutoScrollOffset = new System.Drawing.Point(0, 0);
        }
        private void 折叠所有节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvw_jobs.CollapseAll();
        }
        private void Frm_Tools_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        private void Frm_Tools_DoubleClick(object sender, EventArgs e)
        {

        }

    }
}
