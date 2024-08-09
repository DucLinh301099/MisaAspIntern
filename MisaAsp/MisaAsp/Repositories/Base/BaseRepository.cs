﻿using Dapper;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace MisaAsp.Repositories.Base
{
    public interface IBaseRepository
    {
        Task<int> ExecuteAsync(string sql, object parameters = null);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null);
        Task<T> ExecuteScalarAsync<T>(string sql, object parameters = null);
        Task<T> ExecuteProcScalarAsync<T>(string procedureName, object parameters);
        Task<T> QuerySingleOrDefaultAsync<T>(string sql, object parameters = null);
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null);
    }

    public class BaseRepository : IBaseRepository
    {
        protected readonly IDbConnection _connection;

        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            try
            {
                _connection.Open();
                return await _connection.ExecuteAsync(sql, parameters);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
        {
            try
            {
                _connection.Open();
                return await _connection.QueryAsync<T>(sql, parameters);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }
        public async Task<IEnumerable<T>> QueryProcAsync<T>(string functionName, object parameters = null)
        {
            var sql = string.Empty;
            if (parameters != null)
            {
                var listParam = parameters.GetType().GetProperties().Select(p => "@" + p.Name).ToList();
                sql = $"SELECT * FROM {functionName}({string.Join(',', listParam)})";
            }
            else
            {
                sql = $"SELECT * FROM {functionName}()";
            }

            try
            {
                _connection.Open();
                return await _connection.QueryAsync<T>(sql, parameters);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }




        public async Task<T> ExecuteScalarAsync<T>(string sql, object parameters = null)
        {
            try
            {
                _connection.Open();
                return await _connection.ExecuteScalarAsync<T>(sql, parameters);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }

        public async Task<T> ExecuteProcScalarAsync<T>(string procedureName, object parameters)
        {
            var listParam = new List<string>();
            var sql = string.Empty;
            var listProp = parameters.GetType().GetProperties().ToList();
            foreach (var item in listProp)
            {
                listParam.Add($"@{item.Name}");
            }
            if (listParam.Count > 0)
            {
                sql = $"SELECT {procedureName}({string.Join(',', listParam)})";
            }
            else
            {
                sql = $"SELECT {procedureName}()";
            }

            try
            {
                _connection.Open();
                return await _connection.ExecuteScalarAsync<T>(sql, parameters);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }
        public async Task<T> ExecuteProcSingleScalarAsync<T>(string procedureName, object parameters)
        {
            var listParam = new List<string>();
            var sql = string.Empty;
            var listProp = parameters.GetType().GetProperties().ToList();

            foreach (var item in listProp)
            {
                listParam.Add($"@{item.Name}");
            }

            if (listParam.Count > 0)
            {
                sql = $"SELECT * FROM {procedureName}({string.Join(',', listParam)})";
            }
            else
            {
                sql = $"SELECT * FROM {procedureName}()";
            }

            try
            {
                _connection.Open();
                var result = await _connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
                return result;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }
        public async Task<IEnumerable<T>> ExecuteProcObScalarAsync<T>(string procedureName, object parameters)
        {
            var listParam = new List<string>();
            var sql = string.Empty;
            var listProp = parameters.GetType().GetProperties().ToList();

            foreach (var item in listProp)
            {
                listParam.Add($"@{item.Name}");
            }

            if (listParam.Count > 0)
            {
                sql = $"SELECT * FROM {procedureName}({string.Join(',', listParam)})";
            }
            else
            {
                sql = $"SELECT * FROM {procedureName}()";
            }

            try
            {
                _connection.Open();
                var result = await _connection.QueryAsync<T>(sql, parameters);
                return result;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object parameters = null)
        {
            try
            {
                _connection.Open();
                return await _connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null) // Thêm phương thức này
        {
            try
            {
                _connection.Open();
                return await _connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }
        public async Task<T> ExecuteProcQueryWithMappingAsync<T, U>(
    string procedureName,
    object parameters,
    Func<T, U, T> map,
    string splitOn = "Id")
        {
            var listParam = new List<string>();
            var sql = string.Empty;
            var listProp = parameters.GetType().GetProperties().ToList();

            foreach (var item in listProp)
            {
                listParam.Add($"@{item.Name}");
            }

            if (listParam.Count > 0)
            {
                sql = $"SELECT * FROM {procedureName}({string.Join(',', listParam)})";
            }
            else
            {
                sql = $"SELECT * FROM {procedureName}()";
            }

            try
            {
                _connection.Open();
                var result = await _connection.QueryAsync<T, U, T>(
                    sql,
                    map,
                    parameters,
                    splitOn: splitOn
                );
                return result.FirstOrDefault();
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }
    }
}
