using System;

namespace Liaoxin.ViewModel
{
    public class PageViewModel
    {
        public int Size { get; set; } = 10;

        public int Index { get; set; } = 1;

        public string SortType { get; set; } = "Asc";

        public string SortField { get; set; } = "UpdateTime";
    }

    public class BaseModel
    {
        public Guid Id { get; set; }
    }

    public class BaseClientModel
    {
       public Guid ClientId { get; set; }
    }
}