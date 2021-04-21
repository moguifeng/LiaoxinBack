namespace AllLottery.Spider
{
    public class ShiShiCaiSpiderThread : Base168Spider
    {
        public override string Url => "http://api.api68.com/CQShiCai/getBaseCQShiCai.do?issue=&lotCode=";
    }
}