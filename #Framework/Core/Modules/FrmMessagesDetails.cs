using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Finance.MarketAccess;
using Finance.Framework.Types;

namespace Finance.Framework.Core.Graphics
{
    public partial class FrmMessagesDetails : Form
    {
        private readonly IList<GfxMessage> _messages;

        public FrmMessagesDetails(IList<GfxMessage> messages)
        {
            InitializeComponent();
            _messages = messages;
        }

        private static void AddDataToNode(TreeNode node, Data data)
        {
            TreeNode nd = node.Nodes.Add("Guid: " + data.Guid);
            nd.Tag = ".Guid";
            nd = node.Nodes.Add("Time: " + data.Time);
            nd.Tag = ".Time";
            nd = node.Nodes.Add("Security: " + data.Security);
            nd.Tag = ".Security";
            TreeNode fields = node.Nodes.Add("Fields");
            foreach (DataFieldItem currentDataField in data)
            {
                nd = fields.Nodes.Add(currentDataField.Field + ": " + currentDataField.Value);
                nd.Tag = "[\"" + currentDataField.Field + "\"]";
            }
        }

        private static void AddHistoryDataToNode(TreeNode node, HistoryData data)
        {
            TreeNode nd = node.Nodes.Add("Guid: " + data.Guid);
            nd.Tag = ".Guid";
            nd = node.Nodes.Add("Time: " + data.Time);
            nd.Tag = ".Time";
            nd = node.Nodes.Add("Security: " + data.Security);
            nd.Tag = ".Security";
            TreeNode fields = node.Nodes.Add("Dates");
            fields.Tag = "";
            foreach (HistoryDataItem currentDataItem in data)
            {
                TreeNode ndRoot = fields.Nodes.Add(currentDataItem.Time.ToShortDateString());
                ndRoot.Tag = "[\"" + currentDataItem.Time.ToShortDateString() + "\"]";
                foreach (DataFieldItem currentField in currentDataItem)
                {
                    nd = ndRoot.Nodes.Add(currentField.Field + ": " + currentField.Value);
                    nd.Tag = "[\"" + currentField.Field + "\"]";
                }
            }
        }

        private void FrmMessagesDetails_Load(object sender, EventArgs e)
        {
            foreach (GfxMessage current in _messages)
            {
                string key = current.MessageID.ToString();
                if (treeView.Nodes.ContainsKey(key))
                    continue;
                if (current.ContentType == typeof(MultiData))
                {
                    TreeNode root = treeView.Nodes.Add(key, "MultiData (" + current.Content + ")");
                    root.Tag = current;
                    MultiData data = (MultiData)current.Content;
                    foreach (Data currentData in data)
                    {
                        TreeNode newRoot = root.Nodes.Add(key, "Data (" + currentData + ")");
                        newRoot.Tag = "[\"" + currentData.Security + "\"]";
                        AddDataToNode(newRoot, currentData);
                    }
                }
                else if (current.ContentType == typeof(Data))
                {
                    TreeNode root = treeView.Nodes.Add(key, "Data (" + current.Content + ")");
                    root.Tag = current;
                    AddDataToNode(root, (Data)current.Content);
                }
                else if (current.ContentType == typeof(MultiHistoryData))
                {
                    TreeNode root = treeView.Nodes.Add(key, "MultiHistoryData (" + current.Content + ")");
                    root.Tag = current;
                    MultiHistoryData data = (MultiHistoryData)current.Content;
                    TreeNode nd = root.Nodes.Add("Guid: " + data.Guid);
                    nd.Tag = ".Guid";
                    nd = root.Nodes.Add("Time: " + data.Time);
                    nd.Tag = ".Time";
                    foreach (HistoryData currentData in data)
                    {
                        TreeNode newRoot = root.Nodes.Add(key, "HistoryData (" + currentData + ")");
                        newRoot.Tag = "[\"" + currentData.Security + "\"]";
                        AddHistoryDataToNode(newRoot, currentData);
                    }
                }
                else if (current.ContentType == typeof(HistoryData))
                {
                    TreeNode root = treeView.Nodes.Add(key, "HistoryData (" + current.Content + ")");
                    root.Tag = current;
                    AddHistoryDataToNode(root, (HistoryData)current.Content);
                }
                else
                {
                    TreeNode root = treeView.Nodes.Add(key, "Unknown format : " + current.MessageType + " (" + current.Content + ")");
                    root.Tag = current;
                }

            }
            lblMessages.Text = treeView.Nodes.Count.ToString();
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode currentNode = e.Node;
            string str = "";
            while (currentNode.Tag is String)
            {
                str = (string)currentNode.Tag + str;
                currentNode = currentNode.Parent;
            }
            lblPath.Text = "i" + str;
        }

        private void mnuRemove_Click(object sender, EventArgs e)
        {
            if (!(treeView.SelectedNode.Tag is GfxMessage)) return;
            _messages.Remove((GfxMessage)treeView.SelectedNode.Tag);
            treeView.SelectedNode.Remove();
            lblMessages.Text = treeView.Nodes.Count.ToString();
        }

        private void mnuRemoveAll_Click(object sender, EventArgs e)
        {
            _messages.Clear();
            treeView.Nodes.Clear();
            lblMessages.Text = treeView.Nodes.Count.ToString();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            FrmMessagesDetails_Load(sender, e);
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            refreshTimer.Stop();
            Close();
        }
    }
}