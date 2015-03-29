using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.TypeEditors
{
    public class CountryInfoLookupEditor : UITypeEditor
    {

        public override bool IsDropDownResizable
        {
            get
            {
                return true;
            }
        }


        private IWindowsFormsEditorService _editorService;
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            _editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            // use a list box
            ListBox lb = new ListBox();
            lb.SelectionMode = SelectionMode.One;
            lb.SelectedValueChanged += OnListBoxSelectedValueChanged;

            // use the IBenchmark.Name property for list box display
            lb.DisplayMember = "CountryNameUkr";
            lb.ValueMember = "CountryISONr";
            // get the analytic object from context
            // this is how we get the list of possible benchmarks
            CountryInfo selectedCountry = (CountryInfo)value;
            foreach (CountryInfo c in CountryInfo.AllCountries)
            {
                // we store benchmarks objects directly in the listbox
                int index = lb.Items.Add(c);
                if (selectedCountry != null && selectedCountry.CountryISONr == c.CountryISONr)
                {
                    lb.SelectedIndex = index;
                }
            }

            // show this model stuff
            _editorService.DropDownControl(lb);
            if (lb.SelectedItem == null) // no selection, return the passed-in value as is
                return value;

            return lb.SelectedItem;
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown | UITypeEditorEditStyle.Modal;
        }

        private void OnListBoxSelectedValueChanged(object sender, EventArgs e)
        {
            // close the drop down as soon as something is clicked
            _editorService.CloseDropDown();
        }
    }
}