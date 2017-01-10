using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace GameKit.UITextSys
{
    /// <summary>
    /// 文本类型
    /// </summary>
    public enum TextType
    {
        ScrollType = 1,
        WindowType = 2,
        JustString = 3,
        Dialog = 4,
        TextLabel = 5,
        TextTip = 6,
        GetType = 7,
    }

    /// <summary>
    /// 文本读取
    /// </summary>
    public class TextReader : MonoBehaviour
    {
        public static readonly string TextXMLPath = "/UITextSys/Config/Text.bytes";
        // Use this for initialization
        public static Dictionary<int, Text> mDict = new Dictionary<int, Text>();
        void Start()
        {
            Init();
            //Test();
        }

        // Update is called once per frame
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            Dictionary<int, Text> retDict = TextReader.ReadFromXml(TextXMLPath);
        }

        /// <summary>
        /// 文本读取
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public static Dictionary<int, Text> ReadFromXml(string _path)
        {
            XmlDocument xmlDoc = TextUtils.OpenXml(_path);

            if(xmlDoc == null || xmlDoc.DocumentElement == null)
            {
                Debug.LogError("Not Find Resource , Path: " + _path);
                return null;
            }

            XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;
            Dictionary<int, Text> retDict = new Dictionary<int, Text>();
            foreach (XmlNode node in nodeList)
            {
                //Debuger.Log(node.ChildNodes.Count + "," + node.Name);
                if(node.NodeType != XmlNodeType.Element)
                    continue;

                Text text = new Text();
                XmlElement elem = ((XmlElement)node);
                text.Id = int.Parse(elem.GetAttribute("id").ToString());
                text.Type = int.Parse(elem.GetAttribute("type").ToString());
                text.Content = elem.GetAttribute("content").ToString();

                // 这里为了支持换行符，要对字符串中的\n作特殊处理
                // Added by xubing@2016.02.23
                text.Content = text.Content.Replace("\\n", "\n");

                //Debuger.Log(text.Id + "," + text.Type + "," + text.Content);
                if (retDict.ContainsKey(text.Id) == false)
                {
                    retDict.Add(text.Id, text);
                }
            }
            mDict = retDict;
            xmlDoc = null;
            return retDict;
        }

        /// <summary>
        /// 获取特定id文本串
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public static Text GetTextById(int _id)
        {
            if (_id == null || mDict.Count <= 0)
                return null;
            Text text = new Text();
            if (mDict.TryGetValue(_id,out text))
            {
                Text retText = new Text();
                retText.Id = text.Id;
                retText.Type = text.Type;
                retText.Content = text.Content;
                retText.Args = text.Args;
                return retText;
            }
            return null;
        }

        /// <summary>
        /// 获取特定id的文本串。若未找到具有指定id的文本，则返回空串
        /// </summary>
        public static string GetText(int id)
        {
            Text textObj = GetTextById(id);
            if (textObj != null)
            {
                return textObj.Content;
            }
            else
            {
                return "";
            }
        }

        public void Test()
        {
            string strOutput = "";
            foreach(KeyValuePair<int,Text> curObj in mDict)
            {
                //Debuger.LogError(curObj.Key);
                //Debuger.LogError(curObj.Value.Content);
                string strText = "<Text id = " + curObj.Key + " type = " + curObj.Value.Type + " content = \"" + curObj.Value.Content + "\"/>\n";
                strOutput += strText;
            }
            Debug.LogError(strOutput);
        }
    }

}