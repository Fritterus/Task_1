﻿using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// Method for reading data from csv file and writing it to database
        /// </summary>
        /// <param name="pathFile">path to file</param>
        public Task ImportFromCsvToDatabase(string pathFile);

        /// <summary>
        /// Method for writing data to csv file
        /// </summary>
        /// <param name="pathFile">path to file</param>
        /// <param name="users">list of users</param>
        public void WriteToCsvFile(string pathFile, List<User> users);

        /// <summary>
        /// Method for writing data to xml file
        /// </summary>
        /// <param name="pathFile">path to file</param>
        /// <param name="users">list of users</param>
        public void WriteToXmlFile(string pathFile, List<User> users);
    }
}
