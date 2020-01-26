namespace Myrmidon.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// - _mForceChildToExpandWidth : Cet attribut permet de déterminer si le layout doit forcer les enfants à occuper toute la largeur disponible du layout
    /// - _mForceChildToExpandHeight : Cet attribut permet de déterminer si le layout doit forcer les enfants à occuper toute la hauteur disponible du layout 
    /// </summary>
    /// 
    
    /// <summary>
    /// MyrmidonEditorLayout is the base class for layout system in Myrmidon.
    /// It basically defines all needed functionnality for the layout.
    /// This class is abstract and need to be overrided. In Myrmidon, there is
    ///     2 classes overriding : Vertical and Horizontal, feel free to create new layout
    /// </summary>
    public abstract class MyrmidonEditorLayout : MyrmidonLayoutElement
    {
        #region Internal Fields
        
        /// <summary>
        /// List of all MyrmidonLayoutElement contains in the layout
        /// @see Myrmidon.Editor.MyrmidonLayoutElement
        /// </summary>
        protected List<MyrmidonLayoutElement> _mElements;
        
        /// <summary>
        /// Float representing the padding wanted on top of the layout
        /// </summary>
        protected float _mPaddingTop;
        
        /// <summary>
        /// Float representing the padding wanted on bottom of the layout
        /// </summary>
        protected float _mPaddingBottom;
        
        /// <summary>
        /// Float representing the padding wanted on left of the layout
        /// </summary>
        protected float _mPaddingLeft;

        /// <summary>
        /// Float representing the padding wanted on right of the layout
        /// </summary>
        protected float _mPaddingRight;

        /// <summary>
        /// Float representing the spacing wanted between each element of the layout
        /// </summary>
        protected float _mSpacing;

        /// <summary>
        /// Boolean to indicate if we want the layout to force child to expand on the maximum width available
        /// If this boolean is set to false, and element in layout doesn't have 
        /// </summary>
        protected bool _mForceChildToExpandWidth;

        /// <summary>
        /// 
        /// </summary>
        protected bool _mForceChildToExpandHeight;

        /// <summary>
        /// 
        /// </summary>
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

            if(_mCanResizeElements && _mElements.Count > 1)
            {
                AddResizableToElements();
            }
        }

        
        public MyrmidonEditorLayout(List<MyrmidonLayoutElement> elements, bool forceExpandWidth, bool forceExpandHeight, bool canResize, float preferredWidth, float preferredHeight, float flexibleWidth, float flexibleHeight):
            base(preferredWidth, preferredHeight, flexibleWidth, flexibleHeight)
        {
            _mIsResizableWidth = true;
            _mElements = elements;
            _mForceChildToExpandWidth = forceExpandWidth;
            _mForceChildToExpandHeight = forceExpandHeight;
            _mCanResizeElements = canResize;

            if(_mCanResizeElements && _mElements.Count > 1)
            {
                AddResizableToElements();
            }
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
        protected abstract MyrmidonResizerElement CreateResizerForLayout(MyrmidonLayoutElement previous, MyrmidonLayoutElement next);
        protected void AddResizableToElements()
        {
            List<MyrmidonLayoutElement> elementsWithResizable = new List<MyrmidonLayoutElement>();
            for (int i = 0; i < _mElements.Count; i++)
            {
                elementsWithResizable.Add(_mElements[i]);

                if (i < _mElements.Count - 1)
                {
                    MyrmidonLayoutElement previousPanel = _mElements[i];
                    MyrmidonLayoutElement nextPanel = _mElements[i + 1];
                    MyrmidonResizerElement resizer = CreateResizerForLayout(previousPanel, nextPanel);
                    resizer.AssignBackgroundColor(Color.black);
                    elementsWithResizable.Add(resizer);
                }
            }

            _mElements = elementsWithResizable;
        }

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

        protected float GetWidthPercentageOverLayout(MyrmidonLayoutElement element)
        {
            float widthLayout = _mRect.width - (PaddingLeft + PaddingRight);
            float widthElement = element.Rect.width;

            return widthElement / widthLayout;
        }

        protected float GetHeightPercentageOverLayout(MyrmidonLayoutElement element)
        {
            float heightLayout = _mRect.height - (PaddingTop + PaddingBottom);
            float heightElement = element.Rect.height;

            return heightElement / heightLayout;
        }

        protected float GetTotalWidthPercentageOverLayout()
        {
            float widthAcculumator = 0;
            foreach(MyrmidonLayoutElement element in _mElements)
            {
                widthAcculumator += GetWidthPercentageOverLayout(element);
            }

            return widthAcculumator;
        }

        protected float GetTotalHeightPercentageOverLayout()
        {
            float heightAccumulator = 0f;
            foreach(MyrmidonLayoutElement element in _mElements)
            {
                heightAccumulator += GetHeightPercentageOverLayout(element);
            }

            return heightAccumulator;
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

        public override bool ProcessEvents(Event e)
        {
            bool repaint = false;

            repaint |= base.ProcessEvents(e);
            foreach(MyrmidonLayoutElement element in _mElements)
            {
                repaint |= element.ProcessEvents(e);
            }

            return repaint;
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