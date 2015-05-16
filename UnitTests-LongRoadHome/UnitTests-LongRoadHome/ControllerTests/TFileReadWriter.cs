using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uk.ac.dundee.arpond.longRoadHome.Controller;

namespace UnitTests_LongRoadHome.ControllerTests
{
    [TestClass]
    public class TFileReadWriter
    {
        FileReadWriter frw;

        [TestInitialize]
        public void Setup()
        {
             frw = new FileReadWriter();
        }

        [TestCategory("FileReadWriter"), TestCategory("Controller"), TestMethod()]
        public void FileReadWriter_WriteFile()
        {
            String filename = "Test.txt";
            String toWrite = "Test Text";
            Assert.IsTrue(frw.WriteSaveDataFile(filename, toWrite), "File should be written to succesfully");

            String written = System.IO.File.ReadAllText(FileReadWriter.SAVE_PATH + filename);
            Assert.AreEqual(toWrite, written, "Text should be the same");
        }

        [TestCategory("FileReadWriter"), TestCategory("Controller"), TestMethod()]
        public void FileReadWriter_ReadFile()
        {
            String filename = "Test.txt";
            String filename2 = "Test2.txt";
            String text = "Test Text";
            String read = frw.ReadSaveDataFile(filename);

            Assert.AreEqual(text, read, "File should contain expected text");
            read = frw.ReadSaveDataFile(filename2);
            Assert.AreEqual("", read, "Reading from non existent file should return empty string");
        }
    }
}
