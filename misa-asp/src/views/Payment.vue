<template>
  <section class="payment">
    <HeaderPayment
      :voucherType="voucherType"
      :paymentMethod="paymentMethod"
      @update:voucherType="voucherType = $event"
      @update:paymentMethod="paymentMethod = $event"
      class="header-payment"
    />
    <div class="input-information">
      <div class="input-information-right">
        <div class="account-input-container">
          <div class="account-input-wrapper">
            <MSCombobox
              label="Tài khoản chi"
              @update:selectedRow="updateSelectedRow('bankExpense', $event)"
              :config="paymentConfigCombo.comboxConfig.bankExpense"
              :value="payment.accountExpenseNumber"
              :ComponentAdd="createBankAccountComponent"
            />
            <MSInput
              class="second-input"
              :type="type"
              :value="payment.bankExpenseName"
            />
          </div>
          <div class="account-input-wrapper">
            <MSCombobox
              label="Đối Tượng"
              @update:selectedRow="updateSelectedRow('customer', $event)"
              :config="paymentConfigCombo.comboxConfig.customer"
              :value="payment.customerName"
              :ComponentAdd="createCustomerComponent"
            />
            <MSInput
              class="second-input"
              :type="type"
              :value="payment.customerAddress"
            />
          </div>
          <div class="account-input-wrapper">
            <MSCombobox
              v-if="!hideAccountReceive"
              label="Tài Khoản Nhận"
              :showButton="false"
              @update:selectedRow="updateSelectedRow('bankReceive', $event)"
              :value="payment.accountReceiveNumber"
              :config="paymentConfigCombo.comboxConfig.bankReceive"
            />
            <MSInput
              v-if="!hideAccountReceive"
              class="second-input"
              :type="type"
              :value="payment.bankReceiveName"
            />
          </div>
          <div v-if="!hideInformationInput" class="information-input-wrapper">
            <div class="input-container-info-1">
              <label for="so-cmnd">Số CMND</label>
              <MSInput
                :value="payment.cmndNumber"
                @input="updateValue('cmndNumber', $event.target.value)"
                type="text"
                class="base-input-info"
              />
            </div>
            <div class="input-container-info-2">
              <div class="input-field">
                <label for="ngay-cap">Ngày cấp</label>
                <MSInput
                  :value="payment.licenseDate"
                  @input="updateValue('licenseDate', $event.target.value)"
                  type="date"
                  class="base-input-info"
                />
              </div>
              <div class="input-field">
                <label for="noi-cap">Nơi cấp</label>
                <MSInput
                  type="text"
                  :value="payment.licenseAddress"
                  @input="updateValue('licenseAddress', $event.target.value)"
                  class="base-input-info"
                />
              </div>
            </div>
          </div>

          <div class="bill-content-input-wrapper">
            <label for="bill-content-input">Nội dung thanh toán</label>
            <div class="input-container">
              <MSInput
                :value="defaultBillContent"
                :validator="inputValidator"
                class="base-input-content"
                @input="updateBillContent"
              />
            </div>
          </div>
          <div class="account-input-wrapper">
            <MSCombobox
              v-if="!hideCreateEmployeeInput"
              label="Nhân viên"
              @update:selectedRow="updateSelectedRow('employee', $event)"
              :config="paymentConfigCombo.comboxConfig.employee"
              :value="payment.employeeCode"
              :ComponentAdd="createEmployeeComponent"
            />
          </div>
        </div>

        <div class="document">
          <div class="document-content">
            <label for="document-content">Tham chiếu chứng từ</label>
          </div>
          <div class="document-link">
            <a href="#href"> ... </a>
          </div>
        </div>
      </div>
      <div class="input-information-center">
        <DateTimeComponent
          :voucherType="voucherType"
          :value="{
            ngayHachToan: payment.ngayHachToan,
            ngayChungTu: payment.ngayChungTu,
            soChungTu: payment.soChungTu,
            hanQuyetToan: payment.hanQuyetToan,
          }"
          @update:value="updateDateTimeData"
        />
      </div>
      <div class="input-information-left">
        <div class="summary-component">
          <div class="total-label">Tổng tiền</div>
          <div class="total-amount">{{ formattedTotalAmount }}</div>
        </div>
      </div>
    </div>
    <div class=" ">
      <MSGrid
        label="Hạch toán"
        :modelValue="payment.paymentDetail"
        @changeValueInput="changeValueInput"
        :configColumGrid="paymentConfigCombo.gridConfig"
        @selectedCombox="selectedGridCombox"
      />
      <div>
        <AttachFile />
      </div>
    </div>
    <div>
      <div>
        <FooterPayment class="footer-payment-a" />
      </div>
    </div>
  </section>
</template>

<script>
import HeaderPayment from "../components/PaymentPage/HeaderPayment.vue";
import MSCombobox from "../components/ControlComponent/MSCombobox.vue";
import MSInput from "../components/Base/MSInput.vue";
import DateTimeComponent from "../components/PaymentPage/DateTimeComponent.vue";
import FooterPayment from "../components/PaymentPage/FooterPayment.vue";

import MSGrid from "../components/ControlComponent/MSGrid.vue";
import AttachFile from "../components/PaymentPage/AttachFile.vue";

import CreateBankAccount from "../components/PaymentPage/CreateBankAccount.vue";
import CreateCustomer from "../components/PaymentPage/CreateCustomer.vue";
import CreateEmployee from "../components/PaymentPage/CreateEmployee.vue";

import paymentConfig from "../config/PaymentConfig";

export default {
  name: "Payment",
  components: {
    HeaderPayment,
    MSCombobox,
    MSInput,
    DateTimeComponent,
    FooterPayment,

    MSGrid,
    AttachFile,

    CreateBankAccount,
    CreateCustomer,
    CreateEmployee,
  },
  data() {
    return {
      voucherType: "1.Trả tiền nhà cung cấp",
      paymentMethod: "Ủy nhiệm chi",
      errorMessage: "",
      inputValue: "",
      inputValueCustomer: "",
      addressValue: "",
      totalAmount: 0,
      bankNameInput: "",
      accountReceiveValue: "",
      paymentConfigCombo: paymentConfig,
      payment: {
        accountExpenseNumber: null,
        accountReceiveNumber: null,
        bankExpenseName: null,
        bankReceiveName: null,
        customerName: null,
        customerAddress: null,
        employeeCode: null,
        ngayHachToan: null,
        ngayChungTu: null,
        soChungTu: null,
        hanQuyetToan: null,
        cmndNumber: null,
        licenseDate: null,
        licenseAddress: null,
        paymentDetail: [],
      },
      createCustomerComponent: CreateCustomer,
      createBankAccountComponent: CreateBankAccount,
      createEmployeeComponent: CreateEmployee,
    };
  },
  computed: {
    hideInformationInput() {
      return (
        this.paymentMethod === "Ủy nhiệm chi" ||
        this.paymentMethod === "Séc chuyển khoản"
      );
    },
    hideAccountReceive() {
      return this.paymentMethod === "Séc tiền mặt";
    },
    hideCreateEmployeeInput() {
      return (
        this.voucherType === "2.Trả các khoản vay" ||
        this.voucherType === "3.Tạm ứng cho nhân viên"
      );
    },
    defaultBillContent() {
      return `Chi tiền cho ${this.inputValueCustomer}`;
    },
    formattedTotalAmount() {
      return this.totalAmount;
    },
  },
  methods: {
    selectedGridCombox(record, column, selectedOption) {
      if (column.fieldName === "customer") {
        record.objectId = selectedOption.objectId;
        record.objectName = selectedOption.objectName;
        record.description = `Chi tiền cho ${selectedOption.objectName}`;
      }
    },
    changeValueInput(record, column) {
      if (column.fieldName === "amount") {
        this.updateTotalAmount(record);
      }
    },
    updateTotalAmount(record) {
      this.totalAmount = record.amount;
    },
    updateSelectedRow(type, item) {
      switch (type) {
        case "bankExpense":
          this.bankNameInput = item.bankName;
          this.payment.accountExpenseNumber = item.accountNumber;
          this.payment.bankExpenseName = item.bankName;
          break;
        case "customer":
          this.payment.customerName = item.objectName;
          this.payment.customerAddress = item.address;
          this.payment.inputValueCustomer = item.objectName;
          this.inputValueCustomer = item.objectName;
          this.addressValue = item.address;
          break;
        case "bankReceive":
          this.accountReceiveValue = item.bankName;
          this.payment.accountReceiveNumber = item.accountNumber;
          this.payment.bankReceiveName = item.bankName;
          break;
        case "employee":
          this.payment.employeeCode = item.employeeCode;
          break;
      }
    },
    updateDateTimeData(updatedValue) {
      this.payment = updatedValue;
    },
    updateBillContent(newValue) {
      this.inputValueCustomer = newValue.replace("Chi tiền cho ", "");
    },
    updateDateTimeData(updatedValue) {
      this.payment = { ...this.payment, ...updatedValue };
    },
  },
};
</script>

<style scoped>
.second-input:focus {
  border-color: green;
}
.input-container .input-with-button-1:focus-within {
  border-color: green;
}
.summary-component {
  display: flex;
  flex-direction: column;
  align-items: flex-end;
  padding: 0px 20px 0 20px;
}

.total-label {
  font-size: 16px;
  font-weight: bold;
  margin-bottom: 8px;
}

.total-amount {
  font-size: 36px;
  font-weight: bold;
}

.information-left {
  display: flex;
}
.information-right {
  width: 45%;
}
.customer-right {
  width: 45%;
}

.input-container {
  display: flex;
  align-items: center;
}

.input-group {
  display: flex;
  gap: 16px;
}

label {
  margin-bottom: 8px;
  font-weight: bold;
}
.accountingGrid {
  margin-bottom: 50px;
}
.payment {
  padding-top: 40px;
  font-family: AvertaStdCY, Helvetica, Arial, sans-serif;
}

.header-payment {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  z-index: 1000;
  background-color: #fff;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}
.footer-payment-a {
  position: fixed;
  bottom: 0;
  left: 0;
  width: 100%;
  z-index: 1000;
}
.account-input-container {
  display: flex;
  flex-direction: column;
  margin-bottom: 8px;
}

.account-input-wrapper {
  display: flex;
  align-items: center;
  margin-bottom: 4px;
}

.second-input {
  border: 1px solid #999;
  border-radius: 2px;
  padding: 8px;
  box-sizing: border-box;
  height: 32px;
  flex-grow: 1;
  margin-left: 15px;
  width: 50%;
  margin-top: 16px;
  display: flex;
  align-items: center;
}

label {
  margin-bottom: 8px;
  font-weight: bold;
}
.input-information {
  display: flex;
  font-size: 14px;
  margin-top: 20px;
  margin-bottom: 15px;
}
.input-information-right {
  width: 50%;
  margin-right: 25px;
  margin-left: 18px;
  margin-top: 4px;
}
.input-information-center {
  margin-bottom: auto;
}
.input-information-left {
  width: 30%;
}

.bill-content-input-wrapper {
  display: flex;
  flex-direction: column;
  margin-bottom: 8px;
}

.input-container {
  display: flex;
  align-items: center;
}
.document {
  margin-bottom: 5px;
  display: flex;
}
.document-link {
  color: inherit;
  margin-left: 10px;
  font-weight: bold;
}

.base-input-1 {
  border: none;
  padding: 8px;
  box-sizing: border-box;
  height: 40px;
  width: 300px;
}
.base-input input {
  width: 100%;
  height: 100%;
  border: none;
  outline: none;
  padding: 0 8px;
}

.information-input-wrapper {
  display: flex;
  gap: 15px;
  margin-bottom: 8px;
}

.input-field {
  flex: 1;
  display: flex;
  flex-direction: column;
}
.base-input-info {
  width: 100%;
  height: 32px;
  border: 1px solid #999;
  border-radius: 2px;
  padding: 0 8px;
  box-sizing: border-box;
  outline: none;
  display: flex;
  align-items: center;
}
.base-input-info:focus-within {
  border-color: green;
}
.input-container-info-2 {
  flex: 1;
  display: flex;
  justify-content: space-between;
  gap: 16px;
  width: 45%;
}
.input-container-info-1 {
  flex: 1;
  max-width: 45%;
  display: flex;
  flex-direction: column;
}
.base-input-info input[type="date"] {
  width: 100%;
  height: 100%;
  border: none;
  outline: none;
  padding: 0 8px;
}
.base-input-content {
  width: 100%;
  border: 1px solid #999;
  display: flex;
  align-items: center;
  height: 30px;
  padding-left: 10px;
}
</style>
