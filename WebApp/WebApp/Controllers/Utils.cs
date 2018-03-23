using Dapper;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Utils {
    public static class Utils {
        /// <summary>
        /// Metodo que retorna la ubicación del archivo javascript basado en el nombre de la vista donde se esta invocando
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns>path de archivo javascript</returns>
        public static string GetJsFilePath(string virtualPath) {
            var path = (string)(Path.GetDirectoryName(virtualPath).Replace("Views", "Scripts") + "\\" + Path.GetFileNameWithoutExtension(virtualPath) + ".js").Replace("\\", "/");
            path = string.Format("{0}", path);
            return path;
        }

        public static DynamicParameters GetFilters<T>(DataSourceRequest request) where T : new() {

            var filters = request.Filters;

            // Define objeto que almacenara parametros del filtro basado en generico
            var dynamicParameters = new DynamicParameters();

            // Nullea todos los parametros del tipo de objeto generico
            var getProperties = (new T()).GetType().GetProperties();
            foreach (var info in getProperties) {
                // Obtinene el tipo de propiedad       
                var pType = info.PropertyType;

                // Evalua el tipo "cadena de texto"
                if (pType == typeof(string)) {
                    dynamicParameters.Add(info.Name, null, DbType.String);
                }

                // Evalua el tipo "numerico"
                else if (pType == typeof(int) ||
                         pType == typeof(int?) ||
                         pType == typeof(double) ||
                         pType == typeof(double?) ||
                         pType == typeof(decimal) ||
                         pType == typeof(decimal?)) {
                    dynamicParameters.Add(info.Name, null, DbType.Double);
                }

                // Evalua el tipo "fecha"
                else if (pType == typeof(DateTime?) || pType == typeof(DateTime)) {
                    dynamicParameters.Add(info.Name + "_From", null, DbType.DateTime);
                    dynamicParameters.Add(info.Name + "_To", null, DbType.DateTime);
                }

                // Evalua cualquiera diferente a los anteriores
                else {
                    dynamicParameters.Add(info.Name, null, DbType.Object);
                }
            }

            // Acceso a metodo recursivo para especificacion de parametros
            ModifyFilters(filters, dynamicParameters);

            // Adiciona variables para paginación
            dynamicParameters.Add("Page", request.Page);
            dynamicParameters.Add("PageSize", request.PageSize);

            // Retorna parametros con los valores usados para el filtro
            return dynamicParameters;
        }

        private static void ModifyFilters(IEnumerable<IFilterDescriptor> filters, DynamicParameters dynamicParameters) {
            if (filters.Any()) {
                foreach (var filter in filters) {

                    if (filter is CompositeFilterDescriptor) {
                        ModifyFilters(((CompositeFilterDescriptor)filter).FilterDescriptors, dynamicParameters);
                    } else {
                        var descriptor = filter as FilterDescriptor;
                        object value = descriptor.Value;

                        var filterName = descriptor.Member.ToString();
                        var typeName = value.GetType().Name;
                        var filterOperator = descriptor.Operator;

                        switch (filterOperator) {
                            case FilterOperator.IsEqualTo:
                                switch (typeName) {
                                    case "Double": //Equivale a Int32
                                        dynamicParameters.Add(filterName, value, DbType.Double);
                                        break;
                                }
                                break;


                            case FilterOperator.StartsWith:
                                switch (typeName) {
                                    case "String":
                                        value = value + "%";
                                        dynamicParameters.Add(filterName, value, DbType.String);
                                        break;
                                }
                                break;

                            case FilterOperator.IsLessThanOrEqualTo:
                                switch (typeName) {
                                    case "DateTime":
                                        filterName = filterName + "_To";
                                        // En caso de no especificar la hora ni minutos le suma 24 horas para tomar el dia completo.
                                        value = ((DateTime?)value).Value.Hour == 0 &&
                                                ((DateTime?)value).Value.Minute == 0 ? (DateTime?)value + new TimeSpan(23, 59, 0) : value;
                                        dynamicParameters.Add(filterName, value, DbType.DateTime);
                                        break;
                                }
                                break;

                            case FilterOperator.IsGreaterThanOrEqualTo:
                                switch (typeName) {
                                    case "DateTime":
                                        filterName = filterName + "_From";
                                        dynamicParameters.Add(filterName, value, DbType.DateTime);
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Metodo que retorna objeto dinamico basado en filtros de kendo "DataSourceRequest"
        /// </summary>
        /// <typeparam name="T">tipo de dato en que se basara la conversion</typeparam>
        /// <param name="request">filtros de entrada generados por kendo</param>
        /// <returns></returns>
        public static DynamicParameters GetDsFilters<T>(DataSourceRequest request) where T : new() {
            // Define objeto que almacenara parametros del filtro basado en generico
            var parameters = new DynamicParameters();

            // Adiciona variables para la paginación
            parameters.Add("Page", request.Page);
            parameters.Add("PageSize", request.PageSize);

            // Adiciona variables para ordenamiento especificado una sola columna y dirección
            if (request.Sorts.Any()) {
                parameters.Add("SortCol", request.Sorts.FirstOrDefault().Member);
                parameters.Add("SortDir", request.Sorts.FirstOrDefault().SortDirection == Kendo.Mvc.ListSortDirection.Ascending ? "ASC" :
                                          request.Sorts.FirstOrDefault().SortDirection == Kendo.Mvc.ListSortDirection.Descending ? "DESC" : null);
            } else {
                parameters.Add("SortCol", null);
                parameters.Add("SortDir", null);
            }

            // Nullea todos los parametros del tipo de objeto generico
            var getProperties = (new T()).GetType().GetProperties();

            foreach (var info in getProperties) {
                // Obtinene el tipo de propiedad       
                var pType = info.PropertyType;

                // Evalua el tipo "fecha"
                if (pType == typeof(DateTime?) || pType == typeof(DateTime)) {
                    parameters.Add(info.Name + "_From", null);
                    parameters.Add(info.Name + "_To", null);
                }

                // Evalua cualquiera tipo de dato diferente a los anteriores
                else {
                    parameters.Add(info.Name, null);
                }
            }

            // Metodo recursivo que carga los datos encontrados en los filtros
            PopulateFilters(request.Filters, parameters);

            // Retorna parametros con los valores usados para el filtro
            return parameters;
        }



        /// <summary>
        ///  Metodo recursivo que carga los datos encontrados en los filtros
        /// </summary>
        /// <param name="filters">filtros de kendo</param>
        /// <param name="parameters">parametros de objeto base</param>
        private static void PopulateFilters(IEnumerable<IFilterDescriptor> filters, DynamicParameters parameters) {
            if (filters.Any()) {
                foreach (var filter in filters) {
                    if (filter is CompositeFilterDescriptor) {
                        PopulateFilters(((CompositeFilterDescriptor)filter).FilterDescriptors, parameters);
                    } else {
                        var descriptor = filter as FilterDescriptor;
                        object value = descriptor.Value;
                        var filterName = descriptor.Member.ToString();
                        var typeName = value.GetType().Name;
                        var filterOp = descriptor.Operator;
                        
                        switch (filterOp) {
                            case FilterOperator.IsEqualTo:
                                switch (typeName) {
                                    case "Double": //Equivale a Int32
                                        parameters.Add(filterName, value);
                                        break;
                                        
                                    case "String":
                                        parameters.Add(filterName, value);
                                        break;
                                }

                                break;
                                
                            case FilterOperator.StartsWith:
                                switch (typeName) {
                                    case "String":
                                        value = (value as string).ToUpper().Trim() + "%";
                                        parameters.Add(filterName, value);
                                        break;
                                }
                                break;
                                
                            case FilterOperator.Contains:
                                switch (typeName) {
                                    case "String":
                                        value = "%" + (value as string).ToUpper().Trim() + "%";
                                        parameters.Add(filterName, value);
                                        break;
                                }
                                break;
                                
                            case FilterOperator.IsLessThanOrEqualTo:
                            case FilterOperator.IsLessThan:
                                switch (typeName) {
                                    case "DateTime":
                                        filterName = filterName + "_To";
                                        // En caso de no especificar la hora ni minutos le suma 24 horas para tomar el dia completo.
                                        value = ((DateTime?)value).Value.Hour == 0 &&
                                                ((DateTime?)value).Value.Minute == 0 ? (DateTime?)value + new TimeSpan(23, 59, 0) : value;
                                        parameters.Add(filterName, value);
                                        break;
                                }
                                break;

                            case FilterOperator.IsGreaterThanOrEqualTo:
                            case FilterOperator.IsGreaterThan:
                                switch (typeName) {
                                    case "DateTime":
                                        filterName = filterName + "_From";
                                        parameters.Add(filterName, value);
                                        break;
                                }
                                break;

                            // En caso de no corresponder a ninguno
                            default:
                                switch (typeName) {
                                    case "String":
                                        value = (value as string).ToUpper() + "%";
                                        parameters.Add(filterName, value);
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
        }
        
    }
}
