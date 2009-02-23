using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ClearCanvas.Enterprise.Hibernate.Ddl.Model
{
    [DataContract]
    public class TableInfo : ElementInfo
    {
        public TableInfo()
        {

        }

        public TableInfo(string name, string schema, List<ColumnInfo> columns, ConstraintInfo primaryKey, List<IndexInfo> indexes, List<ForeignKeyInfo> foreignKeys, List<ConstraintInfo> uniqueKeys)
        {
            Name = name;
            Schema = schema;
            Columns = columns;
            PrimaryKey = primaryKey;
            Indexes = indexes;
            ForeignKeys = foreignKeys;
            UniqueKeys = uniqueKeys;
        }

        [DataMember]
        public string Name;
        [DataMember]
        public string Schema;
        [DataMember]
        public List<ColumnInfo> Columns;
        [DataMember]
        public ConstraintInfo PrimaryKey;
        [DataMember]
        public List<IndexInfo> Indexes;
        [DataMember]
        public List<ForeignKeyInfo> ForeignKeys;
        [DataMember]
        public List<ConstraintInfo> UniqueKeys;

        public override bool IsSame(ElementInfo other)
        {
            TableInfo that = other as TableInfo;
            return that != null && that.Name == this.Name;
        }

        public override bool IsIdentical(ElementInfo other)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override string SortKey
        {
            get { return Name; }
        }
    }
}
