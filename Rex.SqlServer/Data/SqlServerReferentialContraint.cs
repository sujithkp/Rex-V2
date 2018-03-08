using Rex.Common.Data;

namespace Rex.SqlServer.Data
{
    public class SqlServerReferentialContraint : ReferentialConstraint
    {
        internal SqlServerReferentialContraint(string fkName)
            : base(fkName)
        {
            
        }
    }
}
