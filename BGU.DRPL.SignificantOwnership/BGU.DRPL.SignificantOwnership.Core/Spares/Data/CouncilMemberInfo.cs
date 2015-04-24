﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Evolvex.Utility.Core.ComponentModelEx;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    /// <summary>
    /// Інформація про особу-члена керівного колегіального органу (наглядової ради, ради директорів, тощо)
    /// </summary>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.CouncilMemberInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class CouncilMemberInfo
    {

        /// <summary>
        /// Як завжди, тут тільки ідентифікатор особи; самі реквізити - в MentionedEntities (чи еквіваленті)
        /// </summary>
        [Description("Член органу управління")]
        [DisplayName("Член органу управління")]
        [Required]
        public GenericPersonID Member { get; set; }
        /// <summary>
        /// за змовчанням - просто "член"
        /// </summary>
        [Description("Посада")]
        [DisplayName("Посада")]
        [Required]
        public string PositionName { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1}", PositionName, Member);
        }
    }
}
