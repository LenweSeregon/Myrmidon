namespace  Myrmidon.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using Myrmidon.Editor;
    
    public abstract class MyrmidonEditorWindow : EditorWindow
    {
        //==========================================
        // Constantes
        //==========================================
        #region Constantes
        #endregion
		
        //==========================================
        // Fields
        //==========================================
        #region Fields
		
        #region Serialized Fields
        #endregion
		
        #region Internal Fields
        
        private string _mWindowName;
        protected MyrmidonEditorLayout _mWindowContainer;
        protected float _mLastWidthRegistered;
        protected float _mLastHeightRegistered;
        
        #endregion
		
        #endregion
		
        //==========================================
        // Methods
        //==========================================
        #region Methods
		
        #region Constructors / Lifecycle
        
        private void OnValidate() 
        {
	        InitializeWindow();
        }
        
        private void Update()
        {
	        float currentWidth = position.width;
	        float currentHeight = position.height;

	        if (currentWidth != _mLastWidthRegistered || currentHeight != _mLastHeightRegistered)
	        {
		        float deltaWidth = currentWidth - _mLastWidthRegistered;
		        float deltaHeight = currentHeight - _mLastHeightRegistered;
		        _mLastWidthRegistered = currentWidth;
		        _mLastHeightRegistered = currentHeight;
		        Resizing(deltaWidth, deltaHeight);
		        Repaint();
	        }
        }
        
        private void OnGUI() 
        {
	        if (_mWindowContainer == null) return;
            
	        _mWindowContainer.Draw();
	        bool repaintEvents = _mWindowContainer.ProcessEvents(Event.current);
	        if(repaintEvents || GUI.changed)
	        {
		        Repaint();
	        }
        }
        
        #endregion
		
        #region Publics
		
        #region Commons
        #endregion
        #region Getters / Setters
        #endregion
        #region Abstracts / Virtuals / Overrides
        
        public virtual void InitializeWindow()
        {
	        _mLastWidthRegistered = position.width;
	        _mLastHeightRegistered = position.height;
        }
        
        #endregion
		
        #endregion
		
        #region Protected / Privates
		
        #region Commons
        
        private void Resizing(float deltaWidth, float deltaHeight)
        {
	        if (_mWindowContainer == null) return;
            
	        _mWindowContainer.ProcessResizing(deltaWidth, deltaHeight);
        }
        
        #endregion		
        #region Abstract / Virtuals / Overrides
        #endregion
		
        #endregion
		
        #endregion
    }    
}