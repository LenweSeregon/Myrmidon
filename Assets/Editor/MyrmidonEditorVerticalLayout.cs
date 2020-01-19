namespace Myrmidon.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MyrmidonEditorVerticalLayout : MyrmidonEditorLayout
    {
        #region Methods
        
        public MyrmidonEditorVerticalLayout(MyrmidonLayoutElement[] elements, bool forceExpandWidth, bool forceExpandHeight):
            base(elements, forceExpandWidth, forceExpandHeight)
        {

        }

        /// <summary>
        /// La manière de fonctionner est la suivante :
        ///     - On assigne tout d'abord toutes les preferred size qui sont des valeurs en pixels 
        ///     - Si on le layout force l'occupation du panel où qu'au moins 1 element est en 'FlexibleSize', on assigne ensuite les flexible size qui sont des valeurs relatives 
        ///         par rapport à la taille qui reste après les preferred size enlevés . On va donc pour cela récupérer les valeurs de bornes inférieurs et supérieur et normaliser 
        ///         les valeurs pour connaître l'espace qu'ils vont occuper en plus
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public override void ComputeRects()
        {
            if(_mRect != null)
            {
                int nbElement = _mElements.Length;
                float widthAvailable = ComputeWidthAvailable(_mRect);
                float heightAvailable = ComputeHeightAvailable(_mRect);
                float remainingHeight = heightAvailable;
                Rect[] rects = new Rect[_mElements.Length];
                
                // Assigning width and X position
                for(int i = 0; i < _mElements.Length; i++)
                {
                    MyrmidonLayoutElement element = _mElements[i];
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
                for(int i = 0; i < _mElements.Length; i++)
                {
                    MyrmidonLayoutElement element = _mElements[i];
                    rects[i].height = element.PreferredHeight;

                    remainingHeight -= rects[i].height;
                }

                // Assigning height flexible size
                if(_mForceChildToExpandHeight || Atleast1FlexibleHeight())
                {
                    float minBoundFlexibleHeight = 0f;
                    float maxBoundFlexibleHeight = 0f;
                    ComputeRangeFlexibleHeight(out minBoundFlexibleHeight, out maxBoundFlexibleHeight);
                
                    for(int i = 0; i < _mElements.Length; i++)
                    {
                        MyrmidonLayoutElement element = _mElements[i];
                        float normalizedValue = (element.FlexibleHeight - minBoundFlexibleHeight) / (maxBoundFlexibleHeight - minBoundFlexibleHeight);
                        float additionalHeight = remainingHeight * normalizedValue;
                        rects[i].height += additionalHeight;
                    }
                }

                // Assigning Y position
                float currentY = _mRect.y + _mPaddingTop;
                for(int i = 0; i < _mElements.Length; i++)
                {
                    rects[i].y = currentY;
                    currentY += rects[i].height + _mSpacing; 
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

            return widthAvailable;
        }

        protected override float ComputeHeightAvailable(Rect position)
        {
            float heightAvailable = position.height;
            heightAvailable -= _mPaddingTop;
            heightAvailable -= _mPaddingBottom;
            heightAvailable -= _mSpacing * (_mElements.Length - 1);
        
            return heightAvailable;
        }

        #endregion
    }
}