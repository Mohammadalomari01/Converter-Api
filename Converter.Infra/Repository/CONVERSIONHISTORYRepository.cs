using Converter.Core.Common;
using Converter.Core.Models;
using Converter.Core.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter.Infra.Repository
{
    public class CONVERSIONHISTORYRepository: ICONVERSIONHISTORYRepository
    {
        private readonly IDbContext _dbContext;

        public CONVERSIONHISTORYRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Conversionhistory> GetAllConversions()
        {
            // Retrieve all conversion records from the database
            IEnumerable<Conversionhistory> result = _dbContext.Connection.Query<Conversionhistory>(
                "ConversionHistory_Package.GetAllConversionHistory",
                commandType: CommandType.StoredProcedure
            );
            return result.ToList();
        }

        public Conversionhistory GetConversionById(int id)
        {
            // Retrieve a specific conversion record by ID
            var p = new DynamicParameters();
            p.Add("p_ConversionId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var result = _dbContext.Connection.Query<Conversionhistory>(
                "ConversionHistory_Package.GetConversionHistoryById",
                p,
                commandType: CommandType.StoredProcedure
            );

            return result.SingleOrDefault();
        }

        public void CreateConversion(Conversionhistory conversionHistory)
        {
            // Insert a new conversion record into the database
            var p = new DynamicParameters();
            p.Add("p_UserId", conversionHistory.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_SourceFile", conversionHistory.Sourcefile, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_OutputFile", conversionHistory.Outputfile, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_FileSize", conversionHistory.Filesize, dbType: DbType.Double, direction: ParameterDirection.Input);
            p.Add("p_Status", conversionHistory.Status, dbType: DbType.String, direction: ParameterDirection.Input);

            _dbContext.Connection.Execute(
                "ConversionHistory_Package.CreateConversionHistory",
            p,
                commandType: CommandType.StoredProcedure
            );
        }

        public void UpdateConversion(Conversionhistory conversionHistory)
        {
            // Update an existing conversion record in the database
            var p = new DynamicParameters();
            p.Add("p_ConversionId", conversionHistory.Conversionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_UserId", conversionHistory.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("p_SourceFile", conversionHistory.Sourcefile, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_OutputFile", conversionHistory.Outputfile, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_FileSize", conversionHistory.Filesize, dbType: DbType.Double, direction: ParameterDirection.Input);
            p.Add("p_Status", conversionHistory.Status, dbType: DbType.String, direction: ParameterDirection.Input);

            _dbContext.Connection.Execute(
                "ConversionHistory_Package.UpdateConversionHistory",
                p,
                commandType: CommandType.StoredProcedure
            );
        }

        public void DeleteConversion(int id)
        {
            // Delete a conversion record from the database
            var p = new DynamicParameters();
            p.Add("p_ConversionId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            _dbContext.Connection.Execute(
                "ConversionHistory_Package.DeleteConversionHistory",
                p,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
