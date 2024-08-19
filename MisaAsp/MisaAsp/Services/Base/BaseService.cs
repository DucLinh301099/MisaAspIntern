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
using System.Text.RegularExpressions;

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
        // Hàm GetPaging: Thực hiện chức năng phân trang và lấy dữ liệu từ cơ sở dữ liệu
        // dựa trên các tiêu chí lọc, sắp xếp, và phân trang.
        public async Task<PagingResult> GetPaging(PagingFilter pagingFilter)
        {
            var result = new PagingResult
            {
                TotalAmount = "0" // Gán giá trị mặc định là "0"
            };

            if (pagingFilter != null)
            {
                // Xây dựng câu lệnh where từ các tiêu chí lọc.
                var whereQuery = GetWhereQuery(pagingFilter.Filters);

                // Xây dựng câu lệnh sort từ các tiêu chí sắp xếp.
                var sortQuery = GetSortQuery(pagingFilter.Sort);

                // Xây dựng câu lệnh paging từ thông tin trang hiện tại và số lượng mục trên mỗi trang.
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
                    // Lấy dữ liệu trang (PageData) từ kết quả đầu tiên (resultData[0]).
                    if (resultData[0] != null && resultData[0].Count > 0)
                    {
                        result.PageData = resultData[0];
                    }
                    if (resultData[1] != null && resultData[1].Count > 0)
                    {
                        var countItem = resultData[1].FirstOrDefault() as IDictionary<string, object>;
                        // Lấy tổng số lượng bản ghi (Total) từ kết quả thứ hai (resultData[1]).

                        if (countItem != null && countItem.TryGetValue("count", out var countValue))
                        {
                            if (int.TryParse(countValue.ToString(), out int count))
                            {
                                result.Total = count ;
                            }
                        }
                        // Lấy tổng số tiền của các bản ghi (TotalAmount) từ kết quả thứ 3 (resultData[2]).

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
            if (whereQuery == null)
            {
                whereQuery = new StringBuilder();
            }
            var filters = filterObject.Filters; // Lấy danh sách các bộ lọc từ đối tượng filterObject.
            if (filters != null && filters.Any())
            {
                // Duyệt qua từng bộ lọc trong danh sách.
                for (int i = 0; i < filters.Count; i++)
                {
                    var item = filters[i];
                    if (item is JsonElement jsonElement)
                    {
                        switch (jsonElement.ValueKind)
                        {
                            case JsonValueKind.Array:
                                // Nếu là mảng, xử lý từng phần tử trong mảng
                                var arrayAsList = new List<object>();
                                foreach (var element in jsonElement.EnumerateArray())
                                {
                                    arrayAsList.Add(element);
                                }
                                // Đệ quy để xử lý mảng này.
                                BuildWhereQuery(new Filter(arrayAsList, filterObject.ParamIndex), whereQuery);
                                break;

                            case JsonValueKind.Object:
                                // Nếu là đối tượng, thêm ngoặc đơn vào câu lệnh where và xử lý từng thuộc tính.
                                whereQuery.Append(" (");
                                foreach (var objectFilter in jsonElement.EnumerateObject())
                                {
                                    if (objectFilter.Name == "value")
                                    {
                                        var valueValid = ValidateSqlInput(objectFilter.Value.ToString());
                                        whereQuery.Append($"'{valueValid}'");
                                        filterObject.ParamIndex++;
                                    }
                                    else
                                    {
                                        var value = objectFilter.Value.ToString();
                                        // Tìm và thay thế từ khóa LIKE đúng cách
                                        value = value.Replace("ILIKE", " ILIKE ");
                                        whereQuery.Append(value);
                                    }
                                }
                                whereQuery.Append(") ");
                                break;

                            case JsonValueKind.String:
                                // Nếu là chuỗi, thêm chuỗi vào câu lệnh where.
                                var stringValue = item.ToString();
                                // Tìm và thay thế từ khóa LIKE đúng cách
                                stringValue = stringValue.Replace("ILIKE", " ILIKE ");
                                whereQuery.Append(stringValue + " ");
                                break;
                        }
                    }
                }
            }
            return whereQuery.ToString();
        }



        // Hàm GetWhereQuery: Tạo câu lệnh where từ danh sách bộ lọc.
        public string GetWhereQuery(List<object> filters)
        {
            if (filters == null || filters.Count == 0)
            {
                return ""; // Trả về chuỗi rỗng nếu không có bộ lọc.
            }
            else
            {
                var queryWhere = BuildWhereQuery(new Filter(filters)); // Gọi hàm đệ quy để xây dựng câu lệnh where.
                return @$"where {queryWhere}"; // Trả về câu lệnh where hoàn chỉnh.
            }
        }
        #endregion

        #region Sort

        // Hàm BuildSortQuery: Tạo câu lệnh sắp xếp (order by) 
        public string BuildSortQuery(List<SortVM> sort)
        {
            var sortQuery = new List<string>();
            if(sort != null && sort.Any())
            {
                // Duyệt qua từng sort.
                foreach (var item in sort)
                {
                    var itemSort = $"{item.Property} ";
                    if (item.Desc)
                    {
                        itemSort += "desc"; // Thêm "desc" nếu sắp xếp giảm dần
                    }
                    else
                    {
                        itemSort += "asc"; // Thêm "asc" nếu sắp xếp tăng dần.
                    }
                    sortQuery.Add(itemSort);
                }
            }
            return string.Join(",", sortQuery);
        }

        // Hàm GetSortQuery: Tạo câu lệnh sắp xếp (order by) từ danh sách tiêu chí sắp xếp.
        public string GetSortQuery(List<SortVM> sort)
        {
            if (sort == null || sort.Count == 0)
            {
                return "";
            }
            else
            {
                var queryWhere = BuildSortQuery(sort); // Gọi hàm xây dựng câu lệnh sắp xếp.
                return @$"order by {queryWhere}";
            }
            
        }

        #endregion

        #region Paging

        // Hàm GetPagingQuery: Tạo câu lệnh phân trang (offset, limit) dựa trên trang hiện tại và số lượng mục trên mỗi trang.
        public string GetPagingQuery(int currentPage, int itemsPerPage)
        {
            var recordStart = (currentPage - 1) * itemsPerPage; // Tính toán bản ghi bắt đầu cho trang hiện tại.
            var recordStartValid = ValidateSqlInput(recordStart.ToString()); // Xác thực giá trị bản ghi bắt đầu.
            var itemsPerPageValid = ValidateSqlInput(itemsPerPage.ToString()); // Xác thực giá trị số lượng mục trên mỗi trang.
            //parameter.Add(valueValid);
            return @$"offset {recordStartValid} limit {itemsPerPageValid}";
        }

        #endregion

        // Hàm ValidateSqlInput: Xác thực đầu vào để ngăn chặn các tấn công SQL Injection.
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
