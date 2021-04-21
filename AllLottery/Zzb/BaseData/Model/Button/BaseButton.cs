namespace Zzb.BaseData.Model.Button
{
    public abstract class BaseButton
    {
        protected BaseButton()
        {
        }

        protected BaseButton(string id, string name)
        {
            Id = id;
            ButtonName = name;
        }

        public string Id { get; set; }

        public string ButtonName { get; set; }

        public string Icon { get; set; }

        public string Type { get; set; } = "primary";

        public virtual string ButtonType => this.GetType().Name;
    }
}