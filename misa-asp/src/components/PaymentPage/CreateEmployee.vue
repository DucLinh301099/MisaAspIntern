<!-- src/components/CreateEmployee.vue -->
<template>
  <div class="create-employee">
    <h2 class="form-title">Thông tin nhân viên</h2>
    <form @submit.prevent="handleSubmit">
      <div class="form-row">
        <div class="input-container">
          <label for="employee-code"
            >Mã nhân viên<span class="required">*</span></label
          >
          <MSInput
            type="text"
            ref="EmployeeCode"
            data-field="employeeCode"
            :errors="employeeCodeErrors"
            :value="employeeCode"
            @input="updateValue('employeeCode', $event.target.value)"
            class="input-field"
            required
          />
        </div>
        <div class="input-container">
          <label for="employee-name"
            >Tên nhân viên<span class="required">*</span></label
          >
          <MSInput
            type="text"
            ref="EmployeeName"
            data-field="employeeName"
            :errors="employeeNameErrors"
            :value="employeeName"
            @input="updateValue('employeeName', $event.target.value)"
            class="input-field"
            required
          />
        </div>
      </div>
      <div class="form-row">
        <div class="input-container">
          <label for="mobile-phone">Điện thoại</label>
          <MSInput
            type="text"
            ref="PhoneNumber"
            data-field="phoneNumber"
            :errors="phoneNumberErrors"
            :value="phoneNumber"
            @input="updateValue('phoneNumber', $event.target.value)"
            class="input-field"
          />
        </div>
        <div class="input-container">
          <label for="department">Đơn vị</label>
          <MSInput
            type="text"
            ref="Department"
            data-field="department"
            :errors="departmentErrors"
            :value="department"
            @input="updateValue('department', $event.target.value)"
            class="input-field"
          />
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
import { account } from "../../api/account";

import BaseForm from "../Base/BaseForm.vue";

export default {
  name: "CreateEmployee",
  extends: BaseForm,

  data() {
    return {
      employeeCode: "",
      employeeName: "",
      phoneNumber: "",
      department: "",

      employeeCodeErrors: [],
      employeeNameErrors: [],
      phoneNumberErrors: [],
      departmentErrors: [],
    };
  },
  methods: {
    async customHandleLogic() {
      return await account.createEmployee(
        this.employeeCode,
        this.employeeName,
        this.phoneNumber,
        this.department
      );
    },
    updateValue(field, value) {
      this[field] = value;
      switch (field) {
        case "employeeCode":
          this.employeeCodeErrors = [];
          break;
        case "employeeName":
          this.employeeNameErrors = [];
          break;
        case "phoneNumber":
          this.phoneNumberErrors = [];
          break;
        case "department":
          this.departmentErrors = [];
          break;
        default:
          break;
      }
    },
  },
};
</script>

<style scoped>
.create-employee {
  background-color: #fff;
  padding-top: 8px;
  border-radius: 10px;
  max-width: 900px;
}
.divide {
  width: 100%;
  border-top: 1px solid #e0e0e0;
  margin-top: 25px;
  position: relative;
}
.form-title {
  font-size: 24px;
  margin-bottom: 25px;
  color: #333;
}

.form-row {
  display: flex;
  justify-content: space-between;
  margin-bottom: 18px;
}

.input-container {
  display: flex;
  flex-direction: column;
  flex: 1;
  margin-right: 40px;
}

.input-container.full-width {
  flex: 0 0 100%;
  margin-right: 0;
}
.required {
  color: red;
}
.input-container:last-child {
  margin-right: 0;
}

label {
  font-weight: 600;
  margin-bottom: 8px;
}

.input-field {
  padding: 1px;
  border: 1px solid #babec5;
  border-radius: 2.5px;
  font-size: 14px;
  color: #333;
  outline: none;
  width: 200px;
}

.input-field:focus {
  border-color: #28a745;
}

.form-actions {
  display: flex;
  justify-content: flex-end;
  margin-top: 20px;
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
button:hover {
  background-color: #f4f5f8;
}
.error-message {
  color: red;
  margin-bottom: 20px;
}
</style>
