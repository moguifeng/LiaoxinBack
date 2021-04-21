using System.Text;

namespace AllLottery.Spider
{
    public class LuckySpiderThread : Base168Spider
    {
        public override string Url => "http://api.api68.com/LuckTwenty/getBaseLuckTewnty.do?issue=847993&lotCode=";

        protected override string GetData(string data)
        {
            var datas = data.Split(',');
            datas[datas.Length - 1] = datas[datas.Length - 1].Split('+')[0];
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
                int sum = 0;
                for (int j = 0; j < 4; j++)
                {
                    sum += int.Parse(datas[i * 4 + j]);
                }
                sb.Append(sum % 10 + ",");
            }
            return sb.ToString().Trim(',');
        }
    }
}