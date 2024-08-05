<template>
  <section class="payment">
    <HeaderPayment
      :voucherType="currentItem.voucherType"
      :paymentMethod="currentItem.paymentMethod"
      :documentNumber="currentItem.documentNumber"
      @update:voucherType="updateValue('voucherType', $event)"
      @update:paymentMethod="updateValue('paymentMethod', $event)"
      class="header-payment"
      :disabled="isDisabled"
    />

    <div class="input-information">
      <div class="input-information-right">
        <div class="account-input-container">
          <!--html của input bankexpense -->
          <div class="account-input-wrapper">
            <MSCombobox
              label="Tài khoản chi"
              @update:selectedRow="updateSelectedRow('bankExpense', $event)"
              :config="paymentConfigCombo.comboxConfig.bankExpense"
              :value="currentItem.accountNumber"
              :ComponentAdd="createBankAccountComponent"
              ref="BankAccountId"
              :disabled="isDisabled"
            />
            <MSInput
              class="second-input"
              :value="currentItem.bankName"
              @input="updateValue('bankExpenseName', $event.target.value)"
              :disabled="isDisabled"
            />
          </div>
          <!--html của input customer -->
          <div class="account-input-wrapper">
            <MSCombobox
              label="Đối Tượng"
              @update:selectedRow="updateSelectedRow('customer', $event)"
              :config="paymentConfigCombo.comboxConfig.customer"
              :value="currentItem.objectName"
              :ComponentAdd="createCustomerComponent"
              ref="CustomerId"
              :disabled="isDisabled"
            />
            <MSInput
              class="second-input"
              :value="currentItem.address"
              @input="updateValue('customerAddress', $event.target.value)"
              :disabled="isDisabled"
            />
          </div>
          <!--html của input bankReceive -->
          <div class="account-input-wrapper">
            <MSCombobox
              v-if="!hideAccountReceive"
              label="Tài Khoản Nhận"
              :showButton="false"
              @update:selectedRow="updateSelectedRow('bankReceive', $event)"
              :value="currentItem.accountReceiveNumber"
              :config="paymentConfigCombo.comboxConfig.bankReceive"
              :disabled="isDisabled"
            />
            <MSInput
              v-if="!hideAccountReceive"
              class="second-input"
              :value="currentItem.bankReceiveName"
              @input="updateValue('bankReceiveName', $event.target.value)"
              :disabled="isDisabled"
            />
          </div>
          <!-- html của các input CMND, Ngày cấp, Nơi cấp -->
          <div v-if="!hideInformationInput" class="information-input-wrapper">
            <div class="input-container-info-1">
              <label for="so-cmnd">Số CMND</label>
              <MSInput
                :value="currentItem.cmndNumber"
                @input="updateValue('cmndNumber', $event.target.value)"
                class="base-input-info"
                :disabled="isDisabled"
              />
            </div>
            <div class="input-container-info-2">
              <div class="input-field">
                <label for="ngay-cap">Ngày cấp</label>
                <MSInput
                  :value="currentItem.licenseDate"
                  @input="updateValue('licenseDate', $event.target.value)"
                  type="date"
                  class="base-input-info"
                  :disabled="isDisabled"
                />
              </div>
              <div class="input-field">
                <label for="noi-cap">Nơi cấp</label>
                <MSInput
                  :value="currentItem.licenseAddress"
                  @input="updateValue('licenseAddress', $event.target.value)"
                  class="base-input-info"
                  :disabled="isDisabled"
                />
              </div>
            </div>
          </div>
          <!--html của input nội dung thanh toán -->
          <div class="bill-content-input-wrapper">
            <label for="bill-content-input">Nội dung thanh toán</label>
            <div class="input-container">
              <MSInput
                :value="defaultBillContent"
                class="base-input-content"
                @input="updateBillContent"
                :disabled="isDisabled"
              />
            </div>
          </div>
          <!--html của input nhân viên -->
          <div class="account-input-wrapper">
            <MSCombobox
              v-if="!hideCreateEmployeeInput"
              label="Nhân viên"
              @update:selectedRow="updateSelectedRow('employee', $event)"
              :config="paymentConfigCombo.comboxConfig.employee"
              :value="currentItem.employeeName"
              :ComponentAdd="createEmployeeComponent"
              ref="EmployeeId"
              :disabled="isDisabled"
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
      <!-- Các thành phần của Datetime Input -->
      <div class="input-information-center">
        <div class="datetime-wrapper">
          <div class="form-group">
            <label for="ngay-hach-toan">Ngày hạch toán</label>
            <MSDatetime
              type="date"
              class="date-input"
              ref="AccountingDate"
              :value="currentItem.accountingDate"
              @change="updateCurrentItem('accountingDate', $event)"
              :disabled="isDisabled"
            />
          </div>
          <div class="form-group">
            <label for="ngay-chung-tu">Ngày chứng từ</label>
            <MSDatetime
              type="date"
              class="date-input"
              ref="DocumentDate"
              :value="currentItem.documentDate"
              @change="updateCurrentItem('documentDate', $event)"
              :disabled="isDisabled"
            />
          </div>
          <div class="form-group">
            <label for="so-chung-tu">Số chứng từ</label>
            <MSDatetime
              class="date-input"
              type="text"
              ref="DocumentNumber"
              :value="currentItem.documentNumber"
              @change="updateCurrentItem('documentNumber', $event)"
              :disabled="isDisabled"
            />
          </div>
          <div
            class="form-group"
            v-if="currentItem.voucherType === '3. Tạm ứng cho nhân viên'"
          >
            <label for="han-quyet-toan">Hạn quyết toán</label>
            <MSDatetime
              type="date"
              class="date-input"
              :value="currentItem.hanQuyetToan"
              @input="updateValue('hanQuyetToan', $event)"
              :disabled="isDisabled"
            />
          </div>
        </div>
      </div>
      <div class="input-information-left">
        <div class="summary-component">
          <div class="total-label">Tổng tiền</div>
          <div class="total-amount">{{ formattedTotalAmount }}</div>
        </div>
      </div>
    </div>

    <div class=" ">
      <!--html của Grid  -->
      <MSGrid
        label="Hạch toán"
        :modelValue="currentItem.paymentDetails"
        @changeValueInput="changeValueInput"
        :configColumGrid="paymentConfigCombo.gridConfig"
        @selectedCombox="selectedGridCombox"
        field="PaymentDetails"
        ref="PaymentDetails"
        :disabled="isDisabled"
      />
      <div>
        <AttachFile :disabled="isDisabled" />
      </div>
    </div>
    <div>
      <div>
        <FooterPayment
          :payment="currentItem"
          class="footer-payment-a"
          @submit="handleSubmit"
        />
      </div>
    </div>
  </section>
</template>

<script>
import HeaderPayment from "../components/PaymentPage/HeaderPayment.vue";
import MSCombobox from "../components/ControlComponent/MSCombobox.vue";

import FooterPayment from "../components/PaymentPage/FooterPayment.vue";

import MSGrid from "../components/ControlComponent/MSGrid.vue";
import AttachFile from "../components/PaymentPage/AttachFile.vue";

import CreateBankAccount from "../components/PaymentPage/CreateBankAccount.vue";
import CreateCustomer from "../components/PaymentPage/CreateCustomer.vue";
import CreateEmployee from "../components/PaymentPage/CreateEmployee.vue";

import paymentConfig from "../config/PaymentConfig";
import BaseSubmit from "../components/Base/BaseSubmit.vue";
import Api from "../api/apiConst";
import MSDatetime from "../components/Base/MSDateTime.vue";

export default {
  name: "Payment",
  extends: BaseSubmit,
  components: {
    HeaderPayment,
    MSCombobox,
    MSDatetime,

    FooterPayment,

    MSGrid,
    AttachFile,

    CreateBankAccount,
    CreateCustomer,
    CreateEmployee,
  },

  data() {
    return {
      isDisabled: false,
      apiUrl: Api.payment.url,
      errorMessage: "",
      inputValue: "",
      inputBillContent: "",
      totalAmount: 0,

      paymentConfigCombo: paymentConfig,
      currentItem: {
        voucherType: "5. Chi khác",
        paymentMethod: "Ủy nhiệm chi",
        totalAmount: null,
        billContent: null,

        bankName: null,
        accountNumber: null,
        bankAccountId: null,

        objectName: null,
        address: null,
        customerId: null,

        employeeName: null,
        employeeId: null,

        accountingDate: null,
        documentDate: null,
        documentNumber: "UNC001",
        paymentDetails: [],
      },
      documentNumberMapping: {
        "Ủy nhiệm chi": "UNC001",
        "Séc chuyển khoản": "SCK001",
        "Séc tiền mặt": "STM001",
      },

      createCustomerComponent: CreateCustomer,
      createBankAccountComponent: CreateBankAccount,
      createEmployeeComponent: CreateEmployee,
    };
  },

  computed: {
    /**
     * Các function này xử lý ẩn hiện các input theo nghiệp vụ đc yêu cầu
     */
    hideInformationInput() {
      return (
        this.currentItem.paymentMethod === "Ủy nhiệm chi" ||
        this.currentItem.paymentMethod === "Séc chuyển khoản"
      );
    },
    hideAccountReceive() {
      return this.currentItem.paymentMethod === "Séc tiền mặt";
    },
    hideCreateEmployeeInput() {
      return (
        this.currentItem.voucherType === "2. Trả các khoản vay" ||
        this.currentItem.voucherType === "3. Tạm ứng cho nhân viên"
      );
    },
    /** */

    defaultBillContent() {
      return `Chi tiền cho ${this.inputBillContent}`;
    },
    /**
     * xử lý format hiển thị của số tiền
     */
    formattedTotalAmount() {
      return this.totalAmount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    },
    TotalAmount() {
      return this.totalAmount;
    },
  },
  watch: {
    "currentItem.paymentMethod"(newMethod) {
      this.currentItem.documentNumber =
        this.documentNumberMapping[newMethod] || "";
    },
  },
  methods: {
    /**
     * function gán giá trị vào record để lưu thông tin vào modelvalue
     * trong MSGrid và sẽ lưu lại vào paymentDetails thông qua props
     * @param record
     * @param column
     * @param selectedOption
     */
    selectedGridCombox(record, column, selectedOption) {
      if (column.fieldName === "customer") {
        record.objectId = selectedOption.objectId;
        record.objectName = selectedOption.objectName;
      }
      if (column.fieldName === "debitAccount") {
        record.debitAccount = selectedOption.debitAccountNumber;
      }
      if (column.fieldName === "creditAccount") {
        record.creditAccount = selectedOption.creditAccountNumber;
      }
    },
    /**
     * 2 function xử lý việc update số tiền khi có thêm một hàng mới được thêm
     * trong grid.
     * @param record
     * @param column
     */
    changeValueInput(record, column) {
      if (column.fieldName === "amount") {
        this.updateTotalAmount();
      }
    },

    /**
     * function cập nhật giá trị của totalamount
     * ở đây sẽ quay lại MSGrid lấy giá trị của amount rồi tính tổng
     * ra thành totalamount hiển thị ra màn hình.
     */
    updateTotalAmount() {
      const total = this.currentItem.paymentDetails.reduce((sum, row) => {
        return sum + Number(row.amount.toString().replace(/\./g, "") || 0);
      }, 0);
      this.totalAmount = total;
      this.currentItem.totalAmount = this.formattedTotalAmount;
      this.$emit("updateTotalAmount", this.totalAmount);
    },
    //------------------------------------
    /**
     * function update value của các MSInput và các MSdatetime
     * @param field
     * @param value
     */
    updateValue(field, value) {
      this.currentItem[field] = value;
    },
    updateCurrentItem(field, event) {
      const value = event.target.value;
      this.currentItem[field] = value;
    },

    /**
     * fuction update giá trị khi chọn 1 option trong multiselect
     * và gán nó vào object currentItem
     * @param type
     * @param item
     */
    updateSelectedRow(type, item) {
      switch (type) {
        case "bankExpense":
          this.currentItem.accountNumber = item.accountNumber;
          this.currentItem.bankName = item.bankName;
          this.currentItem.bankAccountId = item.id;

          break;
        case "customer":
          this.currentItem.objectName = item.objectName;
          this.currentItem.address = item.address;
          this.currentItem.billContent = item.objectName;
          this.inputBillContent = item.objectName;
          this.currentItem.customerId = item.id;
          this.updateGridDescription(item.objectName);

          break;
        case "bankReceive":
          this.currentItem.accountReceiveNumber = item.accountNumber;
          this.currentItem.bankReceiveName = item.bankName;
          break;
        case "employee":
          this.currentItem.employeeName = item.employeeName;
          this.currentItem.employeeId = item.id;

          break;
      }
    },

    /**
     * hàm gọi từ BaseSubmit thực hiện việc sau khi submit thành công
     * sẽ thực hiện các yêu cầu tùy theo ng dùng mong muốn
     * @param responseData
     */
    afterCallSuccess(responseData) {
      this.showAlert("Lưu thành công thông tin", () => {
        if (responseData) {
          this.$router.push("/payment");
        }
      });
    },

    /**
     * các function thực hiện gán data cho các input khác nhau
     * @param
     */

    updateBillContent(newValue) {
      this.inputBillContent = newValue.replace("Chi tiền cho ", "");
    },

    updateGridDescription(customerName) {
      this.currentItem.paymentDetails.forEach((record) => {
        record.description = `Chi tiền cho ${customerName}`;
      });
    },
    generateGridRefs(rowIndex, fieldName) {
      return `${rowIndex}-${fieldName}`;
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
  background-color: #fff;
}

.input-group {
  display: flex;
  gap: 16px;
}

label {
  margin-bottom: 7px;
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

  box-sizing: border-box;
  height: 28px;
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
  padding-top: 20px;
  margin-bottom: 15px;
  background-color: #f4f5f8;
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
  width: 35%;
}

/* css của datetimeinput */

.form-group {
  display: flex;
  flex-direction: column;
  margin-bottom: 14px;
}
.date-input {
  box-sizing: border-box;
  outline: none;
  border: 1px solid #999;
  border-radius: 2px;
  overflow: hidden;
  height: 27px;
  width: 100%;
  display: flex;
  align-items: center;
  background-color: #fff;
}
.datetime-wrapper {
  display: flex;
  flex-direction: column;
  width: 200px;
  margin: 0 auto;
  border: 1px solid #999;
  border-top: none;
  border-bottom: none;
  border-right: none;
  padding-left: 25px;
  padding-top: 5px;
}
/**----------- */

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
  height: 30px;
  border: 1px solid #999;
  border-radius: 2px;
  padding: 0 8px;
  box-sizing: border-box;
  outline: none;
  display: flex;
  align-items: center;
  background-color: #fff;
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
  height: 26px;
}
</style>
