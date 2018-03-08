namespace Rex.SqlServer.Store
{
    public static class SchemaQueries
    {
        private static string _referentialConstraintQuery = "SELECT RC.CONSTRAINT_NAME FK_Name, KF.TABLE_SCHEMA FK_Schema"
                            + ", KF.TABLE_NAME FK_Table, KF.COLUMN_NAME FK_Column"
                            + ", RC.UNIQUE_CONSTRAINT_NAME PK_Name, KP.TABLE_SCHEMA PK_Schema"
                            + ", KP.TABLE_NAME PK_Table, KP.COLUMN_NAME PK_Column"
                            + ", RC.MATCH_OPTION MatchOption, RC.UPDATE_RULE UpdateRule"
                            + ", RC.DELETE_RULE DeleteRule FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC"
                            + "  JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KF ON RC.CONSTRAINT_NAME = KF.CONSTRAINT_NAME"
                            + " JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KP ON RC.UNIQUE_CONSTRAINT_NAME = KP.CONSTRAINT_NAME;";

        public static string ReferentialConstraintQuery { get { return _referentialConstraintQuery; } }

    }
}
