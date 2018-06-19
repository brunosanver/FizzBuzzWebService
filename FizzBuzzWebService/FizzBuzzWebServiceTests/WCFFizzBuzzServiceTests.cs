using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FizzBuzzWebService.Tests
{
    [TestClass()]
    public class WCFFizzBuzzServiceTests
    {
        /* Comprueba que si se introduce un valor 
         * superior al límite establecido se arroja una excepción
         * informando de que el valor está fuera de rango */
        [TestMethod()]
        public void TestOutOfRangeExceptionCase()
        {
            WCFFizzBuzzService ws = new WCFFizzBuzzService();
            try
            {
                ws.GetData(101);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Input value greater than limit.");
                return;
            }
            Assert.Fail("No exception was thrown");
        }

        /* Comprueba que si el entero introducido está dentro 
         * del límite, se retorna efectivamente una lista de strings */
        [TestMethod()]
        public void TestCorrectValueCase()
        {
            WCFFizzBuzzService ws = new WCFFizzBuzzService();
            Assert.IsInstanceOfType(ws.GetData(0), typeof(List<string>));
        }
    }
}