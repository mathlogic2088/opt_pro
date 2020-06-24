using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace VisionAndMotionPro
{
   internal class RegexJudge
    {

       /// <summary>
       /// 为整数
       /// </summary>
       /// <param name="value"></param>
       /// <returns></returns>
        public static bool IsInt(string value)
        {
            return Regex.IsMatch(value, "^-?\\d+$");
        }

    }
}
