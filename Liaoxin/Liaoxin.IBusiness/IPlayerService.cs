using Liaoxin.Model;
using System;
using Liaoxin.ViewModel;

namespace Liaoxin.IBusiness
{
    public interface IPlayerService
    {
        Player Login(string name, string password, string code, bool isApp);

        Player GetPlayer(int id);

        void ChangePassword(int id, string oldPassword, string newPassword);

        void SetPlayerTitle(int id, string title);
 

    }
}
