// apiConfig.js
import accApi from "../api/apiConst";
import RowModel from "../models/RowModel";
let paymentConfig = {
  comboxConfig: {
    bankExpense: {
      endpoint: accApi.bankExpense.url,
      method: accApi.bankExpense.method,
      columnConfig: [
        {
          columnName: "Số tài khoản",
          fieldName: "accountNumber",
          isValue: true,
          isDisplay: true,
        },
        {
          columnName: "Tên ngân hàng",
          fieldName: "bankName",
          isDisplaySecond: true,
          isValue: true,
        },
        { columnName: "Chi nhánh", fieldName: "branch" },
      ],
      params: {
        roleId: 1
      }
    },
    bankReceive: {
      endpoint: accApi.bankExpense.url,
      method: accApi.bankExpense.method,
      columnConfig: [
        {
          columnName: "Số tài khoản",
          fieldName: "accountNumber",
          isDisplay: true,
          isValue: true,
        },
        { columnName: "Tên ngân hàng", fieldName: "bankName" },
        {
          columnName: "Chi nhánh",
          fieldName: "branch",
          isDisplaySecond: true,
          isValue: true,
        },
      ],
      params: {
        roleId: 2
      }
    },
    employee: {
      endpoint: accApi.employee.url,
      method: accApi.employee.method,
      columnConfig: [
        { columnName: 'Mã nhân viên', fieldName: 'employeeCode', isDisplay: true, isValue: true, },
        { columnName: 'Tên nhân viên', fieldName: 'employeeName' },
        { columnName: "Đơn vị", fieldName: "department" },
        { columnName: "ĐT di động", fieldName: "mobilePhone" },
      ],
    },
    customer: {
      endpoint: 'Customer/customer',
      method: 'get',
      roleId: null,
      columnConfig: [
        { columnName: "Đối tượng", fieldName: "objectId", isDisplayGrid: true, isValue: true, },
        {
          columnName: "Tên đối tượng",
          fieldName: "objectName",
          isDisplaySecondGrid: true,
          isDisplay: true,
          isValue: true,
        },
        { columnName: "Mã số thuế", fieldName: "taxCode" },
        {
          columnName: "Địa chỉ",
          fieldName: "address",
          isDisplaySecond: true,
          isValue: true,
        },
        { columnName: "Điện thoại", fieldName: "phoneNumber" },
      ],
    },
    debitAccount: {
      endpoint: null,
      method: null,
      options: [
        { accountNumber: "1111", accountName: "Tiền Việt Nam" },
        { accountNumber: "1112", accountName: "Ngoại Tệ" },
        { accountNumber: "121", accountName: "Chứng khoán" },
        { accountNumber: "1281", accountName: "Tiền gửi có kỳ hạn" },
        { accountNumber: "141", accountName: "tạm ứng" },
        { accountNumber: "153", accountName: "Công cụ, dung cụ" },
        { accountNumber: "154", accountName: "Chi phí sản xuất" },
      ],
      columnConfig: [
        {
          columnName: "Số tài khoản",
          fieldName: "accountNumber",
          isDisplayGrid: true,
          isValue: true,
        },
        { columnName: "Tên tài khoản", fieldName: "accountName" },
      ],
    },
    creditAccount: {
      options: [
        { accountNumber: "1121", accountName: "Tiền Việt Nam" },
        { accountNumber: "1122", accountName: "Ngoại Tệ" },
      ],
      columnConfig: [
        {
          columnName: "Số tài khoản",
          fieldName: "accountNumber",
          isDisplayGrid: true,
          isValue: true,
        },
        { columnName: "Tên tài khoản", fieldName: "accountName" },
      ],
    },
  },
};

paymentConfig.gridConfig = {
  url: '',
  columnConfig: [
    { columnName: "Diễn giải", fieldName: "description", dataType: "text" },
    {
      columnName: "TK Nợ",
      fieldName: "debitAccount",
      dataType: "dropdown",
      dropDownConfig: paymentConfig.comboxConfig.debitAccount
    },
    {
      columnName: "TK Có",
      fieldName: "creditAccount",
      dataType: "dropdown",
      dropDownConfig: paymentConfig.comboxConfig.creditAccount
    },
    { columnName: "Số tiền", fieldName: "amount", dataType: "currency" },
    {
      columnName: "Đối tượng",
      fieldName: "customer",
      dataType: "dropdown",
      dropDownConfig: paymentConfig.comboxConfig.customer
    },
    {
      columnName: "Tên đối tượng",
      fieldName: "objectName",
      dataType: "text",
        
    },
  ],
  model: RowModel
};


export default paymentConfig;
