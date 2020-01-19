namespace Myrmidon.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MyrmidonEditorHorizontalLayout : MyrmidonEditorLayout
    {
        #region Methods
        
        public MyrmidonEditorHorizontalLayout(MyrmidonLayoutElement[] elements, bool forceExpandWidth, bool forceExpandHeight):
            base(elements, forceExpandWidth, forceExpandHeight)
        {

        }

        public MyrmidonEditorHorizontalLayout(MyrmidonLayoutElement[] elements, bool forceExpandWidth, bool forceExpandHeight, float preferredWidth, float preferredHeight, float flexibleWidth, float flexibleHeight):
            base(elements, forceExpandWidth, forceExpandHeight, preferredWidth, preferredHeight, flexibleWidth, flexibleHeight)
        {

        }

        public override void ComputeRects()
        {
            if(_mRect != null)
            {
                int nbElement = _mElements.Length;
                float widthAvailable = ComputeWidthAvailable(_mRect);
                float heightAvailable = ComputeHeightAvailable(_mRect);
                float remainingWidth = widthAvailable;
                Rect[] rects = new Rect[_mElements.Length];
                
                // Assigning height and Y position
                for(int i = 0; i < _mElements.Length; i++)
                {
                    MyrmidonLayoutElement element = _mElements[i];
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
                for(int i = 0; i < _mElements.Length; i++)
                {
                    MyrmidonLayoutElement element = _mElements[i];
                    rects[i].width = element.PreferredWidth;

                    remainingWidth -= rects[i].width;
                }

                // Assigning height flexible size
                if(_mForceChildToExpandWidth || AtLeast1FlexibleWidth())
                {
                    float minBoundFlexibleWidth = 0f;
                    float maxBoundFlexibleWidth = 0f;
                    ComputeRangeFlexibleWidth(out minBoundFlexibleWidth, out maxBoundFlexibleWidth);
                
                    for(int i = 0; i < _mElements.Length; i++)
                    {
                        MyrmidonLayoutElement element = _mElements[i];
                        float normalizedValue = (element.FlexibleWidth - minBoundFlexibleWidth) / (maxBoundFlexibleWidth - minBoundFlexibleWidth);
                        Debug.Log("Normalized value for " + i + " : " +  normalizedValue);
                        float additionalWidth = remainingWidth * normalizedValue;
                        Debug.Log("Additional width for " + i + " : " +  additionalWidth);
                        rects[i].width += additionalWidth;
                    }
                }

                // Assigning X position
                float currentX = _mRect.x + _mPaddingLeft;
                for(int i = 0; i < _mElements.Length; i++)
                {
                    rects[i].x = currentX;
                    currentX += rects[i].width + _mSpacing; 
                }

                // Assigning rects
                for(int i = 0; i < _mElements.Length; i++)
                {
                    _mElements[i].AssignRect(rects[i]);
                }
            }
        }

        protected override float ComputeWidthAvailable(Rect position)
        {
            float widthAvailable = position.width;
            widthAvailable -= _mPaddingLeft;
            widthAvailable -= _mPaddingRight;
            widthAvailable -= _mSpacing * (_mElements.Length - 1);

            return widthAvailable;
        }

        protected override float ComputeHeightAvailable(Rect position)
        {
            float heightAvailable = position.height;
            heightAvailable -= _mPaddingTop;
            heightAvailable -= _mPaddingBottom;
        
            return heightAvailable;
        }

        #endregion
    }
}