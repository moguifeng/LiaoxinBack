using System.Collections.Generic;

namespace AllLottery.ViewModel
{
    public class ChasingOrderAddOrderViewModel
    {
        public bool IsWinStop { get; set; }

        public int BetModeId { get; set; }

        public int LotteryPlayDetailId { get; set; }

        public string BetNo { get; set; }

        public List<AddOrderViewModel> Orders { get; set; }
    }

    public class AddOrderViewModel
    {
        public int Times { get; set; }

        public string Number { get; set; }
    }
}