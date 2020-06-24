using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VisionAndMotionPro
{
    /// <summary>
    /// 权限类
    /// </summary>
    class Permission
    {

        /// <summary>
        /// 当前权限等级
        /// </summary>
        private static PermissionLevel currentPermission = PermissionLevel.NoPermission;
        internal static PermissionLevel CurrentPermission
        {
            get { return Permission.currentPermission; }
            set
            {
                try
                {
                    Permission.currentPermission = value;
                    string loginInfo = string.Empty;
                    switch (value)
                    {
                        case PermissionLevel.NoPermission:
                                if(Configuration .enablePermissionControl )
                            loginInfo = (Configuration.language == Language.English ? "Not logged in, default to minimum permissions" : "未登录，默认为最低权限");
                            else
                                    loginInfo = (Configuration.language == Language.English ? "Not logged in, default to minimum permissions" : "未登录");
                            break;
                        case PermissionLevel.Operator:
                            loginInfo =(Configuration .language ==Language .English ?"Operator": "操作员");
                            break;
                        case PermissionLevel.Admin:
                            loginInfo =(Configuration .language ==Language .English ?"Admin": "管理员");
                            break;
                        case PermissionLevel.Developer:
                            loginInfo =(Configuration .language ==Language .English ?"Developer": "开发人员");
                            break;
                    }
                
                    Frm_Main.Instance.tss_permissionInfo.Text =(Configuration .language ==Language .English ?"当前登录：": "当前登录：") + loginInfo;
                }
                catch (Exception ex)
                {
                   LogHelper.SaveErrorInfo(ex);
                }
            }
        }


        /// <summary>
        /// 检查权限等级
        /// </summary>
        /// <param name="permission">能进行此操作的最低权限等级</param>
        /// <returns></returns>
        internal static bool CheckPermission(PermissionLevel permission)
        {
            if (!Configuration.enablePermissionControl)
                return true;
            if ((int)currentPermission < (int)permission)
            {
                Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Insufficient permissions, please login to a higher level of permissions and try again" : "权限不足，请登录更高一级权限后重试", Color.Red);
                return false;
            }
            return true;
        }

    }
    internal enum PermissionLevel
    {
        NoPermission,
        Operator,
        Admin,
        Developer,
    }
}
