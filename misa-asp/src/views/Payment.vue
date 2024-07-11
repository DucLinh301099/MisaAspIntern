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
            <ComboboxInput
              label="Tài khoản chi"
              @update:selectedRow="updateSelectedRow('bankExpense', $event)"
              :config="paymentConfigCombo.comboxConfig.bankExpense"
              :value="payment.accountExpenseNumber"
              :ComponentAdd="createBankAccountComponent"
            />
            <BaseInput
              v-model="bankNameInput"
              class="base-input second-input"
              :class="{ onFocus: isBankNameInputFocused }"
              :type="type"
              :value="payment.bankExpenseName"
              @focus="handleFocus('isBankNameInputFocused')"
              @blur="handleBlur('isBankNameInputFocused')"
            />
          </div>
          <div class="account-input-wrapper">
            <ComboboxInput
              label="Đối Tượng"
              @update:selectedRow="updateSelectedRow('customer', $event)"
              :config="paymentConfigCombo.comboxConfig.customer"
              :value="payment.customerName"
              :ComponentAdd="createCustomerComponent"
            />
            <BaseInput
              v-model="addressValue"
              class="base-input second-input"
              :class="{ onFocus: isCustomerFocused }"
              :type="type"
              :value="payment.customerAddress"
              @focus="handleFocus('isCustomerFocused')"
              @blur="handleBlur('isCustomerFocused')"
            />
          </div>
          <div class="account-input-wrapper">
            <ComboboxInput
              v-if="!hideAccountReceive"
              label="Tài Khoản Nhận"
              :showButton="false"
              @update:selectedRow="updateSelectedRow('bankReceive', $event)"
              :value="payment.accountReceiveNumber"
              :config="paymentConfigCombo.comboxConfig.bankReceive"
            />
            <BaseInput
              v-if="!hideAccountReceive"
              v-model="accountReceiveValue"
              class="base-input second-input"
              :class="{ onFocus: isAccountReceiveValueFocused }"
              :type="type"
              :value="payment.bankReceiveName"
              @focus="handleFocus('isAccountReceiveValueFocused')"
              @blur="handleBlur('isAccountReceiveValueFocused')"
            />
          </div>
          <InformationInput v-if="!hideInformationInput" />

          <div class="bill-content-input-wrapper">
            <label for="bill-content-input">Nội dung thanh toán</label>
            <div class="input-container">
              <div
                class="input-with-button-1"
                :class="{ onFocus: isInputValueFocused }"
              >
                <BaseInput
                  v-model="inputValueCustomer"
                  :value="defaultBillContent"
                  :validator="inputValidator"
                  class="base-input-1"
                  @input="updateBillContent"
                  @focus="handleFocus('isInputValueFocused')"
                  @blur="handleBlur('isInputValueFocused')"
                />
              </div>
            </div>
          </div>
          <div class="account-input-wrapper">
            <ComboboxInput
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
            hanQuyetToan: payment.hanQuyetToan,
          }"
          @input="updateDateTimeData"
        />
      </div>
      <div class="input-information-left">
        <SummaryComponent :totalAmount="totalAmount" />
      </div>
    </div>
    <div class=" ">
      <MSGrid
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
import ComboboxInput from "../components/ControlComponent/ComboboxInput.vue";
import BaseInput from "../components/BaseComponent/BaseInputComponent.vue";
import DateTimeComponent from "../components/ControlComponent/DateTimeComponent.vue";
import FooterPayment from "../components/PaymentPage/FooterPayment.vue";
import SummaryComponent from "../components/ControlComponent/SummaryComponent.vue";
import MSGrid from "../components/ControlComponent/MSGrid.vue";
import AttachFile from "../components/PaymentPage/AttachFile.vue";

import CreateBankAccount from "../components/PaymentPage/CreateBankAccount.vue";
import CreateCustomer from "../components/PaymentPage/CreateCustomer.vue";
import CreateEmployee from "../components/PaymentPage/CreateEmployee.vue";

import InformationInput from "../components/PaymentPage/InformationInput.vue";
import paymentConfig from "../config/PaymentConfig";

export default {
  name: "Payment",
  components: {
    HeaderPayment,
    ComboboxInput,
    BaseInput,
    DateTimeComponent,
    FooterPayment,
    SummaryComponent,
    MSGrid,
    AttachFile,
    InformationInput,
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
        paymentDetail: [],
      },
      createCustomerComponent: CreateCustomer,
      createBankAccountComponent: CreateBankAccount,
      createEmployeeComponent: CreateEmployee,
      isBankNameInputFocused: false,
      isCustomerFocused: false,
      isAccountReceiveValueFocused: false,
      isInputValueFocused: false,
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
      this.payment = { ...this.payment, ...updatedValue };
    },
    updateBillContent(newValue) {
      this.inputValueCustomer = newValue.replace("Chi tiền cho ", "");
    },
    handleFocus(field) {
      this.resetFocusStates();
      this[field] = true;
    },
    handleBlur(field) {
      this[field] = false;
    },
    resetFocusStates() {
      this.isBankNameInputFocused = false;
      this.isCustomerFocused = false;
      this.isAccountReceiveValueFocused = false;
      this.isInputValueFocused = false;
    },
  },
};
</script>

<style scoped>
.information-input-wrapper {
  display: flex;
  flex-direction: row;
  gap: 16px;
  margin-bottom: 16px;
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
.input-field-cmnd,
.input-field {
  display: flex;
  flex-direction: column;
}
.input-field-date {
  display: flex;
  flex-direction: column;
  margin-right: 15px;
}
.input-field-address {
  display: flex;
  flex-direction: column;
}
.input-container {
  display: flex;
  align-items: center;
}
.input-container-infor {
  display: flex;
  outline: none;
  align-items: center;
  border: 1px solid #999;
  height: 34px;
}
.base-input {
  outline: none;
  padding: 8px;
  box-sizing: border-box;
  border: none;
  height: 35px;
  width: 100%;
}
.base-input-infor {
  width: 100%;
  height: 30px; /* Bạn có thể thay đổi giá trị này nếu cần */
  border: none;
  padding: 0 8px;
  box-sizing: border-box;
  outline: none;
}
.input-information-right {
  width: 50%;
  margin-right: 25px;
  margin-left: 18px;
  margin-top: 4px;
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
  padding-top: 60px;
}
.focused {
  border-color: green;
}
.input-wrapper.base-input.second-input.onFocus {
  border: 1px solid green;
}
.input-with-button-1.onFocus {
  border-color: green;
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

.base-input {
  border: 1px solid #999;
  padding: 8px;
  box-sizing: border-box;
  height: 35px;
  margin-left: 15px;
  width: 100%;
}

.second-input {
  border: 1px solid #999;
  border-radius: 2px;
  padding: 8px;
  box-sizing: border-box;
  height: 33px;
  flex-grow: 1;
  width: 50%;
  margin-top: 17px;
}

label {
  margin-bottom: 8px;
  font-weight: bold;
}
.input-information {
  display: flex;
  font-size: 15px;
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
.payment {
  font-family: AvertaStdCY, Helvetica, Arial, sans-serif;
}
.bill-content-input-wrapper {
  display: flex;
  flex-direction: column;
  margin-bottom: 8px;
}
label {
  margin-bottom: 8px;
  font-weight: bold;
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

.input-with-button-1 {
  display: flex;
  align-items: center;
  border: 1px solid #999;
  border-radius: 2px;
  overflow: hidden;
  flex-grow: 2;
  height: 31px;
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
.add-button {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 40px;
  height: 40px;
  border: none;
  background-color: white;
  cursor: pointer;
  font-size: 24px;
  color: green;
}
.app-container {
  padding-top: 80px;
}
.main-contain {
  font-family: AvertaStdCY, Helvetica, Arial, sans-serif;
}
</style>
