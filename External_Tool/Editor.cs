using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
// Neha Ghanta
// 11/28/19
// HW 7
// Editor Form where map can be edited with colors, saved, and loaded
namespace External_Tool
{
    public partial class Editor : Form
    {
        // fields: size of picture boxes, total number of pictureboxes, x and y coordinates, picture boxes created currently
        private int picSize;
        private int total;
        private int x;
        private int y;
        private int created;
        private int w;
        private int h;
        private bool changed;


        // dictionary that saves location, type of sprite need, and order if it is an interactable
        private string type;

        // properties to get width and height of tiles
        public int W
        {
            get
            {
                return this.w;
            }
        }

        public int H
        {
            get
            {
                return this.h;
            }
        }


        // Constructor that takes height and width
        public Editor(int h, int w)
        {
            // calculating the picture box sizes based on the number of boxes height wise and the size of the form
            this.picSize = 480 / h;
            this.total = h * w;
            this.x = 0;
            this.y = 15;
            this.created = 0;
            this.w = w;
            this.h = h;
            this.changed = false;
            
            InitializeComponent();

            // changing the size of the form and groupbox based on the picSize
            this.mapBox.Width = 10 + (picSize * w);
            this.mapBox.Height = this.mapBox.Height;
            this.Size = new Size(150 + (picSize * w), this.Size.Height);

            // creating pictureboxes based on user width and height and picSize
            for (int i = 0; i < total; i++)
            {
                // creating picture boxes and adding to the groupbox
                PictureBox box = new PictureBox();
                this.mapBox.Controls.Add(box);
                box.Location = new Point(x, y);
                box.Size = new Size(picSize, picSize);
                box.BackColor = Color.White;
                created++;

                // adding the mapclick method to the click event
                box.MouseClick += MapClick;


                // moving the location of each picturebox after each iteration of the for loop
                if (created % w == 0)
                {
                    this.x = 0;
                    y += picSize;
                }
                else
                {
                    x += picSize;
                }
            }

            // adding closeclicked method to formclosing event
            this.FormClosing += CloseClicked;

            // dictionary save stuff
            type = "";
        }

        // contructor that loads file data and makes map
        public Editor(string path, string name)
        {

            // messagebox saying file loaded successfully
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Exclamation;
            MessageBox.Show("File loaded successfully", "File Loaded", button, icon);

            // getting the selected path and opening a read stream to reading the selected file
            FileStream readStream = File.OpenRead(path);
            StreamReader reader = new StreamReader(readStream);

            // getting height and width from file
            string heightAsString = reader.ReadLine();
            int height = int.Parse(heightAsString);
            string widthAsString = reader.ReadLine();
            int width = int.Parse(widthAsString);


            // reading characters from file
            int read = 0;

            // array to hold colors
            int[] colors = new int[height * width];

            for (int i = 0; i < (width * height); i++)
            {
                // reader goes to the next line if at the end of the current line
                if (read % width == 0)
                {
                    reader.ReadLine();
                }

                char type = (char)reader.Read();

                // switch statement to determine what color to use
                switch (type)
                {
                    // platforms
                    case '-':
                        colors[i] = Color.Silver.ToArgb();
                        break;

                    // buttons
                    case 'b':
                        colors[i] = Color.Green.ToArgb();
                        break;

                    // player
                    case 'p':
                        colors[i] = Color.Blue.ToArgb();
                        break;

                    // levers
                    case 'l':
                        colors[i] = Color.SaddleBrown.ToArgb();
                        break;

                    // null
                    case '*':
                        colors[i] = Color.White.ToArgb();
                        break;
                }
                read++;
            }

            // closing reader stream
            reader.Close();

            // creating level
            // calculating the picture box sizes based on the number of boxes height wise and the size of the form
            this.picSize = 480 / height;
            this.total = height * width;
            this.x = 0;
            this.y = 15;
            this.created = 0;
            this.w = width;
            this.h = height;

            InitializeComponent();

            // changing the size of the form and groupbox based on the picSize
            this.mapBox.Width = 10 + (picSize * w);
            this.mapBox.Height = this.mapBox.Height;
            this.Size = new Size(150 + (picSize * w), this.Size.Height);

            // creating pictureboxes based on user width and height and picSize
            for (int i = 0; i < total; i++)
            {
                // creating picture boxes and adding to the groupbox
                PictureBox box = new PictureBox();
                this.mapBox.Controls.Add(box);
                box.Location = new Point(x, y);
                box.Size = new Size(picSize, picSize);
                box.BackColor = Color.FromArgb(colors[i]);
                created++;

                // adding the mapclick method to the click event
                box.MouseClick += MapClick;


                // moving the location of each picturebox after each iteration of the for loop
                if (created % w == 0)
                {
                    this.x = 0;
                    y += picSize;
                }
                else
                {
                    x += picSize;
                }
            }

            // changing form title
            this.Text = "Level Editor - " + name;

            // adding closeclicked method to formclosing event
            this.FormClosing += CloseClicked;
        }


        // changes the currentColor picture box based on which color button is clicked
        public void ColorClick(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button colorRef = (Button)sender;
                this.currentColor.BackColor = colorRef.BackColor;
            }
        }

        // click method for changing the color on the map based on the current color selected 
        public void MapClick(object sender, EventArgs e)
        {
            if (sender is PictureBox)
            {
                PictureBox box = (PictureBox)sender;
                box.BackColor = this.currentColor.BackColor;
                this.changed = true;
                
            }

            // adding asterix to title for unchanged changes
            while (this.changed == true && !(this.Text.Contains("*")))
            {
                this.Text += " *";
            }
        }

        // save button function that saves map to computer
        public void SaveButton(object sender, EventArgs e)
        {
            

            // opening save file dialog and getting selected file if selected
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Level Files|*.level";
            save.Title = "Save a file";
            DialogResult result = save.ShowDialog();
            if (result == DialogResult.OK)
            {
                FileStream writeStream = File.OpenWrite(Path.GetFullPath(save.FileName));
                StreamWriter writer = new StreamWriter(writeStream);

                // writes h and w of form
                writer.WriteLine(this.h);
                writer.WriteLine(this.w);

                created = 0;

                // writing the colors of the pictureboxes onto the file
                foreach (Control pixBox in this.mapBox.Controls)
                {
                    

                    if (created % this.w == 0)
                    {
                        writer.Write(Environment.NewLine);
                    }

                    // changing type string based on currently selected string
                    // platforms
                    if (pixBox.BackColor == Color.Silver)
                    {
                        type = "-";
                        writer.Write(type + "");
                        created++;
                    }
                    // type : buttons
                    else if (pixBox.BackColor == Color.Green)
                    {
                        type = "b";
                        writer.Write(type + "");
                        created++;
                    }
                    // type : player
                    else if (pixBox.BackColor == Color.Blue)
                    {
                        type = "p";
                        writer.Write(type + "");
                        created++;
                    }
                    // type : levers
                    else if (pixBox.BackColor == Color.SaddleBrown)
                    {
                        type = "l";
                        writer.Write(type + "");
                        created++;
                    }
                    else
                    {
                        type = "*";
                        writer.Write(type + "");
                        created++;
                    }

                }

                // closing writer stream
                writer.Close();
            }

            // removing asterix and adding the asterix;
            this.changed = false;
            string name = Path.GetFileName(save.FileName);
            this.Text = "Level Editor - " + name;

            // messagebox saying file loaded successfully
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Exclamation;
            MessageBox.Show("File saved successfully", "File Loaded", button, icon);

        }

        // load button function that loads file and changes map accordingly
        public void LoadButton(object sender, EventArgs e)
        {

            // opening filedialog and letting user pick a .level file
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Level Files|*.level";
            openFile.Title = "Open a level file";
            DialogResult result = openFile.ShowDialog();

            // if user clicks open, opens the editor form
            if (result == DialogResult.OK)
            {

                // getting the selected path and opening a read stream to reading the selected file
                FileStream readStream = File.OpenRead(Path.GetFullPath(openFile.FileName));
                StreamReader reader = new StreamReader(readStream);

                // getting height and width from file
                string heightAsString = reader.ReadLine();
                int height = int.Parse(heightAsString);
                string widthAsString = reader.ReadLine();
                int width = int.Parse(widthAsString);

                // reading characters from file
                int read = 0;

                // array to hold colors
                int[] colors = new int[height * width];

                for (int i = 0; i < (width * height); i++)
                {
                    // reader goes to the next line if at the end of the current line
                    if (read % width == 0)
                    {
                        reader.ReadLine();
                    }

                    char type = (char)reader.Read();

                    // switch statement to determine what color to use
                    switch (type)
                    {
                        // platforms
                        case '-':
                            colors[i] = Color.Silver.ToArgb();
                            break;

                        // buttons
                        case 'b':
                            colors[i] = Color.Green.ToArgb();
                            break;

                        // player
                        case 'p':
                            colors[i] = Color.Blue.ToArgb();
                            break;

                        // levers
                        case 'l':
                            colors[i] = Color.SaddleBrown.ToArgb();
                            break;

                        // null
                        case '*':
                            colors[i] = Color.White.ToArgb();
                            break;
                    }
                    read++;
                }
                // closing reader stream
                reader.Close();

                // creating level
                // calculating the picture box sizes based on the number of boxes height wise and the size of the form
                this.picSize = 480 / height;
                this.total = height * width;
                this.x = 0;
                this.y = 15;
                this.created = 0;
                this.w = width;
                this.h = height;

                // clearing the current picture box
                this.mapBox.Controls.Clear();

                // changing the size of the form and groupbox based on the picSize
                this.mapBox.Width = 10 + (picSize * w);
                this.mapBox.Height = this.mapBox.Height;
                this.Size = new Size(150 + (picSize * w), this.Size.Height);

                // creating pictureboxes based on user width and height and picSize
                for (int i = 0; i < total; i++)
                {
                    // creating picture boxes and adding to the groupbox
                    PictureBox box = new PictureBox();
                    this.mapBox.Controls.Add(box);
                    box.Location = new Point(x, y);
                    box.Size = new Size(picSize, picSize);
                    box.BackColor = Color.FromArgb(colors[i]);
                    created++;

                    // adding the mapclick method to the click event
                    box.MouseClick += MapClick;


                    // moving the location of each picturebox after each iteration of the for loop
                    if (created % w == 0)
                    {
                        this.x = 0;
                        y += picSize;
                    }
                    else
                    {
                        x += picSize;
                    }
                }
                
            }

            // messagebox saying file loaded successfully
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Exclamation;
            MessageBox.Show("File loaded successfully", "File Loaded", button, icon);
        }

        public void CloseClicked(object sender, FormClosingEventArgs e)
        {
            // settings for messagebox
            MessageBoxButtons button = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Question;


            // checking if there are unsaved changes
            if (this.Text.Contains("*"))
            {
                DialogResult result = MessageBox.Show("There are unsaved changes. Are you sure you want to quit?", "Unsaved changes", button, icon);

                // if no is clicked
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }


            }
        }
    }
}
