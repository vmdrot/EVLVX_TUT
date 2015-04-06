using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;

namespace BGU.DRPL.SignificantOwnership.BasicUILib.Forms
{
    public partial class UltimateOwnershipTreeForm : Form
    {
        public UltimateOwnershipTreeForm()
        {
            InitializeComponent();
        }

        private List<OwnershipStructure> _dataSource;

        public List<GenericPersonInfo> MentionedEntities { get; set; }

        public List<OwnershipStructure> DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
                ReBindTree();
            }
        }

        private void ReBindTree()
        {
            
        }
    }
}
