using AllLottery.Model;

namespace AllLottery.IBusiness
{
    public interface IMessageService
    {
        void AddAllUserMessage(string message, MessageTypeEnum type);

        Message[] GetPlayerMessages(int playerId, int index, int size, out int total);

        Message[] GetUserMessage(int userId, int index, int size, out int total);

        void ReadMessage(int id);

        void ClearUserMessage(int userId);
    }
}