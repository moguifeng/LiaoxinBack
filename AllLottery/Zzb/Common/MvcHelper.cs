using System.Text;

namespace Zzb.Common
{
    public static class MvcHelper
    {
        public static string LogDifferent(params LogDifferentViewModel[] models)
        {
            StringBuilder sb = new StringBuilder();
            if (models != null)
            {
                foreach (LogDifferentViewModel model in models)
                {
                    if (model.New != model.Old)
                    {
                        sb.Append($",原{model.Title}[{model.Old}]改为[{model.New}]");
                    }
                }
            }
            return sb.ToString();
        }
    }

    public class LogDifferentViewModel
    {
        public LogDifferentViewModel()
        {
        }

        public LogDifferentViewModel(string old, string @new, string title)
        {
            Old = old;
            New = @new;
            Title = title;
        }

        public string Old { get; set; }

        public string New { get; set; }

        public string Title { get; set; }
    }
}