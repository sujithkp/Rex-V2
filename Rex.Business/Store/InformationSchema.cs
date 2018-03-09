﻿using Rex.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rex.Business.Store
{
    public class InformationSchema
    {
        private IList<ReferentialConstraint> _referentialConstraints;

        public void Initialize(IEnumerable<ReferentialConstraint> constraints)
        {
            _referentialConstraints = constraints as IList<ReferentialConstraint>;
        }

        /// <summary>
        /// Gets the name of tables referenced by the table with tableName
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public IList<TableColumnPair> GetReferencedTables(string tableName)
        {
            if (_referentialConstraints == null)
                throw new InvalidOperationException();

            var referencedTables = new List<TableColumnPair>();
            foreach (var constraint in _referentialConstraints)
            {
                referencedTables.AddRange(constraint.Participators.Where(x => x.Source.Table.Equals(tableName)));
            }

            return referencedTables;
        }

        /// <summary>
        /// Gets the name of tables which references table name.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public IList<TableColumnPair> GetReferencingTables(String tableName)
        {
            if (_referentialConstraints == null)
                throw new InvalidOperationException();

            var referencedTables = new List<TableColumnPair>();
            foreach (var constraint in _referentialConstraints)
            {
                referencedTables.AddRange(constraint.Participators.Where(x => x.Target.Table.Equals(tableName)));
            }

            return referencedTables;
        }

        public IList<String> GetPrimaryColumns(string table)
        {
            var primaryCols = new List<string>();
            foreach (var constraint in _referentialConstraints)
            {
                primaryCols.AddRange(constraint.Participators.Where(x => x.Target.Table.Equals(table)).Select(x => x.Target.Column));
            }
            return primaryCols.Distinct().ToList();
        }

        public IList<string> GetAllTables()
        {
            var tables = new List<string>();
            foreach (var constraints in _referentialConstraints)
            {
                tables.AddRange(constraints.Participators.Select(x => x.Source.Table).Distinct());
                tables.AddRange(constraints.Participators.Select(x => x.Target.Table).Distinct());
            }

            return tables.Distinct().ToList();
        }

        public IEnumerable<string> GetForeignKeyColumns(string table)
        {
            var foreignColumns = new List<string>();

            foreach (var constraint in _referentialConstraints)
                foreignColumns.AddRange(constraint.Participators.Where(x => x.Source.Table.Equals(table)).Select(x => x.Source.Column));

            return foreignColumns;

        }
    }
}
