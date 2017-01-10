using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GameKit.UITextSys
{
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
                    //ItemSys.CreateIcon(new Vector2(100f, 100f), _dialog.m_itemid, _dialog.m_itemtype, grid.gameObject);
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

            UIGlobal.myScaleTo(gameObject, "OnCloseScaleDone", gameObject);
        }

        public void OnSure(GameObject _object)
        {
            DialogButton button = m_dialog.GetButton(ButtonType.sure);
            if (button == null)
                return;
            if (button.m_action != null)
                button.m_action(gameObject);

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
            if (button.close)
                OnClose(_object);
        }

        public void OnCloseScaleDone()
        {
            if (gameObject != null)
                GameObject.Destroy(gameObject);
        }
    }
}