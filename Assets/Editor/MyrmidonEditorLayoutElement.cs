namespace Myrmidon.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    /// <summary>
    /// MyrmidonEditorLayoutElement is the base class for Myrmidon's layout system for window and custom editor.
    /// All element that you want to be rendered and computed by Myrmidon's layout system have you derive from that class.
    /// </summary>
    public class MyrmidonEditorLayoutElement
    {
        #region Fields

        #region Internal Fields

        /// <summary>
        /// Rect used for rendering et computing position in layout system
        /// @see Rect in Unity's documentation
        /// </summary>
        protected Rect _mRect;
        
        /// <summary>
        /// Color that must be display in background of the layout element
        /// @see Color in Unity's documentation
        /// </summary>
        protected Color _mBackgroundColor;
        
        /// <summary>
        /// Boolean to know if the element can be resized on the width
        /// </summary>
        protected bool _mIsResizableWidth;

        /// <summary>
        /// Boolean to know if the elemnt can be resized on the height
        /// </summary>
        protected bool _mIsResizableHeight;

        /// <summary>
        /// Float representing the preferred width of the layout element.
        /// 
        /// Preferred width is computed first in the layout system's algorithm. It mean that
        /// this value represent a size in pixel unit that our element need to have and the 
        /// system will prioritize it over the flexible width
        /// </summary>
        protected float _mPreferredWidth;

        /// <summary>
        /// Float representing the preferred height of the layout element
        /// 
        /// Preferred height is computed first in the layout system's algorithm. It mean that
        /// this value represent a size in pixel unit that our element need to have and the 
        /// system will prioritize it over the flexible height
        /// </summary>
        protected float _mPreferredHeight;
        
        /// <summary>
        /// Float representing the flexible width of the layout element
        /// 
        /// Flexible width is used at last in the layout system's algorithm. This value represent
        /// a size in relative unit that is normalized in the range of [0, SumOfAllFlexibleWidthInLayout].
        /// After that all preferred width has been assigned, the algorithm will takes the remaining space and
        /// attribute a portion according to the normalized value to the rect of the layout element.
        /// 
        /// A flexible width over 0 will be considered and will get attributed with a portion of the remaining width.
        /// Whereas, a flexible width of 0 will not get any additional width in a layout forcing the elements to fill 
        /// all the width available.
        /// </summary>
        protected float _mFlexibleWidth;
        
        /// <summary>
        /// Float representing the flexible height of the layout element
        /// 
        /// Flexible height is used at last in the layout system's algorithm. This value represent
        /// a size in relative unit that is normalized in the range of [0, SumOfAllFlexibleHeightInLayout].
        /// After that all preferred height has been assigned, the algorithm will takes the remaining space and
        /// attribute a portion according to the normalized value to the rect of the layout element.
        ///     
        /// A flexible height over 0 will be considered and will get attributed with a portion of the remaining height.
        /// Whereas, a flexible height of 0 will not get any additional height in a layout forcing the elements to fill 
        /// all the height available.
        /// </summary>
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

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        /// Default constructor of a layout element.
        /// The default constructor initialize as follow :
        ///     - The element is resizable in both width and height
        ///     - The element has no preferred width/height and no flexible width / height
        ///       Basically meaning that they are 0x0 pixels and so not visible.
        /// </summary>
        public MyrmidonEditorLayoutElement()
        {
            _mIsResizableWidth = true;
            _mIsResizableHeight = true;
            _mPreferredWidth = 0;
            _mPreferredHeight = 0;
            _mFlexibleWidth = 0;
            _mFlexibleHeight = 0;
        }

        /// <summary>
        /// Recommended constructor of a layout element.
        /// This constructor will ensure that user define intentionally the preferred and flexible size.
        /// This constructor is also setting resizable true on both width and height
        /// </summary>
        /// <param name="preferredWidth">the preferred width in pixel unit</param>
        /// <param name="preferredHeight">the preferred height in pixel unit</param>
        /// <param name="flexibleWidth">the flexible width in layout's relative unit</param>
        /// <param name="flexibleHeight">the flexible height in layout's relative unit</param>
        public MyrmidonEditorLayoutElement(float preferredWidth, float preferredHeight, float flexibleWidth, float flexibleHeight)
        {
            _mIsResizableWidth = true;
            _mIsResizableHeight = true;
            _mPreferredWidth = (preferredWidth < 0) ? (0) : (preferredWidth);
            _mPreferredHeight = (preferredHeight < 0) ? (0) : (preferredHeight);
            _mFlexibleWidth = (flexibleWidth < 0) ? (0) : (flexibleWidth);
            _mFlexibleHeight = (flexibleHeight < 0) ? (0) : (flexibleHeight);
        }

        #endregion

        #region Publics

        /// <summary>
        /// ProcessResizing handle the resizing process in different situations.
        /// 
        /// It can be call when the window is resized or when the layout element is part of
        /// a MyrmidonEditorLayout with has a resizable elements property.
        /// 
        /// The method basically add width and height to the rect if the respective resizable property
        /// is set to true
        /// </summary>
        /// <param name="deltaWidth">the width to add on rect</param>
        /// <param name="deltaHeight">the height to add on rect</param>
        public virtual void ProcessResizing(float deltaWidth, float deltaHeight)
        {
            if (_mIsResizableWidth)
            {
                _mRect.width += deltaWidth;
            }

            if (_mIsResizableHeight)
            {
                _mRect.height += deltaHeight;
            }
        }

        /// <summary>
        /// ProcessEvent is called at OnGUI EditorWindow's method, basically each time an event is trigger or repaint is called.
        /// Then the event is dispatched to every MyrmidonEditorLayoutElement and handle directly in this method
        /// @see Event in Unity's documentation
        /// @see EditorWindow in Unity's documentation
        /// </summary>
        /// <param name="e">Event coming from Unity's editor window</param>
        /// <returns></returns>
        public virtual bool ProcessEvents(Event e)
        {
            return false;
        }

        /// <summary>
        /// ProcessEvent is called at OnGUI EditorWindow's method, basically each time an event is trigger or repaint is called.
        /// Then the call is propagated to every layout and element and handle directly in this method
        /// @see EditorWindow in Unity's documentation
        /// </summary>
        public virtual void Draw()
        {
            if (_mRect != null)
            {
                EditorGUI.DrawRect(_mRect, _mBackgroundColor);
                GUILayout.BeginArea(_mRect);
                GUILayout.EndArea();
            }
        }

        /// <summary>
        /// AssignRect can we called when we want to assign a new rect to our element. For example when the window's size
        /// change or we are resizing panel in the layout.
        /// </summary>
        /// <param name="rect">rect that will be assigned</param>
        public void AssignRect(Rect rect)
        {
            _mRect = rect;
        }

        public void AssignBackgroundColor(Color color)
        {
            _mBackgroundColor = color;
        }

        #endregion

        #region Privates / Protected
        #endregion

        #endregion
    }
}

