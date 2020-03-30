namespace External_Tool
{
    partial class Main
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
            this.loadButton = new System.Windows.Forms.Button();
            this.widthLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.widthEdit = new System.Windows.Forms.TextBox();
            this.heightEdit = new System.Windows.Forms.TextBox();
            this.createButton = new System.Windows.Forms.Button();
            this.createGroupBox = new System.Windows.Forms.GroupBox();
            this.createGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(33, 12);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(475, 122);
            this.loadButton.TabIndex = 0;
            this.loadButton.Text = "Load Map";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadClick);
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.widthLabel.Location = new System.Drawing.Point(84, 57);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(118, 20);
            this.widthLabel.TabIndex = 1;
            this.widthLabel.Text = "Width (in tiles)";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heightLabel.Location = new System.Drawing.Point(84, 120);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(124, 20);
            this.heightLabel.TabIndex = 2;
            this.heightLabel.Text = "Height (in tiles)";
            // 
            // widthEdit
            // 
            this.widthEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.widthEdit.Location = new System.Drawing.Point(230, 57);
            this.widthEdit.Name = "widthEdit";
            this.widthEdit.Size = new System.Drawing.Size(240, 27);
            this.widthEdit.TabIndex = 3;
            // 
            // heightEdit
            // 
            this.heightEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heightEdit.Location = new System.Drawing.Point(230, 113);
            this.heightEdit.Name = "heightEdit";
            this.heightEdit.Size = new System.Drawing.Size(240, 27);
            this.heightEdit.TabIndex = 4;
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(54, 174);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(406, 126);
            this.createButton.TabIndex = 5;
            this.createButton.Text = "Create Map";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.Create_Click);
            // 
            // createGroupBox
            // 
            this.createGroupBox.Controls.Add(this.createButton);
            this.createGroupBox.Controls.Add(this.heightEdit);
            this.createGroupBox.Controls.Add(this.widthEdit);
            this.createGroupBox.Controls.Add(this.heightLabel);
            this.createGroupBox.Controls.Add(this.widthLabel);
            this.createGroupBox.Location = new System.Drawing.Point(21, 195);
            this.createGroupBox.Name = "createGroupBox";
            this.createGroupBox.Size = new System.Drawing.Size(497, 328);
            this.createGroupBox.TabIndex = 6;
            this.createGroupBox.TabStop = false;
            this.createGroupBox.Text = "Create New Map";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 543);
            this.Controls.Add(this.createGroupBox);
            this.Controls.Add(this.loadButton);
            this.Name = "Main";
            this.Text = "Form1";
            this.createGroupBox.ResumeLayout(false);
            this.createGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.TextBox widthEdit;
        private System.Windows.Forms.TextBox heightEdit;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.GroupBox createGroupBox;
    }
}

