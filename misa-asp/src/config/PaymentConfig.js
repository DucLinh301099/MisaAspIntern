// apiConfig.js
import Api from "../api/apiConst";

import RowModel from "../models/RowModel";
let paymentConfig = {
  comboxConfig: {
    bankExpense: {
      endpoint: Api.bankExpense.url,
      method: Api.bankExpense.method,
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
        typeOfBank: 1
      }
    },
    bankReceive: {
      endpoint: Api.bankExpense.url,
      method: Api.bankExpense.method,
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
        typeOfBank: 2
      }
    },
    employee: {
      endpoint: Api.employee.url,
      method: Api.employee.method,
      columnConfig: [
        { columnName: 'Mã nhân viên', fieldName: 'employeeCode' },
        { columnName: 'Tên nhân viên', fieldName: 'employeeName',isDisplay: true, isValue: true, },
        { columnName: "Đơn vị", fieldName: "department" },
        { columnName: "ĐT di động", fieldName: "phoneNumber" },
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
        { debitAccountNumber: "1111", accountName: "Tiền Việt Nam" },
        { debitAccountNumber: "1112", accountName: "Ngoại Tệ" },
        { debitAccountNumber: "121", accountName: "Chứng khoán" },
        { debitAccountNumber: "1281", accountName: "Tiền gửi có kỳ hạn" },
        { debitAccountNumber: "141", accountName: "tạm ứng" },
        { debitAccountNumber: "153", accountName: "Công cụ, dung cụ" },
        { debitAccountNumber: "154", accountName: "Chi phí sản xuất" },
      ],
      columnConfig: [
        {
          columnName: "Số tài khoản",
          fieldName: "debitAccountNumber",
          isDisplayGrid: true,
          isValue: true,
        },
        { columnName: "Tên tài khoản", fieldName: "accountName" },
      ],
    },
    creditAccount: {
      options: [
        { creditAccountNumber: "1121", accountName: "Tiền Việt Nam" },
        { creditAccountNumber: "1122", accountName: "Ngoại Tệ" },
      ],
      columnConfig: [
        {
          columnName: "Số tài khoản",
          fieldName: "creditAccountNumber",
          isDisplayGrid: true,
          isValue: true,
        },
        { columnName: "Tên tài khoản", fieldName: "accountName" },
      ],
    },
    model: RowModel
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
      fieldName: "objectId",
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
