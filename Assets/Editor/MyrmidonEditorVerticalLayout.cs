namespace Myrmidon.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// MyrmidonEditorVerticalLayout is an inheritant class from MyrmidonEditorLayout
    /// It handle vertical layout, meaning element that are placed sequentially behind each others
    /// @see MyrmidonEditorLayout
    /// </summary>
    public class MyrmidonEditorVerticalLayout : MyrmidonEditorLayout
    {
        #region Methods

        public MyrmidonEditorVerticalLayout(List<MyrmidonEditorLayoutElement> elements, bool forceExpandWidth, bool forceExpandHeight, bool canResize) :
            base(elements, forceExpandWidth, forceExpandHeight, canResize)
        {
            
        }

        public MyrmidonEditorVerticalLayout(List<MyrmidonEditorLayoutElement> elements, bool forceExpandWidth, bool forceExpandHeight, bool canResize, float preferredWidth, float preferredHeight, float flexibleWidth, float flexibleHeight):
            base(elements, forceExpandWidth, forceExpandHeight, canResize, preferredWidth, preferredHeight, flexibleWidth, flexibleHeight)
        {

        }

        protected override MyrmidonEditorResizerElement CreateResizerForLayout(MyrmidonEditorLayoutElement previous, MyrmidonEditorLayoutElement next)
        {
            float resizerWidth = MyrmidonEditorResizerElement.RESIZER_SIZE;
            MyrmidonEditorResizerElement resizer = new MyrmidonEditorResizerElement(previous, next, 0, resizerWidth, 1, 0, MyrmidonResizerType.Vertical, true, false);
            return resizer;
        }
        public override void ComputeRects()
        {
            if(_mRect != null)
            {
                int nbElement = _mElements.Count;
                float widthAvailable = ComputeWidthAvailable(_mRect);
                float heightAvailable = ComputeHeightAvailable(_mRect);
                float remainingHeight = heightAvailable;
                Rect[] rects = new Rect[_mElements.Count];
                
                // Assigning width and X position
                for(int i = 0; i < _mElements.Count; i++)
                {
                    MyrmidonEditorLayoutElement element = _mElements[i];
                    rects[i].width = 0f;
                    rects[i].x = _mRect.x + _mPaddingLeft;

                    if(_mForceChildToExpandWidth)
                    {
                        rects[i].width = widthAvailable;
                    }
                    else if(element.FlexibleWidth > 0)
                    {
                        rects[i].width = widthAvailable;
                    }
                    else if(element.PreferredWidth > 0)
                    {
                        rects[i].width = Mathf.Clamp(element.PreferredWidth, 0, widthAvailable);
                    }
                }
                
                // Assigning height preferred size
                for(int i = 0; i < _mElements.Count; i++)
                {
                    MyrmidonEditorLayoutElement element = _mElements[i];
                    rects[i].height = element.PreferredHeight;

                    remainingHeight -= rects[i].height;
                }

                // Assigning height flexible size
                if(_mForceChildToExpandHeight || Atleast1FlexibleHeight())
                {
                    float minBoundFlexibleHeight = 0f;
                    float maxBoundFlexibleHeight = 0f;
                    ComputeRangeFlexibleHeight(out minBoundFlexibleHeight, out maxBoundFlexibleHeight);
                
                    for(int i = 0; i < _mElements.Count; i++)
                    {
                        MyrmidonEditorLayoutElement element = _mElements[i];
                        float normalizedValue = (element.FlexibleHeight - minBoundFlexibleHeight) / (maxBoundFlexibleHeight - minBoundFlexibleHeight);
                        float additionalHeight = remainingHeight * normalizedValue;
                        rects[i].height += additionalHeight;
                    }
                }

                // Assigning Y position
                float currentY = _mRect.y + _mPaddingTop;
                for(int i = 0; i < _mElements.Count; i++)
                {
                    rects[i].y = currentY;
                    currentY += rects[i].height + _mSpacing; 
                }

                // Assigning rects
                for(int i = 0; i < _mElements.Count; i++)
                {
                    _mElements[i].SetRect(rects[i]);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        protected override float ComputeWidthAvailable(Rect position)
        {
            float widthAvailable = position.width;
            widthAvailable -= _mPaddingLeft;
            widthAvailable -= _mPaddingRight;

            return widthAvailable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        protected override float ComputeHeightAvailable(Rect position)
        {
            float heightAvailable = position.height;
            heightAvailable -= _mPaddingTop;
            heightAvailable -= _mPaddingBottom;
            heightAvailable -= _mSpacing * (_mElements.Count - 1);
        
            return heightAvailable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deltaWidth"></param>
        /// <param name="deltaHeight"></param>
        public override void ProcessResizing(float deltaWidth, float deltaHeight)
        {
            base.ProcessResizing(deltaWidth, deltaHeight);
            float totalHeightPercentage = GetTotalHeightPercentageOverLayout();

            float heightToAdd = 0f;
            for (int i = 0; i < _mElements.Count; i++)
            {
                MyrmidonEditorLayoutElement element = _mElements[i];
                float percentageOverLayoutNormalized = GetHeightPercentageOverLayout(element) / totalHeightPercentage;
                float heightAttributed = (percentageOverLayoutNormalized * deltaHeight);

                element.ProcessResizing(deltaWidth, heightAttributed);

                Rect rect = element.Rect;
                rect.y += heightToAdd;
                element.SetRect(rect);

                if (element.IsResizableHeight)
                    heightToAdd += heightAttributed;
            }
        }

        #endregion
    }
}