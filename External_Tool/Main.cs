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
// Main Form where user inputs height and width of map or can load an existing map
namespace External_Tool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, EventArgs e)
        {
            // setting the messagebox button to Ok and the icon to the Error icon
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Error;

            // creating the message and title variables
            string message = "";
            string title = "Error creating map";

            // bool to see if message is actually needed
            bool needMessage = false;

            // checks if the width is a valid number
            int width;
            bool widthNumber = int.TryParse(this.widthEdit.Text, out width);

            // checks if the height is a valid number
            int height;
            bool heightNumber = int.TryParse(this.heightEdit.Text, out height);

            // messagebox that prints error if inputed 
            if (widthNumber == false || heightNumber == false)
            {
                message = "Did not input a number" + System.Environment.NewLine;
                MessageBox.Show(message, title, button, icon);
            }

            // checks if the inputted width and height is in the ranges and sets the message of the message box accordingly
            else
            {
                if (width >= 10 && width <= 30)
                {

                } else if (width < 10)
                {
                    message = "Width is too small. Minimum is 10" + System.Environment.NewLine;
                    needMessage = true;

                } else
                {
                    message = "Width is too big. Maximum is 30" + System.Environment.NewLine;
                    needMessage = true;

                }

                if (height >= 10 && height <= 30)
                {

                }
                else if (height < 10)
                {
                    message += "Height is too small. Minimum is 10";
                    needMessage = true;
                }
                else
                {
                    message += "Height is too big. Maximum is 30";
                    needMessage = true;
                }

                if (needMessage == true)
                {
                    MessageBox.Show(message, title, button, icon);
                }

                // opening the editor form after wiidth and height are valid numbers
                else
                {
                    Editor editor = new Editor(height, width);
                    editor.ShowDialog();
                }

                
            }
        }

        // load button function
        private void LoadClick(object sender, EventArgs e)
        {
            // opening filedialog and letting user pick a .level file
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Level Files|*.level";
            openFile.Title = "Open a level file";
            DialogResult result = openFile.ShowDialog();

            // if user clicks open, opens the editor form
            if (result == DialogResult.OK)
            {

                // getting path of selected file and opening editor form with it
                string path = Path.GetFullPath(openFile.FileName);
                string name = Path.GetFileName(path);
                Editor editor = new Editor(path, name);
                editor.ShowDialog();
            }
        }
    }
}
