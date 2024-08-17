using MisaAsp.Models.Ulti;
using MisaAsp.Models.ViewModel;
using MisaAsp.Repositories;
using MisaAsp.Repositories.Base;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace MisaAsp.Services.Base
{
    public interface IBaseService
    {
        Task<PagingResult> GetPaging(PagingFilter pagingFilter);
    }
    public class BaseService: IBaseService
    {
        private IBaseRepository _baseRepository;
        public BaseService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task<PagingResult> GetPaging(PagingFilter pagingFilter)
        {
            var result = new PagingResult
            {
                TotalAmount = "0" // Gán giá trị mặc định là "0"
            };

            if (pagingFilter != null)
            {
                var whereQuery = GetWhereQuery(pagingFilter.Filters);
                var sortQuery = GetSortQuery(pagingFilter.Sort);
                var pagingQuery = GetPagingQuery(pagingFilter.CurrentPage, pagingFilter.ItemsPerPage);

                var paramGet = new Dictionary<string, object>
        {
            { "p_view", pagingFilter.View },
            { "p_where", whereQuery },
            { "p_sort", sortQuery },
            { "p_paging", pagingQuery }
        };

                var resultData = await _baseRepository.ExecuteProcReturnMultiAsync("get_paging_filter", paramGet);

                if (resultData != null && resultData.Count > 1)
                {
                    if (resultData[0] != null && resultData[0].Count > 0)
                    {
                        result.PageData = resultData[0];
                    }

                    if (resultData[1] != null && resultData[1].Count > 0)
                    {
                        var countItem = resultData[1].FirstOrDefault() as IDictionary<string, object>;
                        if (countItem != null && countItem.TryGetValue("count", out var countValue))
                        {
                            if (int.TryParse(countValue.ToString(), out int count))
                            {
                                result.Total = count ;
                            }
                        }

                        if (resultData.Count > 2 && resultData[2] != null && resultData[2].Count > 0)
                        {
                            var totalAmountItem = resultData[2].FirstOrDefault() as IDictionary<string, object>;
                            if (totalAmountItem != null && totalAmountItem.TryGetValue("sum", out var totalAmountValue))
                            {
                                result.TotalAmount = totalAmountValue?.ToString() ?? "0";
                            }
                        }
                    }
                }
            }

            return result;
        }






        #region Where
        /// <summary>
        /// Hàm đệ qui build lệnh where query
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        public string BuildWhereQuery(Filter filterObject, StringBuilder whereQuery = null)
        {
            if(whereQuery == null)
            {
                whereQuery = new StringBuilder();
            }
            var filters = filterObject.Filters;
            if (filters != null && filters.Any())
            {
                for (int i = 0; i < filters.Count; i++)
                {
                    var item = filters[i];
                    if(item is JsonElement jsonElement)
                    {
                        switch (jsonElement.ValueKind)
                        {
                            case JsonValueKind.Array:
                                // nếu type là array thì thêm ngoặc và quét tiếp
                                var arrayAsList = new List<object>();
                                foreach (var element in jsonElement.EnumerateArray())
                                {
                                    arrayAsList.Add(element);
                                }

                                BuildWhereQuery(new Filter(arrayAsList, filterObject.ParamIndex), whereQuery);

                                break;
                            case JsonValueKind.Object:
                                whereQuery.Append(" (");
                                foreach (var objectFilter in jsonElement.EnumerateObject())
                                {
                                    if (objectFilter.Name == "value")
                                    {
                                        var valueValid = ValidateSqlInput(objectFilter.Value.ToString());
                                        //parameter.Add(valueValid);
                                        whereQuery.Append($"'{valueValid}'");
                                        filterObject.ParamIndex++;
                                    }
                                    else
                                    {
                                        whereQuery.Append(objectFilter.Value);
                                    }
                                }
                                whereQuery.Append(") ");
                                break;
                            case JsonValueKind.String:
                                whereQuery.Append(item.ToString() + " ");
                                break;
                        }
                    }
                }
            }
            return whereQuery.ToString();
        }

        public string GetWhereQuery(List<object> filters)
        {
            if (filters == null || filters.Count == 0)
            {
                return "";
            }
            else
            {
                var queryWhere = BuildWhereQuery(new Filter(filters));
                return @$"where {queryWhere}";
            }
        }
        #endregion

        #region Sort

        public string BuildSortQuery(List<SortVM> sort)
        {
            var sortQuery = new List<string>();
            if(sort != null && sort.Any())
            {
                foreach (var item in sort)
                {
                    var itemSort = $"{item.Property} ";
                    if (item.Desc)
                    {
                        itemSort += "desc";
                    }
                    else
                    {
                        itemSort += "asc";
                    }
                    sortQuery.Add(itemSort);
                }
            }
            return string.Join(",", sortQuery);
        }

        public string GetSortQuery(List<SortVM> sort)
        {
            if (sort == null || sort.Count == 0)
            {
                return "";
            }
            else
            {
                var queryWhere = BuildSortQuery(sort);
                return @$"order by {queryWhere}";
            }
            
        }

        #endregion

        #region Paging

        public string GetPagingQuery(int currentPage, int itemsPerPage)
        {
            var recordStart = (currentPage - 1) * itemsPerPage + 1;
            var recordStartValid = ValidateSqlInput(recordStart.ToString());
            var itemsPerPageValid = ValidateSqlInput(itemsPerPage.ToString());
            //parameter.Add(valueValid);
            return @$"offset {recordStartValid} limit {itemsPerPageValid}";
        }

        #endregion

        public static string ValidateSqlInput(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input; // Nếu chuỗi rỗng hoặc null, không cần xử lý
            }

            // Các ký tự nguy hiểm cần tránh
            string[] blacklist = { "--", ";", "/*", "*/", "@@", "'", "\"", "xp_" };

            // Chuyển chuỗi về dạng chữ thường để dễ kiểm tra
            string lowerInput = input.ToLower();

            foreach (var item in blacklist)
            {
                if (lowerInput.Contains(item))
                {
                    throw new ArgumentException("Input contains potentially dangerous characters.");
                }
            }

            // Loại bỏ các dấu nháy đơn nếu có
            return input.Replace("'", "''");
        }

    }
}
