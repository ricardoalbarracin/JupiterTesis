using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Core.Models.Utils
{
    [Table("information_schema.columns")]
    public class ColumnTable
    {
        public string TableCatalog { get; set; }

        public string TableSchema { get; set; }

        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public string IsNullable { get; set; }

        public string DataType { get; set; }

        public int CharacterMaximumLength { get; set; }

        public string PropertyWhitAnnotations
        {
            get
            {
                return GetStringPropertyWhitAnnotations();
            }
        }

        public string GetStringPropertyWhitAnnotations()
        {
            return $"{GetStringLength()}{GetStringRequired()}{GetStringDisplayName()}{GetStringProperty()}";
        }
        public string GetStringProperty()
        {
            var type = "";
            switch (DataType)
            {
                case "boolean":
                    type = "bool";
                    break;
                case "smallint":
                    type = "short";
                    break;
                case "integer":
                    type = "int";
                    break;
                case "bigint":
                    type = "long";
                    break;
                case "real":
                    type = "float";
                    break;
                case "double precision":
                    type = "double";
                    break;
                case "numeric":
                    type = "decimal";
                    break;
                case "money":
                    type = "decimal";
                    break;
                case "text":
                    type = "string";
                    break;
                case "character varying":
                    type = "string";
                    break;
                case "character":
                    type = "string";
                    break;
                case "citext":
                    type = "string";
                    break;
                case "json":
                    type = "string";
                    break;
                case "jsonb":
                    type = "string";
                    break;
                case "xml":
                    type = "string";
                    break;
                case "date":
                    type = "DateTime";
                    break;
                default:
                    throw new Exception("tipo no encontrado");

            }
            return $"public {type} {GetStringPropertyName()} {{ get; set;}}{Environment.NewLine}{Environment.NewLine}";
        }

        public string GetStringPropertyName()
        {

            var words = ColumnName.Split(new[] { '-', '_' }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(word => word.Substring(0, 1).ToUpper() +
                                         word.Substring(1).ToLower());

            var result = String.Concat(words);
            return result;
        }

        public string GetStringTableName()
        {

            var words = TableName.Split(new[] { '-', '_' }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(word => word.Substring(0, 1).ToUpper() +
                                         word.Substring(1).ToLower());

            var result = String.Concat(words);
            return result;
        }

        public string GetStringSchemaName()
        {

            var words = TableSchema.Split(new[] { '-', '_' }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(word => word.Substring(0, 1).ToUpper() +
                                         word.Substring(1).ToLower());

            var result = String.Concat(words);
            return result;
        }

        public string GetStringRequired()
        {

            if(String.Equals(IsNullable, "NO"))
            {
                return $"[Required(ErrorMessage = \"REQUIRED_ERROR_{GetStringPropertyName().ToUpper()}\")]{Environment.NewLine}";
            }
            return "";

        }

        public string GetStringLength()
        {

            if (CharacterMaximumLength > 0)
            {
                return $"[MaxLength({CharacterMaximumLength}, ErrorMessage = \"LENGTH_ERROR_{GetStringPropertyName().ToUpper()}\")]{Environment.NewLine}";
            }
            return "";

        }

        public string GetStringDisplayName()
        {
            return $"[Display(Name = \"PROPERTY_NAME_{GetStringPropertyName().ToUpper()}\")]{Environment.NewLine}";
        }

    }
}
