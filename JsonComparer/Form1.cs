using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonComparer
{
    public partial class Form1 : Form
    {
        private int treeViewSpace;

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.DefaultExt = openFileDialog2.DefaultExt = "object";
            openFileDialog1.FileName = openFileDialog2.FileName = "scene.object";
            treeViewSpace = treeView2.Location.X - treeView1.Location.X - treeView1.Size.Width;
        }

        private void btnOpenFile1_Click(object sender, EventArgs e)
        {
            GetFileFromDialog(openFileDialog1, tbFile1, treeView1);
        }

        private void btnOpenFile2_Click(object sender, EventArgs e)
        {
            GetFileFromDialog(openFileDialog2, tbFile2, treeView2);
        }

        private void GetFileFromDialog(FileDialog dialog, TextBox fileName, TreeView destTreeView)
        {
            var result = dialog.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK)
                return;
            fileName.Text = dialog.FileName;
            var jsonObject = GetJsonText(dialog.FileName);
            if (jsonObject == null)
                return;
            SetJsonToTreeView(jsonObject, destTreeView);
        }

        private JObject GetJsonText(string fileName)
        {
            var o1 = JObject.Parse(File.ReadAllText(fileName).Replace("\t", "    "));
            return o1;
        }

        private void SetJsonToTreeView(JToken json, TreeView destTreeView)
        {
            destTreeView.BeginUpdate();
            destTreeView.Nodes.Clear();
            var rootNode = new TreeNode("root");
            destTreeView.Nodes.Add(rootNode);
            AddChildNodesToTree(json, rootNode);
            destTreeView.ExpandAll();
            destTreeView.EndUpdate();
        }

        private void AddChildNodesToTree(JToken json, TreeNode parent)
        {
            foreach (var childNode in json.Children())
            {
                var namedNode = childNode as JProperty;
                var nextLevelParent = parent;
                if (namedNode != null)
                {
                    nextLevelParent = new TreeNode(namedNode.Name);
                    parent.Nodes.Add(nextLevelParent);
                }
                if (childNode.HasValues)
                    AddChildNodesToTree(childNode, nextLevelParent);
                else
                    nextLevelParent.Text = string.Format("{0}: \"{1}\"", nextLevelParent.Text, childNode.ToString());
            }
        }

        private void FormSizeChanged(object sender, EventArgs e)
        {
            var newWidth = (treeView2.Location.X + treeView2.Size.Width - treeView1.Location.X - treeViewSpace) / 2;
            treeView1.Size = new System.Drawing.Size(newWidth, treeView1.Size.Height);
            treeView2.Location = new Point(treeView1.Location.X + newWidth + treeViewSpace, treeView2.Location.Y);
            treeView2.Size = new System.Drawing.Size(newWidth, treeView2.Size.Height);
        }
    }
}
