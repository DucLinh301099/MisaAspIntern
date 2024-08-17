<template>
  <div id="filter-dropdown">
    <div class="dropdown-secondary ms-dropdown">
      <button
        ref="dropdownButton"
        name="button"
        shortkey-target=""
        class="ms-component ms-button ms-button-secondary ms-button ms-con-dropdown-radius-true-true expand-more-button"
        @click="toggleDropdown"
      >
        <div class="ms-button--text flex align-center">
          <span class="pr-4">Lọc</span>
          <div class="mi mi-16 mi-arrow-down--black">&nbsp;</div>
        </div>
      </button>
    </div>
    <!-- Dropdown content -->
    <div
      ref="dropdownContent"
      v-if="isDropdownVisible"
      class="dropdown-content"
    >
      <!-- Dropdown content as before -->
      <div v-if="dateField">
        <div class="dropdown-item">
          <label>Thời gian *</label>
          <div class="select-container">
            <div
              class="select-box"
              tabindex="0"
              @click.stop="toggleSelectDropdown"
            >
              {{ selectedTime }}
              <div class="mi mi-16 mi-arrow-down--black"></div>
            </div>
            <div v-if="isSelectDropdownVisible" class="select-options">
              <div
                v-for="option in timeOptions"
                :key="option"
                :class="[
                  'option-dropdown',
                  { 'selected-option': option === selectedTime },
                ]"
                @click="onSelectOption(option)"
              >
                {{ option }}
              </div>
            </div>
          </div>
        </div>
        <div class="flex-row">
          <div class="dropdown-item">
            <label>Từ ngày</label>
            <input
              type="date"
              :value="startDate"
              @input="startDate = $event.target.value"
            />
          </div>
          <div class="dropdown-item">
            <label>Đến ngày</label>
            <input
              type="date"
              :value="endDate"
              @input="endDate = $event.target.value"
            />
          </div>
        </div>
      </div>

      <div class="dropdown-item-checkbox">
        <input type="checkbox" />
        <label>Áp dụng tham số thời gian cho tất cả các danh sách</label>
      </div>
      <div class="dropdown-actions">
        <button class="reset-button" @click="resetFilter">Đặt lại</button>
        <button class="apply-button" @click="applyFilter">Lọc</button>
      </div>
    </div>
  </div>
</template>

<script>
import withdrawListConfig from "../../config/WithdrawListConfig";

export default {
  name: "MSButtonFilter",
  props: {
    dateField: {
      type: Array,
      default: () => [],
    },
  },
  data() {
    return {
      isDropdownVisible: false,
      isSelectDropdownVisible: false,
      selectedTime: "Đầu năm đến hiện tại",
      timeOptions: Object.keys(withdrawListConfig.timeOptionsConfig), // Lấy danh sách tùy chọn từ config
      startDate: "", // Ngày bắt đầu mặc định
      endDate: "",
      filters: [], // Mảng filter
    };
  },
  methods: {
    toggleDropdown(event) {
      event.stopPropagation(); // Ngăn sự kiện click lan truyền
      this.isDropdownVisible = !this.isDropdownVisible;
    },
    toggleSelectDropdown(event) {
      event.stopPropagation(); // Ngăn sự kiện click lan truyền
      this.isSelectDropdownVisible = !this.isSelectDropdownVisible;
    },
    onSelectOption(option) {
      if (event) {
        event.stopPropagation(); // Ngăn sự kiện click lan truyền khi chọn option
      } // Ngăn sự kiện click lan truyền khi chọn option
      this.selectedTime = option;
      this.isSelectDropdownVisible = false; // Ẩn dropdown con, không ảnh hưởng đến dropdown chính

      const selectedConfig = withdrawListConfig.timeOptionsConfig[option];
      if (selectedConfig) {
        this.startDate = selectedConfig.startDate;
        this.endDate = selectedConfig.endDate;
      } else {
        this.startDate = "";
        this.endDate = "";
      }
    },
    resetFilter() {
      this.selectedTime = "Đầu năm đến hiện tại";
      this.onSelectOption(this.selectedTime);
    },
    applyFilter() {
      if (this.dateField && this.dateField.length) {
        this.filters = []; // Đặt lại mảng filters

        for (let index = 0; index < this.dateField.length; index++) {
          const item = this.dateField[index];
          if (item && this.startDate && this.endDate) {
            let filterDate = [
              {
                posted_date: "accountingdate",
                condition: ">=",
                value: this.startDate,
              },
              "and",
              {
                posted_date: "accountingdate",
                condition: "<=",
                value: this.endDate,
              },
            ];

            // Gán giá trị vào mảng filters
            this.addFilter(filterDate);
          }
        }

        this.$emit("filters-updated", this.filters); // Phát ra sự kiện filters-updated với giá trị this.filters
      }
      this.isDropdownVisible = false;
    },
    addFilter(data) {
      if (this.filters.length) {
        this.filters.push("and");
      }
      this.filters.push(data);
    },
    clickOutside(event) {
      if (
        this.$refs.dropdownContent &&
        !this.$refs.dropdownContent.contains(event.target) &&
        !this.$refs.dropdownButton.contains(event.target)
      ) {
        this.isDropdownVisible = false;
        this.isSelectDropdownVisible = false; // Đảm bảo đóng cả dropdown bên trong khi nhấp ra ngoài
      }
    },
  },
  mounted() {
    this.onSelectOption(this.selectedTime); // Cập nhật ngày khởi tạo khi component mount
    document.addEventListener("click", this.clickOutside);
  },
  beforeDestroy() {
    document.removeEventListener("click", this.clickOutside);
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
.mi.mi-arrow-down--black {
  background-position: -560px -359px;
}
.mi {
  background: url(https://actaspcdng1.misacdn.net/assets/Sprites-11d892c3.svg)
    no-repeat;
  cursor: pointer;
  transition: transform 0.3s ease;
}

.arrow-up {
  transform: rotate(180deg);
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
  font-size: 13px;
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

.select-container {
  position: relative;
  display: inline-block;
  width: 100%;
  cursor: pointer;
}

.select-box {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 5px 10px 5px 10px;
  border-radius: 2.5px;
  border: 1px solid #ced4da;
  background-color: #ffffff;
  transition: border-color 0.3s ease;
}
.select-box:focus,
.select-box:focus-within {
  border-color: #28a745; /* Green border on focus */
}
.select-box.active {
  border-color: #28a745; /* Màu xanh lá khi tùy chọn được mở */
}
.select-options {
  position: absolute;
  top: 100%;
  left: 0;
  right: 0;
  background-color: #ffffff;
  border: 1px solid #ced4da;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  z-index: 100;
  margin-top: 2px;
  max-height: 150px; /* Giới hạn chiều cao để hiển thị tối đa 4 hàng */
  overflow-y: auto; /* Thêm thanh cuộn dọc nếu vượt quá 4 hàng */
}

.option-dropdown {
  padding: 8px 10px;
  font-size: 14px;
  color: #333;
  cursor: pointer;
  transition: background-color 0.3s, color 0.3s;
}

.option-dropdown:hover:not(.selected-option) {
  background-color: #e0e0e0; /* Nền xám khi hover */
  color: #28a745; /* Chữ màu xanh lá khi hover */
}

.selected-option {
  background-color: #28a745; /* Nền xanh lá khi được chọn */
  color: #ffffff; /* Chữ màu trắng khi được chọn */
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

  outline: none;
}
</style>
