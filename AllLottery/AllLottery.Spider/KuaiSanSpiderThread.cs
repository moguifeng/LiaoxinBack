namespace AllLottery.Spider
{
    public class KuaiSanSpiderThread : Base168Spider
    {
        public override string Url => "http://api.api68.com/lotteryJSFastThree/getBaseJSFastThree.do?issue=&lotCode=";
    }
}