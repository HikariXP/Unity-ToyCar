/*
 * Author: CharSui
 * Created On: 2025.02.16
 * Description: 提供指令系统的一些静态辅助方法
 */
public static class InstructionHelper
{
    private static ulong _uidCount = 0;

    /// <summary>
    /// 会自动+1，理论上应该指令自己创建或者被创建的时候调用
    /// </summary>
    /// <returns></returns>
    public static ulong GetInstructionUid()
    {
        return _uidCount++;
    }
}
