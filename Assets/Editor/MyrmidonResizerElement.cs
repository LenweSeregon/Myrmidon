namespace Myrmidon.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    public enum MyrmidonResizerType
    {
        Vertical,
        Horizontal
    }

    public class MyrmidonResizerElement : MyrmidonLayoutElement
    {
        #region Internal Fields
        private MyrmidonResizerType _mResizerType;
        private MyrmidonLayoutElement _mPreviousPanel;
        private MyrmidonLayoutElement _mNextPanel;
        private bool _mIsResizing;
        #endregion

        #region Methods
        public MyrmidonResizerElement(MyrmidonLayoutElement previousPanel, MyrmidonLayoutElement nextPanel, MyrmidonResizerType type):
            base()
        {
            _mPreviousPanel = previousPanel;
            _mNextPanel = nextPanel;
            _mResizerType = type;
        }

        public MyrmidonResizerElement(MyrmidonLayoutElement previousPanel, MyrmidonLayoutElement nextPanel, float preferredWidth, float preferredHeight, float flexibleWidth, float flexibleHeight, MyrmidonResizerType type):
            base(preferredWidth, preferredHeight, flexibleWidth, flexibleHeight)
        {
            _mPreviousPanel = previousPanel;
            _mNextPanel = nextPanel;
            _mResizerType = type;
        }

        public override void ProcessEvents(Event e)
        {
            base.ProcessEvents(e);

            switch(e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0 && _mRect.Contains(e.mousePosition))
                    {
                        _mIsResizing = true;
                    }
                    break;

                case EventType.MouseUp:
                    _mIsResizing = false;
                    break;
            }

            if(_mIsResizing)
            {
                switch(_mResizerType)
                {
                    case MyrmidonResizerType.Vertical:
                    float saveY = _mRect.y;
                    float mouseY = e.mousePosition.y;
                    Debug.Log("SAVE Y : " + saveY);
                    Debug.Log("MOUSE Y : " + mouseY);
                    if(mouseY < saveY)
                    {
                        float deltaY = saveY - mouseY;
                        Debug.Log("DELTA Y : " + deltaY);
                        Rect newPreviousRect = _mPreviousPanel.Rect;
                        Rect newResizableRect = _mRect;
                        Rect newNextRect = _mNextPanel.Rect;
                        
                        newPreviousRect.height -= deltaY;
                        newNextRect.y -= deltaY;
                        newNextRect.height += deltaY;
                        newResizableRect.y -= deltaY;

                        _mRect = newResizableRect;
                        _mPreviousPanel.AssignRect(newPreviousRect);
                        _mNextPanel.AssignRect(newNextRect);
                        
                    }
                    else if(mouseY > saveY)
                    {
                        float deltaY = mouseY - saveY;
                        Debug.Log("DELTA : " + deltaY);
                        Rect newPreviousRect = _mPreviousPanel.Rect;
                        Rect newResizableRect = _mRect;
                        Rect newNextRect = _mNextPanel.Rect;
                        
                        newPreviousRect.height += deltaY;
                        newNextRect.y -= deltaY;
                        newNextRect.height -= deltaY;
                        newResizableRect.y += deltaY;

                        _mRect = newResizableRect;
                        _mPreviousPanel.AssignRect(newPreviousRect);
                        _mNextPanel.AssignRect(newNextRect);
                    }

                    break;
                }
            }
        }

        public override void Draw()
        {
            base.Draw();

            EditorGUIUtility.AddCursorRect(_mRect, MouseCursor.ResizeVertical);
        }

        #endregion
    }
}

