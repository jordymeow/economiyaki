using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Finance.Framework.Core.Serialization;
using System.ComponentModel;

internal class EmbeddedResourceFinder { }

namespace Finance.Framework.Core
{
    public delegate void MessageEventHandler(object source, GfxMessage msg, IList<GfxMessage> msgList);
    public delegate void UnlinkedEventHandler(object source, IGfxModule module);

    public enum GraphicBorderStyle
    {
        Fixed,
        Sizable
    }

    public interface IGfxModule
    {
        /// <summary>
        /// Occurs when there is a one or more messages.
        /// </summary>
        event MessageEventHandler MessageEvent;

        /// <summary>
        /// Occurs when there is a move request.
        /// </summary>
        event MouseEventHandler MoveRequest;

        /// <summary>
        /// Occurs when the module is unlinked.
        /// </summary>
        event UnlinkedEventHandler UnlinkedEvent;

        /// <summary>
        /// Gets the ID.
        /// </summary>
        /// <value>The ID.</value>
        Guid ID { get; set; }

        /// <summary>
        /// Gets the name of the market CTRL.
        /// </summary>
        /// <value>The name of the market CTRL.</value>
        string MyakiName { get; }

        /// <summary>
        /// Gets the size of the market CTRL.
        /// </summary>
        /// <value>The size of the market CTRL.</value>
        Size MyakiSize { get; }

        /// <summary>
        /// Gets the market CTRL form border style.
        /// </summary>
        /// <value>The market CTRL form border style.</value>
        GraphicBorderStyle MyakiBorderStyle { get; }

        IList<Guid> InputModules { get; }

        /// <summary>
        /// Links with the given module as input.
        /// </summary>
        /// <param name="module">The module.</param>
        void AddInputModule(IGfxModule module);

        /// <summary>
        /// Get the MWF data for this module.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="size">The size.</param>
        /// <param name="location">The location.</param>
        /// <param name="windowState">State of the window.</param>
        /// <returns>The MWF data.</returns>
        MWFBase GetMWF(string title, Size size, Point location, FormWindowState windowState);

        /// <summary>
        /// Pushes the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void AddMessage(GfxMessage message);

        /// <summary>
        /// Pushes the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void AddMessages(IList<GfxMessage> messages);
    }
}
