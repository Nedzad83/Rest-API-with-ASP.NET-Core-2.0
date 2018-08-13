using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RestAPI.Data.Helpers
{
    public class DataNamesMapper<TEntity> where TEntity : class//, new()
    {
        public TEntity Map(DataRow row)
        {
            var columnNames = row.Table.Columns
                                   .Cast<DataColumn>()
                                   .Select(x => x.ColumnName)
                                   .ToList();

            var properties = (typeof(TEntity)).GetProperties().ToList();

            TEntity entity = Activator.CreateInstance<TEntity>();
            foreach (var prop in properties)
            {
                PropertyMapHelper.Map(row, prop, entity);
            }

            return entity;
        }

        public IEnumerable<TEntity> Map(DataTable table)
        {
            var columnNames = table.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();

            var properties = (typeof(TEntity)).GetProperties().ToList();

            List<TEntity> entities = new List<TEntity>();
            foreach (DataRow row in table.Rows)
            {
                TEntity entity = Activator.CreateInstance<TEntity>();
                foreach (var prop in properties)
                {
                    PropertyMapHelper.Map(row, prop, entity);
                }
                entities.Add(entity);
            }
            return entities;
        }
    }
}
