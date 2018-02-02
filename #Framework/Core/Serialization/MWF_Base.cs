using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Finance.Framework.Core.Serialization
{
    [Serializable]
    [XmlInclude(typeof(MWFData))]
    [XmlInclude(typeof(MWFExport))]
    [XmlInclude(typeof(MWFGraph))]
    [XmlInclude(typeof(MWFGrid))]
    [XmlInclude(typeof(MWFLogger))]
    [XmlInclude(typeof(MWFPython))]
    public abstract class MWFBase : ISerializable
    {
        public Guid ID;
        public string Title;
        public string Author;
        public string Description;
        public Size Size;
        public Point Location;
        public Guid[] InputModules;
        public FormWindowState WindowState;

        public abstract Type ModuleType { get; }

        protected MWFBase(Guid id, string title, Size size, Point location, FormWindowState windowState, ICollection<Guid> inputModules)
        {
            ID = id == Guid.Empty ? Guid.NewGuid() : id;
            Title = title;
            Size = size;
            Location = location;
            WindowState = windowState;
            if (inputModules != null)
            {
                InputModules = new Guid[inputModules.Count];
                inputModules.CopyTo(InputModules, 0);
            }
            else
                InputModules = new Guid[0];
        }

        protected MWFBase(SerializationInfo info, StreamingContext context)
        {
            foreach (SerializationEntry o in info)
            {
                switch (o.Name)
                {
                    case "ID":
                        ID = (Guid)info.GetValue("ID", typeof(Guid));
                        break;
                    case "Title":
                        Title = info.GetString("Title");
                        break;
                    case "Size":
                        Size = (Size)info.GetValue("Size", typeof(Size));
                        break;
                    case "Location":
                        Location = (Point)info.GetValue("Location", typeof(Point));
                        break;
                    case "WindowState":
                        WindowState = (FormWindowState)info.GetValue("WindowState", typeof(FormWindowState));
                        break;
                    case "InputModules":
                        InputModules = (Guid[])info.GetValue("InputModules", typeof(Guid[]));
                        break;
                }
            }
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", ID);
            info.AddValue("Title", Title);
            info.AddValue("Size", Size);
            info.AddValue("Location", Location);
            info.AddValue("WindowState", WindowState);
            info.AddValue("InputModules", InputModules);
        }
    }
}
