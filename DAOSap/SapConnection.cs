using System;
using SAPbobsCOM;

namespace DAOSap
{
    public class SapConnection
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
                Comany.Server       = "VM-DESENVSAP9";
                Comany.CompanyDB    = "sbo_demobr_teste01";
                Comany.UserName     = "manager";
                Comany.UseTrusted   = false;
                Comany.Password     = "nms@sap";
                Comany.DbServerType = BoDataServerTypes.dst_MSSQL2008;
                //Comany.language     = BoSuppLangs.ln_Portuguese_Br;
                Comany.DbUserName   = "sa";
                Comany.DbPassword   = "nms@sa";

                var conCode = Comany.Connect();
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
        }
    }
}