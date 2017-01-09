using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UITextSys
{
    public class UIText : MonoBehaviour
    {

        // Use this for initialization
        /// <summary>
        /// 当前文本
        /// </summary>
        public string mContent;
        /// <summary>
        /// 当前类型
        /// </summary>
        public string mType;
        /// 坐标
        public Vector3 mPos;
        //坐标类型
        public int mPosType = 0;

        public bool fading;
        /// <summary>
        /// 显示文本
        /// </summary>
        public void OnShow()
        {
            if (mContent == null || mContent == "")
                return;
            Init();
        }

        /// <summary>
        /// 当前窗口关闭回调函数
        /// </summary>
        public void OnClose()
        {
            UIGlobal.myScaleTo(gameObject, "OnTweenScaleDone", gameObject);
        }

        /// <summary>
        /// 缩放窗口完成回调函数
        /// </summary>
        public void OnTweenScaleDone()
        {
            Free();
        }

        /// <summary>
        /// 缓动窗口回调函数
        /// </summary>
        public void Free()
        {
            //Debug.LogError("Free");
            //Transform tran = transform.parent;
            gameObject.SetActive(false);
            if (int.Parse(mType) == (int)TextType.TextTip)
            {
                //Debug.LogError("Free TextType.GetType");
                fading = true;
                return;
            }
            //ResourcePool.Instance.FreeObject(gameObject);
            iTween it = gameObject.GetComponent<iTween>();
            if (it != null)
            {
                GameObject.Destroy(it);
            }
        }

        /// <summary>
        /// 淡出窗口回调函数
        /// </summary>
        public void OnTweenAlphaDone()
        {
            //Debug.LogError("OnTweenAlphaDone");
            Free();
        }

        /// <summary>
        /// 缓动窗口回调函数
        /// </summary>
        public void OnTweenPositionDone()
        {
            Debug.Log("OnTweenAlphaDone");
            Free();
        }

        public void OnEnable()
        {
            ////Init();
            ////OnShow();
        }

        /// <summary>
        /// 初始化窗口属性
        /// </summary>
        public void Init(int screenHeight, int screenWidth)
        {
            ///滚动类型窗口
            if (int.Parse(mType) == (int)TextType.ScrollType || int.Parse(mType) == (int)TextType.GetType)
            {
                if (mPos != Vector3.zero)
                {
                    //局部坐标
                    if (mPosType == 1)
                    {
                        transform.localPosition = mPos + new Vector3(0, 30f, 0);
                    }
                    else
                    {
                        //世界坐标
                        transform.localPosition = transform.parent.InverseTransformPoint(mPos) + new Vector3(0, 30f, 0);
                    }
                }

                float endPos = 0;
                if (transform.localPosition.y + screenHeight / 8 + 10 > screenHeight / 4)
                {
                    endPos = screenHeight / 4 - 10f;
                }
                else
                {
                    endPos = transform.localPosition.y + screenHeight / 8;
                }
                myMoveToy(gameObject, endPos, true, "OnTweenPositionDone", gameObject);
                myFadeTo();
            }
            ///弹出类型窗口
            else if (int.Parse(mType) == (int)TextType.WindowType)
            {
                gameObject.transform.localPosition = Vector3.zero;
                UIGlobal.myScaleFrom(gameObject);
            }
            Transform labelTran = gameObject.transform.Find("Content");
            if (labelTran != null)
            {
                UILabel label = labelTran.gameObject.GetComponent<UILabel>();
                if (label != null)
                {
                    label.text = mContent;
                    if (int.Parse(mType) > 0 && int.Parse(mType) != (int)TextType.WindowType)
                    {
                        Transform curBgTrans = gameObject.transform.Find("Bg");
                        if (curBgTrans != null)
                        {
                            UISprite sprite = curBgTrans.GetComponent<UISprite>();
                            if (sprite != null)
                            {
                                sprite.width = label.width + 225;
                            }
                        }
                    }
                }
            }
            UIWidget uiwidget = gameObject.GetComponent<UIWidget>();
            if (uiwidget != null)
            {
                uiwidget.alpha = 1f;
            }
            fading = false;
        }

        /// <summary>
        /// 替换文本，使用匹配%s进行文本替换
        /// </summary>
        /// <param name="_src">原文本</param>
        /// <param name="_args">替换文本参数</param>
        /// <returns></returns>
        public static string ReplaceStringByValue(string _src, params object[] _args)
        {
            if (string.IsNullOrEmpty(_src))
                return "";
            _src.Trim();
            if (_src.IndexOf("%s") < 0)
                return _src;
            int index = 0;
            int startIndex = 0;
            string retStr = "";
            foreach (object curArg in _args)
            {
                startIndex = _src.IndexOf("%s", index);
                if (startIndex < 0)
                    break;
                string curStr = _src.Substring(index, startIndex - index);
                retStr += curStr;
                retStr += curArg.ToString();
                index = startIndex + 2;
            }
            retStr += _src.Substring(index);
            return retStr;
        }

        public void myMoveToy(GameObject obj, float y, bool islocal, string completeFun, GameObject completeObj)
        {
            iTween iT = obj.GetComponent<iTween>();
            if (null != iT)
            {

            }
            iTween.MoveTo(obj, iTween.Hash("easeType", iTween.EaseType.linear, "time", 1f, "y", y, "islocal", islocal, "oncomplete", completeFun, "oncompletetarget", completeObj));

        }

        public void myFadeTo(bool bCallback = false)
        {
            //TweenAlpha tweenAlpha = TweenAlpha.Begin(gameObject, 1f, 0f);
            //////tweenALpha.from = 1f;
            //////tweenALpha.to = 0f;
            ////tweenAlpha.gameObject.GetComponent<UIWidget>().alpha = 1f;
            //tweenAlpha.eventReceiver = gameObject;
            //tweenAlpha.callWhenFinished = "OnTweenAlphaDone";
            //Debug.LogError("myFadeTo");
            if (gameObject.activeSelf == false)
            {
                //Debug.LogError("myFadeTo gameObject.activeSelf == false");
                return;
            }
            TweenAlpha comp = UITweener.Begin<TweenAlpha>(gameObject, 1f);
            comp.from = 1f;
            comp.to = 0f;
            //Debug.LogError("myFadeTo TweenAlpha");
            if (bCallback == true)
            {
                //Debug.LogError("myFadeTo bCallback");
                comp.eventReceiver = gameObject;
                comp.callWhenFinished = "OnTweenAlphaDone";
            }
        }
    }
}