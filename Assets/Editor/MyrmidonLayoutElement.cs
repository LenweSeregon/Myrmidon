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
        protected bool _mIsResizableWidth;
        protected bool _mIsResizableHeight;

        protected float _mPreferredWidth;
        protected float _mPreferredHeight;
        protected float _mFlexibleWidth;
        protected float _mFlexibleHeight;

        #endregion

        #region Properties
        public Rect Rect => _mRect;
        public bool IsResizableWidth => _mIsResizableWidth;
        public bool IsResizableHeight => _mIsResizableHeight;
        public float PreferredWidth => _mPreferredWidth;
        public float PreferredHeight => _mPreferredHeight;
        public float FlexibleWidth => _mFlexibleWidth;
        public float FlexibleHeight => _mFlexibleHeight;

        #endregion

        #region Methods

        public MyrmidonLayoutElement()
        {
            _mIsResizableWidth = true;
            _mIsResizableHeight = true;
            _mPreferredWidth = 0;
            _mPreferredHeight = 0;
            _mFlexibleWidth = 0;
            _mFlexibleHeight = 0;
        }

        public MyrmidonLayoutElement(float preferredWidth, float preferredHeight, float flexibleWidth, float flexibleHeight)
        {
            _mIsResizableWidth = true;
            _mIsResizableHeight = true;
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

        public virtual void ProcessResizing(float deltaWidth, float deltaHeight)
        {
            if(_mIsResizableWidth)
            {
                _mRect.width += deltaWidth;
            }
            
            if(_mIsResizableHeight)
            {
                _mRect.height += deltaHeight;
            }
        }

        public virtual bool ProcessEvents(Event e)
        {
            return false;
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

