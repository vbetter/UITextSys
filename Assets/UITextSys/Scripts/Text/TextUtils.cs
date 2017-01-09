using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class TextUtils
{
    /* 函数说明：打开一个xml配置文件*/
    public static XmlDocument OpenXml(string fileName)
    {
        string configPath = Application.persistentDataPath + "/Config/" + fileName;
        if (File.Exists(configPath) == false)
        {
            return null;
        }

        byte[] bytes = File.ReadAllBytes(configPath);

        if (bytes == null || bytes.Length <= 0)
        {
            Debug.LogError("can't load the xml file '" + fileName + "'");
            return null;
        }
        MemoryStream memStream = new MemoryStream();
        memStream.Write(bytes, 0, bytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(memStream);

        memStream.Close();
        memStream = null;
        return xmlDoc;
    }

    /* 函数说明：把BYTE数组转换成XML配置文件*/
    public static XmlDocument ByteConvertXml(byte[] bytes)
    {
        MemoryStream memStream = new MemoryStream();
        memStream.Write(bytes, 0, bytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(memStream);

        memStream.Close();
        memStream = null;
        return xmlDoc;
    }

    /* 函数说明：读取xml的int子节点 */
    public static int XmlReadInt(XmlNode node, string key, int def)
    {
        int result;
        try { result = int.Parse(node.Attributes[key].Value); }
        catch (Exception) { result = def; }
        return result;
    }

    /* 函数说明：读取xml的float子节点 */
    public static float XmlReadFloat(XmlNode node, string key, float def)
    {
        float result;
        try { result = float.Parse(node.Attributes[key].Value); }
        catch (Exception) { result = def; }
        return result;
    }

    /* 函数说明：读取xml的string子节点 */
    public static string XmlReadString(XmlNode node, string key, string def)
    {
        string result = "";
        try { result = node.Attributes[key].Value; }
        catch (Exception) { result = def; }
        return result;
    }
}
