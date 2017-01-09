using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIGlobal
{
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


}
