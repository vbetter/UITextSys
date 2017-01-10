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

        UITextSys.Instance.ShowText(101);
	}
	
}
