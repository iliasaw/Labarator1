using System;
using System.Windows.Forms;

namespace OrganizerApp
{
    public partial class LoginForm : Form
    {
        private TableLayoutPanel tableLayoutPanel = null!;
        private Label lblLogin = null!;
        private Label lblPassword = null!;
        private TextBox txtLogin = null!;
        private TextBox txtPassword = null!;
        private CheckBox chkShowPassword = null!;
        private Button btnLogin = null!;
        private Button btnExit = null!;
        private ToolTip toolTip = null!;

        public LoginForm()
        {
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            lblLogin = new Label();
            lblPassword = new Label();
            txtLogin = new TextBox();
            txtPassword = new TextBox();
            chkShowPassword = new CheckBox();
            btnLogin = new Button();
            btnExit = new Button();
            toolTip = new ToolTip();

            this.Text = "Органайзер - Авторизация";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(400, 250);
            this.MinimumSize = new System.Drawing.Size(300, 200);

            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel.RowCount = 4;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));

            lblLogin.Text = "Логин:";
            lblLogin.Anchor = AnchorStyles.Right;
            lblLogin.AutoSize = true;

            txtLogin.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            toolTip.SetToolTip(txtLogin, "Введите ваш логин");

            lblPassword.Text = "Пароль:";
            lblPassword.Anchor = AnchorStyles.Right;
            lblPassword.AutoSize = true;

            txtPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.UseSystemPasswordChar = true;
            toolTip.SetToolTip(txtPassword, "Введите ваш пароль");

            chkShowPassword.Text = "Показать пароль";
            chkShowPassword.Anchor = AnchorStyles.Left;
            chkShowPassword.CheckedChanged += ChkShowPassword_CheckedChanged;

            btnLogin.Text = "Войти";
            btnLogin.Anchor = AnchorStyles.None;
            btnLogin.Click += BtnLogin_Click;

            btnExit.Text = "Выход";
            btnExit.Anchor = AnchorStyles.None;
            btnExit.Click += BtnExit_Click;

            tableLayoutPanel.Controls.Add(lblLogin, 0, 0);
            tableLayoutPanel.Controls.Add(txtLogin, 1, 0);
            tableLayoutPanel.Controls.Add(lblPassword, 0, 1);
            tableLayoutPanel.Controls.Add(txtPassword, 1, 1);
            tableLayoutPanel.Controls.Add(chkShowPassword, 1, 2);
            tableLayoutPanel.Controls.Add(btnExit, 0, 3);
            tableLayoutPanel.Controls.Add(btnLogin, 1, 3);

            this.Controls.Add(tableLayoutPanel);
        }

        private void ChkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (login == "user" && password == "12345")
            {
                MessageBox.Show("Авторизация успешна!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OrganizerForm organizerForm = new OrganizerForm();
                organizerForm.FormClosed += (s, args) => this.Close();
                organizerForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong login or password!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}