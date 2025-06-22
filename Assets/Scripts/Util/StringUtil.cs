/*
 * Author: CharSui
 * Created On: 2024.12.10
 * Description: 定义一些对于常规string类使用相关的API
 */

namespace Util
{
    public static class StringUtil
    {
        /// <summary>
        /// 这两个方法只要是让代码写起来符合我们的说话逻辑
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string arg)
        {
            return string.IsNullOrEmpty(arg);
        }
    
        /// <summary>
        /// 这两个方法只要是让代码写起来符合我们的说话逻辑
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string arg)
        {
            return string.IsNullOrWhiteSpace(arg);
        }
    }
}
