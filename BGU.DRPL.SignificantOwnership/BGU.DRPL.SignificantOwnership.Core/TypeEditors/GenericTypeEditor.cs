using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;

namespace BGU.DRPL.SignificantOwnership.Core.TypeEditors
{
    public abstract class GenericTypeEditor<T> : UITypeEditor  
    {
        protected abstract ITypeEditorFormFactoryBase TypeEditorFormFactory { get;}
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            System.Windows.Forms.Form frm = TypeEditorFormFactory.SpawnInstance();
            ((IDataSourcedForm<T>)frm).DataSource = (T)value;
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return value;
            else
                return ((IDataSourcedForm<T>)frm).DataSource;
            
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

    }
}
