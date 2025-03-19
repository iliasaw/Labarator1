using System;
using System.Drawing;
using System.Windows.Forms;

namespace OrganizerApp
{
    public partial class AddEventForm : Form
    {
        private MonthCalendar monthCalendar;
        private DateTimePicker dtpTime;
        private ComboBox cmbCategory;
        private TextBox txtDescription;
        private Button btnOK;
        private Button btnCancel;

        public AddEventForm()
        {
            InitializeComponentCustom();
        }

        private void InitializeComponentCustom()
        {
            this.Text = "Добавление события";
            this.Size = new Size(400, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            TableLayoutPanel layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 2;
            layout.RowCount = 5;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 200));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            Label lblDate = new Label();
            lblDate.Text = "Дата:";
            lblDate.Dock = DockStyle.Fill;
            lblDate.TextAlign = ContentAlignment.MiddleRight;
            layout.Controls.Add(lblDate, 0, 0);

            monthCalendar = new MonthCalendar();
            monthCalendar.MaxSelectionCount = 1;
            layout.Controls.Add(monthCalendar, 1, 0);

            Label lblTime = new Label();
            lblTime.Text = "Время:";
            lblTime.Dock = DockStyle.Fill;
            lblTime.TextAlign = ContentAlignment.MiddleRight;
            layout.Controls.Add(lblTime, 0, 1);

            dtpTime = new DateTimePicker();
            dtpTime.Format = DateTimePickerFormat.Time;
            dtpTime.ShowUpDown = true;
            dtpTime.Dock = DockStyle.Fill;
            layout.Controls.Add(dtpTime, 1, 1);

            Label lblCategory = new Label();
            lblCategory.Text = "Категория:";
            lblCategory.Dock = DockStyle.Fill;
            lblCategory.TextAlign = ContentAlignment.MiddleRight;
            layout.Controls.Add(lblCategory, 0, 2);

            cmbCategory = new ComboBox();
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Items.AddRange(new string[] { "memo", "meeting", "task" });
            cmbCategory.SelectedIndex = 0;
            cmbCategory.Dock = DockStyle.Fill;
            layout.Controls.Add(cmbCategory, 1, 2);

            Label lblDescription = new Label();
            lblDescription.Text = "Описание:";
            lblDescription.Dock = DockStyle.Fill;
            lblDescription.TextAlign = ContentAlignment.MiddleRight;
            layout.Controls.Add(lblDescription, 0, 3);

            txtDescription = new TextBox();
            txtDescription.Multiline = true;
            txtDescription.Dock = DockStyle.Fill;
            layout.Controls.Add(txtDescription, 1, 3);

            FlowLayoutPanel panelButtons = new FlowLayoutPanel();
            panelButtons.FlowDirection = FlowDirection.RightToLeft;
            panelButtons.Dock = DockStyle.Fill;

            btnOK = new Button();
            btnOK.Text = "ОК";
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Click += BtnOK_Click;
            panelButtons.Controls.Add(btnOK);

            btnCancel = new Button();
            btnCancel.Text = "Отмена";
            btnCancel.DialogResult = DialogResult.Cancel;
            panelButtons.Controls.Add(btnCancel);

            layout.Controls.Add(panelButtons, 0, 4);
            layout.SetColumnSpan(panelButtons, 2);

            this.Controls.Add(layout);

            dtpTime.Value = DateTime.Now;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = monthCalendar.SelectionStart.Date;
            if (selectedDate < DateTime.Today)
            {
                MessageBox.Show("Нельзя добавить событие на прошедшую дату.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
                return;
            }
        }

        public DateTime EventDateTime
        {
            get
            {
                DateTime date = monthCalendar.SelectionStart.Date;
                DateTime time = dtpTime.Value;
                return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
            }
        }

        public string EventCategory
        {
            get { return cmbCategory.SelectedItem?.ToString() ?? ""; }
        }

        public string EventDescription
        {
            get { return txtDescription.Text; }
        }
    }
}
