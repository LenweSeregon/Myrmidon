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

    public class MyrmidonEditorResizerElement : MyrmidonEditorLayoutElement
    {
        #region Constantes
        public readonly static float RESIZER_SIZE = 4f;
        #endregion


        #region Internal Fields
        private MyrmidonResizerType _mResizerType;
        private MyrmidonEditorLayoutElement _mPreviousPanel;
        private MyrmidonEditorLayoutElement _mNextPanel;
        private bool _mIsResizing;
        #endregion

        #region Methods
        public MyrmidonEditorResizerElement(MyrmidonEditorLayoutElement previousPanel, MyrmidonEditorLayoutElement nextPanel, MyrmidonResizerType type, bool resizableWidth, bool resizableHeight):
            base()
        {
            _mIsResizableWidth = resizableWidth;
            _mIsResizableHeight = resizableHeight;
            _mPreviousPanel = previousPanel;
            _mNextPanel = nextPanel;
            _mResizerType = type;
        }

        public MyrmidonEditorResizerElement(MyrmidonEditorLayoutElement previousPanel, MyrmidonEditorLayoutElement nextPanel, float preferredWidth, float preferredHeight, float flexibleWidth, float flexibleHeight, MyrmidonResizerType type, bool resizableWidth, bool resizableHeight) :
            base(preferredWidth, preferredHeight, flexibleWidth, flexibleHeight)
        {
            _mIsResizableWidth = resizableWidth;
            _mIsResizableHeight = resizableHeight;
            _mPreviousPanel = previousPanel;
            _mNextPanel = nextPanel;
            _mResizerType = type;
        }

        public override bool ProcessEvents(Event e)
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
                ProcessResizeEvent(e);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ProcessResizeEvent(Event e)
        {
            Rect newPreviousRect = _mPreviousPanel.Rect;
            Rect newResizableRect = _mRect;
            Rect newNextRect = _mNextPanel.Rect;
            float currentX = _mRect.x;
            float currentY = _mRect.y;
            float mouseX = e.mousePosition.x;
            float mouseY = e.mousePosition.y;

            switch(_mResizerType)
            {
                case MyrmidonResizerType.Vertical:

                    if(mouseY < currentY && mouseY > _mPreviousPanel.Rect.y + RESIZER_SIZE)
                    {
                        float deltaY = currentY - mouseY;

                        newPreviousRect.height -= deltaY;
                        newNextRect.y -= deltaY;
                        newNextRect.height += deltaY;
                        newResizableRect.y -= deltaY;
                    }
                    else if(mouseY > currentY && mouseY < _mNextPanel.Rect.y + _mNextPanel.Rect.height - (RESIZER_SIZE*2))
                    {
                        float deltaY = mouseY - currentY;

                        newPreviousRect.height += deltaY;
                        newNextRect.y += deltaY;
                        newNextRect.height -= deltaY;
                        newResizableRect.y += deltaY;
                    }

                    break;

                case MyrmidonResizerType.Horizontal:
                    if(mouseX < currentX && mouseX > _mPreviousPanel.Rect.x + RESIZER_SIZE)
                    {
                        float deltaX = currentX - mouseX;

                        newPreviousRect.width -= deltaX;
                        newNextRect.x -= deltaX;
                        newNextRect.width += deltaX;
                        newResizableRect.x -= deltaX;
                    }
                    else if(mouseX > currentX && mouseX < _mNextPanel.Rect.x + _mNextPanel.Rect.width - (RESIZER_SIZE*2))
                    {
                        float deltaX = mouseX - currentX;

                        newPreviousRect.width += deltaX;
                        newNextRect.x += deltaX;
                        newNextRect.width -= deltaX;
                        newResizableRect.x += deltaX;
                    }
                    break;
            }

            _mRect = newResizableRect;
            _mPreviousPanel.AssignRect(newPreviousRect);
            _mNextPanel.AssignRect(newNextRect);
        }

        public override void Draw()
        {
            base.Draw();

            switch(_mResizerType)
            {
                case MyrmidonResizerType.Horizontal:
                    EditorGUIUtility.AddCursorRect(_mRect, MouseCursor.ResizeHorizontal);
                    break;
                case MyrmidonResizerType.Vertical:
                    EditorGUIUtility.AddCursorRect(_mRect, MouseCursor.ResizeVertical);
                    break;
            }

        }

        #endregion
    }
}

