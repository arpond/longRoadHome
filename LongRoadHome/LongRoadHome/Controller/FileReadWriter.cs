using System;
using System.IO;
using System.Windows.Forms;
namespace uk.ac.dundee.arpond.longRoadHome.Controller
{
    public class FileReadWriter
    {
        public const String CATALOGUE_PATH = ".\\catalogues\\";
        public const String SAVE_PATH = ".\\saveData\\";

        static FileReadWriter()
        {
            if (!Directory.Exists(CATALOGUE_PATH))
            {
                Directory.CreateDirectory(CATALOGUE_PATH);
            }
            if (!Directory.Exists(SAVE_PATH))
            {
                Directory.CreateDirectory(SAVE_PATH);
            }
        }

        /// <summary>
        /// For reading from a catalogue file
        /// </summary>
        /// <param name="catalogue">The catalogue to read</param>
        /// <returns>The catalogue file as a string</returns>
        public String ReadCatalogueFile(String catalogue)
        {
            String filename = CATALOGUE_PATH + catalogue;
            return ReadFile(filename);
        }

        /// <summary>
        /// The save data to read
        /// </summary>
        /// <param name="filename">The save data file to read</param>
        /// <returns>The save data file as a string</returns>
        public String ReadSaveDataFile(String filename)
        {
            filename = SAVE_PATH + filename;
            return ReadFile(filename);
        }

        /// <summary>
        /// The save data file to write
        /// </summary>
        /// <param name="filename">The save data file to write</param>
        /// <param name="toWrite">The data to write</param>
        /// <returns>If the data was written correctly</returns>
        public bool WriteSaveDataFile(String filename, String toWrite)
        {
            filename = SAVE_PATH + filename;
            return WriteFile(filename, toWrite);
        }

        /// <summary>
        /// Read from a file
        /// </summary>
        /// <param name="filename">File to read from</param>
        /// <returns>What is read from the file</returns>
        private String ReadFile(String filename)
        {
            String toRead = "";
            try
            {
                toRead = System.IO.File.ReadAllText(filename);
                return toRead;
            }
            catch (Exception e)
            {
                return toRead;
            }

        }

        /// <summary>
        /// Writes text to a file
        /// </summary>
        /// <param name="filename">File to write to</param>
        /// <param name="toWrite">Text to write</param>
        /// <returns>If succesful</returns>
        private bool WriteFile(String filename, String  toWrite)
        {
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(filename);
                file.Write(toWrite);
                file.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

}
