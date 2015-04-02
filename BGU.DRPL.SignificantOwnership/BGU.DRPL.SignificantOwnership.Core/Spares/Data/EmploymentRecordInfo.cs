using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EmploymentRecordInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class EmploymentRecordInfo
    {
        [DisplayName("Роботодавець")]
        public GenericPersonID Employer { get; set; }
        [DisplayName("Посада/функція")]
        public string JobTitle { get; set; }
        [DisplayName("Дата початку роботи")]
        public DateTime DateStarted { get; set; }
        [DisplayName("Дата кінця роботи")]
        public DateTime DateQuit { get; set; }
        [DisplayName("Тип зайнятості")]
        public EmploymentState State { get; set; }
        [DisplayName("Тип закінчення діяльності")]
        public EmploymentTerminationType TerminationType { get; set; }
        [DisplayName("Причина звільнення")]
        [Description("причина звільнення; якщо трудовий стаж переривався, то слід зазначити причину")]
        public string DismissalOrUnemployedReason { get; set; }
        [DisplayName("Контакти роботодавця")]
        public ContactInfo EmployerContact { get; set; }
    }
}
