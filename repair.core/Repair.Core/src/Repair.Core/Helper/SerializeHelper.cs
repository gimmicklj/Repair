using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace Repair.Core.Helper;

/// <summary>
/// JSON序列化
/// </summary>
public class SerializeHelper
{
    /// <summary>
    /// 序列化
    /// </summary>
    public static string SerializeObject(object item)
    {
        return JsonConvert.SerializeObject(item);
    }
    /// <summary>
    /// 序列化
    /// </summary>
    public static byte[] Serialize(object item)
    {
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),  //使用驼峰样式
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        var jsonString = JsonConvert.SerializeObject(item, settings);
        return Encoding.UTF8.GetBytes(jsonString);
    }
    /// <summary>
    /// 反序列化
    /// </summary>
    public static T? Deserialize<T>(byte[] value)
    {
        if (value == null)
        {
            return default;
        }
        var jsonString = Encoding.UTF8.GetString(value);
        return JsonConvert.DeserializeObject<T>(jsonString);
    }
}
