using System.Collections.Generic;
using System.Drawing;
using GameTools;


//打印等级
public enum ELogType
{
    //<<二进制左移运算符。左操作数的值向左移动右操作数指定的位数。
    //即2的二进制表示为10，向左移动1位即为100，十进制即为4
    None = 0,
    Normal = 2 << 1, //4
    Warning = 2 << 2, //8
    Error = 2 << 3, //16
    All = Normal | Warning | Error
}

public class Logger
{
    //日志输出类型
    private static ELogType logType = ELogType.All;
    private static Main _mainForm;

    public static Main MainForm
    {
        get { return _mainForm; }
        set
        {
            _mainForm = value;
            while (CacheLogs.Count > 0)
            {
                object[] log = CacheLogs.Dequeue();
                MainForm.Log(log[0] as string, (Color) log[1]);
            }
        }
    }

    public static void SetLogType(ELogType type)
    {
        logType = type;
    }

    /// <summary>
    /// 缓存消息的队列
    /// </summary>
    private static Queue<object[]> CacheLogs = new Queue<object[]>();

    /// <summary>
    /// 普通日志
    /// </summary>
    /// <param name="args"></param>
    public static void Log(params object[] args)
    {
        if ((logType & ELogType.Normal) == ELogType.Normal)
            Log(GetParamsStr(args), Color.White);
    }

    /// <summary>
    /// 警告
    /// </summary>
    /// <param name="args"></param>
    public static void LogWarning(params object[] args)
    {
        if ((logType & ELogType.Warning) == ELogType.Warning)
            Log(GetParamsStr(args), Color.Yellow);
    }

    /// <summary>
    /// 错误日志
    /// </summary>
    /// <param name="args"></param>
    public static void LogError(params object[] args)
    {
        if ((logType & ELogType.Error) == ELogType.Error)
            Log(GetParamsStr(args), Color.Red);
    }

    /// <summary>
    /// 操作行为日志
    /// </summary>
    /// <param name="args"></param>
    public static void LogAction(params object[] args)
    {
        Log(GetParamsStr(args), Color.LawnGreen);
    }

    /// <summary>
    /// 清除日志
    /// </summary>
    public static void Clean()
    {
        if (MainForm == null)
            CacheLogs.Enqueue(new object[] {null, Color.White});
        else
            MainForm.Log(null, Color.White);
    }

    private static void Log(string str, Color color)
    {
        if (MainForm == null)
            CacheLogs.Enqueue(new object[] {str, color});
        else
            MainForm.Log(str, color);
    }

    private static string GetParamsStr(params object[] args)
    {
        if (args == null)
        {
            return string.Empty;
        }

        string rtn = string.Empty;
        for (int i = 0; i < args.Length; i++)
        {
            rtn += (args[i] == null ? "null" : args[i].ToString()) + ",";
        }

        return rtn.TrimEnd(',');
    }
}