using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

public class ReadSignPics
{
    public static Dictionary<int, SignPicsItem> DitSignPicsData = new Dictionary<int, SignPicsItem>();
    public static void GetSignPicsData()
    {
        Dictionary<int, SignPicsItem> resultManagerDic = new Dictionary<int, SignPicsItem>();
        XmlDocument xmlDoc = null;
		#if APPSTORE
		    xmlDoc = TextUtils.OpenXml("LocalConfig/Sign/SignPics_AppStore");
		#else
		    xmlDoc = TextUtils.OpenXml("LocalConfig/Sign/SignPics");
		#endif
        if (xmlDoc != null)
        {
			XmlNodeList pNodeList = xmlDoc.SelectNodes(string.Format("/SignPicsS/SignPics"));
            foreach (XmlNode pNode in pNodeList)
            {
                SignPicsItem item = new SignPicsItem();
                item.SelfData.id = UIGlobal.XmlReadInt(pNode, "id", 0);
                item.SelfData.name = UIGlobal.XmlReadString(pNode, "Name", "");
                item.SelfData.des = UIGlobal.XmlReadString(pNode, "Des", "");
                XmlNode pChildNode = pNode.ChildNodes[0];
                // 开始条件
                if (pChildNode.Name == "SignPicChilds")
                {
                    foreach (XmlNode startPNode in pChildNode)
                    {
                        if (startPNode.Name == "condition")
                        {
                            SignPicsData childData = new SignPicsData();
                            childData.id = UIGlobal.XmlReadInt(startPNode, "id", 0);
                            childData.name = UIGlobal.XmlReadString(startPNode, "Name", "");
                            childData.des = UIGlobal.XmlReadString(startPNode, "Des", "");
                            item.ChildData.Add(childData);
                        }
                    }
                }
                resultManagerDic[item.SelfData.id] = item;
            }
            xmlDoc = null;
        }
        DitSignPicsData = resultManagerDic;
    }
}

public class SignPicsItem
{
    public SignPicsData SelfData =new SignPicsData();
    public List<SignPicsData> ChildData =new List<SignPicsData>();
}
public struct SignPicsData
{
    public int id ;
    public string name ;
    public string des;
}