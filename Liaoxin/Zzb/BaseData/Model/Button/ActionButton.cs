namespace Zzb.BaseData.Model.Button
{
    public class ActionButton : BaseButton
    {
        public string Action { get; set; }

        public ActionButton(string action, string name) : base(action, name)
        {
            Action = action;
        }
    }
}