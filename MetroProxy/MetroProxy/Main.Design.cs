namespace MetroProxy
{
    using MetroProxy.My;
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public partial class Main : Form
    {
       
        [AccessedThroughProperty("Button1")]
        private Button _Button1;
        [AccessedThroughProperty("Button2")]
        private Button _Button2;
        [AccessedThroughProperty("Button3")]
        private Button _Button3;
        [AccessedThroughProperty("Button4")]
        private Button _Button4;
        [AccessedThroughProperty("Button5")]
        private Button _Button5;
        [AccessedThroughProperty("Button6")]
        private Button _Button6;
        [AccessedThroughProperty("Button7")]
        private Button _Button7;
        [AccessedThroughProperty("Label1")]
        private Label _Label1;
        [AccessedThroughProperty("Label2")]
        private Label _Label2;
        [AccessedThroughProperty("Label3")]
        private Label _Label3;
        [AccessedThroughProperty("Label4")]
        private Label _Label4;
        [AccessedThroughProperty("Label5")]
        private Label _Label5;
        [AccessedThroughProperty("Label6")]
        private Label _Label6;
        [AccessedThroughProperty("ListBox1")]
        private ListBox _ListBox1;
        [AccessedThroughProperty("ListBox2")]
        private ListBox _ListBox2;
        [AccessedThroughProperty("TextBox1")]
        private TextBox _TextBox1;
        private IContainer components;

        [DebuggerNonUserCode]
        public Main()
        {
            __ENCAddToList(this);
            this.InitializeComponent();
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> list = __ENCList;
            lock (list)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num3 = __ENCList.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
            }
        }

      
        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (this.components != null))
                {
                    this.components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Main));
            this.Button1 = new Button();
            this.ListBox1 = new ListBox();
            this.ListBox2 = new ListBox();
            this.Button2 = new Button();
            this.Button3 = new Button();
            this.Button4 = new Button();
            this.Label1 = new Label();
            this.Label2 = new Label();
            this.Button5 = new Button();
            this.Label3 = new Label();
            this.Label4 = new Label();
            this.Button6 = new Button();
            this.TextBox1 = new TextBox();
            this.Label5 = new Label();
            this.Label6 = new Label();
            this.Button7 = new Button();
            this.SuspendLayout();
            Point point2 = new Point(170, 12);
            this.Button1.Location = point2;
            this.Button1.Name = "Button1";
            Size size2 = new Size(0x8a, 0x17);
            this.Button1.Size = size2;
            this.Button1.TabIndex = 0;
            this.Button1.Text = "List Metro Apps";
            this.Button1.UseVisualStyleBackColor = true;
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.ItemHeight = 12;
            point2 = new Point(0x10, 0x44);
            this.ListBox1.Location = point2;
            this.ListBox1.Name = "ListBox1";
            size2 = new Size(0xdd, 0x58);
            this.ListBox1.Size = size2;
            this.ListBox1.TabIndex = 1;
            this.ListBox2.FormattingEnabled = true;
            this.ListBox2.ItemHeight = 12;
            point2 = new Point(0xff, 0x44);
            this.ListBox2.Location = point2;
            this.ListBox2.Name = "ListBox2";
            size2 = new Size(0xc50, 0x58);
            this.ListBox2.Size = size2;
            this.ListBox2.TabIndex = 2;
            point2 = new Point(0x1a, 0xb5);
            this.Button2.Location = point2;
            this.Button2.Name = "Button2";
            size2 = new Size(0xca, 0x17);
            this.Button2.Size = size2;
            this.Button2.TabIndex = 3;
            this.Button2.Text = "Enable Proxy for Selected";
            this.Button2.UseVisualStyleBackColor = true;
            point2 = new Point(0x1a, 0xdb);
            this.Button3.Location = point2;
            this.Button3.Name = "Button3";
            size2 = new Size(0xca, 0x17);
            this.Button3.Size = size2;
            this.Button3.TabIndex = 4;
            this.Button3.Text = "Disable Proxy for Selected";
            this.Button3.UseVisualStyleBackColor = true;
            point2 = new Point(0xff, 0xb5);
            this.Button4.Location = point2;
            this.Button4.Name = "Button4";
            size2 = new Size(0xca, 0x17);
            this.Button4.Size = size2;
            this.Button4.TabIndex = 5;
            this.Button4.Text = "Disable All";
            this.Button4.UseVisualStyleBackColor = true;
            this.Label1.AutoSize = true;
            point2 = new Point(0x59, 0x2b);
            this.Label1.Location = point2;
            this.Label1.Name = "Label1";
            size2 = new Size(0x35, 12);
            this.Label1.Size = size2;
            this.Label1.TabIndex = 6;
            this.Label1.Text = "Metro ID";
            this.Label2.AutoSize = true;
            point2 = new Point(0x135, 0x2b);
            this.Label2.Location = point2;
            this.Label2.Name = "Label2";
            size2 = new Size(0x59, 12);
            this.Label2.Size = size2;
            this.Label2.TabIndex = 7;
            this.Label2.Text = "Metro App Name";
            point2 = new Point(0xff, 0xdb);
            this.Button5.Location = point2;
            this.Button5.Name = "Button5";
            size2 = new Size(0xca, 0x17);
            this.Button5.Size = size2;
            this.Button5.TabIndex = 8;
            this.Button5.Text = "Show Proxy-enabled Apps";
            this.Button5.UseVisualStyleBackColor = true;
            this.Label3.AutoSize = true;
            point2 = new Point(0x157, 0x14b);
            this.Label3.Location = point2;
            this.Label3.Name = "Label3";
            size2 = new Size(0x65, 12);
            this.Label3.Size = size2;
            this.Label3.TabIndex = 9;
            this.Label3.Text = "Powered by dlhxr";
            this.Label4.AutoSize = true;
            this.Label4.Font = new Font("宋体", 12f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            point2 = new Point(40, 0x147);
            this.Label4.Location = point2;
            this.Label4.Name = "Label4";
            size2 = new Size(0xfb, 0x10);
            this.Label4.Size = size2;
            this.Label4.TabIndex = 10;
            this.Label4.Text = "Please Run as Administrator";
            point2 = new Point(0x11e, 280);
            this.Button6.Location = point2;
            this.Button6.Name = "Button6";
            size2 = new Size(0x4b, 0x17);
            this.Button6.Size = size2;
            this.Button6.TabIndex = 11;
            this.Button6.Text = "Set Proxy";
            this.Button6.UseVisualStyleBackColor = true;
            point2 = new Point(0x5b, 0x11a);
            this.TextBox1.Location = point2;
            this.TextBox1.Name = "TextBox1";
            size2 = new Size(0xbd, 0x15);
            this.TextBox1.Size = size2;
            this.TextBox1.TabIndex = 12;
            this.Label5.AutoSize = true;
            this.Label5.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            point2 = new Point(0x17, 0x11b);
            this.Label5.Location = point2;
            this.Label5.Name = "Label5";
            size2 = new Size(0x2f, 14);
            this.Label5.Size = size2;
            this.Label5.TabIndex = 13;
            this.Label5.Text = "Proxy";
            this.Label6.AutoSize = true;
            point2 = new Point(0x75, 0x108);
            this.Label6.Location = point2;
            this.Label6.Name = "Label6";
            size2 = new Size(0x83, 12);
            this.Label6.Size = size2;
            this.Label6.TabIndex = 14;
            this.Label6.Text = "Example: 127.0.0.1:80";
            point2 = new Point(0x16f, 280);
            this.Button7.Location = point2;
            this.Button7.Name = "Button7";
            size2 = new Size(90, 0x17);
            this.Button7.Size = size2;
            this.Button7.TabIndex = 15;
            this.Button7.Text = "Cancel Proxy";
            this.Button7.UseVisualStyleBackColor = true;
            SizeF ef2 = new SizeF(6f, 12f);
            this.AutoScaleDimensions = ef2;
            this.AutoScaleMode = AutoScaleMode.Font;
            size2 = new Size(0x1d5, 0x169);
            this.ClientSize = size2;
            this.Controls.Add(this.Button7);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Button6);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Button5);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Button4);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.ListBox2);
            this.Controls.Add(this.ListBox1);
            this.Controls.Add(this.Button1);
            this.Icon = (Icon)manager.GetObject("$this.Icon");
            this.Name = "Main";
            this.Text = "Metro Proxy";
            this.ResumeLayout(false);
            this.PerformLayout();
        }


        internal virtual Button Button1
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Button1;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                EventHandler handler = new EventHandler(this.Button1_Click);
                if (this._Button1 != null)
                {
                    this._Button1.Click -= handler;
                }
                this._Button1 = value;
                if (this._Button1 != null)
                {
                    this._Button1.Click += handler;
                }
            }
        }

        internal virtual Button Button2
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Button2;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                EventHandler handler = new EventHandler(this.Button2_Click);
                if (this._Button2 != null)
                {
                    this._Button2.Click -= handler;
                }
                this._Button2 = value;
                if (this._Button2 != null)
                {
                    this._Button2.Click += handler;
                }
            }
        }

        internal virtual Button Button3
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Button3;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                EventHandler handler = new EventHandler(this.Button3_Click);
                if (this._Button3 != null)
                {
                    this._Button3.Click -= handler;
                }
                this._Button3 = value;
                if (this._Button3 != null)
                {
                    this._Button3.Click += handler;
                }
            }
        }

        internal virtual Button Button4
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Button4;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                EventHandler handler = new EventHandler(this.Button4_Click);
                if (this._Button4 != null)
                {
                    this._Button4.Click -= handler;
                }
                this._Button4 = value;
                if (this._Button4 != null)
                {
                    this._Button4.Click += handler;
                }
            }
        }

        internal virtual Button Button5
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Button5;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                EventHandler handler = new EventHandler(this.Button5_Click);
                if (this._Button5 != null)
                {
                    this._Button5.Click -= handler;
                }
                this._Button5 = value;
                if (this._Button5 != null)
                {
                    this._Button5.Click += handler;
                }
            }
        }

        internal virtual Button Button6
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Button6;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                EventHandler handler = new EventHandler(this.Button6_Click);
                if (this._Button6 != null)
                {
                    this._Button6.Click -= handler;
                }
                this._Button6 = value;
                if (this._Button6 != null)
                {
                    this._Button6.Click += handler;
                }
            }
        }

        internal virtual Button Button7
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Button7;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                EventHandler handler = new EventHandler(this.Button7_Click);
                if (this._Button7 != null)
                {
                    this._Button7.Click -= handler;
                }
                this._Button7 = value;
                if (this._Button7 != null)
                {
                    this._Button7.Click += handler;
                }
            }
        }

        internal virtual Label Label1
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Label1;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                this._Label1 = value;
            }
        }

        internal virtual Label Label2
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Label2;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                this._Label2 = value;
            }
        }

        internal virtual Label Label3
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Label3;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                this._Label3 = value;
            }
        }

        internal virtual Label Label4
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Label4;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                this._Label4 = value;
            }
        }

        internal virtual Label Label5
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Label5;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                this._Label5 = value;
            }
        }

        internal virtual Label Label6
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Label6;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                this._Label6 = value;
            }
        }

        internal virtual ListBox ListBox1
        {
            [DebuggerNonUserCode]
            get
            {
                return this._ListBox1;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                EventHandler handler = new EventHandler(this.ListBox1_SelectedIndexChanged);
                if (this._ListBox1 != null)
                {
                    this._ListBox1.SelectedIndexChanged -= handler;
                }
                this._ListBox1 = value;
                if (this._ListBox1 != null)
                {
                    this._ListBox1.SelectedIndexChanged += handler;
                }
            }
        }

        internal virtual ListBox ListBox2
        {
            [DebuggerNonUserCode]
            get
            {
                return this._ListBox2;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                EventHandler handler = new EventHandler(this.ListBox2_SelectedIndexChanged);
                if (this._ListBox2 != null)
                {
                    this._ListBox2.SelectedIndexChanged -= handler;
                }
                this._ListBox2 = value;
                if (this._ListBox2 != null)
                {
                    this._ListBox2.SelectedIndexChanged += handler;
                }
            }
        }

        internal virtual TextBox TextBox1
        {
            [DebuggerNonUserCode]
            get
            {
                return this._TextBox1;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                this._TextBox1 = value;
            }
        }
    }
}

