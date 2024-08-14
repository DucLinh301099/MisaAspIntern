<template>
  <div class="pagination">
    <div class="total-records">
      Tổng số:
      <strong class="bold-number">{{ totalRecords }}</strong>
      bản ghi
    </div>
    <div class="page-controls">
      <div class="select-container">
        <div class="select-box" tabindex="0" @click="toggleDropdown">
          {{ itemsPerPage }} bản ghi trên 1 trang
          <div class="mi mi-16 mi-arrow-down--black"></div>
        </div>
        <div v-if="dropdownOpen" class="select-options">
          <div
            v-for="option in itemsPerPageOptions"
            :key="option"
            :class="[
              'option-dropdown',
              { 'selected-option': option === itemsPerPage },
            ]"
            @click.stop="onSelectOption(option)"
          >
            {{ option }} bản ghi trên 1 trang
          </div>
        </div>
      </div>
      <button :disabled="currentPage === 1" @click="goToPreviousPage">
        Trước
      </button>
      <input
        type="number"
        :value="currentPageInput"
        @input="onPageInputChange"
        @change="goToPage(currentPageInput)"
        min="1"
        :max="totalPages"
      />
      <button :disabled="currentPage === totalPages" @click="goToNextPage">
        Sau
      </button>
    </div>
  </div>
</template>

<script>
export default {
  name: "MSPagination",
  props: {
    totalRecords: {
      type: Number,
      required: true,
    },
    itemsPerPage: {
      type: Number,
      required: true,
    },
    currentPage: {
      type: Number,
      required: true,
    },
    totalPages: {
      type: Number,
      required: true,
    },
    itemsPerPageOptions: {
      type: Array,
      required: true,
    },
  },
  data() {
    return {
      currentPageInput: this.currentPage,
      dropdownOpen: false,
    };
  },
  watch: {
    currentPage(newVal) {
      this.currentPageInput = newVal;
    },
  },
  methods: {
    onItemsPerPageChange(option) {
      this.$emit("update:itemsPerPage", option);
      this.$emit("updatePageData", {
        itemsPerPage: option,
        totalPages: Math.ceil(this.totalRecords / option),
        currentPage: 1, // Reset về trang đầu khi thay đổi số bản ghi trên mỗi trang
        totalRecords: this.totalRecords,
      });
      this.dropdownOpen = false; // Ẩn dropdown sau khi chọn
    },
    goToPreviousPage() {
      this.$emit("previous-page");
      this.$emit("updatePageData", {
        currentPage: this.currentPage - 1,
      });
    },
    goToNextPage() {
      this.$emit("next-page");
      this.$emit("updatePageData", {
        currentPage: this.currentPage + 1,
      });
    },
    goToPage(page) {
      if (page >= 1 && page <= this.totalPages) {
        this.$emit("update:currentPage", page);
        this.$emit("updatePageData", {
          currentPage: page,
          totalRecords: this.totalRecords,
        });
      }
    },
    onPageInputChange(event) {
      this.currentPageInput = Number(event.target.value);
    },
    toggleDropdown() {
      this.dropdownOpen = !this.dropdownOpen;
    },
    onSelectOption(option) {
      this.$emit("update:itemsPerPage", option);
      this.$emit("updatePageData", {
        itemsPerPage: option,
        totalPages: Math.ceil(this.totalRecords / option),
        currentPage: 1, // Reset về trang đầu khi thay đổi số bản ghi trên mỗi trang
      });
      this.dropdownOpen = false; // Ẩn dropdown sau khi chọn
    },
  },
};
</script>

<style scoped>
.total-records {
  font-size: 13px;
}

.pagination {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 0;
  font-family: AvertaStdCY, Helvetica, Arial, sans-serif;
}

.page-controls {
  display: flex;
  align-items: center;
  font-size: 14px;
}

.select-container {
  position: relative;
  display: inline-block;
  width: 200px;
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
}
.select-box:focus,
.select-box:focus-within {
  border-color: #28a745; /* Green border on focus */
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
}

.option-dropdown {
  padding: 8px 10px;
  font-size: 14px;
  color: #333;
}

.option-dropdown:hover:not(.selected-option) {
  background-color: #e6e3e3;
  color: #1fa13e;
}

.selected-option {
  background-color: #28a745;
  color: #ffffff;
}

.page-controls button {
  margin-left: 5px;
  padding: 5px 10px;
  border-radius: 2.5px;
  border: none;
  background-color: #fff;
  cursor: pointer;
}

.page-controls button:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.page-controls input[type="number"] {
  width: 35px;
  padding: 5px;
  border: 1px solid #ced4da;
  border-radius: 2.5px;
  text-align: center;

  outline: none;
}

.bold-number {
  font-weight: 800;
}

.mi-16 {
  width: 16px;
  height: 16px;
  min-width: 16px;
  min-height: 16px;
}

.mi {
  background: url(https://actaspcdng1.misacdn.net/assets/Sprites-11d892c3.svg)
    no-repeat;
  cursor: pointer;
}

.mi-arrow-down--black {
  background-position: -560px -359px;
}
</style>
