using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAOSap;
using SAPbobsCOM;

namespace DataNavigation
{
    public partial class Navigation : Form
    {
        private SapConnectionOld _con;
        private Items _oItens;
        private Recordset _recordSet;

        public Navigation()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            _con = new SapConnectionOld();
            _con.Connect();
            _oItens = _con.Comany.GetBusinessObject(BoObjectTypes.oItems);
            _recordSet = _con.Comany.GetBusinessObject(BoObjectTypes.BoRecordset);
            _recordSet.DoQuery("select ItemCode from OITM");
            _oItens.Browser.Recordset = _recordSet;

            _oItens.Browser.MoveFirst();

            SetFields();
        }

        private void SetFields()
        {
            txtCodigo.Text = _oItens.ItemCode;
            txtDescricao.Text = _oItens.ItemName;
            txtUnidade.Text = _oItens.SalesUnit;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _oItens.Browser.MoveNext();

            if (_oItens.Browser.EoF)
                _oItens.Browser.MoveFirst();


            SetFields();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (_oItens.Browser.BoF)
                _oItens.Browser.MoveLast();
            else
                _oItens.Browser.MovePrevious();

            SetFields();
        }

        private void btnLat_Click(object sender, EventArgs e)
        {
            _oItens.Browser.MoveLast();
            SetFields();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            _oItens.Browser.MoveFirst();
            SetFields();
        }
    }
}
