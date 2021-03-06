﻿namespace Myrmidon.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MyrmidonEditorHorizontalLayout : MyrmidonEditorLayout
    {
        #region Methods
        
        public MyrmidonEditorHorizontalLayout(List<MyrmidonEditorLayoutElement> elements, bool forceExpandWidth, bool forceExpandHeight, bool canResize):
            base(elements, forceExpandWidth, forceExpandHeight, canResize)
        {
            
        }

        public MyrmidonEditorHorizontalLayout(List<MyrmidonEditorLayoutElement> elements, bool forceExpandWidth, bool forceExpandHeight, bool canResize, float preferredWidth, float preferredHeight, float flexibleWidth, float flexibleHeight):
            base(elements, forceExpandWidth, forceExpandHeight, canResize, preferredWidth, preferredHeight, flexibleWidth, flexibleHeight)
        {
            
        }

        protected override MyrmidonEditorResizerElement CreateResizerForLayout(MyrmidonEditorLayoutElement previous, MyrmidonEditorLayoutElement next)
        {
            float resizerWidth = MyrmidonEditorResizerElement.RESIZER_SIZE;
            MyrmidonEditorResizerElement resizer = new MyrmidonEditorResizerElement(previous, next, resizerWidth, 0, 0, 1, MyrmidonResizerType.Horizontal, false, true);
            return resizer;
        }

        public override void ComputeRects()
        {
            if(_mRect != null)
            {
                int nbElement = _mElements.Count;
                float widthAvailable = ComputeWidthAvailable(_mRect);
                float heightAvailable = ComputeHeightAvailable(_mRect);
                float remainingWidth = widthAvailable;
                Rect[] rects = new Rect[_mElements.Count];
                
                // Assigning height and Y position
                for(int i = 0; i < _mElements.Count; i++)
                {
                    MyrmidonEditorLayoutElement element = _mElements[i];
                    rects[i].height = 0f;
                    rects[i].y = _mRect.y + _mPaddingTop;

                    if(_mForceChildToExpandHeight)
                    {
                        rects[i].height = heightAvailable;
                    }
                    else if(element.FlexibleHeight > 0)
                    {
                        rects[i].height = heightAvailable;
                    }
                    else if(element.PreferredHeight > 0)
                    {
                        rects[i].height = Mathf.Clamp(element.PreferredHeight, 0, heightAvailable);
                    }
                }
                
                // Assigning width preferred size
                for(int i = 0; i < _mElements.Count; i++)
                {
                    MyrmidonEditorLayoutElement element = _mElements[i];
                    rects[i].width = element.PreferredWidth;

                    remainingWidth -= rects[i].width;
                }

                // Assigning width flexible size
                if(_mForceChildToExpandWidth || AtLeast1FlexibleWidth())
                {
                    float minBoundFlexibleWidth = 0f;
                    float maxBoundFlexibleWidth = 0f;
                    ComputeRangeFlexibleWidth(out minBoundFlexibleWidth, out maxBoundFlexibleWidth);
                
                    for(int i = 0; i < _mElements.Count; i++)
                    {
                        MyrmidonEditorLayoutElement element = _mElements[i];
                        float normalizedValue = (element.FlexibleWidth - minBoundFlexibleWidth) / (maxBoundFlexibleWidth - minBoundFlexibleWidth);
                        float additionalWidth = remainingWidth * normalizedValue;
                        rects[i].width += additionalWidth;
                    }
                }

                // Assigning X position
                float currentX = _mRect.x + _mPaddingLeft;
                for(int i = 0; i < _mElements.Count; i++)
                {
                    rects[i].x = currentX;
                    currentX += rects[i].width + _mSpacing; 
                }

                // Assigning rects
                for(int i = 0; i < _mElements.Count; i++)
                {
                    _mElements[i].SetRect(rects[i]);
                }
            }
        }

        protected override float ComputeWidthAvailable(Rect position)
        {
            float widthAvailable = position.width;
            widthAvailable -= _mPaddingLeft;
            widthAvailable -= _mPaddingRight;
            widthAvailable -= _mSpacing * (_mElements.Count - 1);

            return widthAvailable;
        }

        protected override float ComputeHeightAvailable(Rect position)
        {
            float heightAvailable = position.height;
            heightAvailable -= _mPaddingTop;
            heightAvailable -= _mPaddingBottom;
        
            return heightAvailable;
        }

        public override void ProcessResizing(float deltaWidth, float deltaHeight)
        {
            base.ProcessResizing(deltaWidth, deltaHeight);
            float totalWidthPercentage = GetTotalWidthPercentageOverLayout();

            float widthToAdd = 0f;
            for (int i = 0; i < _mElements.Count; i++)
            {
                MyrmidonEditorLayoutElement element = _mElements[i];
                float percentageOverLayoutNormalized = GetWidthPercentageOverLayout(element) / totalWidthPercentage;
                float widthAttributed = (percentageOverLayoutNormalized * deltaWidth);

                element.ProcessResizing(widthAttributed, deltaHeight);

                Rect rect = element.Rect;
                rect.x += widthToAdd;
                element.SetRect(rect);

                if (element.IsResizableWidth)
                    widthToAdd += widthAttributed;
            }
        }

        #endregion
    }
}