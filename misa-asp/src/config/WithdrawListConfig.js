import RowModel from "../models/RowModel";
import Api from "../api/apiConst";

const currentYear = new Date().getFullYear();
const today = new Date().toISOString().split("T")[0];
const currentMonth = new Date().getMonth();  // Lấy tháng hiện tại (0-11).

let withdrawListConfig = {
    endpoint: Api.withdrawList.url,
    method: Api.withdrawList.method,
  columnConfig: [
    { columnName: "Ngày hạch toán", fieldName: "accountingDate", dataType: "date" },
    { columnName: "Ngày chứng từ", fieldName: "documentDate", dataType: "date" },
    { columnName: "Số chứng từ", fieldName: "documentNumber", dataType: "text" },
    { columnName: "Diễn giải", fieldName: "billContent", dataType: "text" },
    { columnName: "Số tiền", fieldName: "totalAmount", dataType: "currency" },
    {
      columnName: "Đối tượng",
      fieldName: "customerName",
      dataType: "text",
      
    },
    { columnName: "Số tài khoản NH", fieldName: "accountNumber", dataType: "text" },
    { columnName: "Lý do thu/chi", fieldName: "voucherType", dataType: "text" },
    { columnName: "Loại chứng từ", fieldName: "paymentMethod", dataType: "text" },
    {
      columnName: "Chức năng",
      fieldName: "actions",
      dataType: "action",
      
    }
  ],
  model: RowModel,
  timeOptionsConfig: {
    "Đầu năm đến hiện tại": {
      startDate: `${currentYear}-01-01`,
      endDate: today
    },
    "6 tháng đầu năm": {
      startDate: `${currentYear}-01-01`,
      endDate: `${currentYear}-06-30`
    },
    "6 tháng cuối năm": {
      startDate: `${currentYear}-07-01`,
      endDate: `${currentYear}-12-31`
    },
    "Tháng này": {
      startDate: `${currentYear}-${String(currentMonth + 1).padStart(2, '0')}-01`, // Ngày đầu tiên của tháng
      endDate: `${currentYear}-${String(currentMonth + 1).padStart(2, '0')}-${new Date(currentYear, currentMonth + 1, 0).getDate()}` 
    },
    "Năm nay": {
      startDate: `${currentYear}-01-01`,
      endDate: `${currentYear}-12-31`
    },
    "Hôm nay": {
      startDate: today,
      endDate: today
    }
  }
};

// Hàm xác định số ngày trong tháng
function getLastDayOfMonth(year, month) {
  return new Date(year, month + 1, 0).getDate(); // Lấy ngày cuối cùng của tháng (1-based)
}

// Thêm logic cho các tháng từ tháng 1 đến tháng 12
for (let month = 0; month < 12; month++) {
  const monthName = new Date(currentYear, month).toLocaleString('default', { month: 'long' });
  const lastDay = getLastDayOfMonth(currentYear, month);
  
  withdrawListConfig.timeOptionsConfig[`Tháng ${month + 1}`] = {
    startDate: `${currentYear}-${String(month + 1).padStart(2, '0')}-01`,
    endDate: `${currentYear}-${String(month + 1).padStart(2, '0')}-${lastDay}`
  };
}

export default withdrawListConfig;
