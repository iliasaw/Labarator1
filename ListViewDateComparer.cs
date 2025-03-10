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
                if (DateTime.TryParse(itemX.SubItems[0].Text, out DateTime dateX) &&
                    DateTime.TryParse(itemY.SubItems[0].Text, out DateTime dateY))
                {
                    return dateX.CompareTo(dateY);
                }
            }
            return 0;
        }
    }
}