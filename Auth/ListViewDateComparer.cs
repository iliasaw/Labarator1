using System;
using System.Collections;
using System.Windows.Forms;

namespace OrganizerApp
{
    public class ListViewDateComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is ListViewItem itemX && y is ListViewItem itemY)
            {
                // Объединяем дату (первый столбец) и время (второй столбец)
                string dateTimeX = $"{itemX.SubItems[0].Text} {itemX.SubItems[1].Text}";
                string dateTimeY = $"{itemY.SubItems[0].Text} {itemY.SubItems[1].Text}";
                if (DateTime.TryParse(dateTimeX, out DateTime dtX) &&
                    DateTime.TryParse(dateTimeY, out DateTime dtY))
                {
                    return dtX.CompareTo(dtY);
                }
            }
            return 0;
        }
    }
}