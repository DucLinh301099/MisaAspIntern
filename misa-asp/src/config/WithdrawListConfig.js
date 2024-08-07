let withdrawListConfig = {
  url: '',
  columnConfig: [
    { columnName: "Ngày hạch toán", fieldName: "accountingDate", dataType: "date" },
    { columnName: "Ngày chứng từ", fieldName: "documentDate", dataType: "date" },
    { columnName: "Số chứng từ", fieldName: "documentNumber", dataType: "text" },
    { columnName: "Diễn giải", fieldName: "description", dataType: "text" },
    { columnName: "Số tiền", fieldName: "amount", dataType: "currency" },
    {
      columnName: "Đối tượng",
      fieldName: "objectId",
      dataType: "dropdown",
      dropDownConfig: paymentConfig.comboxConfig.customer
    },
    { columnName: "Số tài khoản NH", fieldName: "bankAccountNumber", dataType: "text" },
    { columnName: "Lý do thu/chi", fieldName: "reason", dataType: "text" },
    { columnName: "Loại chứng từ", fieldName: "documentType", dataType: "text" },
    {
      columnName: "Chức năng",
      fieldName: "actions",
      dataType: "action",
      
    }
  ],
  model: RowModel
};

export default withdrawListConfig;
