<template>
    <div class="withdraw-list-component">
      <label for="withdraw-list" class="withdraw-list">{{ label }}</label>
      <table class="withdraw-list-table">
        <thead class="thead">
          <tr>
            <th class="th-index">#</th>
            <th v-for="(column, index) in columnConfig" :key="index">
              {{ column.columnName }}
            </th>
            <th></th>
          </tr>
        </thead>
        <tbody class="table-grid">
          <tr
            class="table-tbody"
            v-for="(row, rowIndex) in optionsData"
            :key="rowIndex"
          >
            <td>{{ rowIndex + 1 }}</td>
            <td
              class="td-grid"
              :class="getColumnClass(column.columnName)"
              v-for="(column, colIndex) in columnConfig"
              :key="colIndex"
            >
              <span>{{ row[column.fieldName] }}</span>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </template>
  
  <script>
  import { baseApi } from "../../api/baseApi";
  import withdrawListConfig from "../../config/WithdrawListConfig"; 
  
  export default {
    name: "MSWithdrawList",
    data() {
      return {
        columnConfig: withdrawListConfig.columnConfig,
        optionsData: [],
        label: "Danh sách rút tiền",
      };
    },
    mounted() {
      this.fetchData();
    },
    methods: {
      async fetchData() {
        if (!withdrawListConfig.endpoint) {
          return;
        }
        try {
          const response = await baseApi.getAuthenApi(withdrawListConfig.endpoint);
          if (response.data && Array.isArray(response.data)) {
            this.optionsData = response.data;
          } else if (
            response.data &&
            response.data.data &&
            Array.isArray(response.data.data)
          ) {
            this.optionsData = response.data.data;
          } else {
            this.optionsData = [];
          }
        } catch (error) {
          this.optionsData = [];
        }
      },
      getColumnClass(columnName) {
        if (
          columnName === "Số tiền" ||
          columnName === "Số tài khoản NH"
        ) {
          return "narrow-column";
        }
        return "";
      },
    },
  };
  </script>
  
  <style scoped>
  .withdraw-list-component {
    padding-left: 20px;
    padding-right: 20px;
  }
  
  .table-tbody {
    background-color: #e5f3ff;
  }
  
  .td-grid {
    height: 40px;
  }
  
  .td-grid.narrow-column {
    width: 170px; /* Adjust as necessary */
  }
  
  @media screen and (max-width: 320px) {
    .td-grid.narrow-column {
      width: 60px; /* Adjust as necessary */
    }
  }
  
  .th-index {
    width: 28px;
    align-items: center;
  }
  
  .thead {
    background-color: #f4f5f8;
  }
  
  .withdraw-list {
    margin-bottom: 16px;
    font-weight: bold;
    color: #0075c0;
    font-weight: 700;
    text-decoration: underline;
  }
  
  .withdraw-list-table {
    width: 100%;
    border-collapse: collapse;
    margin-bottom: 16px;
    margin-top: 8px;
    font-size: 12.5px;
  }
  
  .withdraw-list-table th {
    border: 1px solid #ccc;
    padding: 8px;
    text-align: left;
    font-size: 14;
  }
  
  .withdraw-list-table td {
    border: 1px solid #ccc;
    padding: 0px 10px 0px 10px;
    text-align: left;
  }
  
  .withdraw-list-table th {
    font-weight: bold;
  }
  
  span {
    width: 100%;
    display: block;
    padding: 4px;
    box-sizing: border-box;
    height: 27px;
    border-radius: 3px;
    outline: none;
  }
  </style>
  