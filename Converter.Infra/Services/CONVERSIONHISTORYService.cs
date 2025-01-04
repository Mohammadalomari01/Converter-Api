using Converter.Core.Models;
using Converter.Core.Repository;
using Converter.Core.Services;
using Converter.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter.Infra.Services
{
    public class CONVERSIONHISTORYService : ICONVERSIONHISTORYService
    {
        private readonly ICONVERSIONHISTORYRepository _conversionHistoryRepository;

        public CONVERSIONHISTORYService(ICONVERSIONHISTORYRepository conversionHistoryRepository)
        {
            _conversionHistoryRepository = conversionHistoryRepository;
        }

        public List<Conversionhistory> GetAllConversions()
        {
            return _conversionHistoryRepository.GetAllConversions();
        }

        public Conversionhistory GetConversionById(int id)
        {
            return _conversionHistoryRepository.GetConversionById(id);
        }

        public void CreateConversion(Conversionhistory conversionHistory)
        {
            _conversionHistoryRepository.CreateConversion(conversionHistory);
        }

        public void UpdateConversion(Conversionhistory conversionHistory)
        {
            _conversionHistoryRepository.UpdateConversion(conversionHistory);
        }

        public void DeleteConversion(int id)
        {
            _conversionHistoryRepository.DeleteConversion(id);
        }
    }
}
