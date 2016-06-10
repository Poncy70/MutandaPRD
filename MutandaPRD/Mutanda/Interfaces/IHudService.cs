using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutanda.Services
{
    public interface IHudService
    {
        void ShowHud(string ProgressText = "Loading...");
        void HideHud();
        void SetText(string Text);
        void SetProgress(double Progress, string ProgressText = "");
    }
}
