using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutanda.Models;

namespace Mutanda.Services
{

    //Restituisce i dati di devicemail e idagente
    public class ParamDispenser:IDisposable
    {
        private static ParamDispenser m_ParamDispenser = null;
        private IDataService _DataService;
        
        public static ParamDispenser GetInstance()
        {
            if (m_ParamDispenser != null)
            {
                //if (m_ParamDispenser.IsDisposed)
                //{
                //    m_ParamDispenser.Dispose();
                //    m_ParamDispenser = null;
                //}
                //else
                //{
                    return m_ParamDispenser;
                //}
            }

            m_ParamDispenser = new ParamDispenser();
            return m_ParamDispenser;
        }

        private  ParamDispenser()
        {
            _DataService = new DataService();
        }

        public void Dispose()
        { }

        async public Task<string>Get_DeviceMail()
        {
            Authorization _Auth = (await _DataService.GetAuthorizationAsync());
            if (_Auth != null)
                return _Auth.DeviceMail;
            else
                return string.Empty;

        }

    }
}
