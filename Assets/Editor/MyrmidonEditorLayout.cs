namespace Myrmidon.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// MyrmidonEditorLayout is the base class for layout system in Myrmidon.
    /// It basically defines all needed functionnality for the layout.
    /// 
    /// This class is abstract and need to be overrided. In Myrmidon, there is
    /// 2 classes overriding : Vertical and Horizontal.
    /// </summary>
    public abstract class MyrmidonEditorLayout : MyrmidonEditorLayoutElement
    {
        #region Fields

        #region Internal Fields

        /// <summary>
        /// List of all MyrmidonEditorLayoutElement contains in the layout
        /// @see Myrmidon.Editor.MyrmidonEditorLayoutElement
        /// </summary>
        protected List<MyrmidonEditorLayoutElement> _mElements;
        
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
        /// Boolean to indicate if we want the layout to force children to expand on the maximum width available
        /// If this boolean is set to false, and element in layout doesn't have a flexible width / preferred width,
        ///     then element will have a width set to 0
        /// </summary>
        protected bool _mForceChildToExpandWidth;

        /// <summary>
        /// Boolean to indicate if we want the layout to force children to expand on the maximum height available
        /// If this boolean is set to false, and element in layout doesn't have a flexible height / preferred height,
        ///     then element will have a height set to 0
        /// </summary>
        protected bool _mForceChildToExpandHeight;

        /// <summary>
        /// Boolean to indicate if we want the layout elements to be resizable.
        /// If this boolean is set to true, catchable handles will be added to the layout to resize the elements
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

        #endregion

        #region Methods

        #region Constructors / Lifecycle

        /// <summary>
        /// Default constructor for MyrmidonEditorLayout, this one is not recommanded because it calls internally
        /// the default constrctor of MyrmidonEditorElement which is like an empty element without any size specified
        /// </summary>
        /// <param name="elements">List of MyrmidonEditorLayoutElement that are part of the layout</param>
        /// <param name="forceExpandWidth">boolean should the layout force expand children on width</param>
        /// <param name="forceExpandHeight">boolean should the layout force expand children on height</param>
        /// <param name="canResize">boolean can the user resize the layout's children</param>
        public MyrmidonEditorLayout(List<MyrmidonEditorLayoutElement> elements, bool forceExpandWidth, bool forceExpandHeight, bool canResize):
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


        /// <summary>
        /// Constructor for MyrmidonEditorLayout, this one is the recommanded one, because it allows you to 
        /// initialized the layout's size at the same time
        /// </summary>
        /// <param name="elements">List of MyrmidonEditorLayoutElement that are part of the layout</param>
        /// <param name="forceExpandWidth">boolean should the layout force expand children on width</param>
        /// <param name="forceExpandHeight">boolean should the layout force expand children on height</param>
        /// <param name="canResize">boolean can the user resize the layout's children</param>
        /// <param name="preferredWidth">the preferred width in pixel unit</param>
        /// <param name="preferredHeight">the preferred height in pixel unit</param>
        /// <param name="flexibleWidth">the flexible width in layout's relative unit</param>
        /// <param name="flexibleHeight">the flexible height in layout's relative unit</param>
        public MyrmidonEditorLayout(List<MyrmidonEditorLayoutElement> elements, bool forceExpandWidth, bool forceExpandHeight, bool canResize, float preferredWidth, float preferredHeight, float flexibleWidth, float flexibleHeight):
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

        #endregion

        #region Publics

        #region Commons
        #endregion

        #region Getters / Setters

        /// <summary>
        /// SetPadding can be called before computing the layouting rects to specify a padding inside the layout
        /// </summary>
        /// <param name="top">float representing top padding</param>
        /// <param name="bottom">float representing bottom padding</param>
        /// <param name="left">float representing left padding</param>
        /// <param name="right">float representing right pading</param>
        public void SetPadding(float top, float bottom, float left, float right)
        {
            _mPaddingTop = top;
            _mPaddingBottom = bottom;
            _mPaddingLeft = left;
            _mPaddingRight = right;
        }

        #endregion

        #region Abstract / Virtuals / Overrides

        /// <summary>
        /// ComputeRects is an abstract method that need to be override by inheritent classes. This
        /// method has to bee called to compute rects of all elements contained in the layout.
        /// 
        /// The method is abtract because the computation is layout dependant and we cannot know without
        /// the layout context how to assign rects (for example, width in a vertical layout is not handle
        /// at all like width in a horizontal layout).
        /// 
        /// However, even if the calculation is context dependant, it follow the same logic for each implementation and
        /// we'll explain it here.
        /// We'll firstly assign sizes that are not dependent from the layout. So if it is a vertical layout, we'll assign
        /// X position and width because it is not dependant from the number of element present and their flexible width. The same
        /// logic applies for y position and height for horizontal layout.
        /// Then we'll make the second part in two step, firstly assigning preferred size then flexible size. For the preferred
        /// size that's not complicated, we simply set the width / height to the preferred value stored in the layout element.
        /// Then, when all preferred width / height has been applied, and if our layout is forcing to expand or some element
        /// has their flexible width / height greater than 0, we'll distribute the remaining space not occupied from the preferred
        /// size calculation. To do so, we'll normalized the flexible width / height within the range [0, SumOfAllFlexible], compute
        /// the remaining width / height and distribute according to normalized value.
        /// </summary>
        public abstract void ComputeRects();
        

        /// <summary>
        /// @see MyrmidonEditorLayoutElement.ProcessEvents(Event)
        /// 
        /// The override method call ProcessEvents on all element and apply a "OR" operation
        /// on result to determine if we need to repaint the window
        /// </summary>
        /// <param name="e">Event coming from Unity's editor window</param>
        /// <returns></returns>
        public override bool ProcessEvents(Event e)
        {
            bool repaint = false;

            repaint |= base.ProcessEvents(e);
            foreach (MyrmidonEditorLayoutElement element in _mElements)
            {
                repaint |= element.ProcessEvents(e);
            }

            return repaint;
        }

        /// <summary>
        /// @see MyrmidonEditorLayoutElement.Draw()
        /// 
        /// The override method call Draw on all element in the layout
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            foreach (MyrmidonEditorLayoutElement element in _mElements)
            {
                element.Draw();
            }
        }

        #endregion

        #endregion

        #region Privates / Protected

        #region Commons

        /// <summary>
        /// ComputeRangeFlexibleWidth allow to compute the minimum flexible width and maximum flexible width in 
        /// the layout. Basically, the method return can be represente as a bound [0,X] where 0 is the minimum
        /// width in relative unity (because if flexible width of elements are less than 0, we clamp the value to 0),
        /// and X is sum of all flexible width in the layout's elements.
        /// 
        /// This method is use during the rects computing because we want to normalize for each layout's element in a range
        /// which is provided by this method. We compute the X as a sum of all flexible width because we want to use 
        /// the normalized value to know how much pixel we need to add according to flexible unit proportion beside all
        /// layout's elements.
        /// </summary>
        /// <param name="minHeight">out float representing the computed minumum width in relative unit (0 in our case)</param>
        /// <param name="maxHeight">out float representing the maximum bound of flexible width</param>
        protected void ComputeRangeFlexibleWidth(out float minWidth, out float maxWidth)
        {
            minWidth = 0f;
            maxWidth = 0f;

            foreach (MyrmidonEditorLayoutElement element in _mElements)
            {
                maxWidth += element.FlexibleWidth;
            }
        }

        /// <summary>
        /// ComputeRangeFlexibleHeight allow to compute the minimum flexible height and maximum flexible height in 
        /// the layout. Basically, the method return can be represente as a bound [0,X] where 0 is the minimum
        /// height in relative unity (because if flexible height of elements are less than 0, we clamp the value to 0),
        /// and X is sum of all flexible height in the layout's elements.
        /// 
        /// This method is use during the rects computing because we want to normalize for each layout's element in a range
        /// which is provided by this method. We compute the X as a sum of all flexible height because we want to use 
        /// the normalized value to know how much pixel we need to add according to flexible unit proportion beside all
        /// layout's elements.
        /// </summary>
        /// <param name="minHeight">out float representing the computed minumum height in relative unit (0 in our case)</param>
        /// <param name="maxHeight">out float representing the maximum bound of flexible height</param>
        protected void ComputeRangeFlexibleHeight(out float minHeight, out float maxHeight)
        {
            minHeight = 0f;
            maxHeight = 0f;

            foreach (MyrmidonEditorLayoutElement element in _mElements)
            {
                maxHeight += element.FlexibleHeight;
            }
        }

        /// <summary>
        /// GetWidthPercentageOverLayout returns a float to know in a normalized value [0, 1] how many space the 
        /// element given in parameter occupied beside the width of the layout.
        /// </summary>
        /// <param name="element">the element that we want to test</param>
        /// <returns>float representing the width occupied in normalized value [0,1]</returns>
        protected float GetWidthPercentageOverLayout(MyrmidonEditorLayoutElement element)
        {
            float widthLayout = _mRect.width - (PaddingLeft + PaddingRight);
            float widthElement = element.Rect.width;

            return widthElement / widthLayout;
        }

        /// <summary>
        /// GetHeightPercentageOverLayout returns a float to know in a normalized value [0, 1] how many space the 
        /// element given in parameter occupied beside the height of the layout.
        /// </summary>
        /// <param name="element">the element that we want to test</param>
        /// <returns>float representing the height occupied in normalized value [0,1]</returns>
        protected float GetHeightPercentageOverLayout(MyrmidonEditorLayoutElement element)
        {
            float heightLayout = _mRect.height - (PaddingTop + PaddingBottom);
            float heightElement = element.Rect.height;

            return heightElement / heightLayout;
        }

        /// <summary>
        /// GetTotalWidthPercentageOverLayout returns a float to know in a normalized value [0,1] how many space 
        /// it is occupied in width by the totality of the elements in the layout. 
        /// 
        /// To compute it, we use another method that compute the width in a normalized value [0,1] of an element
        /// and simply add results.
        /// @see MyrmidonEditorLayout.GetHeightPercentageOverLayout(MyrmidonEditorLayoutElement)
        /// </summary>
        /// <returns>float representing the total width occupied by elements in the layout, normalized value [0,1] </returns>
        protected float GetTotalWidthPercentageOverLayout()
        {
            float widthAcculumator = 0;
            foreach (MyrmidonEditorLayoutElement element in _mElements)
            {
                widthAcculumator += GetWidthPercentageOverLayout(element);
            }

            return widthAcculumator;
        }

        /// <summary>
        /// GetTotalHeightPercentageOverLayout returns a float to know in a normalized value [0,1] how many space 
        /// it is occupied in height by the totality of the elements in the layout. To compute it, we use another method that
        /// compute the height in a normalized value [0,1] of an element and simply add results.
        /// 
        /// @see MyrmidonEditorLayout.GetHeightPercentageOverLayout(MyrmidonEditorLayoutElement)
        /// </summary>
        /// <returns>float representing the total height occupied by elements in the layout, normalized value [0,1] </returns>
        protected float GetTotalHeightPercentageOverLayout()
        {
            float heightAccumulator = 0f;
            foreach (MyrmidonEditorLayoutElement element in _mElements)
            {
                heightAccumulator += GetHeightPercentageOverLayout(element);
            }

            return heightAccumulator;
        }

        /// <summary>
        /// Atleast1FlexibleHeight returns a boolean to know if there is at least 1 element in the layout
        /// that has his flexible height greater than 0. Meaning that if the layout is forcing to expand height
        /// or that's there is at leat 1 element that has a flexible height, we'll compute for elements, the additional
        /// width based on flexible height unit.
        /// </summary>
        /// <returns></returns>
        protected bool Atleast1FlexibleHeight()
        {
            foreach (MyrmidonEditorLayoutElement element in _mElements)
            {
                if (element.FlexibleHeight > 0)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// AtLeast1FlexibleWidth returns a boolean to know if there is at least 1 element in the layout
        /// that has his flexible width greater than 0. Meaning that if the layout is forcing to expand width
        /// or that's there is at leat 1 element that has a flexible width, we'll compute for elements, the additional
        /// width based on flexible width unit.
        /// </summary>
        /// <returns></returns>
        protected bool AtLeast1FlexibleWidth()
        {
            foreach (MyrmidonEditorLayoutElement element in _mElements)
            {
                if (element.FlexibleWidth > 0)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// NbResizableWidthElements compute the number of elements that are resizable in width in the layout.
        /// 
        /// It will be used to calculate the reallocation of size when we are resizing the window.
        /// For example, a MyrmidonEditorResizeElement is not resizable in width when in a MyrmidonEditorHorizontalLayout
        /// </summary>
        /// <returns>int representing the number of element resizable in width in the layout</returns>
        protected int NbResizableWidthElements()
        {
            int counter = 0;
            foreach (MyrmidonEditorLayoutElement element in _mElements)
            {
                if (element.IsResizableWidth)
                {
                    counter++;
                }
            }

            return counter;
        }

        /// <summary>
        /// NbResizableHeightElements compute the number of elements that are resizable in height in the layout.
        /// 
        /// It will be used to calculate the reallocation of size when we are resizing the window.
        /// For example, a MyrmidonEditorResizeElement is not resizable in height when in a MyrmidonEditorVerticalLayout
        /// </summary>
        /// <returns>int representing the number of element resizable in height in the layout</returns>
        protected int NbResizableHeightElements()
        {
            int counter = 0;
            foreach (MyrmidonEditorLayoutElement element in _mElements)
            {
                if (element.IsResizableHeight)
                {
                    counter++;
                }
            }

            return counter;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void AddResizableToElements()
        {
            List<MyrmidonEditorLayoutElement> elementsWithResizable = new List<MyrmidonEditorLayoutElement>();
            for (int i = 0; i < _mElements.Count; i++)
            {
                elementsWithResizable.Add(_mElements[i]);

                if (i < _mElements.Count - 1)
                {
                    MyrmidonEditorLayoutElement previousPanel = _mElements[i];
                    MyrmidonEditorLayoutElement nextPanel = _mElements[i + 1];
                    MyrmidonEditorResizerElement resizer = CreateResizerForLayout(previousPanel, nextPanel);
                    resizer.AssignBackgroundColor(Color.black);
                    elementsWithResizable.Add(resizer);
                }
            }

            _mElements = elementsWithResizable;
        }

        #endregion

        #region Abstract / Virtuals / Overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        protected abstract float ComputeWidthAvailable(Rect position);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        protected abstract float ComputeHeightAvailable(Rect position);

        /// <summary>
        /// CreateResizerForLayout is an abstract method that needs to be override by inheritant class to define
        /// how we create a resizer for the layout. The reason why we are using an override implementation is that
        /// depending on the layout, size's settings will be different (flexible width / height amongst others)
        /// 
        /// We'll also give as arguments the previous and next elements (ie : which are surrounding the resizer), because
        /// the resizer will need thoses references when resizing events in handle.
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        protected abstract MyrmidonEditorResizerElement CreateResizerForLayout(MyrmidonEditorLayoutElement previous, MyrmidonEditorLayoutElement next);
        
        #endregion

        #endregion

        #endregion
    }
}