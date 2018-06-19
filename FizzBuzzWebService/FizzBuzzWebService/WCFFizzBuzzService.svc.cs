using log4net;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace FizzBuzzWebService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,
        InstanceContextMode = InstanceContextMode.PerCall)]
    public class WCFFizzBuzzService : IWCFFizzBuzzService
    {
        private static readonly ILog log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static object _lock = new object();

        public List<string> GetData(int value)
        {
            log.Info("Inicio servicio.");
            List<string> numSequence = new List<string>(); 
            const int limit = 100;
            try
            {
                if (value > limit)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    while (value <= limit)
                    {
                        numSequence.Add((value % 3 == 0 && value % 5 == 0) ? "fizzbuzz" :
                            ((value % 3 == 0) ? "fizz" : (value % 5 == 0) ? "buzz" : value.ToString()));

                        value++;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Warn(ex.Message);
                /* Avisa de que el entero introducido está fuera de límite */
                throw new FaultException(string.Format(
                    "{0} Input value greater than limit.", ex.Message));
            }

            string seq = string.Join(", ", numSequence.ToArray());

            lock(_lock)
            {
                try
                {
                    log.Info("Se inicia escritura registro en fichero");
                    FunctionCollections.WriteToAFile(string.Concat(DateTime.Now.ToString(
                        "[MMMM dd, yyyy hh:mm ss's']"), "\t", seq));
                    /* Si se produce una excepción en la escritura del fichero,
                     * la siguiente Info no quedará registrada */
                    log.Info("Escritura finalizada, fichero cerrrado");
                }
                catch (Exception e)
                { 
                    log.Error(e.Message);
                    /* Des-comentamos si deseamos que la excepción 
                     * se propague hasta conocimiento del cliente */
                    // throw new FaultException(e.GetType().ToString()); 
                }
            }

            numSequence.Insert(0, "GO!");
            /* Si se provoca una excepción por introducir un valor
             * superior al límit, esta Info no llegará a registrarse */
            log.Info("Sale del servicio.");

            return numSequence;
        }        
    }
}
