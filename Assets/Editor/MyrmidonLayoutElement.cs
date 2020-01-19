namespace Myrmidon.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    public class MyrmidonLayoutElement
    {        
        #region Internal Fields
        protected Rect _mRect;
        protected Color _mBackgroundColor;

        protected float _mPreferredWidth;
        protected float _mPreferredHeight;
        protected float _mFlexibleWidth;
        protected float _mFlexibleHeight;

        #endregion

        #region Properties

        public float PreferredWidth => _mPreferredWidth;
        public float PreferredHeight => _mPreferredHeight;
        public float FlexibleWidth => _mFlexibleWidth;
        public float FlexibleHeight => _mFlexibleHeight;

        #endregion

        #region Methods

        public MyrmidonLayoutElement()
        {
            _mPreferredWidth = -1;
            _mPreferredHeight = -1;
            _mFlexibleWidth = -1;
            _mFlexibleHeight = -1;
        }

        public MyrmidonLayoutElement(float preferredWidth, float preferredHeight, float flexibleWidth, float flexibleHeight)
        {
            _mPreferredWidth = (preferredWidth < 0) ? (0) : (preferredWidth);
            _mPreferredHeight = (preferredHeight < 0) ? (0) : (preferredHeight);
            _mFlexibleWidth = (flexibleWidth < 0) ? (0) : (flexibleWidth);
            _mFlexibleHeight = (flexibleHeight < 0) ? (0) : (flexibleHeight);
        }

        public void AssignBackgroundColor(Color color)
        {
            _mBackgroundColor = color;
        }

        public void AssignRect(Rect rect)
        {
            _mRect = rect;
        }

        public virtual void Draw()
        {
            if(_mRect != null)
            {
                EditorGUI.DrawRect(_mRect, _mBackgroundColor);
                GUILayout.BeginArea(_mRect);
                GUILayout.EndArea();
            }
        }
        #endregion
    }
}

