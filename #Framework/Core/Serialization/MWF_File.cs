using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Finance.Framework.Core.Serialization
{
    [Serializable]
    public class MWFFile : ISerializable
    {
        // Description
        private readonly Guid ID = Guid.NewGuid();
        public string Title = "Untitled";
        public string Author = SystemInformation.UserName;
        public string Description = "Created on " + DateTime.Now.ToShortDateString()  + ".";
        public readonly string AuthorID = SystemInformation.UserName;
        public DateTime CreationDate = DateTime.Now;
        
        private string LastAuthorID = SystemInformation.UserName;
        private DateTime LastDate = DateTime.Now;

        // Settings
        public Size WindowSize;
        public List<MWFBase> MWF_Modules;
        public Guid FocusedModule;

        public MWFFile()
        {
            CreationDate = DateTime.Now;
            AuthorID = SystemInformation.UserName;
            MWF_Modules = new List<MWFBase>();
        }

        protected MWFFile(SerializationInfo info, StreamingContext context)
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
                    case "Description":
                        Description = info.GetString("Description");
                        break;
                    case "Author":
                        Author = info.GetString("Author");
                        break;
                    case "AuthorID":
                        AuthorID = info.GetString("AuthorID");
                        break;
                    case "CreationDate":
                        CreationDate = info.GetDateTime("CreationDate");
                        break;
                    case "LastAuthorID":
                        LastAuthorID = info.GetString("LastAuthorID");
                        break;
                    case "LastDate":
                        LastDate = info.GetDateTime("LastDate");
                        break;
                    case "MWF_Modules":
                        MWF_Modules = (List<MWFBase>)info.GetValue("MWF_Modules", typeof(List<MWFBase>));
                        break;
                    case "WindowSize":
                        WindowSize = (Size)info.GetValue("WindowSize", typeof(Size));
                        break;
                    default:
                        throw new Exception("This is not the last version of Application.ProductName.");
                }
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            LastAuthorID = SystemInformation.UserName;
            LastDate = DateTime.Now;

            info.AddValue("ID", ID);
            info.AddValue("Title", Title);
            info.AddValue("Description", Description);
            info.AddValue("Author", Author);
            info.AddValue("AuthorID", AuthorID);
            info.AddValue("CreationDate", CreationDate);
            info.AddValue("LastAuthorID", LastAuthorID);
            info.AddValue("LastDate", LastDate);
            info.AddValue("WindowSize", WindowSize); 
            info.AddValue("MWF_Modules", MWF_Modules);
        }
    }
}
