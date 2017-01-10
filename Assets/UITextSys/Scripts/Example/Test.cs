/********************************************
-	    File Name: Test
-	  Description: 
-	 	   Author: lijing,<979477187@qq.com>
-     Create Date: Created by lijing on #CREATIONDATE#.
-Revision History: 
********************************************/

using UnityEngine;
using System.Collections;
using GameKit.UITextSys;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        UITextSys.Instance.Init();

        //TestGetText();
        //TestShowText();
        TestShowDialog();
	}
	
    /// <summary>
    /// 测试通过id获取字符串
    /// </summary>
    void TestGetText()
    {
        string msg = UITextSys.Instance.GetText(101);
        Debug.Log(msg);
    }

    /// <summary>
    /// 测试通过id显示tips
    /// </summary>
    void TestShowText()
    {
        UITextSys.Instance.ShowText(101);
    }

    /// <summary>
    /// 测试显示确定取消框
    /// </summary>
    void TestShowDialog()
    {
        UITextSys.Instance.ShowDialog(101, new Dialog(DialogType.twobutton).SureCallback(null, true));
    }
}
