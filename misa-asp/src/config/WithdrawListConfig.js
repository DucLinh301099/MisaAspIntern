import RowModel from "../models/RowModel";
import Api from "../api/apiConst";


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
      fieldName: "objectName",
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
  model: RowModel
};

export default withdrawListConfig;
