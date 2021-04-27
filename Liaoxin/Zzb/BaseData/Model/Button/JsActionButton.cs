namespace Zzb.BaseData.Model.Button
{
    public class JsActionButton : BaseButton
    {
        public JsActionButton()
        {
        }

        public JsActionButton(string id, string name, string actionType)
        {
            ActionType = actionType;
            Id = id;
            ButtonName = name;
        }

        public string ActionType { get; set; }
    }
}