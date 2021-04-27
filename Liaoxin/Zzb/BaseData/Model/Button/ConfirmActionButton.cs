namespace Zzb.BaseData.Model.Button
{
    public class ConfirmActionButton : ActionButton
    {
        public ConfirmActionButton(string action, string name, string confirmMessage) : base(action, name)
        {
            ConfirmMessage = confirmMessage;
        }

        public string ConfirmMessage { get; set; }
    }
}