/*
 * Author: CharSui
 * Created On: 2024.12.15
 * Description: 值类型常规判断
 */

namespace Util
{
    public static class ValueUtil
    {
     /// <summary>
     /// 判断当前值是不是二的次方
     /// </summary>
     /// <param name="value"></param>
     /// <returns></returns>
     public static bool IsPowerOfTwo(this int value)
        {
            // 检查正数，且 (value & (value - 1)) == 0 表示是2的次方
            return value > 0 && (value & (value - 1)) == 0;
        }

     /// <summary>
     ///     获取当前int值的下一个符合二进制的值。
     /// </summary>
     /// <param name="value"></param>
     /// <returns></returns>
     public static int NextPowerOfTwo(this int value)
        {
            if (value <= 0) return 1;

            // 如果输入值已经是 int 的最大值，则无法得到下一个2的次方
            if (value >= int.MaxValue) return int.MaxValue;

            value--;
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            return value + 1;
        }
    }
}