using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisionAndMotionPro
{
    [Serializable]
    class CircleCalibrationTool:FindCircleTool 
    {

        /// <summary>
        /// 毫米像素比
        /// </summary>
        private double _MMPixelRoute = 0.001;
        internal double MMPixelRoute
        {
            get
            {
                return Math .Round ( _MMPixelRoute,3);
            }
            set { _MMPixelRoute = value; }
        }

    }
}
