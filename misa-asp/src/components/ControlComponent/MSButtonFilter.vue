<template>
  <div id="filter-dropdown">
    <div class="dropdown-secondary ms-dropdown">
      <button
        name="button"
        shortkey-target=""
        class="ms-component ms-button ms-button-secondary ms-button ms-con-dropdown-radius-true-true expand-more-button"
        @click="toggleDropdown"
      >
        <div class="ms-button--text flex align-center">
          <span class="pr-4">Lọc</span>
          <div class="mi mi-16 mi-arrow-up--black">&nbsp;</div>
        </div>
      </button>
    </div>
    <!-- Dropdown content -->
    <div v-if="isDropdownVisible" class="dropdown-content">
      <div class="dropdown-item">
        <label>Thời gian *</label>
        <select>
          <option>Đầu năm đến hiện tại</option>
          <option>6 tháng đầu năm</option>
          <option>6 tháng cuối năm</option>
        </select>
      </div>
      <div class="flex-row">
        <div class="dropdown-item">
          <label>Từ ngày</label>
          <input type="date" value="2024-01-01" />
        </div>
        <div class="dropdown-item">
          <label>Đến ngày</label>
          <input type="date" value="2024-08-13" />
        </div>
      </div>
      <div class="dropdown-item-checkbox">
        <input type="checkbox" />
        <label>Áp dụng tham số thời gian cho tất cả các danh sách</label>
      </div>
      <div class="dropdown-actions">
        <button class="reset-button">Đặt lại</button>
        <button class="apply-button">Lọc</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MSButtonFilter",
  data() {
    return {
      isDropdownVisible: false,
    };
  },
  methods: {
    toggleDropdown() {
      this.isDropdownVisible = !this.isDropdownVisible;
    },
  },
};
</script>

<style scoped>
.dropdown-secondary .ms-button.ms-button-secondary {
  border: 2px solid #3b3c3f;
}

.dropdown-secondary .ms-component.ms-button {
  padding: 5px 18px;
  background-color: #fff;
}

.dropdown-secondary .ms-component.ms-button:hover {
  background-color: #ddd;
}

.ms-con-dropdown-radius-true-true {
  border-radius: 30px;
  color: #111;
}

.ms-button .ms-button--text {
  font-weight: 600;
  position: relative;
  color: inherit;
  display: inline-block;
  transition: all 0.25s ease;
  white-space: nowrap;
  font-size: 13px;
  line-height: 13px;
}

.flex {
  display: flex !important;
}

.align-center {
  justify-content: center;
  align-items: center;
}

.pr-4 {
  padding-right: 4px !important;
}

.mi.mi-arrow-up--black {
  background-position: -560px -359px;
}

.mi-16 {
  width: 16px;
  height: 16px;
  min-width: 16px;
  min-height: 16px;
}

.expand-more-button {
  height: 100%;
  position: relative;
}
.mi {
  background: url(https://actaspcdng1.misacdn.net/assets/Sprites-11d892c3.svg)
    no-repeat;
  cursor: pointer;
}

/* Dropdown CSS */
#filter-dropdown {
  position: relative;
  z-index: 10;
}

.dropdown-content {
  position: absolute;
  z-index: 1001;
  top: 100%;
  left: 0;
  background-color: #fff;
  width: 350px;
  box-shadow: 0px 8px 16px rgba(0, 0, 0, 0.2);
  border-radius: 2.5px;
  border: 1px solid #ced4da;
  padding: 16px;
  box-sizing: border-box;
  margin-top: 5px;
  font-family: AvertaStdCY, Helvetica, Arial, sans-serif;
}
label {
  font-size: 12px;
  margin-top: 3px;
  margin-left: 5px;
}
.dropdown-item {
  margin-bottom: 16px;
}
.dropdown-item-checkbox {
  margin-bottom: 16px;
  display: flex;
}

.dropdown-item label {
  display: block;
  margin-bottom: 8px;
  font-weight: 600;
  font-size: 12px;
}

.dropdown-item select {
  width: 100%;
  padding: 5px;
  border: 1px solid #ced4da;
  border-radius: 2.5px;
  background-color: #ffffff;
  font-size: 14px;
  color: #495057;
  outline: none;
}

.dropdown-item input[type="checkbox"] {
  margin-right: 8px;
}

.dropdown-actions {
  display: flex;
  justify-content: space-between;
  margin-top: 12px;
}

.dropdown-actions button {
  padding: 6px 12px;
  width: 70px;
  border-radius: 2.5px;
  font-size: 14px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.dropdown-actions button:hover {
  opacity: 0.9;
}

.dropdown-actions .apply-button {
  background-color: #28a745;
  color: #fff;
}

.dropdown-actions .apply-button:hover {
  background-color: #218838;
}

.flex-row {
  display: flex;
  justify-content: space-between;
  gap: 15px;
  margin-bottom: 10px;
}
.reset-button {
  border: 1px solid #8d9096;
}
.apply-button {
  border: none;
}

.flex-row .dropdown-item {
  flex: 1; /* Đảm bảo các phần tử con bên trong có cùng kích thước */
  margin-bottom: 0;
}

.flex-row .dropdown-item label {
  display: block;
  margin-bottom: 8px;
  font-weight: 600;
  font-size: 12px;
}

.flex-row .dropdown-item input[type="date"] {
  width: 138px; /* Đảm bảo input chiếm đầy đủ không gian của dropdown-item */
  padding: 5px;
  border: 1px solid #ced4da;
  border-radius: 2.5px;
  background-color: #ffffff;
  font-size: 14px;
  color: #495057;
  outline: none;
}
</style>
