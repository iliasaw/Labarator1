#nullable enable
using System;
using System.IO;              // Добавлено для работы с файлами
using System.Text.Json;       // Добавлено для сериализации/десериализации JSON
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace OrganizerApp
{
    public partial class OrganizerForm : Form
    {
        private TableLayoutPanel mainLayout = null!;
        private FlowLayoutPanel topPanel = null!;
        private FlowLayoutPanel bottomPanel = null!;
        private CheckBox chkFilter = null!;
        private ComboBox cmbCategory = null!;
        private ListView lvEvents = null!;
        private ImageList imageListCategories = null!;
        private ContextMenuStrip contextMenu = null!;
        private Button btnAdd = null!;
        private Button btnFind = null!;
        private Button btnSort = null!;
        private List<ListViewItem> allItems = new List<ListViewItem>();

        // Добавленный класс для сериализации событий
        private class EventRecord
        {
            public DateTime Date { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
        }

        public OrganizerForm()
        {
            InitializeComponentCustom();
        }

        private void InitializeComponentCustom()
        {
            this.KeyPreview = true;
            this.Text = "Органайзер";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(800, 600);
            this.MinimumSize = new Size(600, 400);

            mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.ColumnCount = 1;
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            topPanel = new FlowLayoutPanel();
            topPanel.Dock = DockStyle.Fill;
            topPanel.AutoSize = true;
            topPanel.Padding = new Padding(10);
            topPanel.FlowDirection = FlowDirection.LeftToRight;

            chkFilter = new CheckBox();
            chkFilter.Text = "Фильтр по категории";
            chkFilter.AutoSize = true;
            chkFilter.CheckedChanged += ChkFilter_CheckedChanged;

            cmbCategory = new ComboBox();
            cmbCategory.Items.AddRange(new string[] { "memo", "meeting", "task" });
            cmbCategory.SelectedIndex = 0;
            cmbCategory.Enabled = false;
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.SelectedIndexChanged += CmbCategory_SelectedIndexChanged;

            topPanel.Controls.Add(chkFilter);
            topPanel.Controls.Add(cmbCategory);

            lvEvents = new ListView();
            lvEvents.Dock = DockStyle.Fill;
            lvEvents.View = View.Details;
            lvEvents.FullRowSelect = true;
            lvEvents.GridLines = true;
            lvEvents.MultiSelect = false;
            lvEvents.Columns.Add("Дата", 100);
            lvEvents.Columns.Add("Время", 80);
            lvEvents.Columns.Add("Описание", 400);
            lvEvents.Columns.Add("Категория", 100);

            imageListCategories = new ImageList();
            imageListCategories.ImageSize = new Size(16, 16);
            Bitmap bmpMemo = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmpMemo))
                g.Clear(Color.Yellow);
            Bitmap bmpMeeting = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmpMeeting))
                g.Clear(Color.LightBlue);
            Bitmap bmpTask = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmpTask))
                g.Clear(Color.LightGreen);
            imageListCategories.Images.Add("memo", bmpMemo);
            imageListCategories.Images.Add("meeting", bmpMeeting);
            imageListCategories.Images.Add("task", bmpTask);
            lvEvents.SmallImageList = imageListCategories;

            AddEventItem(new DateTime(2023, 10, 28, 10, 0, 0), "Событие 1", "memo");
            AddEventItem(new DateTime(2023, 10, 29, 12, 30, 0), "Встреча с клиентом", "meeting");
            AddEventItem(new DateTime(2023, 10, 30, 14, 45, 0), "Выполнить задание", "task");

            contextMenu = new ContextMenuStrip();
            var editItem = new ToolStripMenuItem("Edit");
            editItem.Click += EditItem_Click;
            var removeItem = new ToolStripMenuItem("Remove");
            removeItem.Click += RemoveItem_Click;
            contextMenu.Items.AddRange(new ToolStripItem[] { editItem, removeItem });
            lvEvents.ContextMenuStrip = contextMenu;

            bottomPanel = new FlowLayoutPanel();
            bottomPanel.Dock = DockStyle.Fill;
            bottomPanel.Padding = new Padding(10);
            bottomPanel.FlowDirection = FlowDirection.LeftToRight;

            btnAdd = new Button();
            btnAdd.Text = "Добавить";
            btnAdd.AutoSize = true;
            btnAdd.Anchor = AnchorStyles.Right;
            btnAdd.Click += BtnAdd_Click;

            btnFind = new Button();
            btnFind.Text = "Найти";
            btnFind.AutoSize = true;
            btnFind.Click += BtnFind_Click;

            btnSort = new Button();
            btnSort.Text = "Сортировать";
            btnSort.AutoSize = true;
            btnSort.Click += BtnSort_Click;

            bottomPanel.Controls.Add(btnAdd);
            bottomPanel.Controls.Add(btnFind);
            bottomPanel.Controls.Add(btnSort);

            mainLayout.Controls.Add(topPanel, 0, 0);
            mainLayout.Controls.Add(lvEvents, 0, 1);
            mainLayout.Controls.Add(bottomPanel, 0, 2);
            this.Controls.Add(mainLayout);

            this.KeyDown += OrganizerForm_KeyDown;
        }

        private void AddEventItem(DateTime datetime, string description, string category)
        {
            ListViewItem item = new ListViewItem(datetime.ToShortDateString());
            item.SubItems.Add(datetime.ToShortTimeString());
            item.SubItems.Add(description);
            item.SubItems.Add(category);
            item.ImageKey = category;
            var record = new EventRecord { Date = datetime, Description = description, Category = category };
            item.Tag = record;
            lvEvents.Items.Add(item);
            allItems.Add(item);
        }

        private void EditItem_Click(object? sender, EventArgs e)
        {
            if (lvEvents.SelectedItems.Count > 0)
            {
                ListViewItem item = lvEvents.SelectedItems[0];
                MessageBox.Show("Открытие окна редактирования для записи:\n" +
                                $"Дата: {item.SubItems[0].Text} Время: {item.SubItems[1].Text}\n" +
                                $"Описание: {item.SubItems[2].Text} Категория: {item.SubItems[3].Text}",
                                "Редактирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RemoveItem_Click(object? sender, EventArgs e)
        {
            if (lvEvents.SelectedItems.Count > 0)
            {
                ListViewItem item = lvEvents.SelectedItems[0];
                string info = $"{item.SubItems[0].Text} / {item.SubItems[1].Text} / {item.SubItems[2].Text}";
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete the record <{info}>?",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    lvEvents.Items.Remove(item);
            }
        }

        private void ChkFilter_CheckedChanged(object? sender, EventArgs e)
        {
            cmbCategory.Enabled = chkFilter.Checked;
            ApplyFilter();
        }

        private void CmbCategory_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (chkFilter.Checked)
                ApplyFilter();
        }

        private void ApplyFilter()
        {
            string selectedCategory = cmbCategory.SelectedItem?.ToString() ?? "";
            lvEvents.BeginUpdate();
            lvEvents.Items.Clear();
            foreach (var item in allItems)
            {
                if (!chkFilter.Checked || (item.SubItems[3].Text == selectedCategory))
                {
                    lvEvents.Items.Add(item);
                }
            }
            lvEvents.EndUpdate();
        }

        private void OrganizerForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                e.SuppressKeyPress = true;
                SaveToFile();
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                e.SuppressKeyPress = true;
                OpenFromFile();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
                if (lvEvents.SelectedItems.Count > 0)
                    RemoveItem_Click(sender, e);
            }
        }

        private void SaveToFile()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                sfd.DefaultExt = "json";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    List<EventRecord> records = new List<EventRecord>();
                    foreach (ListViewItem item in allItems)
                    {
                        if (item.Tag is EventRecord record)
                            records.Add(record);
                    }
                    string json = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(sfd.FileName, json);
                    MessageBox.Show("Данные успешно сохранены!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void OpenFromFile()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string json = File.ReadAllText(ofd.FileName);
                    List<EventRecord>? records = JsonSerializer.Deserialize<List<EventRecord>>(json);
                    if (records != null)
                    {
                        // Очистка текущих событий
                        lvEvents.Items.Clear();
                        allItems.Clear();
                        // Добавление загруженных событий
                        foreach (var record in records)
                        {
                            AddEventItem(record.Date, record.Description, record.Category);
                        }
                        MessageBox.Show("Данные успешно загружены!", "Открытие", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            using (AddEventForm addForm = new AddEventForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    DateTime eventDateTime = addForm.EventDateTime;
                    string description = addForm.EventDescription;
                    string category = addForm.EventCategory;
                    AddEventItem(eventDateTime, description, category);
                }
            }
        }

        private void BtnFind_Click(object? sender, EventArgs e)
        {
            string searchText = Prompt.ShowDialog("Введите текст для поиска в описании события:", "Поиск");
            if (!string.IsNullOrEmpty(searchText))
            {
                lvEvents.BeginUpdate();
                lvEvents.Items.Clear();
                foreach (var item in allItems)
                {
                    if (item.SubItems[2].Text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        lvEvents.Items.Add(item);
                    }
                }
                lvEvents.EndUpdate();
            }
        }

        private void BtnSort_Click(object? sender, EventArgs e)
        {
            lvEvents.ListViewItemSorter = new ListViewDateComparer();
            lvEvents.Sort();
            lvEvents.ListViewItemSorter = null;
        }

        private void OrganizerForm_Load(object sender, EventArgs e)
        {

        }
    }
}