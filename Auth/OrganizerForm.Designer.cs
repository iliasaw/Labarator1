#nullable disable
using System.Windows.Forms;

namespace OrganizerApp
{
    partial class OrganizerForm : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // OrganizerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 600);
            Name = "OrganizerForm";
            Text = "Органайзер";
            Load += OrganizerForm_Load;
            ResumeLayout(false);
        }
    }
}