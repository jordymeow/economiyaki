using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Finance.Framework.Core.Graphics;
using Finance.Framework.Core.Properties;

namespace Finance.Framework.Core
{
    public class MarketCtrlHelper
    {
        public event MessageEventHandler MessageEvent;
        public event UnlinkedEventHandler UnlinkEvent;
        public IList<Guid> InputModules 
        { 
            get
            {
                List<Guid> guids = new List<Guid>();
                foreach (IGfxModule mod in _listMarketCtrl)
                    guids.Add(mod.ID);
                return guids;
            } 
        }

        private readonly IComponent _parentControl;
        private readonly IList<IGfxModule> _listMarketCtrl = new List<IGfxModule>();
        private readonly ToolStrip _toolStrip;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketCtrlHelper"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="dropControls">The drop controls.</param>
        /// <param name="toolStrip">The tool strip.</param>
        public MarketCtrlHelper(IComponent parent, IEnumerable<Control> dropControls, ToolStrip toolStrip)
        {
            _toolStrip = toolStrip;
            _parentControl = parent;
            _parentControl.Disposed += MarketExport_Disposed;
            if (dropControls != null)
                foreach (Control current in dropControls)
                    MakeControlDroppable(current);
            MakeControlDroppable(toolStrip);
        }

        /// <summary>
        /// Starts the drag.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        public void StartDrag(object sender, MouseEventArgs e)
        {
            DataObject data = new DataObject(typeof(IGfxModule).FullName, _parentControl);
            if (sender is ToolStripLabel)
                ((ToolStripLabel)sender).DoDragDrop(data, DragDropEffects.Link);
            else if (sender is Control)
                ((Control)sender).DoDragDrop(data, DragDropEffects.Link);
            
        }

        /// <summary>
        /// Makes the control droppable.
        /// </summary>
        /// <param name="ctrl">The CTRL.</param>
        private void MakeControlDroppable(Control ctrl)
        {
            if (ctrl == null) return;
            ctrl.AllowDrop = true;
            ctrl.DragEnter += LinkableObject_DragEnter;
            ctrl.DragDrop += LinkableObject_DragDrop;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketCtrlHelper"/> class.
        /// </summary>
        /// <param name="parent">The control.</param>
        /// <param name="dropControl">The control where to drop another module.</param>
        /// <param name="toolStrip">The toolstrip menu used in the module.</param>
        public MarketCtrlHelper(IComponent parent, Control dropControl, ToolStrip toolStrip)
        {
            _toolStrip = toolStrip;
            _parentControl = parent;
            _parentControl.Disposed += MarketExport_Disposed;
            if (dropControl == null) return;
            MakeControlDroppable(dropControl);
            MakeControlDroppable(toolStrip);
        }

        /// <summary>
        /// Handles the Disposed event of the MarketExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void MarketExport_Disposed(object sender, EventArgs e)
        {
            foreach (IGfxModule item in _listMarketCtrl)
                item.MessageEvent -= Helper_MessageEvent;
            _listMarketCtrl.Clear();
        }

        /// <summary>
        /// Handles the DragEnter event of the LinkableObject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        void LinkableObject_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(IGfxModule))) return;
            IGfxModule ctrl = (IGfxModule)e.Data.GetData(typeof(IGfxModule));
            if (ctrl == null || ctrl == _parentControl || _listMarketCtrl.Contains(ctrl))
                e.Effect = DragDropEffects.None;
            else if (ctrl != null)
                e.Effect = DragDropEffects.Link;
        }

        /// <summary>
        /// Links an input module.
        /// </summary>
        /// <param name="module">The module.</param>
        public void InputModuleLink(IGfxModule module)
        {
            if (module == null || module == _parentControl || _listMarketCtrl.Contains(module)) return;
            module.MessageEvent += Helper_MessageEvent;
            module.UnlinkedEvent += Helper_Unlink;
            ToolStripLabel label = new ToolStripLabel(Resources.link);
            label.MouseEnter += Link_MouseEnter;
            label.MouseLeave += Link_MouseLeave;
            label.MouseUp += Link_MouseClick;
            label.Tag = module;
            _toolStrip.Items.Add(label);
            _listMarketCtrl.Add(module);
        }

        /// <summary>
        /// Handles the DragDrop event of the LinkableObject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        void LinkableObject_DragDrop(object sender, DragEventArgs e)
        {
            InputModuleLink((IGfxModule)e.Data.GetData(typeof(IGfxModule)));
        }

        /// <summary>
        /// Helper_s the message event.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="msgList">The MSG list.</param>
        private void Helper_MessageEvent(object source, GfxMessage msg, IList<GfxMessage> msgList)
        {
            if (MessageEvent == null && (msgList == null || msgList.Count == 0)) return;
            MessageEvent.Invoke(source, msg, msgList);
        }

        /// <summary>
        /// Handles the MouseClick event of the Link control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void Link_MouseClick(object sender, EventArgs args)
        {
            IGfxModule marketCtrl = (IGfxModule)((ToolStripItem)sender).Tag;
            _listMarketCtrl.Remove(marketCtrl);
            marketCtrl.MessageEvent -= Helper_MessageEvent;
            _toolStrip.Items.Remove((ToolStripItem)sender);
            if (UnlinkEvent != null)
                UnlinkEvent.Invoke(sender, marketCtrl);
        }

        /// <summary>
        /// Handles the MouseLeave event of the Link control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void Link_MouseLeave(object sender, EventArgs args)
        {
            ((ToolStripItem)sender).Image = Resources.link;
        }

        /// <summary>
        /// Handles the MouseEnter event of the Link control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void Link_MouseEnter(object sender, EventArgs args)
        {
            ((ToolStripItem)sender).ToolTipText = ((IGfxModule)((ToolStripItem)sender).Tag).MyakiName;
            ((ToolStripItem)sender).Image = Resources.link_break;
        }

        /// <summary>
        /// Handles the Unlink event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void Helper_Unlink(object sender, IGfxModule module)
        {
            for (int c = 0; c < _toolStrip.Items.Count; c++)
                if (_toolStrip.Items[c].Tag == module)
                {
                    _toolStrip.Items.RemoveAt(c);
                    return;
                }
            if (UnlinkEvent != null)
                UnlinkEvent.Invoke(sender, module);
        }
    }
}
