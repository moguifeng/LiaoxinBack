using Liaoxin.Model;
using System;
using Liaoxin.ViewModel;
using static Liaoxin.ViewModel.ClientViewModel;

namespace Liaoxin.IBusiness
{
    public interface IClientService
    {
        Client Login(ClientLoginRequest request);

        Client LoginByCode(ClientLoginByCodeRequest request);

        ClientBaseInfoResponse GetClient();

        bool ChangePassword(ClientChangePasswordRequest request);

        bool ChangeCoinPassword(ClientChangeCoinPasswordRequest request);

    }
}
