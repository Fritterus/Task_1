using AutoMapper;
using BLL.Interfaces;
using BLL.ModelsDTO;
using CsvHelper;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL.WorkFile
{
    /// <summary>
    /// Class for working with files
    /// </summary>
    internal class FileService : IFileService
    {
        private readonly IRepository<User> _db;
        private readonly IMapper _mapper;

        public FileService(IRepository<User> db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task ImportFromCsvToDatabase(string pathFile)
        {
            try
            {
                using var reader = new StreamReader(pathFile);
                using var csv = new CsvReader(reader, CultureInfo.GetCultureInfo("ru-ru"));
                
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.Delimiter = ";";
                csv.Configuration.RegisterClassMap<UserMap>();

                while (csv.Read())
                {
                    var records = csv.GetRecord<User>();
                    await _db.CreateAsync(records);
                }
            }
            catch (Exception)
            {
                throw new IOException("Error open file! File is already open.");
            }
        }

        /// <inheritdoc/>
        public async Task WriteToCsvFile(string pathFile, List<UserDTO> users)
        {
            try
            {
                _mapper.Map<List<User>>(users);
                using var streamReader = new StreamWriter(pathFile);
                using var csvReader = new CsvWriter(streamReader, CultureInfo.GetCultureInfo("ru-ru"));
                csvReader.Configuration.HasHeaderRecord = false;
                csvReader.Configuration.Delimiter = ";";
                csvReader.Configuration.RegisterClassMap<UserMap>();
                await csvReader.WriteRecordsAsync(users);
            }
            catch (Exception)
            {
                throw new IOException("Error save to file! File is already open.");
            }
        }

        /// <inheritdoc/>
        public async Task WriteToXmlFile(string pathFile, List<UserDTO> users)
        {
            try
            {
                _mapper.Map<List<User>>(users);
                var elements = new XElement("TestProgram",
                users.Select(obj => new XElement("Record",
                    new XAttribute("Id", obj.Id),
                        new XElement("Date", obj.Date.ToShortDateString()),
                        new XElement("Name", obj.Name),
                        new XElement("LastName", obj.LastName),
                        new XElement("Patronymic", obj.Patronymic),
                        new XElement("City", obj.City),
                        new XElement("Country", obj.Country))));
                elements.Save(pathFile);
            }
            catch (Exception)
            {
                throw new IOException("Error save to file! File is already open.");
            }
        }
    }
}
