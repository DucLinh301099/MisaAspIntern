<template>
    <div class="pagination">
      <div class="total-records">
        Tổng số:
        <strong class="bold-number">{{ totalRecords }}</strong>
        bản ghi
      </div>
      <div class="page-controls">
        <select :value="itemsPerPage" @change="onItemsPerPageChange">
          <option
            v-for="option in itemsPerPageOptions"
            :key="option"
            :value="option"
            class="option-dropdown"
          >
            {{ option }} bản ghi trên 1 trang
          </option>
        </select>
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
      };
    },
    watch: {
      currentPage(newVal) {
        this.currentPageInput = newVal;
      },
    },
    methods: {
      onItemsPerPageChange(event) {
        this.$emit("update:itemsPerPage", Number(event.target.value));
      },
      goToPreviousPage() {
        this.$emit("previous-page");
      },
      goToNextPage() {
        this.$emit("next-page");
      },
      goToPage(page) {
        if (page >= 1 && page <= this.totalPages) {
          this.$emit("update:currentPage", page);
        }
      },
      onPageInputChange(event) {
        this.currentPageInput = Number(event.target.value);
      },
    },
  };
  </script>
  
  <style scoped>
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
  }
  
  .page-controls select {
    margin-right: 10px;
    padding: 5px;
    border-radius: 2.5px;
    border: 1px solid #ced4da;
    background-color: #ffffff;
    font-size: 14px;
    color: #333;
    appearance: none;
    outline: none;
    background-repeat: no-repeat;
    background-position: right 10px center;
    background-size: 10px;
    transition: border-color 0.2s ease;
  }
  
  .page-controls select:focus {
    border-color: #28a745;
    outline: none;
  }
  
  .page-controls select option {
    padding: 8px;
    font-size: 14px;
    color: #333;
    border-radius: 0.5px;
  }
  
  .option-dropdown {
    border-radius: 2.5px !important;
  }
  
  .page-controls select option:checked {
    background-color: #28a745;
    color: #ffffff;
  }
  
  .page-controls select option:hover {
    background-color: #28a745;
    color: #ffffff;
  }
  
  .page-controls button {
    margin: 0 5px;
    padding: 5px 10px;
    border-radius: 2.5px;
    border: 1px solid #ced4da;
    background-color: #f8f9fa;
    cursor: pointer;
  }
  
  .page-controls button:disabled {
    opacity: 0.5;
    cursor: not-allowed;
  }
  
  .page-controls input[type="number"] {
    width: 50px;
    padding: 5px;
    border: 1px solid #ced4da;
    border-radius: 2.5px;
    text-align: center;
    margin-right: 5px;
    outline: none;
  }
  
  .bold-number {
    font-weight: 800;
  }
  </style>
  