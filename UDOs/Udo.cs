using System.Windows.Forms;
using SAPbobsCOM;

namespace UDOs
{
    public partial class Udo : Form
    {
        private Company _comp;

        public Udo()
        {
            InitializeComponent();

            _comp = new Company();
            _comp.Server = "NMS-PC";
            _comp.CompanyDB = "SBODemoPT";
            _comp.UserName = "manager";
            _comp.UseTrusted = false;
            _comp.Password = "manager";
            _comp.DbServerType = BoDataServerTypes.dst_MSSQL2008;
            _comp.DbUserName = "sa";
            _comp.DbPassword = "nmssa";

            _comp.Connect();
        }

        private void Create()
        {
            var pedidoDefinition = (UserObjectsMD)_comp.GetBusinessObject(BoObjectTypes.oUserObjectsMD);

            pedidoDefinition.Code = "CAPA_PEDIDO";
            pedidoDefinition.Name = "Capa do Pedido";

            pedidoDefinition.ObjectType = BoUDOObjType.boud_Document;
            pedidoDefinition.TableName = "CAPAPEDIDO";
            pedidoDefinition.ChildTables.TableName = "LINHAPEDIDO";

            pedidoDefinition.CanArchive =
                pedidoDefinition.CanCancel =
                pedidoDefinition.CanDelete =
                pedidoDefinition.CanArchive = BoYesNoEnum.tNO;

            pedidoDefinition.CanCreateDefaultForm =
                pedidoDefinition.CanFind =
                pedidoDefinition.CanLog =
                pedidoDefinition.ManageSeries =
                pedidoDefinition.CanCreateDefaultForm = 
                pedidoDefinition.CanYearTransfer = BoYesNoEnum.tYES;

            pedidoDefinition.FindColumns.ColumnAlias = "DocNum";
            pedidoDefinition.FindColumns.ColumnDescription = "Numero Documento";

            pedidoDefinition.FindColumns.Add();

            pedidoDefinition.FindColumns.ColumnAlias = "Status";
            pedidoDefinition.FindColumns.ColumnDescription = "Situação";

            pedidoDefinition.FindColumns.Add();

            pedidoDefinition.FindColumns.ColumnAlias = "U_ParceiroNegocio";
            pedidoDefinition.FindColumns.ColumnDescription = "Cod Parceiro";

            pedidoDefinition.FormColumns.FormColumnAlias = "DocEntry";
            pedidoDefinition.FormColumns.FormColumnDescription = "NumPedido";

            pedidoDefinition.FormColumns.Add();
            pedidoDefinition.FormColumns.FormColumnAlias = "U_ParceiroNegocio";
            pedidoDefinition.FormColumns.FormColumnDescription = "Parceiro";
            
            pedidoDefinition.FormColumns.Add();
            pedidoDefinition.FormColumns.FormColumnAlias = "U_Armazem";
            pedidoDefinition.FormColumns.FormColumnDescription = "Armazem";

            pedidoDefinition.FormColumns.SonNumber = 0;

            pedidoDefinition.EnableEnhancedForm = BoYesNoEnum.tYES;

            pedidoDefinition.EnhancedFormColumns.ChildNumber = 0;
            pedidoDefinition.EnhancedFormColumns.ColumnAlias = "LineId";
            pedidoDefinition.EnhancedFormColumns.ColumnDescription = "NumItem";

            pedidoDefinition.EnhancedFormColumns.Add();
            pedidoDefinition.EnhancedFormColumns.ChildNumber = 0;
            pedidoDefinition.EnhancedFormColumns.ColumnAlias = "U_Item";
            pedidoDefinition.EnhancedFormColumns.ColumnDescription = "Item";

            pedidoDefinition.EnhancedFormColumns.Add();
            pedidoDefinition.EnhancedFormColumns.ChildNumber = 0;
            pedidoDefinition.EnhancedFormColumns.ColumnAlias = "U_Quntidade";
            pedidoDefinition.EnhancedFormColumns.ColumnDescription = "Quantidade";

            pedidoDefinition.MenuUID = "PedidoUDO";
            pedidoDefinition.MenuItem = BoYesNoEnum.tYES;
            pedidoDefinition.FatherMenuID = 2048;
            pedidoDefinition.MenuCaption = "Pedidos";

            pedidoDefinition.Add();

            if (_comp.GetLastErrorCode() < 0)
                MessageBox.Show(_comp.GetLastErrorDescription());
            else
                MessageBox.Show("UDO Criado com sucesso!");
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Create();
        }

        private void Insert()
        {
            GeneralService oDocGeneralService;
            GeneralData oDocGeneralData;
            GeneralDataCollection oDocLinesCollection;
            GeneralData oDocLInesGeneralData;

            var companyService = _comp.GetCompanyService();
            oDocGeneralService = (GeneralService)companyService.GetGeneralService("CAPA_PEDIDO");

            oDocGeneralData = oDocGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

            oDocGeneralData.SetProperty("DocNum", "001");
            oDocGeneralData.SetProperty("U_ParceiroNegocio", "V70000");
            oDocGeneralData.SetProperty("U_Armazem", "AR001");

            oDocLinesCollection = oDocGeneralData.Child("LINHAPEDIDO");

            oDocLInesGeneralData = oDocLinesCollection.Add();
            oDocLInesGeneralData.SetProperty("U_Item", "ITEM-001");
            oDocLInesGeneralData.SetProperty("U_Quntidade", "100");

            oDocGeneralService.Add(oDocGeneralData);
        }

        private void InsertPlus()
        {
            GeneralService oDocGeneralService;
            GeneralData oDocGeneralData;
            GeneralDataCollection oDocLinesCollection;
            GeneralData oDocLInesGeneralData;

            var companyService = _comp.GetCompanyService();

            oDocGeneralService = (GeneralService)companyService.GetGeneralService("CAPA_PEDIDO");

            oDocGeneralData = oDocGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

            oDocGeneralData.SetProperty("DocNum", "001");
            oDocGeneralData.SetProperty("U_ParceiroNegocio", "V70000");
            oDocGeneralData.SetProperty("U_Armazem", "AR001");

            oDocLinesCollection = oDocGeneralData.Child("LINHAPEDIDO");

            oDocLInesGeneralData = oDocLinesCollection.Add();
            oDocLInesGeneralData.SetProperty("U_Item", "ITEM-001");
            oDocLInesGeneralData.SetProperty("U_Quntidade", "100");

            oDocLInesGeneralData = oDocLinesCollection.Add();
            oDocLInesGeneralData.SetProperty("U_Item", "ITEM-002");
            oDocLInesGeneralData.SetProperty("U_Quntidade", "100");

            oDocLInesGeneralData = oDocLinesCollection.Add();
            oDocLInesGeneralData.SetProperty("U_Item", "ITEM-003");
            oDocLInesGeneralData.SetProperty("U_Quntidade", "100");

            oDocGeneralService.Add(oDocGeneralData);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Insert();
            InsertPlus();
        }
    }
}