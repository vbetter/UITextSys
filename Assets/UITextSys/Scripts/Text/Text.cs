using UnityEngine;
using System.Collections;
using System.Xml;

namespace UITextSys
{
    public class Text
    {

        /// <summary>
        /// 文本id
        /// </summary>
        public int mId = 0;
        /// <summary>
        /// 显示类型 1 : 冒泡;2 : 弹框;
        /// </summary>
        public int mType = 0;
        /// <summary>
        /// content：内容
        /// </summary>
        public string mContent = "";
        /// <summary>
        /// args：参数
        /// </summary>
        public object[] mArgs = null;
        /// 坐标
        public Vector3 mPos;
        /// 时间
        public float mInterval;
        /// 队列是否已经有相同文本id
        public bool mRepeat;
        /// <summary>
        /// handler
        /// </summary>
        public UIText mUitext;

        /// <summary>
        /// pos坐标类型,0 = 世界坐标，1 = 局部坐标，
        /// </summary>
        public int mPosType = 0;
        public Text()
        {
            mId = 0;
            mType = 0;
            mPos = Vector3.zero;
            mContent = "";
            mArgs = null;
            mInterval = 0.4f;
        }

        public int Id
        {
            get
            {
                return mId;
            }
            set
            {
                mId = value;
            }
        }


        public int Type
        {
            get
            {
                return mType;
            }
            set
            {
                mType = value;
            }
        }


        public string Content
        {
            get
            {
                return mContent;
            }
            set
            {
                mContent = value;
            }
        }

        public object[] Args
        {
            get
            {
                return mArgs;
            }
            set
            {
                mArgs = value;
            }
        }

        public UIText UIText
        {
            get
            {
                return mUitext;
            }
            set
            {
                mUitext = value;
            }
        }
    }

}