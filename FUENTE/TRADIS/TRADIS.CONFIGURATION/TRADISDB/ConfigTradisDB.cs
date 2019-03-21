using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TRADIS.CONFIGURATION
{
   public class ConfigTradisDB
    {
        private static BDConfiguration mSigCom;
        private static Mutex mMutex = new Mutex();
        private static ConfigTradisDB mInstance;

        public ConfigTradisDB(string aplicacion)
        {
            try
            {
                mSigCom = new BDConfiguration();
                mSigCom = DatoProduccion(aplicacion);
            }
            catch
            {
                throw;
            }
        }

        private BDConfiguration DatoProduccion(string aplicacion)
        {

            mSigCom = new BDConfiguration
            {
                BaseDatos = "TRADISDB",
                Usuario = "sa",
                Password = "fisuncp",
                Servidor = "localhost",
                Provider = "SQL"
            };
            return mSigCom;
        }

        public static ConfigTradisDB GetInstance(string aplicacion)
        {
            mMutex.WaitOne();
            if ((mInstance == null))
            {
                mInstance = new ConfigTradisDB(aplicacion);
            }
            mMutex.ReleaseMutex();
            return mInstance;
        }

        public BDConfiguration Parametros
        {
            get
            {
                return mSigCom;
            }
        }
    }
}
