using AllLottery.Model;
using System;
using AllLottery.ViewModel;

namespace AllLottery.IBusiness
{
    public interface IPlayerService
    {
        Player Login(string name, string password, string code, bool isApp);

        Player GetPlayer(int id);

        void ChangePassword(int id, string oldPassword, string newPassword);

        void SetPlayerTitle(int id, string title);

        PlayerLoginLog[] GetNewLoginLogs(int id, int size);

        void SetInformation(int id, string phone, string qq, string weChat, DateTime? birthday);

        void ChangeCoinPassword(int id, string oldPassword, string newPassword);

        void CreateRegisterLink(int playerId, PlayerTypeEnum type, decimal rebate, string remark);

        ProxyRegister[] GetRegisterLink(int playerId, int index, int size, out int total);

        Player RegisterPlayer(string number, string name, string password, string qq);

        Player[] GetUnderPlayers(int playerId, string name, PlayerTypeEnum? type, int index, int size, out int total, PlayerSortEnum sort, string wechat, string qq, string phone, bool? isOnline);

        void Transfer(int playerId, int underPlayerId, decimal money, string password, string remark);

        void SettingRebate(int playerId, int underPlayerId, decimal rebate);

        void SettingDailyWageRate(int playerId, int underPlayerId, decimal rebate);

        void SettingDividendRate(int playerId, int underPlayerId, decimal rebate);

        void DeleteLink(int playerId, int linkId);

        DividendLog[] GetTeamDividendLogs(int playerId, string name, DateTime? begin, DateTime? end, int index,
            int size, out int total);

        DividendLog[] GetDividendLogs(int playerId, DateTime? begin, DateTime? end, int index, int size, out int total);

        DailyWageLog[] GetTeamDailyWageLogs(int playerId, string name, DateTime? begin, DateTime? end, int index,
            int size, out int total);

        DailyWageLog[] GetDailyWageLogs(int playerId, DateTime? begin, DateTime? end, int index, int size,
            out int total);

        Player CreatePlayer(int playerId, string name, string password, PlayerTypeEnum type, decimal rebate);

        Player CreateTestPlayer(string code);

        SoftwareExpired GetSoftwareExpired(int playerId);
    }
}
