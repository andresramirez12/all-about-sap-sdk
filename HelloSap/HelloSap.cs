using System;
using System.IO;
using System.Windows.Forms;
using DAOSap;
using SAPbobsCOM;

namespace HelloSap
{
    public partial class HelloSap : Form
    {
        private SapConnectionOld _con = new SapConnectionOld();
        private string xml;

        public HelloSap()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            _con.Connect();
            MessageBox.Show(_con.Message);
        }

        private void CriarParceiro()
        {
            BusinessPartners parceiro = _con.Comany.GetBusinessObject(BoObjectTypes.oBusinessPartners);

            parceiro.CardCode = "3333";
            parceiro.CardName = "Eita Lele";
            parceiro.CardType = BoCardTypes.cCustomer;

            parceiro.Add();

            MessageBox.Show(_con.Message);
        }

        private void CriarItem()
        {
            Items item = _con.Comany.GetBusinessObject(BoObjectTypes.oItems);
            item.ItemCode = "XPTO0123";

            item.Add();

            MessageBox.Show(_con.Message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CriarParceiro();
            CriarItem();
        }

        private void AddPessoaContato()
        {
            var oContact = _con.Comany.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            var exist = oContact.GetByKey("F99999");

            if (exist)
            {
                oContact.ContactEmployees.Name = "Jonh Cash";

                oContact.ContactEmployees.Add();

                oContact.ContactEmployees.Name = "Jonh Walker";
                MessageBox.Show(_con.Message);
            }
            else
            {
                MessageBox.Show("Usuario não encontrado!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddPessoaContato();
        }

        private Documents AddOrderCompra()
        {
            Documents compra = _con.Comany.GetBusinessObject(BoObjectTypes.oPurchaseOrders);

            compra.CardCode = "F99999";
            compra.DocDueDate = DateTime.Now;

            compra.Lines.ItemCode = "43211";
            compra.Lines.Quantity = 1;

            compra.Lines.Add();

            compra.Lines.ItemCode = "54321";
            compra.Lines.Quantity = 3;

            compra.Add();

            MessageBox.Show(_con.Message);

            return compra;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddNotaFiscalEntrada(AddOrderCompra());
        }

        private void AddNotaFiscalEntrada(Documents purchaseOrder)
        {
            string code;
            _con.Comany.GetNewObjectCode(out code); // pega o codigo do ultimo objeto criado

            Documents nota = _con.Comany.GetBusinessObject(BoObjectTypes.oPurchaseInvoices);

            nota.CardCode = "F99999";
            nota.DocDueDate = DateTime.Now;

            nota.Lines.BaseType = (int)BoObjectTypes.oPurchaseOrders;
            nota.Lines.BaseEntry = Convert.ToInt32(code);
            nota.Lines.BaseLine = 0;
            nota.Lines.TaxCode = "5403-001";
            nota.Lines.Add();

            nota.Lines.BaseType = (int)BoObjectTypes.oPurchaseOrders;
            nota.Lines.BaseEntry = Convert.ToInt32(code);
            nota.Lines.TaxCode = "5403-001";
            nota.Lines.BaseLine = 1;

            nota.Add();
            MessageBox.Show(_con.Message);
        }

        private void CreateXml()
        {
            BusinessPartners cliente = _con.Comany.GetBusinessObject(BoObjectTypes.oBusinessPartners);

            _con.Comany.XmlExportType = BoXmlExportTypes.xet_ExportImportMode;

            var exist = cliente.GetByKey("C20000");

            if (exist)
            {
                xml = String.Format("{0}/{1}.xml", AppDomain.CurrentDomain.BaseDirectory, cliente.CardCode);
                cliente.SaveXML(xml);

                linkLabel1.Text = xml;
            }
            else
                MessageBox.Show("Parceiro não cadastrado");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CreateXml();
            LoadXml();
        }

        private void LoadXml()
        {
            var novo = new StreamReader(xml).ReadToEnd().Replace("C20000", "C29999").Replace("94.549.548/0001-39", "99.998.989/1111-99");
            _con.Comany.XMLAsString = true;
            BusinessPartners d = _con.Comany.GetBusinessObjectFromXML(novo, 0);

            d.Add();
            MessageBox.Show(_con.Message);
        }
    }
}