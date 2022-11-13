namespace TetraSnake
{
    partial class FormTetraSnake
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTetraSnake));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBoxGameTetraSnake = new System.Windows.Forms.PictureBox();
            this.labelRecord = new System.Windows.Forms.Label();
            this.checkBoxAddTetramino = new System.Windows.Forms.CheckBox();
            this.checkBoxDeathTetramino = new System.Windows.Forms.CheckBox();
            this.checkBoxLevel3 = new System.Windows.Forms.CheckBox();
            this.checkBoxLevel2 = new System.Windows.Forms.CheckBox();
            this.checkBoxLevel1 = new System.Windows.Forms.CheckBox();
            this.buttonExit = new System.Windows.Forms.Button();
            this.labelScore = new System.Windows.Forms.Label();
            this.labelScoreLine = new System.Windows.Forms.Label();
            this.pictureBoxFigures = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timerSnake = new System.Windows.Forms.Timer(this.components);
            this.timerScore = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameTetraSnake)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFigures)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBoxGameTetraSnake);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.labelRecord);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxAddTetramino);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxDeathTetramino);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxLevel3);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxLevel2);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxLevel1);
            this.splitContainer1.Panel2.Controls.Add(this.buttonExit);
            this.splitContainer1.Panel2.Controls.Add(this.labelScore);
            this.splitContainer1.Panel2.Controls.Add(this.labelScoreLine);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBoxFigures);
            this.splitContainer1.Size = new System.Drawing.Size(1095, 466);
            this.splitContainer1.SplitterDistance = 735;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyDown);
            this.splitContainer1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyUp);
            // 
            // pictureBoxGameTetraSnake
            // 
            this.pictureBoxGameTetraSnake.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxGameTetraSnake.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxGameTetraSnake.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxGameTetraSnake.Name = "pictureBoxGameTetraSnake";
            this.pictureBoxGameTetraSnake.Size = new System.Drawing.Size(731, 462);
            this.pictureBoxGameTetraSnake.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxGameTetraSnake.TabIndex = 0;
            this.pictureBoxGameTetraSnake.TabStop = false;
            // 
            // labelRecord
            // 
            this.labelRecord.AutoSize = true;
            this.labelRecord.BackColor = System.Drawing.Color.White;
            this.labelRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelRecord.Font = new System.Drawing.Font("Unispace", 16F, System.Drawing.FontStyle.Bold);
            this.labelRecord.Location = new System.Drawing.Point(238, 13);
            this.labelRecord.Name = "labelRecord";
            this.labelRecord.Size = new System.Drawing.Size(106, 26);
            this.labelRecord.TabIndex = 11;
            this.labelRecord.Text = "Рекорд:";
            // 
            // checkBoxAddTetramino
            // 
            this.checkBoxAddTetramino.AutoSize = true;
            this.checkBoxAddTetramino.Font = new System.Drawing.Font("Unispace", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAddTetramino.Location = new System.Drawing.Point(137, 330);
            this.checkBoxAddTetramino.Name = "checkBoxAddTetramino";
            this.checkBoxAddTetramino.Size = new System.Drawing.Size(170, 27);
            this.checkBoxAddTetramino.TabIndex = 10;
            this.checkBoxAddTetramino.Text = "Доп. Тетрамино";
            this.checkBoxAddTetramino.UseVisualStyleBackColor = true;
            this.checkBoxAddTetramino.Visible = false;
            this.checkBoxAddTetramino.CheckedChanged += new System.EventHandler(this.checkBoxAddTetramino_CheckedChanged);
            this.checkBoxAddTetramino.KeyDown += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyDown);
            this.checkBoxAddTetramino.KeyUp += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyUp);
            // 
            // checkBoxDeathTetramino
            // 
            this.checkBoxDeathTetramino.AutoSize = true;
            this.checkBoxDeathTetramino.Font = new System.Drawing.Font("Unispace", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxDeathTetramino.Location = new System.Drawing.Point(137, 363);
            this.checkBoxDeathTetramino.Name = "checkBoxDeathTetramino";
            this.checkBoxDeathTetramino.Size = new System.Drawing.Size(137, 50);
            this.checkBoxDeathTetramino.TabIndex = 9;
            this.checkBoxDeathTetramino.Text = "Смертельные\r\n тетрамино";
            this.checkBoxDeathTetramino.UseVisualStyleBackColor = true;
            this.checkBoxDeathTetramino.Visible = false;
            this.checkBoxDeathTetramino.Click += new System.EventHandler(this.checkBoxDeathTetramino_Click);
            this.checkBoxDeathTetramino.KeyDown += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyDown);
            this.checkBoxDeathTetramino.KeyUp += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyUp);
            // 
            // checkBoxLevel3
            // 
            this.checkBoxLevel3.AutoSize = true;
            this.checkBoxLevel3.Font = new System.Drawing.Font("Unispace", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxLevel3.Location = new System.Drawing.Point(8, 396);
            this.checkBoxLevel3.Name = "checkBoxLevel3";
            this.checkBoxLevel3.Size = new System.Drawing.Size(113, 27);
            this.checkBoxLevel3.TabIndex = 8;
            this.checkBoxLevel3.Text = "Level 3";
            this.checkBoxLevel3.UseVisualStyleBackColor = true;
            this.checkBoxLevel3.Click += new System.EventHandler(this.checkBoxLevel3_Click);
            this.checkBoxLevel3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyDown);
            this.checkBoxLevel3.KeyUp += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyUp);
            // 
            // checkBoxLevel2
            // 
            this.checkBoxLevel2.AutoSize = true;
            this.checkBoxLevel2.Font = new System.Drawing.Font("Unispace", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxLevel2.Location = new System.Drawing.Point(8, 363);
            this.checkBoxLevel2.Name = "checkBoxLevel2";
            this.checkBoxLevel2.Size = new System.Drawing.Size(113, 27);
            this.checkBoxLevel2.TabIndex = 7;
            this.checkBoxLevel2.Text = "Level 2";
            this.checkBoxLevel2.UseVisualStyleBackColor = true;
            this.checkBoxLevel2.Click += new System.EventHandler(this.checkBoxLevel2_Click);
            this.checkBoxLevel2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyDown);
            this.checkBoxLevel2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyUp);
            // 
            // checkBoxLevel1
            // 
            this.checkBoxLevel1.AutoSize = true;
            this.checkBoxLevel1.Font = new System.Drawing.Font("Unispace", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxLevel1.Location = new System.Drawing.Point(8, 330);
            this.checkBoxLevel1.Name = "checkBoxLevel1";
            this.checkBoxLevel1.Size = new System.Drawing.Size(113, 27);
            this.checkBoxLevel1.TabIndex = 6;
            this.checkBoxLevel1.Text = "Level 1";
            this.checkBoxLevel1.UseVisualStyleBackColor = true;
            this.checkBoxLevel1.Click += new System.EventHandler(this.checkBoxLevel1_Click);
            this.checkBoxLevel1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyDown);
            this.checkBoxLevel1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyUp);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.DarkSlateGray;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.Location = new System.Drawing.Point(244, 414);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(100, 40);
            this.buttonExit.TabIndex = 5;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            this.buttonExit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyDown);
            this.buttonExit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyUp);
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Font = new System.Drawing.Font("Unispace", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScore.Location = new System.Drawing.Point(1, 260);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(179, 50);
            this.labelScore.TabIndex = 2;
            this.labelScore.Text = "Кол-во очков:\r\n0";
            // 
            // labelScoreLine
            // 
            this.labelScoreLine.AutoSize = true;
            this.labelScoreLine.Font = new System.Drawing.Font("Unispace", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScoreLine.Location = new System.Drawing.Point(3, 185);
            this.labelScoreLine.Name = "labelScoreLine";
            this.labelScoreLine.Size = new System.Drawing.Size(222, 75);
            this.labelScoreLine.TabIndex = 1;
            this.labelScoreLine.Text = "Кол-во убранных \r\nлиний:\r\n0";
            // 
            // pictureBoxFigures
            // 
            this.pictureBoxFigures.InitialImage = null;
            this.pictureBoxFigures.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxFigures.Name = "pictureBoxFigures";
            this.pictureBoxFigures.Size = new System.Drawing.Size(354, 182);
            this.pictureBoxFigures.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxFigures.TabIndex = 0;
            this.pictureBoxFigures.TabStop = false;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timerSnake
            // 
            this.timerSnake.Tick += new System.EventHandler(this.TimerSnake_Tick);
            // 
            // timerScore
            // 
            this.timerScore.Tick += new System.EventHandler(this.timerScore_Tick);
            // 
            // FormTetraSnake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 466);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTetraSnake";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TetraSnake";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyUp);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameTetraSnake)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFigures)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.PictureBox pictureBoxFigures;
        private System.Windows.Forms.Label labelScoreLine;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.PictureBox pictureBoxGameTetraSnake;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timerSnake;
        private System.Windows.Forms.Timer timerScore;
        private System.Windows.Forms.CheckBox checkBoxLevel3;
        private System.Windows.Forms.CheckBox checkBoxLevel2;
        private System.Windows.Forms.CheckBox checkBoxLevel1;
        private System.Windows.Forms.CheckBox checkBoxDeathTetramino;
        private System.Windows.Forms.CheckBox checkBoxAddTetramino;
        private System.Windows.Forms.Label labelRecord;
    }
}

