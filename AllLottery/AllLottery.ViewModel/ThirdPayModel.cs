using System.ComponentModel;

namespace AllLottery.ViewModel
{
    public class ThirdPayModel
    {
        public ThirdPayModel()
        {
        }

        public ThirdPayModel(string url)
        {
            Url = url;
        }

        public ThirdPayModel(HttpType type, object data)
        {
            Type = type;
            Data = data;
        }

        public ThirdPayModel(string url, HttpType type, object data) : this(url)
        {
            Type = type;
            Data = data;
        }

        public string Url { get; set; }

        public HttpType Type { get; set; } = HttpType.Get;

        public object Data { get; set; }
    }

    public enum HttpType
    {
        [Description("Get")]
        Get,
        [Description("Post")]
        Post,
        [Description("Img")]
        Img,
        [Description("Form")]
        Form
    }
}