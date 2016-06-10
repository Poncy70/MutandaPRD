using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;

namespace Mutanda.Extensions
{
    public class ListViewAlternateColorCustom : ListView
    {
        protected override void SetupContent(Cell pContent, int pIndex)
        {
            base.SetupContent(pContent, pIndex);
            var currentViewCell = pContent as ViewCell;

            if (currentViewCell != null)
            {
                //currentViewCell.View.BackgroundColor = pIndex % 2 == 0 ? Color.FromRgb(245, 214, 137) : Color.FromRgb(239, 189, 117);
                currentViewCell.View.BackgroundColor = pIndex % 2 == 0 ? Color.Default : Color.FromRgb(239, 189, 117);
            }
        }

        public ListViewAlternateColorCustom(ListViewCachingStrategy strategy) : base(strategy)
        {

        }
    }
}
