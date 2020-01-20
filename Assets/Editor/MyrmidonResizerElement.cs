namespace Myrmidon.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MyrmidonResizerElement : MyrmidonLayoutElement
    {
        #region Internal Fields
        private MyrmidonLayoutElement _mTopPanel;
        private MyrmidonLayoutElement _mBottomPanel;
        #endregion

        #region Methods
        public MyrmidonResizerElement(MyrmidonLayoutElement topPanel, MyrmidonLayoutElement bottomPanel):
            base()
        {
        }

        public MyrmidonResizerElement(float preferredWidth, float preferredHeight, float flexibleWidth, float flexibleHeight):
            base(preferredWidth, preferredHeight, flexibleWidth, flexibleHeight)
        {

        }

        public override void ProcessEvents(Event e)
        {
            switch(e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0 && _mRect.Contains(e.mousePosition))
                    {
                        // resizing
                    }
                    break;

                case EventType.MouseUp:
                    // Not resizing
                    break;

            }
        }

        #endregion
    }
}

