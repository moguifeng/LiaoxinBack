using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace Zzb.BaseData.Model
{
    public abstract class BaseModal : BaseButton
    {
        protected BaseModal()
        {
        }

        protected BaseModal(string id, string name) : base(id, name)
        {
        }

        public virtual BaseButton[] Buttons()
        {
            return new BaseButton[0];
        }

        public abstract string ModalName { get; }

        public string ModalId => SecurityHelper.MD5Encrypt(this.GetType().FullName);

        public virtual void Init()
        {

        }

        public override string ButtonType => "ModalButton";
    }
}