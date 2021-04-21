namespace AllLottery.Spider
{
    public class SaiCheSpiderThread : Base168Spider
    {
        public override string Url => "http://api.api68.com/pks/getLotteryPksInfo.do?issue=&lotCode=";
    }
}