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
        private SapConnection _con;
        private Items _oItens;
        private Recordset _recordSet;

        public Navigation()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            _con = new SapConnection();
            _con.Connect();
            _oItens = _con.Comany.GetBusinessObject(BoObjectTypes.oItems);
            _recordSet = _con.Comany.GetBusinessObject(BoObjectTypes.BoRecordset);
            _recordSet.DoQuery("select ItemCode from OITM");
            _oItens.Browser.Recordset = _recordSet;

            _oItens.Browser.MoveFirst();

            SetFields(_oItens.ItemCode, _oItens.ItemName, _oItens.SalesUnit);
        }

        private void SetFields(string code, string nome, string preco)
        {
            txtCodigo.Text = code;
            txtDescricao.Text = nome;
            txtUnidade.Text = preco;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void btnLat_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {

        }
    }
}
