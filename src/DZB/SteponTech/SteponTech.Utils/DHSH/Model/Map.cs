/*********************************
 * 作者：xzf
 * 日期：2017/7/10 20:12:01
 * 操作：创建
 * ********************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace SteponTech.Utils.DHSH.Model
{
    /// <summary>
    /// Map
    /// </summary>
    public class Map
    {
        List<Element> Data { get; set; }
        public Map() {
            Data = new List<Element>();
        }
        public void Add(string key, string value) {
            Data.Add(new Element { Key = key, Value = value });
        }

        public string Find(string key)
        {
            //var result = Data.AsQueryable().FirstOrDefault(e => e.Key == key);
            var result = Data.AsEnumerable().FirstOrDefault(e => e.Key == key);
            return result == null ? null : result.Value;
        }
    }
}
