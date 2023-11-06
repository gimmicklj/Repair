using System;

namespace Repair.Core.Extensions.String
{
    /// <summary>
    /// 判断字符串是否为null或空
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 将字符串转换为 long，如果转换失败则返回默认值
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns>转换后的 long 值，或者默认值</returns>
        public static long ToLongOrDefault(this string str, long defaultValue = 0)
        {
            if (long.TryParse(str, out long result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 拆分为多个子字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="cut"></param>
        /// <returns></returns>
        public static List<string> JoinAsList(this string source, string cut)
        {
            if (string.IsNullOrWhiteSpace(source))
                return new List<string>();
            return source.Split(cut).ToList();
        }
    }
}
