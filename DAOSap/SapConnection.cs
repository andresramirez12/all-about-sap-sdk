using System;
using SAPbobsCOM;

namespace DAOSap
{
    public interface ISapInteraction
    {
        void Execute();
        bool Success { get; }
        string Message { get; }
    }

    public class SapConnection : ISapInteraction
    {
        private Company _company;
        private ConnectionConf _config;

        public SapConnection(ConnectionConf configuration)
        {

        }

        public void Execute()
        {
            _company              = new Company();
            _company.Server       = _config.Server;// "NMS-PC";
            _company.CompanyDB    = _config.CompanyDB;// "SBODemoPT";
            _company.UserName     = _config.UserName;// "manager";
            _company.Password     = _config.Password;// "manager";
            _company.DbServerType = BoDataServerTypes.dst_MSSQL2008;
            _company.DbUserName   = _config.DbUserName;// "sa";
            _company.DbPassword   = _config.DbPassword;// "nmssa";

        }

        public bool Success
        {
            get { throw new NotImplementedException(); }
        }

        public string Message
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class SapConnectionOld
    {
        private Exception _ex;
        private int _error;

        public string Message
        {
            get
            {
                string errorMsg;

                Comany.GetLastError(out _error, out errorMsg);

                if (Success)
                {
                    return String.Format("Conectado com sucesso! \n {0}", Comany.CompanyName);
                }
                else
                {
                    if (_ex == null)
                        return String.Format("Ocorreu um erro: {0}, code: {1}", errorMsg, _error);
                    else
                        return String.Format("Ocorreu um erro: {0}, code: {1}, exceção", errorMsg, _error, _ex.Message);
                }
            }
        }

        public bool Success
        {
            get
            {
                return _error == 0;
            }
        }

        public Company Comany { get; private set; }

        public void Connect()
        {
            try
            {
                Comany              = new Company();
                Comany.Server = "NMS-PC";
                Comany.CompanyDB = "SBODemoPT";
                Comany.UserName = "manager";
                Comany.UseTrusted   = false;
                Comany.Password = "manager";
                Comany.DbServerType = BoDataServerTypes.dst_MSSQL2008;
                //Comany.language     = BoSuppLangs.ln_Portuguese_Br;
                Comany.DbUserName   = "sa";
                Comany.DbPassword = "nmssa";

                var conCode = Comany.Connect();
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
        }
    }
}