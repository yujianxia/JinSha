/*********************************
 * 作者：xzf
 * 日期：2017-06-18 00:12:31
 * 操作：创建
 * ********************************/
 using SteponTech.Utils.DHSH.Model.Config;
using System;
using System.Xml;

namespace SteponTech.Utils.DHSH.Utils
{
    public class XmlHelper
    {
        private static string _xmlfilepath;
        private XmlDocument _doc;
        private XmlReader _reader;
        public XmlHelper()
        {
            if (_xmlfilepath == null)
                _xmlfilepath = ConfigHelper.GetValue("XMLFilePath");
            _doc = new XmlDocument();
            var settings = new XmlReaderSettings { IgnoreComments = true };
            _reader = XmlReader.Create(_xmlfilepath, settings);
            _doc.Load(_reader);
        }

        public XmlRow GetConfig(string aeskey)
        {
            if (!_doc.HasChildNodes) return null;
            var nodes = _doc.ChildNodes;
            if (nodes.Count != 2) return null;
            var node = nodes[1];
            if (!node.HasChildNodes) return null;
            nodes = node.ChildNodes;
            if (nodes.Count != 12) return null;
            var result = new XmlRow();
            for (var i = 0; i < nodes.Count; i++)
            {
                var row = nodes[i];
                if (row == null) continue;
                var rowAttrs = row.Attributes;
                if (rowAttrs == null || rowAttrs.Count == 0 || rowAttrs.Count != 5) continue;
                var flag = true;
                for (var j = 0; j < rowAttrs.Count; j++)
                {
                    if (rowAttrs[j].Name != "id")
                        continue;
                    if (rowAttrs[j].Value == aeskey) {
                        flag = false;
                        break;
                    }
                }
                if (!flag)
                {
                    var v = "";
                    for (var j = 0; j < rowAttrs.Count; j++)
                    {
                        v = rowAttrs[j].Value;
                        if (rowAttrs[j].Name == "id") result.id = v;
                        else if (rowAttrs[j].Name == "key") result.key = v;
                        else if (rowAttrs[j].Name == "ivbase") result.ivbase = v;
                        else if (rowAttrs[j].Name == "seed") result.seed = v;
                        else if (rowAttrs[j].Name == "seq") result.seq = v;
                        else continue;
                    }
                    break;
                }
            }
            if (result.key == "" ||
                result.id == "" ||
                result.ivbase == "" ||
                result.seed == "" ||
                result.seq == "")
                return null;
            return result;
        }

        public XmlRow GetRandomConfig()
        {
            if (!_doc.HasChildNodes) return null;
            var nodes = _doc.ChildNodes;
            if (nodes.Count != 2) return null;
            var node = nodes[1];
            if (!node.HasChildNodes) return null;
            nodes = node.ChildNodes;
            if (nodes.Count != 12) return null;
            var ran = new Random();
            node = nodes[ran.Next(12)];
            if (node == null) return null;
            var attrs = node.Attributes;
            if (attrs == null || attrs.Count == 0 || attrs.Count != 5) return null;
            string value = "";
            var result = new XmlRow();
            for (var i = 0; i < attrs.Count; i++)
            {
                value = attrs[i].Value;
                if (attrs[i].Name == "id") result.id = value;
                else if (attrs[i].Name == "key") result.key = value;
                else if (attrs[i].Name == "ivbase") result.ivbase = value;
                else if (attrs[i].Name == "seed") result.seed = value;
                else if (attrs[i].Name == "seq") result.seq = value;
                else return null;
            }
            if (result.key == "" ||
                result.id == "" ||
                result.ivbase == "" ||
                result.seed == "" ||
                result.seq == "")
                return null;
            return result;
        }
    }
}
