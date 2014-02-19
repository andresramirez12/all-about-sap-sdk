using System.Windows.Forms;
using DAOSap;
using SAPbobsCOM;
using System;
using System.Runtime.InteropServices;

namespace MetaDados
{
    
    public partial class Metadados : Form
    {
        private Company _comp;

        //private SapConnection _con;
        private const string TABLE_NAME = "TODO";
        private const string FIELD_DUE = "Due";
        private const string FIELD_DESCRIPTION = "Desc";

        public Metadados()
        {
            InitializeComponent();

            //_con = new SapConnection();
            //_con.Connect();

            _comp = new Company();
            _comp.Server = "NMS-PC";
            _comp.CompanyDB = "SBODemoPT";
            _comp.UserName = "manager";
            _comp.UseTrusted = false;
            _comp.Password = "manager";
            _comp.DbServerType = BoDataServerTypes.dst_MSSQL2008;
            //Comany.language     = BoSuppLangs.ln_Portuguese_Br;
            _comp.DbUserName = "sa";
            _comp.DbPassword = "nmssa";

            _comp.Connect();
        }

        private void Create()
        {
            UserTablesMD table = _comp.GetBusinessObject(BoObjectTypes.oUserTables);

            if (table.GetByKey(TABLE_NAME))
            {
                table = null;
                MessageBox.Show("Tabela já criada: " + TABLE_NAME);
            }
            else
            {
                table.TableName = TABLE_NAME;
                table.TableDescription = "List de Tarefas";
                table.Add();

                if (_comp.GetLastErrorCode() < 0)
                    MessageBox.Show(_comp.GetLastErrorDescription());
                else
                    MessageBox.Show("Tabela: " + TABLE_NAME + " criada com sucesso!");

                Marshal.ReleaseComObject(table);
                GC.Collect();
                table = null;
            }
        }

        private void CreateField()
        {
            var field = (UserFieldsMD)_comp.GetBusinessObject(BoObjectTypes.oUserFields);

            field.TableName = TABLE_NAME;
            field.Name = FIELD_DUE;
            field.Description = "Data Conclusão";
            field.Type = BoFieldTypes.db_Date;
            field.Add();

            if (_comp.GetLastErrorCode() < 0)
                MessageBox.Show(_comp.GetLastErrorDescription());
            else
                MessageBox.Show("Campo: " + FIELD_DUE + " criado com sucesso!");

            field.TableName = TABLE_NAME;
            field.Name = FIELD_DESCRIPTION;
            field.Description = "Descrição da Tarefa";
            field.Type = BoFieldTypes.db_Alpha;
            field.EditSize = 254;
            field.Add();

            if (_comp.GetLastErrorCode() < 0)
                MessageBox.Show(_comp.GetLastErrorDescription());
            else
                MessageBox.Show("Campo: " + FIELD_DESCRIPTION + " criado com sucesso!");

            Marshal.ReleaseComObject(field);
            GC.Collect();
            field = null;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Create();
            CreateField();
        }

        private void Record()
        {
            var userTable = (UserTable)_comp.UserTables.Item(TABLE_NAME);

            userTable.Code = "C001";
            userTable.Name = "N001";

            //  coloar a inserto do novo dado
        }
    }
}