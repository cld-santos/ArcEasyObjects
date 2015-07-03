using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ArcEasyObjects.Helper
{
    public static class GDBCursor
    {
        public static ESRI.ArcGIS.Geodatabase.ICursor obterCursor(ESRI.ArcGIS.Geodatabase.IFeatureWorkspace featureWorkspace, string tables, string subFields, string whereClause)
        {
            ESRI.ArcGIS.Geodatabase.IQueryDef queryDef = featureWorkspace.CreateQueryDef();
            queryDef.Tables = tables;
            queryDef.SubFields = subFields;
            queryDef.WhereClause = whereClause;
            return queryDef.Evaluate();
        }

        public static ESRI.ArcGIS.Geodatabase.ICursor obterCursor(ESRI.ArcGIS.Geodatabase.IFeatureWorkspace featureWorkspace, string tables, string subFields, string whereClause, string postFixClause)
        {
            ESRI.ArcGIS.Geodatabase.IQueryDef2 queryDef = (ESRI.ArcGIS.Geodatabase.IQueryDef2)featureWorkspace.CreateQueryDef();
            queryDef.Tables = tables;
            queryDef.SubFields = subFields;
            queryDef.WhereClause = whereClause;
            queryDef.PostfixClause = postFixClause;
            return queryDef.Evaluate();
        }

        public static ESRI.ArcGIS.Geodatabase.ICursor obterCursor(ESRI.ArcGIS.Geodatabase.ITable table, string subFields, string whereClause, string order)
        {
            ESRI.ArcGIS.Geodatabase.IQueryFilter queryFilter = new ESRI.ArcGIS.Geodatabase.QueryFilterClass();
            queryFilter.SubFields = subFields;
            queryFilter.WhereClause = whereClause;

            ESRI.ArcGIS.Geodatabase.IQueryFilterDefinition queryFilterDef = (ESRI.ArcGIS.Geodatabase.IQueryFilterDefinition)queryFilter;
            queryFilterDef.PostfixClause = order;

            return table.Search(queryFilter, true);
        }
    }
}
