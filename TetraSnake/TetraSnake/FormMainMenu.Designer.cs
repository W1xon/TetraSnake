namespace TetraSnake
{
    partial class FormMainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainMenu));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonStartTetris = new System.Windows.Forms.Button();
            this.buttonStartSnake = new System.Windows.Forms.Button();
            this.buttonStartTetraSnake = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonRecord = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(927, 523);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // buttonStartTetris
            // 
            this.buttonStartTetris.BackColor = System.Drawing.Color.Aquamarine;
            this.buttonStartTetris.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStartTetris.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.buttonStartTetris.Location = new System.Drawing.Point(214, 421);
            this.buttonStartTetris.Name = "buttonStartTetris";
            this.buttonStartTetris.Size = new System.Drawing.Size(160, 90);
            this.buttonStartTetris.TabIndex = 1;
            this.buttonStartTetris.Text = "Tetris";
            this.buttonStartTetris.UseVisualStyleBackColor = false;
            this.buttonStartTetris.Click += new System.EventHandler(this.buttonStartTetris_Click);
            // 
            // buttonStartSnake
            // 
            this.buttonStartSnake.BackColor = System.Drawing.Color.Aquamarine;
            this.buttonStartSnake.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStartSnake.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.buttonStartSnake.Location = new System.Drawing.Point(546, 421);
            this.buttonStartSnake.Name = "buttonStartSnake";
            this.buttonStartSnake.Size = new System.Drawing.Size(160, 90);
            this.buttonStartSnake.TabIndex = 2;
            this.buttonStartSnake.Text = "Snake";
            this.buttonStartSnake.UseVisualStyleBackColor = false;
            this.buttonStartSnake.Click += new System.EventHandler(this.buttonStartSnake_Click);
            // 
            // buttonStartTetraSnake
            // 
            this.buttonStartTetraSnake.BackColor = System.Drawing.Color.Aquamarine;
            this.buttonStartTetraSnake.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStartTetraSnake.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.buttonStartTetraSnake.Location = new System.Drawing.Point(380, 351);
            this.buttonStartTetraSnake.Name = "buttonStartTetraSnake";
            this.buttonStartTetraSnake.Size = new System.Drawing.Size(160, 160);
            this.buttonStartTetraSnake.TabIndex = 3;
            this.buttonStartTetraSnake.Text = "Tetris + Snake";
            this.buttonStartTetraSnake.UseVisualStyleBackColor = false;
            this.buttonStartTetraSnake.Click += new System.EventHandler(this.buttonStartTetraSnake_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.Aquamarine;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.Location = new System.Drawing.Point(827, 0);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(100, 40);
            this.buttonExit.TabIndex = 4;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonRecord
            // 
            this.buttonRecord.BackColor = System.Drawing.Color.Aquamarine;
            this.buttonRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRecord.Location = new System.Drawing.Point(0, 0);
            this.buttonRecord.Name = "buttonRecord";
            this.buttonRecord.Size = new System.Drawing.Size(144, 40);
            this.buttonRecord.TabIndex = 5;
            this.buttonRecord.Text = "Record";
            this.buttonRecord.UseVisualStyleBackColor = false;
            this.buttonRecord.Click += new System.EventHandler(this.buttonRecord_Click);
            // 
            // FormMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 523);
            this.ControlBox = false;
            this.Controls.Add(this.buttonRecord);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonStartTetraSnake);
            this.Controls.Add(this.buttonStartSnake);
            this.Controls.Add(this.buttonStartTetris);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Menu";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonStartTetris;
        private System.Windows.Forms.Button buttonStartSnake;
        private System.Windows.Forms.Button buttonStartTetraSnake;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonRecord;
    }
}