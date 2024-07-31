<template>
  <div class="create-bank-account">
    <h2 class="form-title">Thêm Tài khoản ngân hàng</h2>
    <form @submit.prevent="handleSubmit">
      <div class="form-row">
        <div class="input-container">
          <label for="account-number">
            Số tài khoản <span class="required">*</span>
          </label>
          <MSInput
            type="text"
            :value="accountNumber"
            ref="AccountNumber"
            data-field="accountNumber"
            :errors="accountNumberErrors"
            @input="updateValue('accountNumber', $event.target.value)"
            class="input-field"
          />
        </div>
        <div class="input-container">
          <label for="bank-name">
            Tên ngân hàng <span class="required">*</span>
          </label>
          <MSInput
            type="text"
            :value="bankName"
            ref="BankName"
            data-field="bankName"
            :errors="bankNameErrors"
            @input="updateValue('bankName', $event.target.value)"
            class="input-field"
          />
        </div>
      </div>

      <div class="form-row">
        <div class="input-container">
          <label for="branch">
            Chi nhánh <span class="required">*</span>
          </label>
          <MSInput
            type="text"
            :value="branch"
            ref="Branch"
            data-field="branch"
            :errors="branchErrors"
            @input="updateValue('branch', $event.target.value)"
            class="input-field"
          />
        </div>
        <div class="input-container full-width">
          <label for="account-type">Loại tài khoản</label>
          <div class="custom-select">
            <select
              id="account-type"
              v-model="typeOfBank"
              required
              class="input-field"
            >
              <option class="options" value="1">Tài khoản chi</option>
              <option class="options" value="2">Tài khoản nhận</option>
            </select>
          </div>
        </div>
      </div>

      <div class="divide"><!----></div>

      <div class="form-actions">
        <div class="btn-cancel-container">
          <button type="button" class="btn cancel-btn" @click="$emit('close')">
            Hủy
          </button>
        </div>
        <div class="btn-container">
          <button type="button" class="btn save-btn" @click="save">Cất</button>

          <button type="submit" class="btn submit-btn">Cất và Thêm</button>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import { bankAccount } from "../../api/bank";
import BaseForm from "../Base/BaseForm.vue";

export default {
  name: "CreateBankAccount",
  extends: BaseForm,
  data() {
    return {
      accountNumber: "",
      bankName: "",
      branch: "",
      typeOfBank: "",

      branchErrors: [],
      bankNameErrors: [],
      accountNumberErrors: [],
    };
  },
  methods: {
    async customHandleLogic() {
      return await bankAccount.createBankAccount(
        this.accountNumber,
        this.bankName,
        this.branch,
        this.typeOfBank
      );
    },

    afterCallSuccess() {},

    updateValue(field, value) {
      this[field] = value;
      switch (field) {
        case "accountNumber":
          this.accountNumberErrors = [];
          break;
        case "bankName":
          this.bankNameErrors = [];
          break;
        case "branch":
          this.branchErrors = [];
          break;
        default:
          break;
      }
    },
  },
};
</script>

<style scoped>
.create-bank-account {
  background-color: #fff;

  border-radius: 8px;
  max-width: 600px;
}
.divide {
  width: 100%;
  border-top: 1px solid #e0e0e0;
  padding-bottom: 30px;
  position: relative;
}
.form-title {
  font-size: 24px;
  margin-bottom: 30px;
}

.form-row {
  display: flex;
  justify-content: space-between;
  margin-bottom: 30px;
}

.input-container {
  display: flex;
  flex-direction: column;
  flex: 1;
  margin-right: 40px;
}

.input-container.full-width {
  margin-right: 0;
}

.input-container:last-child {
  margin-right: 0;
}

label {
  font-weight: bold;
  margin-bottom: 10px;
}

.required {
  color: red;
}

.input-field {
  padding: 1px;
  border: 1px solid #babec5;
  border-radius: 2.5px;
  outline: none;
  font-size: 14px;
  width: 200px;
}
.input-field:focus {
  border-color: #28a745;
}
.custom-select {
  position: relative;
  border: 1px solid #babec5;
}

.custom-select select {
  appearance: none;
  -webkit-appearance: none;
  -moz-appearance: none;
  padding: 8px;
  border: none;
  border-radius: 2.5px;
  outline: none;
  width: 100%;
  cursor: pointer;
  background: white
    url('data:image/svg+xml;charset=US-ASCII,<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 140 140"><polygon points="70,105 105,35 35,35" fill="%23000"/></svg>')
    no-repeat right 10px center;
  background-size: 15px 15px;
}

.custom-select select:hover {
  border-color: #999;
}

.custom-select select:focus {
  border-color: #666;
}

.custom-select select option {
  padding: 10px;
}

.custom-select select option:focus {
  background-color: #28a745;
  color: white;
}
.form-actions {
  display: flex;
  justify-content: space-between;
}

.btn-container {
  flex: 1;
  display: flex;
  justify-content: flex-end;
  font-weight: bold;
}

.btn {
  padding: 10px 20px;
  border: 1px solid #8d9096;
  border-radius: 3px;
  cursor: pointer;
  background-color: white;
  font-weight: 600;
}

.cancel-btn {
  color: #000;
}

.save-btn {
  color: #000;
  margin-right: 13px;
}

.submit-btn {
  background-color: #28a745;
  color: white;
  border: none;
}

.submit-btn:hover {
  background-color: #218838;
}

.error-message {
  color: red;
  margin-bottom: 20px;
}
</style>
