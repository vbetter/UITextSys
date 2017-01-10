using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class UIGlobal
{
    public static int SCREEN_HEIGHT = 640;
    public static int SCREEN_WIDTH = 1136;


    static public void setParent(GameObject go, GameObject parent, bool updateLayer = false)
    {
        if (go == null || parent == null)
        {
            return;
        }

        if (go.transform.parent == parent)
        {
            return;
        }

        Transform t = go.transform;
        Vector3 localScale = t.localScale;
        Vector3 localPosition = t.localPosition;
        Quaternion localRotation = t.localRotation;

        t.parent = parent.transform;
        t.localPosition = localPosition;
        t.localRotation = localRotation;
        t.localScale = localScale;

        if (updateLayer)
        {
            setLayer(go, parent.layer);
        }
    }

    static public void setLayer(GameObject go, int layer)
    {
        go.layer = layer;
        foreach (Transform t in go.transform)
        {
            UIGlobal.setLayer(t.gameObject, layer);
        }
    }
    public static void myScaleFrom(GameObject obj)
    {
        iTween.ScaleFrom(obj, iTween.Hash("scale", new Vector3(0.3f, 0.3f, 0.3f), "time", 0.22f, "easeType", iTween.EaseType.easeOutBack));
    }

    public static void myScaleFrom(GameObject obj, string completeFun, GameObject completeObj)
    {
        iTween.ScaleFrom(obj, iTween.Hash("scale", new Vector3(0.3f, 0.3f, 0.3f), "time", 0.22f, "easeType", iTween.EaseType.easeOutBack, "oncomplete", completeFun, "oncompletetarget", completeObj));
    }

    public static void myScaleTo(GameObject obj,  string completeFun, GameObject completeObj)
    {
        iTween.ScaleTo(obj, iTween.Hash("scale", new Vector3(0.001f, 0.001f, 0.001f), "time", 0.28f, "easeType", iTween.EaseType.easeInBack, "oncomplete", completeFun, "oncompletetarget", completeObj));
    }

    public static void myMoveTox(GameObject obj, float x, bool islocal, string completeFun, GameObject completeObj)
    {
        iTween.MoveTo(obj, iTween.Hash("easeType", iTween.EaseType.easeInQuad, "time", 0.15f, "x", x, "islocal", islocal, "oncomplete", completeFun, "oncompletetarget", completeObj));
    }

    public static void myMoveTox(GameObject obj, float x, bool islocal)
    {
        iTween.MoveTo(obj, iTween.Hash("easeType", iTween.EaseType.easeInQuad, "time", 0.15f, "x", x, "islocal", islocal));
    }

    public static void myMoveToy(GameObject obj, float y, bool islocal, string completeFun, GameObject completeObj)
    {
        iTween.MoveTo(obj, iTween.Hash("easeType", iTween.EaseType.easeOutQuart, "time", 0.5f, "y", y, "islocal", islocal, "oncomplete", completeFun, "oncompletetarget", completeObj));
    }

    public static void myMoveToy(GameObject obj, float y, bool islocal)
    {
        iTween.MoveTo(obj, iTween.Hash("easeType", iTween.EaseType.easeOutQuart, "time", 0.15f, "y", y, "islocal", islocal));
    }

    public static void myFadeTo(GameObject obj, float alpha,string completeFun, GameObject completeObj)
    {
		iTween.FadeTo(obj,iTween.Hash("amount",alpha,"time",2f,"easetype", iTween.EaseType.linear, "oncomplete", completeFun, "oncompletetarget", completeObj));
    }

    #region 读取解析XML数据
    /************************************
     * 函数说明: 读取一个xml节点的int值
     * 返 回 值: int 
     ************************************/
    public static int XmlReadInt(XmlNode xmlNode, string key, int def)
    {
        int result = 0;
        try
        {
            result = def;
            if (xmlNode != null)
            {
                if (xmlNode.Attributes.GetNamedItem(key) != null)
                {
                    result = int.Parse(xmlNode.Attributes[key].Value);
                }
            }
        }
        catch (System.Exception)
        {
            result = def;
        }
        return result;
    }

    /************************************
     * 函数说明: 读取一个xml节点的float值
     * 返 回 值: float 
     ************************************/
    public static float XmlReadFloat(XmlNode xmlNode, string key, float def)
    {
        float result = 0f;
        try
        {
            result = def;
            if (xmlNode != null)
            {
                if (xmlNode.Attributes.GetNamedItem(key) != null)
                {
                    result = float.Parse(xmlNode.Attributes.GetNamedItem(key).Value);
                }
            }
        }
        catch (System.Exception)
        {
            result = def;
        }
        return result;
    }

    /************************************
     * 函数说明: 读取一个xml节点的long值
     * 返 回 值: float 
     ************************************/
    public static long XmlReadLong(XmlNode xmlNode, string key, long def)
    {
        long result = 0;
        try
        {
            result = def;
            if (xmlNode != null)
            {
                if (xmlNode.Attributes.GetNamedItem(key) != null)
                {
                    result = long.Parse(xmlNode.Attributes[key].Value);
                }
            }
        }
        catch (System.Exception)
        {
            result = def;
        }
        return result;
    }

    /************************************
     * 函数说明: 读取一个xml节点的string值
     * 返 回 值: string 
     ************************************/
    public static string XmlReadString(XmlNode xmlNode, string key, string def)
    {
        string result = "";
        try
        {
            result = def;
            if (xmlNode != null)
            {
                if (xmlNode.Attributes.GetNamedItem(key) != null)
                {
                    result = xmlNode.Attributes[key].Value;
                }
            }
        }
        catch (System.Exception)
        {
            result = def;
        }
        return result;
    }
    #endregion
}
