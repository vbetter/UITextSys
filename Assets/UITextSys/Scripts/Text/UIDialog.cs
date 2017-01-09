using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UITextSys
{
    public delegate void DialogCallback();
    public delegate void DialogCallback1<T>(T obj);
    public delegate void DialogCallback2<T1, T2>(T1 obj1, T2 obj2);

    public enum ButtonType
    {
        close = 0,
        sure = 1,
        cancle = 2,
    }

    public class DialogButton
    {
        public GameObject m_oButton = null;
        public DialogCallback1<GameObject> m_action = null;
        public string m_strName = "确定";
        public ButtonType id = 0;
        public bool close = true;

        public DialogButton(ButtonType _id, string _strName)
        {
            id = _id;
            m_strName = _strName;
        }
    }
    public enum DialogType
    {
        normal = 0x1,
        onebutton = 0x2,
        twobutton = 0x3,
        other = 0x4,
        normalForGiftBag = 0x11,
        onebuttonForGiftBag = 0x12,
        twobuttonForGiftBag = 0x13,
        otherForGiftBag = 0x14,
        normalForGiftBagInactiveText = 0x31,
        onebuttonForGiftBagInactiveText = 0x32,
        twobuttonForGiftBagInactiveText = 0x33,
        otherForGiftBagInactiveText = 0x34,
    }
    public class Dialog
    {
        private string m_title = "系统提示";
        private string m_text = "";
        public Dictionary<ButtonType, DialogButton> m_buttons = new Dictionary<ButtonType, DialogButton>();
        public DialogType m_type;
        public uint m_itemid;
        public wl_res.ResItemType m_itemtype;
        public Dialog()
            : this(DialogType.onebutton)
        {
        }

        public Dialog(DialogType _type)
        {
            m_type = _type;
            TitleStr("系统提示");
            m_buttons[ButtonType.close] = new DialogButton(ButtonType.close, "");
            m_buttons[ButtonType.sure] = new DialogButton(ButtonType.close, "确 定");
            m_buttons[ButtonType.cancle] = new DialogButton(ButtonType.close, "取 消");
            DialogType curType = (DialogType)((int)_type & 0xf);
            if (curType == _type)
            {
                CloseCallback(Test);
            }
            else if (curType == _type)
            {
                CloseCallback(Test);
                SureCallback(Test);
            }
            else if (curType == _type)
            {
                CloseCallback(Test);
                SureCallback(Test);
                CancleCallback(Test);
            }
        }

        public Dialog CloseCallback(DialogCallback1<GameObject> _callback)
        {
            m_buttons[ButtonType.close].m_action = _callback;
            return this;
        }

        public Dialog SureCallback(DialogCallback1<GameObject> _callback = null, bool close = true)
        {
            m_buttons[ButtonType.sure].m_action = _callback;
            m_buttons[ButtonType.sure].close = close;
            return this;
        }

        public Dialog CancleCallback(DialogCallback1<GameObject> _callback = null, bool close = true)
        {
            m_buttons[ButtonType.cancle].m_action = _callback;
            m_buttons[ButtonType.cancle].close = close;
            return this;
        }

        public Dialog ItemInfo(uint _id, wl_res.ResItemType _type)
        {
            m_itemid = _id;
            m_itemtype = _type;
            return this;
        }

        public Dialog ItemInfo(uint _id, uint _type)
        {
            m_itemid = _id;
            m_itemtype = (wl_res.ResItemType)_type;
            return this;
        }

        public void Test(object _object)
        {
            //if(_object != null)
            //    Debuger.LogError(_object.ToString());
        }

        public Dialog CloseStr(string _str)
        {
            m_buttons[ButtonType.close].m_strName = _str;
            return this;
        }

        public Dialog SureStr(string _str)
        {
            m_buttons[ButtonType.sure].m_strName = _str;
            return this;
        }

        public Dialog CancleStr(string _str)
        {
            m_buttons[ButtonType.cancle].m_strName = _str;
            return this;
        }
        public Dialog TitleStr(string _str)
        {
            m_title = _str;
            return this;
        }

        public Dialog TextStr(string _str)
        {
            m_text = _str;
            return this;
        }

        public DialogButton GetButton(ButtonType _type)
        {
            if (m_buttons.ContainsKey(_type))
                return m_buttons[_type];
            else
                return null;
        }

        public string Text
        {
            get
            {
                return m_text;
            }
            set
            {
                m_text = value;
            }
        }
        public string Sure
        {
            get
            {
                return m_buttons[ButtonType.sure].m_strName;
            }
        }
        public string Cancle
        {
            get
            {
                return m_buttons[ButtonType.cancle].m_strName;
            }
        }
        public string Title
        {
            get
            {
                return m_title;
            }
        }
        public DialogType Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }
    }
    public class UIDialog : MonoBehaviour
    {

        // Use this for initialization
        public Dialog m_dialog = null;
        public UILabel m_text = null;
        public UILabel m_title = null;
        public UISprite m_closebutton = null;
        public UISprite m_surebutton = null;
        public UISprite m_canclebutton = null;
        public UILabel m_closeLabel = null;
        public UILabel m_sureLabel = null;
        public UILabel m_cancleLabel = null;
        public UIGrid grid = null;
        private UILabel TempLab;
        private int wightSelf = 0;

        public void Init(Dialog _dialog)
        {
            if (_dialog == null)
                _dialog = new Dialog();
            m_dialog = _dialog;
            //EventDelegate.Add(m_closebutton.onClick, OnClose);
            //EventDelegate.Add(m_surebutton.onClick, OnClose);
            //EventDelegate.Add(m_canclebutton.onClick, OnClose);
            UIEventListener.Get(m_closebutton.gameObject).onClick = OnClose;
            UIEventListener.Get(m_surebutton.gameObject).onClick = OnSure;
            UIEventListener.Get(m_canclebutton.gameObject).onClick = OnCancle;
            m_title.text = m_dialog.Title;
            m_sureLabel.text = m_dialog.Sure;
            m_cancleLabel.text = m_dialog.Cancle;
            DialogType thetype = (DialogType)((int)m_dialog.Type & 0xf);
            if (thetype == DialogType.normal)
            {
                m_surebutton.gameObject.SetActive(false);
                m_canclebutton.gameObject.SetActive(false);
            }
            else if (thetype == DialogType.onebutton)
            {
                m_canclebutton.gameObject.SetActive(false);
                m_surebutton.transform.localPosition = new Vector3(0, m_surebutton.transform.localPosition.y, m_surebutton.transform.localPosition.z);
            }
            else if (thetype == DialogType.twobutton)
            {
            }

            if (((int)m_dialog.Type & 0x20) == 0x20)
            {
                m_text.gameObject.SetActive(false);
            }
            else
            {
                UITextSys.SetText(m_text, m_dialog.Text);
            }
            if (((int)m_dialog.Type & 0x10) == 0x10)
            {
                if (grid != null)
                {
                    ItemSys.CreateIcon(new Vector2(100f, 100f), _dialog.m_itemid, _dialog.m_itemtype, grid.gameObject);
                    grid.gameObject.SetActive(true);
                }
            }
        }

        public void OnClose(GameObject _object)
        {
            DialogButton button = m_dialog.GetButton(ButtonType.close);
            if (button != null && button.m_action != null)
            {
                button.m_action(gameObject);
            }

            if (button != null && button.m_luaFunction != null)
            {
                CallLuaFunction(button.m_luaFunction, _object);
            }
            UIGlobal.myScaleTo(gameObject, "OnCloseScaleDone", gameObject);
        }

        public void OnSure(GameObject _object)
        {
            DialogButton button = m_dialog.GetButton(ButtonType.sure);
            if (button == null)
                return;
            if (button.m_action != null)
                button.m_action(gameObject);
            if (button != null && button.m_luaFunction != null)
            {
                CallLuaFunction(button.m_luaFunction, _object);
            }
            if (button.close)
                OnClose(_object);
        }

        public void OnCancle(GameObject _object)
        {
            DialogButton button = m_dialog.GetButton(ButtonType.cancle);
            if (button == null)
                return;
            if (button.m_action != null)
                button.m_action(gameObject);
            if (button != null && button.m_luaFunction != null)
            {
                CallLuaFunction(button.m_luaFunction, _object);
            }
            if (button.close)
                OnClose(_object);
        }
        public void OnCloseScaleDone()
        {
            if (gameObject != null)
                GameObject.Destroy(gameObject);
        }

        //public void OnClose()
        //{
        //}

        public void CallLuaFunction(LuaInterface.LuaFunction _luaFunction, GameObject _go)
        {
            /*
            if (_luaFunction != null && _go != null)
            {
                int top = _luaFunction.BeginPCall();
                System.IntPtr L = _luaFunction.GetLuaState();
                LuaScriptMgr.Push(L, _go);
                _luaFunction.PCall(top, 1);
                _luaFunction.EndPCall(top);
            }
            */
        }

        //
    }
}