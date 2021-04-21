using System.Collections.Generic;

namespace Zzb.BaseData.Model
{
    public class TableInfomationModel
    {
        public List<NavFieldsViewModels> NavColumns { get; set; } = new List<NavFieldsViewModels>();

        public string TableName { get; set; }

        public object QueryFields { get; set; }

        public object ViewButtons { get; set; }

        public bool ShowOperaColumn { get; set; } = false;

        public int OperaColumnWidth { get; set; } = 100;
    }

    public class NavFieldsViewModels
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public bool IsHidden { get; set; } = false;

        public int Width { get; set; }

        public string ActionType { get; set; }
    }
}