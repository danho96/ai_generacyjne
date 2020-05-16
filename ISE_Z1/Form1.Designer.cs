namespace ISE_Z1
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.buttonRun = new System.Windows.Forms.Button();
			this.graph = new ZedGraph.ZedGraphControl();
			this.buttonCreateFunction = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.WorkerSupportsCancellation = true;
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			// 
			// buttonRun
			// 
			this.buttonRun.Location = new System.Drawing.Point(12, 12);
			this.buttonRun.Name = "buttonRun";
			this.buttonRun.Size = new System.Drawing.Size(75, 23);
			this.buttonRun.TabIndex = 0;
			this.buttonRun.Text = "Uruchom";
			this.buttonRun.UseVisualStyleBackColor = true;
			this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
			// 
			// graph
			// 
			this.graph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.graph.Location = new System.Drawing.Point(12, 83);
			this.graph.Name = "graph";
			this.graph.ScrollGrace = 0D;
			this.graph.ScrollMaxX = 0D;
			this.graph.ScrollMaxY = 0D;
			this.graph.ScrollMaxY2 = 0D;
			this.graph.ScrollMinX = 0D;
			this.graph.ScrollMinY = 0D;
			this.graph.ScrollMinY2 = 0D;
			this.graph.Size = new System.Drawing.Size(268, 171);
			this.graph.TabIndex = 1;
			// 
			// buttonCreateFunction
			// 
			this.buttonCreateFunction.Location = new System.Drawing.Point(113, 12);
			this.buttonCreateFunction.Name = "buttonCreateFunction";
			this.buttonCreateFunction.Size = new System.Drawing.Size(75, 39);
			this.buttonCreateFunction.TabIndex = 2;
			this.buttonCreateFunction.Text = "Stwórz przebieg";
			this.buttonCreateFunction.UseVisualStyleBackColor = true;
			this.buttonCreateFunction.Click += new System.EventHandler(this.buttonCreateFunction_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.buttonCreateFunction);
			this.Controls.Add(this.graph);
			this.Controls.Add(this.buttonRun);
			this.Name = "Form1";
			this.Text = "BIAI";
			this.ResumeLayout(false);

		}

		#endregion

		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.Button buttonRun;
		private ZedGraph.ZedGraphControl graph;
		private System.Windows.Forms.Button buttonCreateFunction;
	}
}

