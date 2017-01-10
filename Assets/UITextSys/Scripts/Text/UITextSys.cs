using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GameKit.UITextSys
{

    public class UITextSys : MonoBehaviour
    {

        public static readonly string UITextPathPreFix = "UIPrefabs/UIText/";   //预设路径前缀

        [SerializeField]
        GameObject RES_UIDialog;        //实例资源

        // Use this for initialization
        /// <summary>
        /// 第一种类型文本对象队列
        /// </summary>
        private Queue<Text> mScrollMessageQueue = new Queue<Text>();
        /// <summary>
        /// 第一种类型的文本间隔时间
        /// </summary>
        private float mfInterval = 0.0f;
        /// <summary>
        /// 第一种类型的文本depth
        /// </summary>
        public static int mIDepth = 0;
        /// <summary>
        /// 单个实例
        /// </summary>
        private static UITextSys mInstance = null;
        /// <summary>
        /// 用于挂载文本窗口的父节点
        /// </summary>
        public GameObject mUItext;

        public GameObject mTipAttribute;
        public GameObject mTipFate;
        public Text mLastText;
        /// <summary>
        /// 第一种类型文本对象队列
        /// </summary>
        private Text mTextTipMessage;
        /// <summary>
        /// 第一种类型文本对象队列
        /// </summary>
        private Queue<Text> mGetMessageQueue = new Queue<Text>();
        private bool mbFrist = true;
        /// <summary>
        /// 第一种类型的文本间隔时间
        /// </summary>
        private float mfGetInterval = 0.0f;
        //
        public float mfTextTipInterval = 0;

        static UITextSys m_Instance = null;
        public static UITextSys Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = GameObject.FindObjectOfType(typeof(UITextSys)) as UITextSys;
                    if (m_Instance == null)
                    {
                        GameObject res = Instantiate(Resources.Load(UITextPathPreFix + "UITextSys")) as GameObject;
                        m_Instance = res.GetComponent<UITextSys>();
                    }

                }
                return m_Instance;
            }
        }

        /// <summary>
        /// 间隔时间显示第一种文本窗口
        /// </summary>
        void Update()
        {
            UpdateScrollMessageQueue();
            UpdateTextTipMessageQueue();
            UpdateGetMessageQueue();
        }

        /// <summary>
        /// 间隔时间显示第一种文本窗口
        /// </summary>
        void UpdateScrollMessageQueue()
        {
            mfInterval -= Time.deltaTime;
            if (mScrollMessageQueue.Count <= 0)
            {
                mLastText = null;
                //mfInterval = 0f;
                return;
            }
            if (mfInterval <= 0f)
            {
                //Debug.LogError("mfInterval <= 0f in " + mScrollMessageQueue.Count + " mfInterval " + mfInterval);
                Text text = mScrollMessageQueue.Peek();
                mScrollMessageQueue.Dequeue();
                OnShow(text);
                mfInterval = text.mInterval;
                //Debug.LogError("mfInterval <= 0f out " + mScrollMessageQueue.Count + " mfInterval " + mfInterval);
            }
            ////mScrollMessageList.Add(curText);
        }

        /// <summary>
        /// 间隔时间显示第一种文本窗口
        /// </summary>
        void UpdateTextTipMessageQueue()
        {
            if (mTextTipMessage == null)
                return;
            mTextTipMessage.mInterval -= Time.deltaTime;
            //Debug.LogError(mTextTipMessage.mInterval);
            if (mTextTipMessage.mInterval < 0 && mTextTipMessage.mUitext != null && mTextTipMessage.mUitext.gameObject != null && mTextTipMessage.mUitext.fading == false)
            {
                mTextTipMessage.mUitext.fading = true;
                //Debug.LogError("mTextTipMessage.mUitext != null");
                mTextTipMessage.mUitext.myFadeTo(true);
            }
        }

        /// <summary>
        /// 间隔时间显示第一种文本窗口
        /// </summary>
        void UpdateGetMessageQueue()
        {
            mfGetInterval -= Time.deltaTime;
            if (mGetMessageQueue.Count <= 0)
            {
                return;
            }
            //Debug.LogError("mGetMessageQueue.Count " + mGetMessageQueue.Count + " mfGetInterval " + mfGetInterval + " Time.deltaTime " + Time.deltaTime);
            if (mfGetInterval <= 0f)
            {
                //Debug.LogError("mfGetInterval <= 0f in " + mGetMessageQueue.Count + " mfGetInterval " + mfGetInterval);
                Text text = mGetMessageQueue.Peek();
                mGetMessageQueue.Dequeue();
                OnShow(text);
                mfGetInterval = text.mInterval;
                //Debug.LogError("mfGetInterval <= 0f out " + mGetMessageQueue.Count + " mfGetInterval " + mfGetInterval);
            }
            ////mScrollMessageList.Add(curText);
        }

        /// <summary>
        /// 第一种类型添加进入对象队列
        /// </summary>
        /// <param name="_text"></param>
        public void AddScrollMessageQueue(Text _text)
        {
            if (_text == null)
            {
                return;
            }
            //if(mbFrist == true)
            _text.mInterval = 1.5f;
            if (mScrollMessageQueue.Count >= 3)
            {
                mScrollMessageQueue.Dequeue();
            }
            mScrollMessageQueue.Enqueue(_text);
            return;
            if (mLastText != null && mLastText.Id == _text.Id && mLastText.Content == _text.Content)
            {
                if (mLastText.mRepeat == true && mLastText.mInterval > 0)
                {
                    mLastText.mInterval += 0.1f;
                    return;
                }
                _text.mRepeat = true;
            }
            mLastText = _text;
            mScrollMessageQueue.Enqueue(_text);
        }

        /// <summary>
        /// 第一种类型删除对象队列
        /// </summary>
        public void DeleteScrollMessageQueue()
        {
            if (mScrollMessageQueue.Count > 0)
                mScrollMessageQueue.Dequeue();
        }

        /// <summary>
        /// 第一种类型添加进入对象队列
        /// </summary>
        /// <param name="_text"></param>
        public void AddTextTipMessageQueue(Text _text)
        {
            if (_text == null)
            {
                return;
            }
            _text.mInterval = 2f;
            if (mTextTipMessage == null || mTextTipMessage.mInterval <= 0 || _text.Id != mTextTipMessage.Id)
            {
                mTextTipMessage = _text;
                OnShow(mTextTipMessage);
            }
        }

        /// <summary>
        /// 第一种类型删除对象队列
        /// </summary>
        public void DeleteTextTipMessageQueue()
        {
        }

        /// <summary>
        /// 第一种类型添加进入对象队列
        /// </summary>
        /// <param name="_text"></param>
        public void AddGetMessageQueue(Text _text)
        {
            if (_text == null)
            {
                return;
            }
            _text.mInterval = 1.5f;
            if (mGetMessageQueue.Count >= 6)
            {
                mGetMessageQueue.Dequeue();
            }
            mGetMessageQueue.Enqueue(_text);
            return;
            if (mLastText != null && mLastText.Id == _text.Id && mLastText.Content == _text.Content)
            {
                if (mLastText.mRepeat == true && mLastText.mInterval > 0)
                {
                    mLastText.mInterval += 0.1f;
                    return;
                }
                _text.mRepeat = true;
            }
            mLastText = _text;
            mScrollMessageQueue.Enqueue(_text);
        }

        /// <summary>
        /// 第一种类型删除对象队列
        /// </summary>
        public void DeleteGetMessageQueue()
        {
            if (mGetMessageQueue.Count > 0)
                mGetMessageQueue.Dequeue();
        }

        public void ShowText(string text, params object[] _args)
        {
            string strContent = string.Format(text, _args);
            Text curText = new Text();
            curText.Content = strContent;
            curText.Args = null;
            curText.Type = (int)TextType.ScrollType;
            curText.Id = 0;
            AddScrollMessageQueue(curText);
        }

        public void ShowText(string text, TextType type, params object[] _args)
        {
            string strContent = string.Format(text, _args);
            Text curText = new Text();
            curText.Content = strContent;
            curText.Args = null;
            curText.Type = (int)type;
            curText.Id = 0;
            switch (type)
            {
                case TextType.ScrollType:
                    AddScrollMessageQueue(curText);
                    break;
                case TextType.WindowType:
                    OnShow(curText);
                    break;
                case TextType.JustString:
                    break;
                case TextType.Dialog:
                    break;
                case TextType.TextLabel:
                    break;
                case TextType.TextTip:
                    AddTextTipMessageQueue(curText);
                    break;
                case TextType.GetType:
                    AddGetMessageQueue(curText);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 文本显示外部接口
        /// </summary>
        /// <param name="_id">ID</param>
        /// <param name="_args">文本替换参数列表</param>
        public void ShowText(int _id, params object[] _args)
        {
            //Debug.LogError("ShowText " + _id);
            Text curText = TextReader.GetTextById(_id);
            if (curText != null)
            {
                curText.Args = _args;
                int curType = curText.Type;
                if (curType == (int)TextType.ScrollType)
                {
                    AddScrollMessageQueue(curText);
                }
                else if (curType == (int)TextType.WindowType)
                {
                    OnShow(curText);
                }
                else if (curType == (int)TextType.TextTip)
                {
                    AddTextTipMessageQueue(curText);
                }
                else if (curType == (int)TextType.GetType)
                {
                    AddGetMessageQueue(curText);
                }
            }
            else
            {
                string defaultText = "未知错误(错误码: " + _id + ")";
                ShowText(defaultText);
            }
        }

        public void ShowText(int _id, Vector3 _pos, params object[] _args)
        {
            if (_pos != Vector3.zero)
            {
                Text curText = TextReader.GetTextById(_id);
                if (curText != null)
                {
                    curText.mPos = _pos;
                    curText.Args = _args;
                    curText.mPosType = 0;
                    int curType = curText.Type;
                    if (curType == (int)TextType.ScrollType)
                    {
                        AddScrollMessageQueue(curText);
                    }
                    else if (curType == (int)TextType.WindowType)
                    {
                        OnShow(curText);
                    }
                    else if (curType == (int)TextType.TextTip)
                    {
                        AddTextTipMessageQueue(curText);
                    }
                    else if (curType == (int)TextType.GetType)
                    {
                        AddGetMessageQueue(curText);
                    }
                }
            }
        }

        public void ShowTextWithLocalPos(int _id, Vector3 _pos, params object[] _args)
        {
            if (_pos != Vector3.zero)
            {
                Text curText = TextReader.GetTextById(_id);
                if (curText != null)
                {
                    curText.mPos = _pos;
                    curText.Args = _args;
                    curText.mPosType = 1;
                    int curType = curText.Type;
                    if (curType == (int)TextType.ScrollType)
                    {
                        AddScrollMessageQueue(curText);
                    }
                    else if (curType == (int)TextType.WindowType)
                    {
                        OnShow(curText);
                    }
                    else if (curType == (int)TextType.TextTip)
                    {
                        AddTextTipMessageQueue(curText);
                    }
                    else if (curType == (int)TextType.GetType)
                    {
                        AddGetMessageQueue(curText);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_args"></param>
        public UIDialog ShowDialog(int _id, Dialog _dialog, params object[] _args)
        {
            UIDialog dialog = null;
            Text curText = TextReader.GetTextById(_id);
            if (curText != null)
            {
                curText.Args = _args;
                int curType = curText.Type;
                //if (curType == (int)TextType.Dialog)
                {
                    if (_dialog == null)
                        _dialog = new Dialog();
                    curText.Content = UIText.ReplaceStringByValue(curText.Content, curText.Args);
                    _dialog.Text = curText.Content;

                    string resPath = UITextPathPreFix + "UIDialog";
                    GameObject res = Resources.Load<GameObject>(resPath) as GameObject;
                    if (res == null)
                    {
                        Debug.LogError("Not Find Resource , Path: " + resPath);
                        return null;
                    }

                    GameObject curObject = Instantiate(RES_UIDialog);
                    if (curObject == null)
                    {
                        Debug.LogError("Not Find Resource , Path: " + resPath);
                        return null;
                    }
                    ////curPrefab
                    if (mUItext == null)
                        return null;
                    curObject.transform.parent = mUItext.transform;
                    curObject.transform.localScale = Vector3.one;
                    curObject.transform.localPosition = Vector3.one;
                    curObject.SetActive(true);
                    UIDialog curHandle = curObject.GetComponent<UIDialog>();
                    if (curHandle != null)
                    {
                        curHandle.Init(_dialog);
                    }

                    dialog = curHandle;
                }
            }

            return dialog;
        }

        /// <summary>
        /// 文本显示
        /// </summary>
        /// <param name="_text">文本对象</param>
        public void OnShow(Text _text)
        {
            if (_text == null)
                return;
            string resPath = UITextPathPreFix + "UIText" + _text.Type.ToString();
            GameObject res = Resources.Load<GameObject>(resPath) as GameObject;
            if(res == null)
            {
                Debug.LogError("Not Find Resource , Path: " + resPath);
                return;
            }

            GameObject curObject = Instantiate(res) as GameObject;
            if (curObject == null)
            {
                Debug.LogError("Not Find Resource , Path: " + resPath);
                return;
            }
            
            if (mUItext == null)
            {
                Debug.LogError("mUItext is null !");
                return;
            }

            curObject.SetActive(true);
            UIText textHandler = curObject.GetComponent<UIText>();
            textHandler.mContent = UIText.ReplaceStringByValue(_text.Content, _text.Args);
            textHandler.mPos = _text.mPos;
            textHandler.mPosType = _text.mPosType;
            _text.mUitext = textHandler;
            textHandler.OnShow();
        }

        public string GetText(int _id, params object[] _args)
        {
            if (_id == null)
                return "";
            Text curText = TextReader.GetTextById(_id);
            if (curText != null)
            {
                curText.Args = _args;
                string retStr = UIText.ReplaceStringByValue(curText.Content, _args);
                return retStr;
            }
            return "";
        }

        public int GetTextType(int _id)
        {
            if (_id == null)
                return -1;
            Text curText = TextReader.GetTextById(_id);
            if (curText != null)
            {
                return curText.Type;
            }
            return -1;
        }

        public struct TextPos
        {
            public string text;
            public Vector3 ver3Pos;
        }

        public static void SetText(UILabel text, string _strContent, int height = 0, int width = 0)
        {
            text.text = _strContent;
            text.UpdateNGUIText();
            if (text.height > text.fontSize + 10)
            {
                text.pivot = UILabel.Pivot.Left;
            }
            List<TextPos> LabelList = new List<TextPos>();
            //保存字体的信息
            Vector3 m_labPos = text.transform.localPosition;
            int m_width = text.width;
            UIWidget.Pivot pivot = text.pivot;
            UILabel.Overflow overflow = text.overflowMethod;
            ChatText(ref _strContent, text, m_width, pivot, overflow, ref LabelList);
            ShowLab(_strContent, text, m_width, pivot, overflow, m_labPos, LabelList, height, width);
        }

        private string labStrCatch;
        // Use this for initialization
        // Update is called once per frame
        static private void ChatText(ref string text, UILabel chatMsg, int m_width, UIWidget.Pivot pivot, UILabel.Overflow overflow, ref List<TextPos> LabelSpriteList)
        {
            NGUIText.finalSize = chatMsg.defaultFontSize;//设置当前使用字体大小
            string strText;                                //保存的文字
            int offectx = chatMsg.fontSize / 2 + 6;
            int offectY = -chatMsg.fontSize - chatMsg.spacingY;
            Vector2 widthIndex = Vector2.zero;             //临时变量--文字的位置 #huizhang;
            int heightIndex = 0;                           //临时变量--文字的高度
            int startIndex = 0;                            //临时变量--表情的开始位置个数
            int endIndex = 0;                              //临时变量--表情的结束位置个数

            string _str;                                   //临时变量--临时保存的文字
            string endString = text;                       //临时变量--临时保存的文字
            string[] strArray;
            string pattern = "\\#[0-9a-zA-Z -]*\\;";       //表情标签显示。例如：#icon_hb_yinbi;
            MatchCollection matchs = Regex.Matches(text, pattern);


            int BASELINEWIDTH = 400;
            string space = "     ";                       //空格代替


            if (matchs.Count > 0)
            {
                foreach (Match item in matchs)
                {
                    _str = item.Value;
                    startIndex = endString.IndexOf(_str);
                    endIndex = startIndex + _str.Length;

                    if (startIndex > -1)
                    {
                        TextPos textPos = new TextPos();
                        textPos.text = _str.Substring(1, _str.Length - 2);
                        strText = endString.Substring(0, startIndex) + space;
                        endString = endString.Remove(startIndex, endIndex - startIndex);
                        endString = endString.Insert(startIndex, space);
                        string strTemp;
                        chatMsg.width = m_width;
                        chatMsg.pivot = pivot;
                        chatMsg.overflowMethod = overflow;
                        if (chatMsg.Wrap(strText, out strTemp))
                        {
                            strArray = strTemp.Split('\n');
                            heightIndex = (strArray.Length - 1) * offectY;
                            chatMsg.overflowMethod = UILabel.Overflow.ResizeFreely;

                            string currentStr = strArray[strArray.Length - 1];

                            chatMsg.text = strArray[strArray.Length - 1];
                            chatMsg.UpdateNGUIText();
                            if ((chatMsg.width + 30) > BASELINEWIDTH)
                            {
                                endString = endString.Insert(startIndex + space.Length, "\n");
                            }
                            textPos.ver3Pos.y = heightIndex;
                            textPos.ver3Pos.x = chatMsg.width - offectx;
                            LabelSpriteList.Add(textPos);
                        }
                    }
                }
            }
            text = endString;
        }

        static void ShowLab(string text, UILabel chatMsg, int m_width, UIWidget.Pivot pivot, UILabel.Overflow overflow, Vector3 m_labPos, List<TextPos> LabelSpriteList, int height, int width)
        {
            if (LabelSpriteList.Count == 0)
                return;

            chatMsg.width = m_width;
            chatMsg.pivot = pivot;
            chatMsg.overflowMethod = overflow;
            chatMsg.transform.localPosition = m_labPos;
            chatMsg.text = text;
            Vector2 offectVec2;
            Caultual(chatMsg, out offectVec2);
            for (int i = 0; i < LabelSpriteList.Count; i++)
            {
                GameObject spriteObj = new GameObject("text");
                UISprite sprite = spriteObj.AddComponent<UISprite>();
                sprite.spriteName = LabelSpriteList[i].text;
                sprite.depth = chatMsg.depth + 1;
                if (height == 0 || width == 0)
                {
                    //   sprite.MakePixelPerfect();
                    sprite.height = chatMsg.fontSize + 15;
                    sprite.width = chatMsg.fontSize + 15;
                }
                else
                {
                    sprite.height = height;
                    sprite.width = width;

                }
                sprite.pivot = UIWidget.Pivot.Center;
                UIGlobal.setParent(spriteObj, chatMsg.gameObject);
                int heighOff = 0;
                if (sprite.height > chatMsg.fontSize)
                {
                    heighOff = sprite.height - chatMsg.fontSize;
                }
                spriteObj.transform.localPosition = new Vector3(LabelSpriteList[i].ver3Pos.x - offectVec2.x, LabelSpriteList[i].ver3Pos.y + offectVec2.y - (sprite.height - heighOff) / 2, 0);
            }
        }

        static private void Caultual(UILabel label, out Vector2 labOffect)
        {
            labOffect = Vector2.zero;
            switch (label.pivot)
            {
                case UIWidget.Pivot.Bottom:
                    labOffect = new Vector2(label.width / 2, label.height);
                    break;
                case UIWidget.Pivot.BottomLeft:
                    labOffect = new Vector2(label.width, label.height);
                    break;
                case UIWidget.Pivot.BottomRight:
                    labOffect = new Vector2(0, label.height);
                    break;
                case UIWidget.Pivot.Center:
                    labOffect = new Vector2(label.width / 2, label.height / 2);
                    break;
                case UIWidget.Pivot.Left:
                    labOffect = new Vector2(0, label.height / 2);
                    break;
                case UIWidget.Pivot.Right:
                    labOffect = new Vector2(label.width, label.height / 2);
                    break;
                case UIWidget.Pivot.Top:
                    labOffect = new Vector2(label.width / 2, 0);
                    break;
                case UIWidget.Pivot.TopLeft:
                    labOffect = new Vector2(0, 0);
                    break;
                case UIWidget.Pivot.TopRight:
                    labOffect = new Vector2(label.width, 0);
                    break;

            }
        }

        public GameObject TipAttribute
        {
            get
            {
                if (mTipAttribute == null)
                {
                    mTipAttribute = Instantiate(Resources.Load("UIPrefabs/UI/UITipAttribute")) as GameObject;
                    UIGlobal.setParent(mTipAttribute, mUItext);
                    mTipAttribute.SetActive(false);
                }
                return mTipAttribute;
            }
        }

        public void ClearData()
        {
            if (mScrollMessageQueue != null)
                mScrollMessageQueue.Clear();
            if (mGetMessageQueue != null)
                mGetMessageQueue.Clear();
            for (int i = 0; i < mUItext.transform.childCount; i++)
            {
                Transform tran = mUItext.transform.GetChild(i);
                if (tran != null)
                {
                    if (tran.gameObject.name.IndexOf("UIText") >= 0)
                    {
                        //ResourcePool.Instance.FreeObject(tran.gameObject);
                    }
                    if (tran.gameObject.name.IndexOf("UIDialog") >= 0)
                    {
                        UIDialog dialog = tran.GetComponent<UIDialog>();
                        if (dialog != null)
                        {
                            dialog.OnCloseScaleDone();
                        }
                    }
                }
            }
        }

        public void Init()
        {
            TextReader.Init();
            mbFrist = true;

            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
}