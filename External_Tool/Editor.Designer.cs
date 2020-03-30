namespace External_Tool
{
    partial class Editor
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
            this.greenColor = new System.Windows.Forms.Button();
            this.greyButton = new System.Windows.Forms.Button();
            this.brownColor = new System.Windows.Forms.Button();
            this.redColor = new System.Windows.Forms.Button();
            this.blueColor = new System.Windows.Forms.Button();
            this.blackColor = new System.Windows.Forms.Button();
            this.colorBox = new System.Windows.Forms.GroupBox();
            this.LeverButtonLabel = new System.Windows.Forms.Label();
            this.ButtonsButtonLabel = new System.Windows.Forms.Label();
            this.PlatformerButtonLabel = new System.Windows.Forms.Label();
            this.PlayerButtonLabel = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.currentColor = new System.Windows.Forms.PictureBox();
            this.currentBox = new System.Windows.Forms.GroupBox();
            this.mapBox = new System.Windows.Forms.GroupBox();
            this.colorBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentColor)).BeginInit();
            this.currentBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // greenColor
            // 
            this.greenColor.BackColor = System.Drawing.Color.Green;
            this.greenColor.Location = new System.Drawing.Point(5, 21);
            this.greenColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.greenColor.Name = "greenColor";
            this.greenColor.Size = new System.Drawing.Size(51, 50);
            this.greenColor.TabIndex = 0;
            this.greenColor.UseVisualStyleBackColor = false;
            this.greenColor.Click += new System.EventHandler(this.ColorClick);
            // 
            // greyButton
            // 
            this.greyButton.BackColor = System.Drawing.Color.Silver;
            this.greyButton.Location = new System.Drawing.Point(61, 21);
            this.greyButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.greyButton.Name = "greyButton";
            this.greyButton.Size = new System.Drawing.Size(51, 50);
            this.greyButton.TabIndex = 1;
            this.greyButton.UseVisualStyleBackColor = false;
            this.greyButton.Click += new System.EventHandler(this.ColorClick);
            // 
            // brownColor
            // 
            this.brownColor.BackColor = System.Drawing.Color.SaddleBrown;
            this.brownColor.Location = new System.Drawing.Point(5, 95);
            this.brownColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.brownColor.Name = "brownColor";
            this.brownColor.Size = new System.Drawing.Size(51, 50);
            this.brownColor.TabIndex = 2;
            this.brownColor.UseVisualStyleBackColor = false;
            this.brownColor.Click += new System.EventHandler(this.ColorClick);
            // 
            // redColor
            // 
            this.redColor.BackColor = System.Drawing.Color.Firebrick;
            this.redColor.Location = new System.Drawing.Point(61, 95);
            this.redColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.redColor.Name = "redColor";
            this.redColor.Size = new System.Drawing.Size(51, 50);
            this.redColor.TabIndex = 3;
            this.redColor.UseVisualStyleBackColor = false;
            this.redColor.Click += new System.EventHandler(this.ColorClick);
            // 
            // blueColor
            // 
            this.blueColor.BackColor = System.Drawing.Color.Blue;
            this.blueColor.Location = new System.Drawing.Point(5, 161);
            this.blueColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.blueColor.Name = "blueColor";
            this.blueColor.Size = new System.Drawing.Size(51, 50);
            this.blueColor.TabIndex = 4;
            this.blueColor.UseVisualStyleBackColor = false;
            this.blueColor.Click += new System.EventHandler(this.ColorClick);
            // 
            // blackColor
            // 
            this.blackColor.BackColor = System.Drawing.Color.Black;
            this.blackColor.Location = new System.Drawing.Point(61, 161);
            this.blackColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.blackColor.Name = "blackColor";
            this.blackColor.Size = new System.Drawing.Size(51, 50);
            this.blackColor.TabIndex = 5;
            this.blackColor.UseVisualStyleBackColor = false;
            this.blackColor.Click += new System.EventHandler(this.ColorClick);
            // 
            // colorBox
            // 
            this.colorBox.Controls.Add(this.LeverButtonLabel);
            this.colorBox.Controls.Add(this.ButtonsButtonLabel);
            this.colorBox.Controls.Add(this.PlatformerButtonLabel);
            this.colorBox.Controls.Add(this.PlayerButtonLabel);
            this.colorBox.Controls.Add(this.blackColor);
            this.colorBox.Controls.Add(this.blueColor);
            this.colorBox.Controls.Add(this.redColor);
            this.colorBox.Controls.Add(this.brownColor);
            this.colorBox.Controls.Add(this.greyButton);
            this.colorBox.Controls.Add(this.greenColor);
            this.colorBox.Location = new System.Drawing.Point(4, 22);
            this.colorBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.colorBox.Name = "colorBox";
            this.colorBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.colorBox.Size = new System.Drawing.Size(120, 229);
            this.colorBox.TabIndex = 6;
            this.colorBox.TabStop = false;
            this.colorBox.Text = "Tile Selector";
            // 
            // LeverButtonLabel
            // 
            this.LeverButtonLabel.AutoSize = true;
            this.LeverButtonLabel.Location = new System.Drawing.Point(4, 143);
            this.LeverButtonLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LeverButtonLabel.Name = "LeverButtonLabel";
            this.LeverButtonLabel.Size = new System.Drawing.Size(51, 17);
            this.LeverButtonLabel.TabIndex = 9;
            this.LeverButtonLabel.Text = "Levers";
            // 
            // ButtonsButtonLabel
            // 
            this.ButtonsButtonLabel.AutoSize = true;
            this.ButtonsButtonLabel.Location = new System.Drawing.Point(5, 73);
            this.ButtonsButtonLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ButtonsButtonLabel.Name = "ButtonsButtonLabel";
            this.ButtonsButtonLabel.Size = new System.Drawing.Size(56, 17);
            this.ButtonsButtonLabel.TabIndex = 8;
            this.ButtonsButtonLabel.Text = "Buttons";
            // 
            // PlatformerButtonLabel
            // 
            this.PlatformerButtonLabel.AutoSize = true;
            this.PlatformerButtonLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlatformerButtonLabel.Location = new System.Drawing.Point(64, 73);
            this.PlatformerButtonLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PlatformerButtonLabel.Name = "PlatformerButtonLabel";
            this.PlatformerButtonLabel.Size = new System.Drawing.Size(50, 13);
            this.PlatformerButtonLabel.TabIndex = 7;
            this.PlatformerButtonLabel.Text = "Platforms";
            // 
            // PlayerButtonLabel
            // 
            this.PlayerButtonLabel.AutoSize = true;
            this.PlayerButtonLabel.Location = new System.Drawing.Point(5, 210);
            this.PlayerButtonLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PlayerButtonLabel.Name = "PlayerButtonLabel";
            this.PlayerButtonLabel.Size = new System.Drawing.Size(48, 17);
            this.PlayerButtonLabel.TabIndex = 6;
            this.PlayerButtonLabel.Text = "Player";
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(13, 545);
            this.loadButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(111, 94);
            this.loadButton.TabIndex = 7;
            this.loadButton.Text = "Load File";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadButton);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(13, 431);
            this.saveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(111, 90);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Save File";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton);
            // 
            // currentColor
            // 
            this.currentColor.Location = new System.Drawing.Point(21, 25);
            this.currentColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.currentColor.Name = "currentColor";
            this.currentColor.Size = new System.Drawing.Size(69, 70);
            this.currentColor.TabIndex = 9;
            this.currentColor.TabStop = false;
            // 
            // currentBox
            // 
            this.currentBox.Controls.Add(this.currentColor);
            this.currentBox.Location = new System.Drawing.Point(7, 256);
            this.currentBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.currentBox.Name = "currentBox";
            this.currentBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.currentBox.Size = new System.Drawing.Size(116, 126);
            this.currentBox.TabIndex = 10;
            this.currentBox.TabStop = false;
            this.currentBox.Text = "Current Tile";
            // 
            // mapBox
            // 
            this.mapBox.Location = new System.Drawing.Point(144, 22);
            this.mapBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mapBox.Name = "mapBox";
            this.mapBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mapBox.Size = new System.Drawing.Size(636, 615);
            this.mapBox.TabIndex = 11;
            this.mapBox.TabStop = false;
            this.mapBox.Text = "Map";
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 654);
            this.Controls.Add(this.mapBox);
            this.Controls.Add(this.currentBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.colorBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Editor";
            this.Text = "Editor";
            this.colorBox.ResumeLayout(false);
            this.colorBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentColor)).EndInit();
            this.currentBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button greenColor;
        private System.Windows.Forms.Button greyButton;
        private System.Windows.Forms.Button brownColor;
        private System.Windows.Forms.Button redColor;
        private System.Windows.Forms.Button blueColor;
        private System.Windows.Forms.Button blackColor;
        private System.Windows.Forms.GroupBox colorBox;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.PictureBox currentColor;
        private System.Windows.Forms.GroupBox currentBox;
        private System.Windows.Forms.GroupBox mapBox;
        private System.Windows.Forms.Label LeverButtonLabel;
        private System.Windows.Forms.Label ButtonsButtonLabel;
        private System.Windows.Forms.Label PlatformerButtonLabel;
        private System.Windows.Forms.Label PlayerButtonLabel;
    }
}