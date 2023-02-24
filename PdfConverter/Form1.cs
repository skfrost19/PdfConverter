using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PdfConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnGenerate.Enabled = false;
            btnSave.Enabled = false;
        }

        // creataing a list of images
        List<string> images = new List<string>();
        PdfDocument finalPdf = new PdfDocument();

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            if (images.Count > 0)
            {
                foreach (string image in images)
                {
                    // create a page and resize the image to fit the page(do not crop the image)
                    PdfPage page = document.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    XImage img = XImage.FromFile(image);
                    gfx.DrawImage(img, 0, 0, page.Width, page.Height);
                }
            }
            finalPdf = document;
            labelSuccess.Text = "Pdf Generated Successfully!";
            btnSave.Enabled = true;
        }

        private void btnSelectImages_Click(object sender, EventArgs e)
        {
            // creating a dialog box
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "jpg files (*.jpg)|*.jpg|*.png|*.JPG|*.JPEG|*.jpeg";
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string[] images = dialog.FileNames;
                // add a popup window to alert the user for the success with images name .
                MessageBox.Show("Image(s) selected:-\n " + string.Join("\n", images));
                foreach (string image in images)
                {
                    // add images to the list
                    this.images.Add(image);
                }
            }
            labelSuccess.Text = images.Count + " images selected.";
            btnGenerate.Enabled = true;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // open a dialog box and ask the user for name and location
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PDF Files (*.pdf)|*.pdf";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                finalPdf.Save(dialog.FileName);
            }
            // a popup window to alert the user for the success.
            MessageBox.Show("Pdf Saved Successfully!");
            labelSuccess.Text = "";
            btnGenerate.Enabled = false;
            btnSave.Enabled = false;
        }
    }
}
