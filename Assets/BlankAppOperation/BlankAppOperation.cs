// /**
//  *　　　　　　　　┏┓　　　┏┓+ +
//  *　　　　　　　┏┛┻━━━┛┻┓ + +
//  *　　　　　　　┃　　　　　　　┃ 　
//  *　　　　　　　┃　　　━　　　┃ ++ + + +
//  *　　　　　　 ████━████ ┃+
//  *　　　　　　　┃　　　　　　　┃ +
//  *　　　　　　　┃　　　┻　　　┃
//  *　　　　　　　┃　　　　　　　┃ + +
//  *　　　　　　　┗━┓　　　┏━┛
//  *　　　　　　　　　┃　　　┃　　　　　　　　　　　
//  *　　　　　　　　　┃　　　┃ + + + +
//  *　　　　　　　　　┃　　　┃　　　　Code is far away from bug with the animal protecting　　　　　　　
//  *　　　　　　　　　┃　　　┃ + 　　　　神兽保佑,代码无bug　　
//  *　　　　　　　　　┃　　　┃
//  *　　　　　　　　　┃　　　┃　　+　　　　　　　　　
//  *　　　　　　　　　┃　 　　┗━━━┓ + +
//  *　　　　　　　　　┃ 　　　　　　　┣┓
//  *　　　　　　　　　┃ 　　　　　　　┏┛
//  *　　　　　　　　　┗┓┓┏━┳┓┏┛ + + + +
//  *　　　　　　　　　　┃┫┫　┃┫┫
//  *　　　　　　　　　　┗┻┛　┗┻┛+ + + +
//  *
//  *
//  * ━━━━━━感觉萌萌哒━━━━━━
//  * 
//  * 说明：
//  *       专用于Android 平台 安装/卸载/是否安装/打开 App 程序的插件脚本
//  *       文件列表：
//  *               .jar 文件一个

//该脚本的主要功能如下:
//		1)通过提供应用url标识,判断iOS中是否可以打开某个应用
//		2)通过提供应用url标识,来打开iOS中某一个应用
//	注意点:
//		1)url标识以string类型存在,但需遵守本地协议(://), 如test2://
//		2)导出后的Xcode工程需要在plist中设置LSApplicationQueriesSchemes(string Array),在内部标明允许跳转的url
//		3)在被跳转的应用内必须设置对应的URL Type(包含identifier和URL Schemes)
//		4)为了区分原生接口和Unity函数,所以在Unity函数前加U前缀,表示Unity调用原生
//  *               
//  * 文件名：BlankAppOperation.cs
//  * 创建时间：2016年07月12日 
//  * 创建人：Blank Alian
//  */


using UnityEngine;
#if UNITY_IOS
using System.Runtime.InteropServices;
#endif

public class BlankAppOperation : MonoBehaviour
{


#if UNITY_IOS



    [DllImport("__Internal")]
    private static extern bool CanOpenURL(string urlStr);

    [DllImport("__Internal")]
    private static extern void OpenURL(string urlStr);

#endif
#if UNITY_ANDROID
    private static AndroidJavaClass m_activityAndroidJavaClass;
    /// <summary>
    /// Plugins Activity 
    /// </summary>
    private static AndroidJavaClass ActivityJavaClass
    {
        get
        {
            if (m_activityAndroidJavaClass == null)
            {
                m_activityAndroidJavaClass = new AndroidJavaClass("com.alianhome.appoperation.MainActivity");
            }
            return m_activityAndroidJavaClass;
        }
    }

#endif

    /// <summary>
    /// 判断程序是否安装
    /// </summary>
    /// <param name="packageName">应用包名 / 通过提供应用url标识,判断iOS中是否可以打开某个应用</param>
    /// <returns>返回程序是否安装</returns>
    public static bool IsExist(string packageName)
    {

#if UNITY_IOS
		return CanOpenURL(packageName);
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
        return ActivityJavaClass.CallStatic<bool>("IsAvilible", packageName);
#endif
        return false;
    }


    /// <summary>
    /// 卸载APK # Android 有效
    /// </summary>
    /// <param name="packageName">应用包名</param>
    /// <returns>返回是否成功执行写作</returns>
    public static bool UninstallApp(string packageName)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return ActivityJavaClass.CallStatic<bool>("UninstallAPK", packageName);
#endif
        return false;
    }

    /// <summary>
    /// 安装App # Android 有效
    /// </summary>
    /// <param name="fileName">文件名存储路径。必须为可读路径</param>
    public static void InstallApp(string fileName)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        ActivityJavaClass.CallStatic("InstallAPK", fileName);
#endif
    }


    /// <summary>
    /// 获取App 的版本  1.0 2.0  # Android 有效
    /// </summary>
    /// <param name="packageName">包名</param>
    /// <returns></returns>
    public static string GetAppVersionName(string packageName)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return ActivityJavaClass.CallStatic<string>("getAppVersionName", packageName);
#endif
        return string.Empty;
    }

    /// <summary>
    /// 获取App 的版本代码  10000  # Android 有效
    /// </summary>
    /// <param name="packageName">包名</param>
    /// <returns></returns>
    public static string GetAppVersionCode(string packageName)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return ActivityJavaClass.CallStatic<string>("getAppVersionCode", packageName);
#endif
        return string.Empty;
    }


    /// <summary>
    /// 打开App
    /// </summary>
    /// <param name="packageName">Android:包名称/iOS 提供应用url标识 </param>
    /// <param name="activityName">Activity 名称 # Android 有效</param>
    /// <param name="paramsKey">参数KEY # Android 有效</param>
    /// <param name="paramValue">参数值 # Android 有效</param>
    /// <returns>返回App是否打开成功</returns>
    public static bool OpenApp(string packageName, string activityName, string paramsKey, string paramValue)
    {
#if UNITY_ANDROID
        return ActivityJavaClass.CallStatic<bool>("OpenAPP", packageName, activityName, paramsKey, paramValue);
#elif UNITY_IOS
        OpenURL(packageName);
        return true;
#else
        return false;
#endif

    }
}
