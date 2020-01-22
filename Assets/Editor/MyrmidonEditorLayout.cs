namespace Myrmidon.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// - _mForceChildToExpandWidth : Cet attribut permet de déterminer si le layout doit forcer les enfants à occuper toute la largeur disponible du layout
    /// - _mForceChildToExpandHeight : Cet attribut permet de déterminer si le layout doit forcer les enfants à occuper toute la hauteur disponible du layout 
    /// </summary>
    public abstract class MyrmidonEditorLayout : MyrmidonLayoutElement
    {
        #region Constantes
        protected const float RESIZER_SIZE = 4f;
        #endregion

        #region Internal Fields
        protected List<MyrmidonLayoutElement> _mElements;
        
        protected float _mPaddingTop;
        protected float _mPaddingBottom;
        protected float _mPaddingLeft;
        protected float _mPaddingRight;

        protected float _mSpacing;
        protected bool _mForceChildToExpandWidth;
        protected bool _mForceChildToExpandHeight;
        protected bool _mCanResizeElements;

        #endregion

        #region Properties
        public float PaddingTop
        { 
            get {return _mPaddingTop;} 
            set { _mPaddingTop = value;}
        }
        public float PaddingBottom
        { 
            get {return _mPaddingBottom;} 
            set { _mPaddingBottom = value;}
        }
        public float PaddingLeft
        { 
            get {return _mPaddingLeft;} 
            set { _mPaddingLeft = value;}
        }
        public float PaddingRight 
        { 
            get {return _mPaddingRight;} 
            set { _mPaddingRight = value;}
        }
        public float Spacing 
        {
            get { return _mSpacing; }
            set { _mSpacing = value; }
        }
        #endregion

        #region Methods

        public MyrmidonEditorLayout(List<MyrmidonLayoutElement> elements, bool forceExpandWidth, bool forceExpandHeight, bool canResize):
            base()
        {
            _mIsResizableWidth = true;
            _mElements = elements;
            _mForceChildToExpandWidth = forceExpandWidth;
            _mForceChildToExpandHeight = forceExpandHeight;
            _mCanResizeElements = canResize;
        }

        
        public MyrmidonEditorLayout(List<MyrmidonLayoutElement> elements, bool forceExpandWidth, bool forceExpandHeight, bool canResize, float preferredWidth, float preferredHeight, float flexibleWidth, float flexibleHeight):
            base(preferredWidth, preferredHeight, flexibleWidth, flexibleHeight)
        {
            _mIsResizableWidth = true;
            _mElements = elements;
            _mForceChildToExpandWidth = forceExpandWidth;
            _mForceChildToExpandHeight = forceExpandHeight;
            _mCanResizeElements = canResize;
        }

        public void SetPadding(float top, float bottom, float left, float right)
        {
            _mPaddingTop = top;
            _mPaddingBottom = bottom;
            _mPaddingLeft = left;
            _mPaddingRight = right;
        }

        public abstract void ComputeRects();
        protected abstract float ComputeWidthAvailable(Rect position);
        protected abstract float ComputeHeightAvailable(Rect position);


        protected void ComputeRangeFlexibleWidth(out float minWidth, out float maxWidth)
        {
            minWidth = 0f;
            maxWidth = 0f;

            foreach(MyrmidonLayoutElement element in _mElements)
            {
                maxWidth += element.FlexibleWidth;
            }
        }

        protected void ComputeRangeFlexibleHeight(out float minHeight, out float maxHeight)
        {
            minHeight = 0f;
            maxHeight = 0f;

            foreach(MyrmidonLayoutElement element in _mElements)
            {
                maxHeight += element.FlexibleHeight;
            }
        }


        protected bool Atleast1FlexibleHeight()
        {
            foreach(MyrmidonLayoutElement element in _mElements)
            {
                if(element.FlexibleHeight > 0)
                    return true;
            }

            return false;
        }

        protected bool AtLeast1FlexibleWidth()
        {
            foreach(MyrmidonLayoutElement element in _mElements)
            {
                if(element.FlexibleWidth > 0)
                    return true;
            }

            return false;
        }

        protected int NbResizableWidthElements()
        {
            int counter = 0;
            foreach(MyrmidonLayoutElement element in _mElements)
            {
                if(element.IsResizableWidth)
                {
                    counter++;
                }
            }

            return counter;
        }

        protected int NbResizableHeightElements()
        {
            int counter = 0;
            foreach (MyrmidonLayoutElement element in _mElements)
            {
                if (element.IsResizableHeight)
                {
                    counter++;
                }
            }

            return counter;
        }

        public override void ProcessEvents(Event e)
        {
            base.ProcessEvents(e);
            foreach(MyrmidonLayoutElement element in _mElements)
            {
                element.ProcessEvents(e);
            }
        }

        public override void Draw()
        {
            base.Draw();
            foreach(MyrmidonLayoutElement element in _mElements)
            {
                element.Draw();
            }
        }

        #endregion
    }
}