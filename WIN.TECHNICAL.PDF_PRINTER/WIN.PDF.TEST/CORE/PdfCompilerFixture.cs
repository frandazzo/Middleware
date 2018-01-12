using System;
using System.Collections.Generic;

using System.Text;
using NUnit.Framework;
using System.IO;

namespace WIN.TECHNICAL.PDF_PRINTER.WIN.PDF.TEST.CORE
{
    
    [TestFixture]
    public class PdfCompilerFixture
    {

        [Test]

        public void PdfCompilerTest()
        {

            PdfCompiler m = new PdfCompiler("c:\\ds21vuoto.pdf", "c:\\ds21prova.pdf");
            Assert.AreEqual(m.GetSorgente, "c:\\ds21vuoto.pdf");
            Assert.AreEqual(m.GetDestinazione, "c:\\ds21prova.pdf");
            m.Setup();
            m.SetCampo("nome", "paolo");
            m.SetCampo("cognome", "berardone");
            m.Compile();
            Assert.AreEqual(m.GetField("nome"), "paolo");
            Assert.AreEqual(m.GetField("cognome"), "berardone");

            m.ClearField("nome");
            Assert.AreEqual(m.GetField("nome"), "");
                       
            m.Clear();
            Assert.AreEqual(m.GetField("nome"), "");
            Assert.AreEqual(m.GetField("cognome"), "");
            m.Dispose();
            File.Delete("c:\\ds21prova3.pdf");

        }






    }
}
