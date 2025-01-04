using Converter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter.Core.Repository
{
    public interface ICONVERSIONHISTORYRepository
    {
        List<Conversionhistory> GetAllConversions();
        Conversionhistory GetConversionById(int id);
        void CreateConversion(Conversionhistory conversionHistory);
        void UpdateConversion(Conversionhistory conversionHistory);
        void DeleteConversion(int id);

    }
}
