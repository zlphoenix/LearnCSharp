using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using SolutionMaker.Core.Model;

namespace SolutionMaker.UI
{
    public partial class PreviewForm : Form
    {
        private static Dictionary<ProjectType, string> ProjectIcons = new Dictionary<ProjectType, string>();
        private Dictionary<SolutionProject, TreeNode> projectNodes = new Dictionary<SolutionProject, TreeNode>();

        public Solution Solution { get; set; }
        public string SolutionFile { get; set; }

        static PreviewForm()
        {
            ProjectIcons.Add(ProjectType.SolutionFolder, "SolutionFolder");
            ProjectIcons.Add(ProjectType.CSharp, "CSharp");
            ProjectIcons.Add(ProjectType.VB, "VB");
            ProjectIcons.Add(ProjectType.VC, "Cpp");
            ProjectIcons.Add(ProjectType.VCX, "Cpp");
            ProjectIcons.Add(ProjectType.FSharp, "FSharp");
            ProjectIcons.Add(ProjectType.Wix, "Wix");
        }

        public PreviewForm()
        {
            InitializeComponent();
        }

        private void PreviewForm_Load(object sender, EventArgs e)
        {
            CreateImageList();
            PopulateSolutionTree();
            this.treeSolution.SelectedNode = this.treeSolution.TopNode;
            this.treeSolution.TopNode.Expand();
            this.labelPath.Text = this.SolutionFile;
        }

        private void CreateImageList()
        {
            string[] iconNames = { "Solution", "SolutionFolder", "CSharp", "VB", "Cpp", "FSharp", "Wix" };
            this.treeSolution.ImageList = new ImageList();
            iconNames.ToList().ForEach(x => this.treeSolution.ImageList.Images.Add(x, CreateIcon(x)));
        }

        private void PopulateSolutionTree()
        {
            this.projectNodes.Clear();
            this.treeSolution.Nodes.Clear();
            var root = CreateRootNode();
            this.treeSolution.Nodes.Add(root);

            foreach (var project in this.Solution.Projects)
            {
                CreateProjectNode(root, project);
            }
        }

        private Icon CreateIcon(string iconName)
        {
            var iconStore = string.Format("{0}.Icons", this.GetType().Namespace);
            return new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("{0}.{1}.ico", iconStore, iconName)));
        }

        private TreeNode CreateRootNode()
        {
            var node = new TreeNode(string.Format(@"Solution '{0}' ({1} projects and folders)", Path.GetFileNameWithoutExtension(this.SolutionFile), Solution.Projects.Count));
            node.ImageKey = "Solution";
            return node;
        }

        private TreeNode CreateProjectNode(TreeNode root, SolutionProject project)
        {
            if (this.projectNodes.ContainsKey(project))
                return projectNodes[project];

            var parent = root;
            if (project.ParentProject != null)
            {
                parent = CreateProjectNode(root, project.ParentProject);
            }

            var node = new TreeNode(project.Name);
            node.Tag = project;
            var projectType = project.IsFolder ? ProjectType.SolutionFolder : ProjectTypes.Find(project.ProjectTypeId);
            node.ImageKey = ProjectIcons[projectType];
            node.SelectedImageKey = node.ImageKey;
            parent.Nodes.Add(node);
            this.projectNodes.Add(project, node);
            return node;
        }

        private void treeSolution_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var project = e.Node.Tag as SolutionProject;
            if (project != null)
            {
                this.labelPath.Text = project.Path;
            }
            else
            {
                this.labelPath.Text = this.SolutionFile;
            }
        }
    }
}
