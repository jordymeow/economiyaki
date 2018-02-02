using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;
using Finance.Framework;
using Finance.Framework.Core.Serialization;
using Finance.Framework.Core;
using System.Xml;
using System.Xml.Serialization;

namespace Finance.Jumbler
{
    public partial class FrmMain : Form
    {
        readonly FrmToolbox toolbox = new FrmToolbox();
        readonly MdiClient _mdiClient;
        private MWFFile _currentFile = new MWFFile();
        private Stream _currentStream;

        public FrmMain()
        {
            InitializeComponent();
            foreach (Control ctl in Controls)
            {
                if (!(ctl is MdiClient)) continue;
                _mdiClient = (MdiClient)ctl;
                _mdiClient.BackColor = Color.FromKnownColor(KnownColor.Desktop);
                break;
            }
            pnlLoading.Hide();
        }

        private void FrmDynamicPlace_Load(object sender, EventArgs e)
        {
            toolbox.MdiParent = this;
            toolbox.Show();
        }

        private void FrmDynamicPlace_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        void frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_currentStream != null)
                _currentStream.Close();
            foreach (Form ctrl in _mdiClient.MdiChildren)
                if (ctrl != null)
                    ctrl.Dispose();
        }

        private void mnuToolbox_Click(object sender, EventArgs e)
        {
            if (mnuToolbox.Checked)
            {
                toolbox.Visible = false;
                mnuToolbox.Checked = false;
            }
            else
            {
                toolbox.Visible = true;
                mnuToolbox.Checked = true;
            }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentStream == null)
                {
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.CheckPathExists = true;
                    saveDialog.Filter = "MWF File (.mwf)|*.mwf";
                    if (saveDialog.ShowDialog() != DialogResult.OK)
                        return;
                    _currentStream = File.Create(saveDialog.FileName);
                    FileInfo fi = new FileInfo(saveDialog.FileName);
                    Text = Application.ProductName + " - " + fi.Name;
                }

                _currentFile.WindowSize = Size;
                _currentFile.MWF_Modules.Clear();
                foreach (Form ctrl in _mdiClient.MdiChildren)
                {
                    FrmContainer container = ctrl as FrmContainer;
                    if (container == null) continue;
                    IGfxModule module = container.Module;
                    UserControl userCtrl = container.Module as UserControl;
                    if (module == null || userCtrl == null) continue;
                    if (userCtrl.ContainsFocus)
                        _currentFile.FocusedModule = module.ID;
                    _currentFile.MWF_Modules.Add(module.GetMWF(module.MyakiName, container.Size, container.Location, container.WindowState));
                }

                _currentStream.Position = 0;
                XmlSerializer xml = new XmlSerializer(typeof(MWFFile));
                xml.Serialize(_currentStream, _currentFile);
            }
            catch (Exception ex)
            {
                if (_currentStream != null)
                {
                    _currentStream.Close();
                    _currentStream = null;
                }
                Text = Application.ProductName;
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmDynamicPlace_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                IList<string> formats = new List<string>(e.Data.GetFormats());
                if (!formats.Contains("System.RuntimeType")) return;
                UserControl ctrl = (UserControl)Activator.CreateInstance((Type)e.Data.GetData("System.RuntimeType"));
                IGfxModule iCtrl = (IGfxModule)ctrl;
                FrmContainer container = new FrmContainer(iCtrl);
                container.StartPosition = FormStartPosition.Manual;
                int BorderWidth = (Width - ClientSize.Width) / 2;
                int TitlebarHeight = Height - ClientSize.Height - 2 * BorderWidth;
                container.Size = iCtrl.MyakiSize;
                container.Location = new Point(e.X - Location.X - 15, e.Y - Location.Y - 55);
                container.MdiParent = this;
                container.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFile(object p)
        {
            string path = p as String;
            try
            {
                pnlLoading.Show();
                SuspendLayout();
                if (_currentStream != null)
                    mnuNew_Click(null, null);
                _currentStream = File.Open(path, FileMode.Open);
                XmlSerializer xml = new XmlSerializer(typeof(MWFFile));
                _currentFile = xml.Deserialize(_currentStream) as MWFFile;
                if (_currentFile == null) return;
                if (_currentFile.WindowSize.Height > 0 && _currentFile.WindowSize.Width > 0)
                    Size = _currentFile.WindowSize;
                Dictionary<Guid, IGfxModule> mods = new Dictionary<Guid, IGfxModule>();
                Dictionary<Guid, MWFBase> mwfs = new Dictionary<Guid, MWFBase>();
                foreach (MWFBase current in _currentFile.MWF_Modules)
                {
                    IGfxModule module = (IGfxModule)Activator.CreateInstance(current.ModuleType, current);
                    if (module.ID == Guid.Empty)
                        module.ID = Guid.NewGuid();
                    mods.Add(module.ID, module);
                    mwfs.Add(module.ID, current);
                    FrmContainer container = new FrmContainer(module);
                    container.MdiParent = this;
                    container.StartPosition = FormStartPosition.Manual;
                    container.Location = current.Location;
                    container.WindowState = current.WindowState;
                    container.Show();
                    container.Size = current.Size;
                }
                foreach (MWFBase current in _currentFile.MWF_Modules)
                {
                    MWFBase mwfConcerned = mwfs[current.ID];
                    IGfxModule moduleConcerned = mods[current.ID];
                    foreach (Guid moduleId in mwfConcerned.InputModules)
                        moduleConcerned.AddInputModule(mods[moduleId]);
                }
                if (mods.ContainsKey(_currentFile.FocusedModule))
                {
                    ActiveControl = ((UserControl)mods[_currentFile.FocusedModule]);
                    ActiveControl.BringToFront();
                }
                FileInfo fi = new FileInfo(path);
                Text = Application.ProductName + " - " + fi.Name;
            }
            catch (Exception ex)
            {
                if (_currentStream != null)
                {
                    _currentStream.Close();
                    _currentStream = null;
                }
                Text = Application.ProductName;
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                pnlLoading.Hide();
                ResumeLayout();
            }
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.CheckPathExists = true;
            openDialog.CheckFileExists = true;
            openDialog.Filter = "MWF File (.mwf)|*.mwf";
            if (openDialog.ShowDialog() != DialogResult.OK)
                return;
            LoadFile(openDialog.FileName);
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            if (_currentStream != null)
                _currentStream.Close();
            _currentStream = null;
            foreach (Form ctrl in _mdiClient.MdiChildren)
            {
                if (ctrl != null && ctrl is FrmContainer)
                    ctrl.Dispose();
            }
            Text = Application.ProductName;
            _currentFile = new MWFFile();
        }

        private void mnuProperties_Click(object sender, EventArgs e)
        {
            FileProperties frmProp = new FileProperties();
            frmProp.txtAuthor.Text = _currentFile.Author;
            frmProp.txtDescription.Text = _currentFile.Description;
            frmProp.txtTitle.Text = _currentFile.Title;
            if (frmProp.ShowDialog() != DialogResult.OK) return;
            _currentFile.Author = frmProp.txtAuthor.Text;
            _currentFile.Description = frmProp.txtDescription.Text;
            _currentFile.Title = frmProp.txtTitle.Text;
        }
    }
}