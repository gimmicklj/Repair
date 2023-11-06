using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Core.Helper.Encrypts
{
    public static class EncryptHelper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strPwd">原字符串</param>
        /// <returns>加密后字符串</returns>
        public static string GetMD5(string strPwd)
        {
            //MD5 对象创建的两种方式
            //MD5 md5 = MD5.Create();
            MD5 md5 = new MD5CryptoServiceProvider();
            //将输入的密码转换成字节数组
            byte[] bPwd = Encoding.UTF8.GetBytes(strPwd);
            //计算指定字节数组的哈希值
            byte[] bMD5 = md5.ComputeHash(bPwd);
            //释放加密服务提供类的所有资源
            md5.Clear();
            StringBuilder sbMD5Pwd = new StringBuilder();
            for (int i = 0; i < bMD5.Length; i++)
            {
                //将每个字节数据转换为2位的16进制的字符
                sbMD5Pwd.Append(bMD5[i].ToString("x2"));
            }
            return sbMD5Pwd.ToString();
        }
    }
}
